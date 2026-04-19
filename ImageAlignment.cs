using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace SolarImageProcessionCsharp
{
    // 图像对齐 (CV2 重写)
    public class ImageAlignment
    {

        // 翻转图像（默认垂直翻转，天文图像常用）
        public static List<Mat> ImageFlip(List<Mat> images)
        {
            for (int i = 0; i < images.Count; i++)
            {
                Mat flipped = new Mat();
                // FlipMode 说明：
                // 0 = 垂直翻转（上下）
                // 1 = 水平翻转（左右）
                // -1 = 双向翻转
                Cv2.Flip(images[i], flipped, FlipMode.Y);

                // 释放原图，替换为翻转后的图
                images[i].Dispose();
                images[i] = flipped;
            }
            return images;
        }

        //  亮度上限截断增强
        public static Mat AdjustBrightness(Mat original, int value)
        {
            value = Math.Clamp(value, 1, 100);
            double maxLimit = value / 100.0;

            // 转成浮点型计算
            Mat floatMat = new Mat();
            original.ConvertTo(floatMat, MatType.CV_32F);

            // 归一化到 0 ~ 1
            Cv2.Normalize(floatMat, floatMat, 0, 1, NormTypes.MinMax);

            // 超过上限的全部截断为 1.0（纯白）
            Cv2.Threshold(floatMat, floatMat, maxLimit, 1.0, ThresholdTypes.Trunc);

            // 把剩下的 0 ~ maxLimit 重新拉伸到 0 ~ 1
            Cv2.Normalize(floatMat, floatMat, 0, 1, NormTypes.MinMax);

            // 转回原格式
            Mat result = new Mat();
            floatMat.ConvertTo(result, original.Type(), 65535.0 / maxLimit);

            floatMat.Dispose();
            return result;
        }

        // 计算单通道图像的质心 (cx, cy)
        private static Point2d GetImageCentroid(Mat grayImage)
        {
            var moments = Cv2.Moments(grayImage);
            double cx = moments.M10 / moments.M00;
            double cy = moments.M01 / moments.M00;
            return new Point2d(cx, cy);
        }

        // 场旋角计算核心函数
        public static List<double> CalculateFieldRotationAngles(MainForm mainForm,List<Mat> images, List<string> imagePaths, double latitudeDeg, double longitudeDeg, int targetIndex = 0)
        {
            List<double> angles = new List<double>();
            List<DateTime> times = new List<DateTime>();

            // 1. 从文件名批量提取拍摄时间
            foreach (var f in imagePaths)
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(f);

                    string timePart = f.Substring(0, 16);

                    // 前14位整体保留：2026-03-07-0736
                    string basePart = timePart.Substring(0, 14);

                    // 取出最后一位数字 0~9
                    int dec = timePart[15] - '0';

                    // 计算秒：0.1分钟 = 6秒
                    int sec = dec * 6;

                    // 拼接成标准格式：2026-03-07 07:36:18
                    string standardTime = $"{basePart.Substring(0, 10)} {basePart.Substring(11, 2)}:{basePart.Substring(13, 2)}:{sec:00}";

                    // 最后只用一次函数解析
                    DateTime dt = DateTime.ParseExact(
                        standardTime,
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal);

                    times.Add(dt);
                }
                catch
                {
                    times.Add(DateTime.UtcNow);
                }
            }

            // 2. 计算参考图（第一张）的场旋角 η0
            DateTime t0 = times[targetIndex];
            var (ra0, dec0) = Astronomy.SunEquatorialCoordinates(t0);
            double eta0 = ComputeSingleImageFieldRotation(t0, latitudeDeg, longitudeDeg, ra0, dec0);

            // 3. 逐张计算相对场旋角 Δη = ηi - η0
            for (int i = 0; i < images.Count; i++)
            {
                if (i == targetIndex) angles.Add(0.0);

                DateTime t = times[i];
                var (ra, dec) = Astronomy.SunEquatorialCoordinates(t);
                double eta = ComputeSingleImageFieldRotation(t, latitudeDeg, longitudeDeg, ra, dec);

                double delta = eta - eta0;

                // 归一到 [-π, π]
                while (delta > Math.PI) delta -= 2 * Math.PI;
                while (delta < -Math.PI) delta += 2 * Math.PI;

                angles.Add(delta * 180 / Math.PI);
                mainForm.Log($"经纬仪场旋校准 → 第{i + 1}张图旋转角度: {delta}°");

            }

            return angles;
        }

        // 单图像场旋角 η 计算函数
        private static double ComputeSingleImageFieldRotation(DateTime utcTime, double latDeg, double lonDeg, double sunRADeg, double sunDecDeg)
        {
            // 角度转弧度
            double lat = latDeg * Math.PI / 180.0;
            double lon = lonDeg * Math.PI / 180.0;
            double ra = sunRADeg * Math.PI / 180.0;
            double dec = sunDecDeg * Math.PI / 180.0;

            // 1. 计算当地恒星时 LST
            double jd = Astronomy.JulianDay(utcTime);
            double gst = Astronomy.GreenwichSiderealTime(jd);
            double lst = gst + lon;

            // 2. 时角 h = LST - RA
            double h = lst - ra;

            // 3. 场旋角公式 η = atan2(y, x)
            double y = Math.Cos(lat) * Math.Sin(h);
            double x = Math.Sin(lat) * Math.Cos(dec) - Math.Cos(lat) * Math.Sin(dec) * Math.Cos(h);
            double eta = Math.Atan2(y, x);

            return eta;
        }



        // 自动检测：图像序列中【后半段开始旋转180°的第一张序号】
        public static int FindFirstRotated180Index(MainForm mainForm, List<Mat> rawImages, bool isDebug = false)
        {
            if (rawImages == null || rawImages.Count < 2)
                return -1;

            // 步骤1：先用【质心法对齐】所有图像
            List<Mat> alignedImages = AlignImages(mainForm, rawImages, targetIndex: 0, isLog: false, isLightEnhance: false, forceMethod: 0);
            alignedImages = AlignImages(mainForm, alignedImages, targetIndex: 0, isLog: false, isLightEnhance: false, forceMethod: 0);

            // 步骤2：统一缩放到小尺寸（加速计算，不影响判断）
            List<Mat> smallAligned = new List<Mat>();
            foreach (var mat in alignedImages)
            {
                Mat small = new Mat();
                Cv2.Resize(mat, small, new Size(320, 320), 0, 0, InterpolationFlags.Nearest);
                smallAligned.Add(small);
            }

            // 步骤3：计算每一张与前一张的相似度
            List<double> directScores = new List<double>();     // 直接匹配相似度
            List<double> rotate180Scores = new List<double>(); // 旋转180°匹配相似度

            for (int i = 1; i < smallAligned.Count; i++)
            {
                Mat prev = smallAligned[i - 1];
                Mat curr = smallAligned[i];

                // 直接相似度
                double simDirect = CompareSimilarity(prev, curr);
                directScores.Add(simDirect);

                // 当前图旋转180°后 与前图相似度
                Mat currRot180 = new Mat();
                Cv2.Rotate(curr, currRot180, RotateFlags.Rotate180);
                double simRot180 = CompareSimilarity(prev, currRot180);
                rotate180Scores.Add(simRot180);

                currRot180.Dispose();
            }

            // 步骤4：寻找突变点（不break，记录差值最大的反转点）
            int firstRotatedIndex = -1;
            double maxDifference = 0; // 记录最大差值

            for (int i = 0; i < directScores.Count; i++)
            {
                double d = directScores[i];
                double r = rotate180Scores[i];

                // 判断是否发生反转
                if (r > d)
                {
                    double currentDiff = r - d; // 当前反转差值
                    if (isDebug)
                        mainForm.Log($"图{i + 2} vs 图{i + 1} → 差值: {currentDiff:F4}");
                    // 如果当前差值更大，就更新序号和最大差值
                    if (currentDiff > maxDifference)
                    {
                        maxDifference = currentDiff;
                        firstRotatedIndex = i + 1;
                    }
                }
            }

            foreach (var m in smallAligned) m.Dispose();
            foreach (var m in alignedImages) m.Dispose();

            if (isDebug)
            {
                mainForm.Log("图像序列相似度分析：");
                for (int i = 0; i < directScores.Count; i++)
                {
                    mainForm.Log($"图{i + 2} vs 图{i + 1} → 直接相似度: {directScores[i]:F4}, 旋转180°相似度: {rotate180Scores[i]:F4}");
                }
                if (firstRotatedIndex != -1)
                    mainForm.Log($"检测到旋转180°的第一张图像为第{firstRotatedIndex + 1}张图");
                else
                    mainForm.Log("未检测到明显的旋转180°图像。");
            }

            return firstRotatedIndex;
        }

        // 相似度计算函数
        private static double CompareSimilarity(Mat a, Mat b)
        {
            // 统一转 单通道 32位浮点图（修复模板匹配报错）
            Mat grayA = new Mat();
            Mat grayB = new Mat();
            if (a.Channels() > 1) Cv2.CvtColor(a, grayA, ColorConversionCodes.BGR2GRAY);
            else a.CopyTo(grayA);
            if (b.Channels() > 1) Cv2.CvtColor(b, grayB, ColorConversionCodes.BGR2GRAY);
            else b.CopyTo(grayB);

            Mat floatA = new Mat();
            Mat floatB = new Mat();
            grayA.ConvertTo(floatA, MatType.CV_32F);
            grayB.ConvertTo(floatB, MatType.CV_32F);

            // 匹配计算
            Mat result = new Mat();
            Cv2.MatchTemplate(floatA, floatB, result, TemplateMatchModes.CCoeffNormed);
            Cv2.MinMaxLoc(result, out double minVal, out double maxVal);

            // 释放资源
            grayA.Dispose(); grayB.Dispose();
            floatA.Dispose(); floatB.Dispose();
            result.Dispose();

            return maxVal;
        }

        /// <summary>
        /// 对图像列表中指定索引范围的图像全部旋转180度
        /// </summary>
        public static List<Mat> FlipImages180InRange(List<Mat> inputImages, int startIndex = 0, int endIndex = -1)
        {
            List<Mat> result = inputImages.Select(mat => mat.Clone()).ToList();

            if (endIndex == -1)
                endIndex = inputImages.Count - 1;

            for (int i = startIndex; i <= endIndex && i < inputImages.Count; i++)
            {
                Mat dst = new Mat();
                Cv2.Rotate(inputImages[i], dst, RotateFlags.Rotate180);
                result[i].Dispose();
                result[i] = dst;
            }
            return result;
        }


        // 平移对齐
        public static List<Mat> AlignImages(MainForm mainForm, List<Mat> images, int targetIndex = 0, bool isLog = true, bool isLightEnhance = true, int forceMethod = -1)
        {
            int imagesCount = images.Count;
            int targetW = images.Max(mat => mat.Width);
            int targetH = images.Max(mat => mat.Height);
            if (isLog)
            {
                mainForm.Log($"共{imagesCount}张图像");
                mainForm.Log($"目标对齐图像 → 第{targetIndex + 1}张");
                mainForm.Log($"图像画布扩充：宽：{targetW:F3}, 高：{targetH:F3}");
            }

            int enhanceValue = mainForm.GetLightEnhanceValue();

            List<Mat> aligned = new List<Mat>();
            List<Mat> referenceImages = images.Select(mat => mat.Clone()).ToList();


            Mat referencetarget = referenceImages[targetIndex];

            // 基准图：填充+高斯模糊
            Mat targetPadded = ImageCalculation.PadToSize(referencetarget, targetW, targetH);
            Mat targetForCalc = new Mat();
            Cv2.GaussianBlur(targetPadded, targetForCalc, new OpenCvSharp.Size(7, 7), 3);
            if (ConfigManager.Config.ImageAlignmentObject == "FullDisk" && enhanceValue < 100 && isLightEnhance)
            {
                if (isLog)
                {
                    mainForm.Log($"全日/月/行星模式且亮度上限截断值小于100%（{enhanceValue}%）");
                    mainForm.Log($"图像亮度上限截断至：0 - {enhanceValue}%");
                }
                targetForCalc = AdjustBrightness(targetPadded, enhanceValue);
            }

            for (int i = 0; i < imagesCount; i++)
            {
                // 预处理：填充+高斯模糊+（可选）亮度增强
                var img = images[i];
                Mat padded = ImageCalculation.PadToSize(img, targetW, targetH);
                Mat imgForCalc = new Mat();
                Cv2.GaussianBlur(padded, imgForCalc, new OpenCvSharp.Size(7, 7), 3);
                if (ConfigManager.Config.ImageAlignmentObject == "FullDisk" && enhanceValue < 100 && isLightEnhance)
                {
                    imgForCalc = AdjustBrightness(padded, enhanceValue);
                }

                // 计算偏移（质心法 or 相位相关法）
                double response;

                targetForCalc.ConvertTo(targetForCalc, MatType.CV_32FC1);
                imgForCalc.ConvertTo(imgForCalc, MatType.CV_32FC1);
                Point2d shift = new Point2d();
                if (ConfigManager.Config.ImageAlignmentMode == "MassCenter" || forceMethod == 0)
                {
                    Point2d targetCentroid = GetImageCentroid(targetForCalc);
                    Point2d imgCentroid = GetImageCentroid(imgForCalc);
                    shift = new Point2d(imgCentroid.X - targetW / 2, imgCentroid.Y - targetH / 2);
                }
                else if (ConfigManager.Config.ImageAlignmentMode == "PhaseCorrelate" || forceMethod == 1)
                {
                    shift = Cv2.PhaseCorrelate(targetForCalc, imgForCalc, new Mat(), out response);
                }
                if (isLog)
                    mainForm.Log($"第{i + 1}/{imagesCount}张图像——计算偏移：X = {shift.X:F3}, Y = {shift.Y:F3}");

                // 仿射变换平移
                Mat alignedImg = new Mat();
                Mat M = Mat.Eye(2, 3, MatType.CV_32F);
                M.Set<float>(0, 2, -(float)shift.X);
                M.Set<float>(1, 2, -(float)shift.Y);

                Cv2.WarpAffine(padded, alignedImg, M, new OpenCvSharp.Size(targetW, targetH),
                    InterpolationFlags.Linear, BorderTypes.Constant, Scalar.All(0));

                aligned.Add(alignedImg);

                // 释放临时资源
                imgForCalc.Dispose();
                padded.Dispose();
                M.Dispose();
            }

            targetPadded.Dispose();
            targetForCalc.Dispose();
            return aligned;
        }

        /// <summary>
        /// 计算两幅图之间的旋转角度 + 缩放系数（Fourier-Mellin 相位相关）
        /// </summary>
        private static (double angle, double scale) EstimateRotationAndScale(Mat reference, Mat target)
        {
            // ==============================
            // 【关键】先裁剪成同样大小的正方形（不缩放！）
            // ==============================
            int cropSize = Math.Min(Math.Min(reference.Cols, reference.Rows), Math.Min(target.Cols, target.Rows));
            Rect roi = new Rect(0, 0, cropSize, cropSize);
            Mat refCrop = new Mat(reference, roi);
            Mat tgtCrop = new Mat(target, roi);

            // 转灰度
            Mat refGray = new Mat();
            Mat tgtGray = new Mat();
            if (refCrop.Channels() == 3)
                Cv2.CvtColor(refCrop, refGray, ColorConversionCodes.BGR2GRAY);
            else
                refCrop.CopyTo(refGray);
            if (tgtCrop.Channels() == 3)
                Cv2.CvtColor(tgtCrop, tgtGray, ColorConversionCodes.BGR2GRAY);
            else
                tgtCrop.CopyTo(tgtGray);

            // 转浮点
            Mat refFloat = new Mat();
            Mat tgtFloat = new Mat();
            refGray.ConvertTo(refFloat, MatType.CV_32F);
            tgtGray.ConvertTo(tgtFloat, MatType.CV_32F);

            // DFT
            Mat refF = new Mat();
            Mat tgtF = new Mat();
            Cv2.Dft(refFloat, refF, DftFlags.ComplexOutput | DftFlags.Scale);
            Cv2.Dft(tgtFloat, tgtF, DftFlags.ComplexOutput | DftFlags.Scale);

            // 拆分通道
            Mat[] refChannels = Cv2.Split(refF);
            Mat[] tgtChannels = Cv2.Split(tgtF);

            // 幅度
            Mat refMag = new Mat();
            Mat tgtMag = new Mat();
            Cv2.Magnitude(refChannels[0], refChannels[1], refMag);
            Cv2.Magnitude(tgtChannels[0], tgtChannels[1], tgtMag);

            refChannels[0].Dispose();
            refChannels[1].Dispose();
            tgtChannels[0].Dispose();
            tgtChannels[1].Dispose();

            // 极坐标变换（现在尺寸完全一样，不会报错）
            int maxRadius = cropSize / 2;
            Point2f center = new Point2f(cropSize / 2f, cropSize / 2f);

            Mat refPolar = new Mat();
            Mat tgtPolar = new Mat();
            Cv2.LogPolar(refMag, refPolar, center, maxRadius, InterpolationFlags.Linear);
            Cv2.LogPolar(tgtMag, tgtPolar, center, maxRadius, InterpolationFlags.Linear);

            // 相位相关
            Mat responseMat = new Mat();
            Point2d shift = Cv2.PhaseCorrelate(refPolar, tgtPolar, responseMat, out double response);

            double angle = shift.Y * 360.0 / refPolar.Rows;
            double scale = Math.Exp(shift.X / maxRadius);

            // 释放
            refCrop.Dispose();
            tgtCrop.Dispose();
            refGray.Dispose();
            tgtGray.Dispose();
            refFloat.Dispose();
            tgtFloat.Dispose();
            refF.Dispose();
            tgtF.Dispose();
            refMag.Dispose();
            tgtMag.Dispose();
            refPolar.Dispose();
            tgtPolar.Dispose();
            responseMat.Dispose();

            return (angle, scale);
        }

        /// <summary>
        /// 根据旋转角度 + 缩放系数 对图像做校正
        /// </summary>
        public static Mat CorrectRotationAndScale(Mat image, double angle = 0.0, double scale = 1.0)
        {
            Point2f center = new Point2f(image.Cols / 2f, image.Rows / 2f);
            Mat rotMat = Cv2.GetRotationMatrix2D(center, angle, 1.0f / (float)scale);

            Mat corrected = new Mat();
            Cv2.WarpAffine(image, corrected, rotMat, image.Size(),
                InterpolationFlags.Linear, BorderTypes.Constant, Scalar.All(0));

            rotMat.Dispose();
            return corrected;
        }

        /// <summary>
        /// 根据旋转角度 + 缩放系数 对图像做校正
        /// </summary>
        public static List<Mat> CorrectRotationAndScaleForImages(List<Mat> imgs, List<double> angles = null, List<double> scales = null)
        {
            List<Mat> images = imgs.Select(mat => mat.Clone()).ToList();
            for (int i = 0; i < images.Count; i++)
            {
                double angle = angles != null && i < angles.Count ? angles[i] : 0.0;
                double scale = scales != null && i < scales.Count ? scales[i] : 1.0;
                Mat corrected = CorrectRotationAndScale(images[i], angle, scale);
                images[i].Dispose();
                images[i] = corrected;
            }
            return images;
        }



        // 选取分辨率最高图像（太阳面积最大）
        private static int FindBestResolutionImageBySunArea(List<Mat> images)
        {
            if (images == null || images.Count == 0) return -1;

            List<double> sunAreas = new List<double>();

            foreach (var img in images)
            {
                // 1. 转灰度
                Mat gray = new Mat();
                if (img.Channels() > 1)
                    Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
                else
                    img.CopyTo(gray);

                // 2. 自适应二值化（分离太阳/背景，鲁棒性最强）
                Mat binary = new Mat();
                Mat gray8U = new Mat();
                gray.ConvertTo(gray8U, MatType.CV_8U);
                Cv2.AdaptiveThreshold(gray8U, binary, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.BinaryInv, 101, 10);

                // 3. 计算白色区域（太阳）像素总数
                double sunArea = Cv2.CountNonZero(binary);
                sunAreas.Add(sunArea);

                // 释放
                gray.Dispose();
                binary.Dispose();
            }

            // 4. 找面积最大的索引
            double maxArea = sunAreas.Max();
            int bestIndex = sunAreas.IndexOf(maxArea);

            return bestIndex;
        }

        // 相位相关法放缩对齐及旋转角度获取
        public static (List<Mat>, List<double>) AlignScaleAndRotation(MainForm mainForm, List<Mat> images, int targetIndex = 0, bool isScale = false, bool isRotate = false, bool isLog = true)
        {
            int imagesCount = images.Count;
            int scaletargetIndex = targetIndex;
            bool isScaleAlignMaxResolution = ConfigManager.Config.ScaleAlignMaxResolution;
            if (isScaleAlignMaxResolution && isScale)
                scaletargetIndex = FindBestResolutionImageBySunArea(images);
            if (isLog)
            {
                mainForm.Log($"共{imagesCount}张图像");
                if (isScaleAlignMaxResolution && isScale)
                    mainForm.Log($"以最高分辨率图像作为放缩对齐参考 → 第{scaletargetIndex + 1}张");
                mainForm.Log($"目标对齐图像 → 第{targetIndex + 1}张");
            }
            Mat reference = images[scaletargetIndex];
            List<Mat> aligned = new List<Mat>();
            List<double> angles = new List<double>();
            for (int i = 0; i < imagesCount; i++)
            {
                if (i == scaletargetIndex)
                {
                    aligned.Add(reference.Clone());
                    angles.Add(0.0);
                    continue;
                }
                Mat img = images[i];
                var (angle, scale) = EstimateRotationAndScale(reference, img);
                Mat corrected = CorrectRotationAndScale(img, scale: scale);
                angles.Add(angle);
                aligned.Add(corrected);
                if (isLog)
                {
                    if (isRotate && isScale)
                        mainForm.Log($"第{i + 1}/{imagesCount}张图像——旋转角度: {angle:F3}°, 缩放系数: {scale:F4}");
                    else if (isRotate)
                        mainForm.Log($"第{i + 1}/{imagesCount}张图像——旋转角度: {angle:F3}°");
                    else if (isScale)
                        mainForm.Log($"第{i + 1}/{imagesCount}张图像——缩放系数: {scale:F4}");
                }

                // 释放临时资源
                img.Dispose();
            }
            for (int i = 0; i < imagesCount; i++)
            {
                angles[i] = angles[i]-angles[targetIndex];
            }
            reference.Dispose();
            return (aligned, angles);
        }

        // PCA 主方向对齐（旋转+缩放+平移）
        public static (List<Mat>, List<double>) AlignByPCAGroup(
            MainForm mainForm,
            List<Mat> images,
            int targetIndex = 0,
            bool isScale = true,
            bool isRotate = true,
            bool isTranslate = true,
            bool useECC = false,
            bool isLog = true)
        {
            int count = images.Count;

            if (count == 0)
                throw new Exception("图像列表为空");

            Mat reference = images[targetIndex];

            List<Mat> aligned = new();
            List<double> angles = new();

            if (isLog)
            {
                mainForm.Log($"共 {count} 张图像");
                mainForm.Log($"目标对齐图像 → 第 {targetIndex + 1} 张");
                if (useECC)
                    mainForm.Log("ECC 精修已启用");
            }

            for (int i = 0; i < count; i++)
            {
                if (i == targetIndex)
                {
                    aligned.Add(reference.Clone());
                    angles.Add(0.0);
                    continue;
                }

                Mat img = new();
                images[i].CopyTo(img);

                try
                {
                    // =========================
                    // 1️⃣ PCA 粗对齐
                    // =========================
                    var (angle, scale, translation, warp) = AlignByPCA(img, reference, isScale: isScale, isRotate: isRotate, isTranslate: isTranslate);
                    if (isLog)
                    {
                        if (isRotate || isScale || isTranslate) mainForm.Log($"第 {i + 1}/{count} 张");
                        if (isScale) mainForm.Log($"比例: {scale:F3}");
                        if (isRotate) mainForm.Log($"角度: {angle:F3}°");
                        if (isTranslate) mainForm.Log($"平移: {translation} pix");
                    }
                    Mat pcaAligned = new();
                    Cv2.WarpAffine(
                        img,
                        pcaAligned,
                        warp,
                        reference.Size(),
                        InterpolationFlags.Linear,
                        BorderTypes.Constant,
                        Scalar.Black
                    );

                    Mat finalResult = pcaAligned;
                    double finalAngle = angle;

                    // =========================
                    // 2️⃣ ECC 精修（可选）
                    // =========================
                    if (useECC)
                    {
                        var ecc = RefineByECC(reference, pcaAligned, warp);
                        mainForm.Log($"开始ECC精修");
                        if (ecc.success)
                        {
                            finalResult = ecc.result;
                            finalAngle = angle + ecc.angle;
                        }
                        // ECC失败 → 自动fallback PCA结果
                    }

                    aligned.Add(finalResult);
                    angles.Add(finalAngle);

                    
                }
                catch (Exception e)
                {
                    aligned.Add(img.Clone());
                    angles.Add(0.0);

                    if (isLog)
                        mainForm.Log($"第 {i + 1} 张失败: {e.Message}");
                }

                img.Dispose();
            }

            // =========================
            // 3️⃣ 角度统一参考修正
            // =========================
            for (int i = 0; i < count; i++)
            {
                angles[i] -= angles[targetIndex];
            }

            reference.Dispose();

            return (aligned, angles);
        }

        // PCA 主方向对齐（旋转+缩放+平移）——返回旋转角度、缩放系数、平移向量、仿射矩阵
        public static (double angle, double scale, Point2d translation, Mat warp) AlignByPCA(
            Mat img1,
            Mat img2,
            bool isScale = true,
            bool isRotate = true,
            bool isTranslate = true)
        {
            // 1. 预处理提取特征
            Mat pre1 = PreprocessSolarFeature(img1);
            Mat pre2 = PreprocessSolarFeature(img2);

            // 1️⃣ 提取所有白点
            var pts1 = ExtractAllWhitePoints(pre1);
            var pts2 = ExtractAllWhitePoints(pre2);

            if (pts1.Count < 10 || pts2.Count < 10)
                throw new Exception("特征点太少");

            // 2️⃣ PCA 主方向
            double angle1 = ComputePCAAngle(pts1);
            double angle2 = ComputePCAAngle(pts2);

            double angle = angle2 - angle1;

            // 处理180°歧义
            if (angle > 90) angle -= 180;
            if (angle < -90) angle += 180;

            // 3️⃣ 计算质心
            Point2d c1 = ComputeCentroid(pts1);
            Point2d c2 = ComputeCentroid(pts2);

            // 4️⃣ 计算尺度（用到PCA的方差）
            double scale1 = ComputeScale(pts1, c1);
            double scale2 = ComputeScale(pts2, c2);

            double scale = scale2 / scale1;

            if (!isScale) scale = 1.0;
            if (!isRotate) angle = 0;

            // 5️⃣ 构建仿射矩阵
            double rad = angle * Math.PI / 180.0;
            double cos = Math.Cos(rad) * scale;
            double sin = Math.Sin(rad) * scale;

            // 旋转+缩放矩阵
            Mat warp = new Mat(2, 3, MatType.CV_64F);

            warp.Set(0, 0, cos);
            warp.Set(0, 1, -sin);
            warp.Set(1, 0, sin);
            warp.Set(1, 1, cos);

            // 6️⃣ 平移（关键：把中心对齐）
            double tx = - c2.X + (cos * c1.X - sin * c1.Y);
            double ty = - c2.Y + (sin * c1.X + cos * c1.Y);


            if (!isTranslate) { tx = 0.0; ty  = 0.0; }

                warp.Set(0, 2, tx);
            warp.Set(1, 2, ty);

            return (angle, scale, new Point2d(tx, ty), warp);
        }

        static List<Point2f> ExtractAllWhitePoints(Mat bin)
        {
            List<Point2f> pts = new();

            for (int y = 0; y < bin.Rows; y++)
            {
                for (int x = 0; x < bin.Cols; x++)
                {
                    if (bin.At<byte>(y, x) > 0)
                    {
                        pts.Add(new Point2f(x, y));
                    }
                }
            }

            return pts;
        }

        static double ComputePCAAngle(List<Point2f> pts)
        {
            using Mat mat = new Mat(pts.Count, 2, MatType.CV_32F);

            for (int i = 0; i < pts.Count; i++)
            {
                mat.Set(i, 0, pts[i].X);
                mat.Set(i, 1, pts[i].Y);
            }

            using Mat mean = new();
            using Mat eigenvectors = new();

            Cv2.PCACompute(mat, mean, eigenvectors);

            // 第一主方向
            float vx = eigenvectors.At<float>(0, 0);
            float vy = eigenvectors.At<float>(0, 1);

            double angle = Math.Atan2(vy, vx) * 180.0 / Math.PI;

            return angle;
        }

        static double ComputeScale(List<Point2f> pts, Point2d center)
        {
            double sum = 0;

            foreach (var p in pts)
            {
                double dx = p.X - center.X;
                double dy = p.Y - center.Y;
                sum += dx * dx + dy * dy;
            }

            return Math.Sqrt(sum / pts.Count);
        }

        static Point2d ComputeCentroid(List<Point2f> pts)
        {
            double sx = 0, sy = 0;

            foreach (var p in pts)
            {
                sx += p.X;
                sy += p.Y;
            }

            return new Point2d(sx / pts.Count, sy / pts.Count);
        }

        public static (bool success, Mat warp, double angle, Mat result) RefineByECC(
                Mat reference,
                Mat moving,
                Mat initWarp = null)
        {
            Mat warp = Mat.Eye(2, 3, MatType.CV_32F);

            try
            {
                // =========================
                // 1. 初始化 warp
                // =========================
                if (initWarp != null && !initWarp.Empty())
                {
                    initWarp.ConvertTo(warp, MatType.CV_32F);
                }

                // =========================
                // 2. 预处理
                // =========================
                Mat ref32 = new();
                Mat mov32 = new();

                reference.ConvertTo(ref32, MatType.CV_32F);
                moving.ConvertTo(mov32, MatType.CV_32F);

                Cv2.GaussianBlur(ref32, ref32, new Size(5, 5), 0);
                Cv2.GaussianBlur(mov32, mov32, new Size(5, 5), 0);

                Cv2.Normalize(ref32, ref32, 0, 1, NormTypes.MinMax);
                Cv2.Normalize(mov32, mov32, 0, 1, NormTypes.MinMax);

                // =========================
                // 3. ECC
                // =========================
                var criteria = new TermCriteria(
                    CriteriaTypes.Eps | CriteriaTypes.Count,
                    1000,
                    1e-6
                );

                Cv2.FindTransformECC(
                    ref32,
                    mov32,
                    warp,
                    MotionTypes.Affine,
                    criteria
                );

                // =========================
                // 4. warp 输出
                // =========================
                Mat result = new();
                Cv2.WarpAffine(
                    moving,
                    result,
                    warp,
                    reference.Size(),
                    InterpolationFlags.Linear,
                    BorderTypes.Constant,
                    Scalar.Black
                );

                double c = warp.At<float>(0, 0);
                double s = warp.At<float>(1, 0);
                double angle = Math.Atan2(s, c) * 180.0 / Math.PI;

                return (true, warp, angle, result);
            }
            catch
            {
                return (false, warp, 0, moving.Clone());
            }
        }

        // 太阳极轴北上旋转：自动提取时间 → 下载当天最接近的GONG图 → 计算北极角
        public static List<double> SolarPoleNorthUpCalc(MainForm mainForm, List<Mat> images, List<string> imagePaths, int targetIndex = 0, bool isLog = true)
        {
            List<double> angles = new List<double>();
            if (images == null || images.Count == 0 || imagePaths == null || imagePaths.Count == 0)
                return Enumerable.Repeat(0.0, images.Count).ToList();

            // 本地函数：获取角度（自动使用缓存的当天文件列表）
            double GetNorthAngle(int i, Dictionary<string, string> gongCache = null)
            {
                string file = Path.GetFileName(imagePaths[i]);
                DateTime time = ExtractUniversalTimeFromName(file);

                string gongUrl = FindClosestGongImageUrlCached(time, gongCache);
                if (string.IsNullOrEmpty(gongUrl)) return 0.0;

                if (isLog)
                    mainForm.Log($"GONG Ha 参考图像 → {gongUrl}");

                using Mat reference = DownloadAndConvertGongToMat(gongUrl, mainForm, isLog);
                if (reference == null || reference.Empty()) return 0.0;

                using Mat gray = new();

                if (images[i].Channels() > 1)
                    Cv2.CvtColor(images[i], gray, ColorConversionCodes.BGR2GRAY);
                else
                    images[i].CopyTo(gray);

                try
                {
                    // =========================
                    // 1️⃣ PCA粗对齐（单帧版）
                    // =========================
                    
                    var (angle, scale, translation, warp) = AlignByPCA(gray, reference);

                    Mat pcaAligned = new();
                    Cv2.WarpAffine(
                        gray,
                        pcaAligned,
                        warp,
                        reference.Size(),
                        InterpolationFlags.Linear,
                        BorderTypes.Constant,
                        Scalar.Black
                    );

                    double finalAngle = angle;
                    if (isLog)
                        mainForm.Log($"PCA旋转对齐角度差 → {finalAngle}°");

                    // =========================
                    // 2️⃣ ECC精修（可选）
                    // =========================
                    if (isLog)
                        mainForm.Log("开始ECC精修");
                    if (ConfigManager.Config.ECCAlign)
                    {
                        var ecc = RefineByECC(reference, pcaAligned, warp);

                        if (ecc.success)
                        {
                            finalAngle = angle + ecc.angle;
                            if (isLog)
                                mainForm.Log($"ECC精修旋转对齐角度差 → {finalAngle}°");
                        }
                    }

                    return -finalAngle;
                }
                catch
                {
                    return 0.0;
                }
            }

            // 模式1：只算目标图 → 全部应用同一个角度
            if (ConfigManager.Config.SolarPoleNorthUpMode == "OnlyTarget")
            {
                double angle = GetNorthAngle(targetIndex);
                angles = Enumerable.Repeat(angle, images.Count).ToList();
                if (isLog) mainForm.Log($"校准太阳自转轴方向 → 统一旋转参考图旋转角度: {angle:F2}°");
            }

            // 模式2：每张图计算 → 同一天GONG列表只获取一次（超级快）
            else if (ConfigManager.Config.SolarPoleNorthUpMode == "AllImages")
            {
                var gongDayCache = new Dictionary<string, string>();

                for (int i = 0; i < images.Count; i++)
                {
                    double angle = GetNorthAngle(i, gongDayCache);
                    angles.Add(angle);
                    if (isLog) mainForm.Log($"校准太阳自转轴方向 → 第{i + 1}张图旋转角度: {angle:F2}°");
                }

                if (isLog) mainForm.Log("校准太阳自转轴方向 → 全部独立校准完成");
            }

            return angles;
        }

        // 【优化版】当天GONG文件列表只获取一次，缓存起来
        private static string FindClosestGongImageUrlCached(DateTime targetTime, Dictionary<string, string> dayCache)
        {
            try
            {
                string dayKey = targetTime.ToString("yyyyMMdd");
                if (dayCache != null && dayCache.ContainsKey(dayKey))
                    return dayCache[dayKey];

                string dayPath = $"https://gong2.nso.edu/HA/haf/{targetTime:yyyyMM}/{targetTime:yyyyMMdd}/";
                string page = new System.Net.WebClient().DownloadString(dayPath);

                DateTime bestDt = default;
                string bestFile = null;

                foreach (string line in page.Split('\n'))
                {
                    // 匹配所有以 h.fits.fz 结尾的文件（不限制 Bh）
                    if (line.Contains("Lh.fits.fz"))
                    {
                        int startA = line.IndexOf("href=\"") + 6;
                        int endB = line.IndexOf(".fits.fz", startA) + 8;
                        if (startA < 6 || endB > line.Length) continue;

                        string fn = line.Substring(startA, endB - startA);

                        // 🔥 只改这里：让 dt 保持 UTC，不转本地时间！
                        if (DateTime.TryParseExact(fn.Substring(0, 14), "yyyyMMddHHmmss",
                            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var dt))
                        {
                            if (bestFile == null || Math.Abs((dt - targetTime).TotalSeconds) < Math.Abs((bestDt - targetTime).TotalSeconds))
                            {
                                bestDt = dt;
                                bestFile = fn;
                            }
                        }
                    }
                }

                string bestUrl = bestFile == null ? null : $"{dayPath}{bestFile}";
                if (dayCache != null) dayCache[dayKey] = bestUrl;
                return bestUrl;
            }
            catch { return null; }
        }

        private static DateTime ExtractUniversalTimeFromName(string fileName)
        {
            try
            {
                var digits = new string(fileName.Where(char.IsDigit).ToArray());
                if (digits.Length >= 12)
                {
                    int year = int.Parse(digits.Substring(0, 4));
                    int month = int.Parse(digits.Substring(4, 2));
                    int day = int.Parse(digits.Substring(6, 2));
                    int hour = int.Parse(digits.Substring(8, 2));
                    int min = int.Parse(digits.Substring(10, 2));
                    return new DateTime(year, month, day, hour, min, 0, DateTimeKind.Utc);
                }
            }
            catch { }
            return DateTime.UtcNow;
        }


        private static Mat DownloadAndConvertGongToMat(string url, MainForm mainForm, bool isLog)
        {
            string tempDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");
            Directory.CreateDirectory(tempDir);

            string id = Guid.NewGuid().ToString("N");

            string tempFz = Path.Combine(tempDir, $"{id}.fits.fz");
            string fits = Path.Combine(tempDir, $"{id}.fits");

            try
            {
                // =========================
                // 下载
                // =========================
                using (var wc = new System.Net.WebClient())
                    wc.DownloadFile(url, tempFz);

                // =========================
                // 解压
                // =========================
                string funpack = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Funpack.exe");

                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = funpack,
                    Arguments = $"\"{tempFz}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = tempDir
                });

                process.WaitForExit();

                // 删除压缩包
                File.Delete(tempFz);

                // 🔥 关键：等待 FITS 可访问（避免锁）
                WaitFileReady(fits);

                // =========================
                // 读取
                // =========================
                Mat mat = FitsToMat(fits, isLog);

                // 🔥 立即释放文件（避免后续锁）
                GC.Collect();
                GC.WaitForPendingFinalizers();

                File.Delete(fits);

                if (isLog && mat != null && !mat.Empty())
                    mainForm.Log("✅ GONG 标准图已加载");

                return mat;
            }
            catch (Exception ex)
            {
                if (isLog)
                    mainForm.Log($"❌ GONG 处理失败：{ex.Message}");

                return null;
            }
        }

        private static void WaitFileReady(string path, int timeoutMs = 3000)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        return; // 可以打开说明没被占用
                    }
                }
                catch
                {
                    System.Threading.Thread.Sleep(50);
                }
            }

            throw new IOException($"File still locked: {path}");
        }

        public static Mat FitsToMat(string fitsPath, bool isLog)
        {
            try
            {
                // 转 Mat
                Mat mat = FitsIO.ReadGongMat(fitsPath);

                return mat;
            }
            catch (Exception ex)
            {
                if (isLog)
                    Console.WriteLine("FITS读取失败：" + ex.Message);

                return null;
            }
        }

        // 太阳特征预处理：质心找圆心 + 亮度分布算半径 + 去边缘 + 去小噪点
        private static Mat PreprocessSolarFeature(Mat mat)
        {
            Mat gray = new Mat();
            if (mat.Channels() > 1)
                Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
            else
                mat.CopyTo(gray);

            Mat stretched = new Mat();
            Cv2.Normalize(gray, stretched, 0, 255, NormTypes.MinMax, MatType.CV_8U);

            // ==========================
            // 1. 用你的函数 → 求太阳真实质心（圆心）
            // ==========================
            Point2d centroid = GetImageCentroid(stretched);
            int cx = (int)Math.Round(centroid.X);
            int cy = (int)Math.Round(centroid.Y);
            Point center = new Point(cx, cy);

            // ==========================
            // 2. 🔥 科学计算太阳半径（从中心向外找亮度突变点，100%正确）
            // ==========================
            int radius = EstimateSunRadiusFromEdgeContour(stretched, center);

            // ==========================
            // 3. 内缩掩膜，去掉边缘干扰
            // ==========================
            int innerRadius = (int)(radius * 0.9);
            Mat innerMask = Mat.Zeros(stretched.Size(), MatType.CV_8U);
            Cv2.Circle(innerMask, center, innerRadius, Scalar.White, -1);

            // 提取有效区域
            Mat roi = stretched;

            //Cv2.ImWrite(@"D:\System default\下载\roi.png", roi);

            // ======================
            // 提取耀斑
            // ======================
            Mat blobs = new Mat();
            Mat blurBright = new Mat();

            blurBright = AdaptiveGaussianBlur(roi, radius);
            Cv2.Subtract(roi, blurBright, blobs);
            Cv2.Threshold(blobs, blobs, 18, 255, ThresholdTypes.Binary);
            Cv2.BitwiseAnd(blobs, innerMask, blobs);

            //Cv2.ImWrite(@"D:\System default\下载\blobs.png", blobs);

            // ======================
            // 提取黑子
            // ======================
            /*
            Mat dark = new Mat();
            Mat blurDark = new Mat();

            blurDark = AdaptiveGaussianBlur(roi, radius);
            Cv2.Subtract(blurDark, roi, dark);
            Cv2.Threshold(dark, dark, 10, 255, ThresholdTypes.Binary);
            Cv2.BitwiseAnd(dark, innerMask, dark);

            Cv2.ImWrite(@"D:\System default\下载\dark.png", dark);

            // ======================
            // 合并
            // ======================
            Mat binary = new Mat();
            Cv2.BitwiseOr(blobs, dark, binary);
            Cv2.BitwiseAnd(binary, innerMask, binary);


            Cv2.ImWrite(@"D:\System default\下载\binary.png", binary);

            */

            Mat binary = blobs.Clone();

            // ======================
            // 去除小噪点（你要的）
            // ======================
            // 1. 自适应计算核大小（短边 1/20，自动奇数，最小11）
            int kernelSize = radius / 200;
            kernelSize = kernelSize % 2 == 0 ? kernelSize + 1 : kernelSize;

            Mat cleaned = new Mat();
            using Mat kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(kernelSize, kernelSize));
            Cv2.MorphologyEx(binary, cleaned, MorphTypes.Open, kernel);

            //Cv2.ImWrite(@"D:\System default\下载\cleaned.png", cleaned);

            // 释放资源
            gray.Dispose(); stretched.Dispose(); innerMask.Dispose(); roi.Dispose();
            blobs.Dispose(); blurBright.Dispose();
            //dark.Dispose(); blurDark.Dispose();
            binary.Dispose(); kernel.Dispose();

            return cleaned;
        }

        // ==========================
        // 🔥 按你说的方法：利用边缘轮廓点，统计距离求半径
        // ==========================
        private static int EstimateSunRadiusFromEdgeContour(Mat gray8u, Point center)
        {
            // 1. 做一次高反差保留，让太阳边缘出现一圈白边（就像你这张图的效果）
            Mat edgeMask = new Mat();
            Mat blur = new Mat();

            // 1. 自适应计算核大小（短边 1/20，自动奇数，最小11）
            int shortSide = Math.Min(gray8u.Cols, gray8u.Rows);
            int kernelSize = shortSide / 20;
            kernelSize = kernelSize % 2 == 0 ? kernelSize + 1 : kernelSize;

            Cv2.GaussianBlur(gray8u, blur, new Size(kernelSize, kernelSize), 0);
            Cv2.Subtract(gray8u, blur, edgeMask);

            // 2. 二值化提取边缘轮廓点
            Cv2.Threshold(edgeMask, edgeMask, 15, 255, ThresholdTypes.Binary);

            // 3. 找出所有轮廓点，计算它们到圆心的距离
            List<double> distances = new List<double>();
            var contours = Cv2.FindContoursAsArray(edgeMask, RetrievalModes.List, ContourApproximationModes.ApproxSimple);

            foreach (var cnt in contours)
            {
                foreach (var p in cnt)
                {
                    double dx = p.X - center.X;
                    double dy = p.Y - center.Y;
                    double dist = Math.Sqrt(dx * dx + dy * dy);
                    distances.Add(dist);
                }
            }

            edgeMask.Dispose();
            if (distances.Count == 0)
                return Math.Min(gray8u.Cols, gray8u.Rows) / 2;

            // 4. 统计：用直方图/频率法，找出“出现次数最多的距离”，就是太阳边缘
            // 这里用一种简单但有效的方式：
            // - 先排序
            // - 找局部密度最高的区间（也就是大部分点都集中在的那个半径附近）
            distances.Sort();

            double maxCount = 0;
            double bestRadius = 0;
            int windowSize = distances.Count / 20; // 取 5% 的点做滑动窗口

            for (int i = 0; i < distances.Count - windowSize; i++)
            {
                double windowEnd = distances[i + windowSize] - distances[i];
                if (windowEnd < 10.0) // 距离差小于10像素，说明这些点集中在同一个半径附近
                {
                    double avg = distances.Skip(i).Take(windowSize).Average();
                    if (windowSize > maxCount)
                    {
                        maxCount = windowSize;
                        bestRadius = avg;
                    }
                }
            }

            // 如果没找到，就用中位数兜底
            if (bestRadius < 1)
            {
                bestRadius = distances[distances.Count / 2];
            }

            return (int)Math.Round(bestRadius);
        }

        /// <summary>
        /// 自适应高斯模糊：自动按图像短边 1/20 计算核大小，返回模糊后的图像
        /// </summary>
        private static Mat AdaptiveGaussianBlur(Mat image, int radius)
        {
            // 1. 自适应计算核大小
            int kernelSize = radius / 10;
            kernelSize = kernelSize % 2 == 0 ? kernelSize + 1 : kernelSize;

            // 2. 执行高斯模糊
            Mat blurred = new Mat();
            Cv2.GaussianBlur(image, blurred, new Size(kernelSize, kernelSize), 0);

            return blurred;
        }
    }
}
