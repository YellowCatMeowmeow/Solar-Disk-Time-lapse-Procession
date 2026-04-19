using OpenCvSharp;
using System.Collections.Generic;

namespace SolarImageProcessionCsharp
{
    // 亮度归一化 (CV2 重写)
    public class ImageLightNormalization
    {
        public static List<Mat> NormalizeLight(MainForm mainForm, List<Mat> images, int targetIndex = 0)
        {
            int imagesCount = images.Count;
            mainForm.Log($"共{imagesCount}张图像");
            mainForm.Log($"目标对齐图像 → 第{targetIndex + 1}张");
            List<Mat> result = new List<Mat>();
            Mat target = images[targetIndex];
            Mat dst_target = target.Clone();
            dst_target.ConvertTo(dst_target, MatType.CV_32F);

            // 获取目标图像百分位和均值
            ImageCalculation.GetPercentileRange(dst_target, 1, 99, out float tMin, out float tMax, out float tMean);
            float targetRange = tMax - tMin;

            for (int i = 0; i < imagesCount; i++)
            {
                var img = images[i];
                Mat dst = img.Clone();
                dst.ConvertTo(dst, MatType.CV_32F);
                // 亮度对齐
                ImageCalculation.GetMean(dst, out float mean);
                float shiftMean = tMean - mean;

                // 对比度对齐
                ImageCalculation.GetPercentileRange(dst, 1, 99, out float sMin, out float sMax, out _);
                float currentRange = sMax - sMin;
                float scale = targetRange / (currentRange + 1e-6f);

                // 应用变换
                Cv2.Add(dst, new Scalar(shiftMean), dst);
                Cv2.Multiply(dst, new Scalar(scale), dst);
                // 替换 Clamp -> 用 Threshold 实现截断
                Cv2.Threshold(dst, dst, 0, 65535, ThresholdTypes.Tozero);
                Cv2.Threshold(dst, dst, 65535, 65535, ThresholdTypes.Trunc);

                // 最小值对齐
                ImageCalculation.GetPercentileRange(dst, 1, 99, out float aMin, out _, out _);
                float shift = tMin - aMin;
                Cv2.Add(dst, new Scalar(shift), dst);
                Cv2.Threshold(dst, dst, 0, 65535, ThresholdTypes.Tozero);
                Cv2.Threshold(dst, dst, 65535, 65535, ThresholdTypes.Trunc);


                // 官方标准写法：一次性转 16U，不会黑、不会负
                Mat dst16U = new Mat();
                dst.ConvertTo(dst16U, MatType.CV_16U);
                result.Add(dst16U);

                mainForm.Log($"第{i + 1}/{imagesCount}张图像亮度归一化完成");
            }
            return result;
        }
    }
}
