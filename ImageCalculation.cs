using OpenCvSharp;
using System;
using System.Collections.Generic;

namespace SolarImageProcessionCsharp
{
    // 图像计算工具
    public class ImageCalculation
    {
        /// <summary>
        /// 获取百分位范围（CV2原生实现）
        /// </summary>
        public static void GetPercentileRange(Mat img, float p1, float p2, out float min, out float max, out float mean)
        {
            Mat flat = img.Reshape(1, 1);
            Mat sorted = new Mat();
            Cv2.Sort(flat, sorted, SortFlags.Ascending);

            int total = (int)sorted.Total();
            int idx1 = (int)(total * p1 / 100);
            int idx2 = (int)(total * p2 / 100);
            idx1 = Math.Clamp(idx1, 0, total - 1);
            idx2 = Math.Clamp(idx2, 0, total - 1);

            min = sorted.Get<float>(0, idx1);
            max = sorted.Get<float>(0, idx2);
            mean = (float)Cv2.Mean(img).Val0;

            sorted.Dispose();
            flat.Dispose();
        }

        /// <summary>
        /// 获取均值
        /// </summary>
        public static void GetMean(Mat img, out float mean)
        {
            mean = (float)Cv2.Mean(img).Val0;
        }

        /// <summary>
        /// 居中填充到指定尺寸（黑边）
        /// </summary>
        public static Mat PadToSize(Mat src, int targetW, int targetH)
        {
            Mat dst = new Mat(new OpenCvSharp.Size(targetW, targetH), src.Type(), Scalar.All(0));
            int x = (targetW - src.Width) / 2;
            int y = (targetH - src.Height) / 2;

            Rect roi = new Rect(x, y, src.Width, src.Height);
            src.CopyTo(dst[roi]);
            return dst;
        }

        /// <summary>
        /// Mat 转浮点数组
        /// </summary>
        public static float[,] MatToFloatBuffer(Mat mat)
        {
            int h = mat.Height;
            int w = mat.Width;
            float[,] buf = new float[h, w];
            mat.ConvertTo(mat, MatType.CV_32F);

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    buf[y, x] = mat.Get<float>(y, x);

            return buf;
        }

        /// <summary>
        /// 寻找非零外接矩形（CV2原生）
        /// </summary>
        public static Rect FindNonZeroBoundingRect(Mat img)
        {
            Mat binary = new Mat();
            Cv2.Threshold(img, binary, 0, 65535, ThresholdTypes.Binary);
            Rect rect = Cv2.BoundingRect(binary);
            binary.Dispose();
            return rect;
        }

        /// <summary>
        /// 自动裁剪
        /// </summary>
        public static List<Mat> AutoCrop(List<Mat> images)
        {
            List<Mat> cropped = new List<Mat>();
            int maxCropX = 0, maxCropY = 0;

            foreach (var img in images)
            {
                Rect rect = FindNonZeroBoundingRect(img);
                int cx = img.Width - rect.Width;
                int cy = img.Height - rect.Height;
                maxCropX = Math.Max(maxCropX, cx);
                maxCropY = Math.Max(maxCropY, cy);
            }

            int margin = 2;
            int x = maxCropX / 2 + margin;
            int y = maxCropY / 2 + margin;
            int w = images[0].Width - maxCropX - margin * 2;
            int h = images[0].Height - maxCropY - margin * 2;
            Rect cropRoi = new Rect(x, y, w, h);

            foreach (var img in images)
            {
                Mat crop = new Mat(img, cropRoi);
                cropped.Add(crop);
            }
            return cropped;
        }
    }
}
