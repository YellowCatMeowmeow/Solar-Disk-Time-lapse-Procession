using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolarImageProcessionCsharp
{
    // 图像IO (CV2 重写)
    public class ImageIO
    {
        public static async void LoadImages(MainForm mainform, ImageForm imageform, string folder, List<Mat> images, List<string> imagePaths, string stateName = "原图")
        {
            mainform.SetAllControlsEnabled(false);
            mainform.UpdateStatus("正在加载图像，请稍候...");
            await Task.Run(() =>
            {
                try
                {
                    // 释放旧图像资源
                    foreach (var img in images) img.Dispose();
                    images.Clear();
                    imagePaths.Clear();

                    // 获取文件夹所有文件
                    var allFiles = Directory.GetFiles(folder, "*.*").ToList();
                    var targetFiles = new List<string>();

                    // 根据配置筛选需要读取的格式
                    if (ConfigManager.Config.EnableReadTif)
                        targetFiles.AddRange(allFiles.Where(f => f.ToLower().EndsWith(".tif") || f.ToLower().EndsWith(".tiff")));

                    if (ConfigManager.Config.EnableReadJpg)
                        targetFiles.AddRange(allFiles.Where(f => f.ToLower().EndsWith(".jpg") || f.ToLower().EndsWith(".jpeg")));

                    if (ConfigManager.Config.EnableReadPng)
                        targetFiles.AddRange(allFiles.Where(f => f.ToLower().EndsWith(".png")));

                    if (ConfigManager.Config.EnableReadFit)
                        targetFiles.AddRange(allFiles.Where(f => f.ToLower().EndsWith(".fit") || f.ToLower().EndsWith(".fits")));

                    // 遍历读取
                    foreach (var f in targetFiles)
                    {
                        Mat mat = null;
                        try
                        {
                            string ext = Path.GetExtension(f).ToLower();

                            // FIT 格式专用读取
                            if (ext == ".fit" || ext == ".fits")
                            {
                                mat = FitsIO.ReadMat(f);
                            }
                            // 普通图像格式
                            else
                            {
                                mat = Cv2.ImRead(f, ImreadModes.Unchanged);
                            }

                            // 验证图像是否有效
                            if (mat == null || mat.Empty())
                            {
                                mat?.Dispose();
                                continue;
                            }

                            // ==============================================
                            // 核心：统一转为 灰度 + 16 位图像（关键改动）
                            // ==============================================
                            Mat finalMat = new Mat();

                            // 1. 转灰度（如果是彩色图）
                            if (mat.Channels() > 1)
                                Cv2.CvtColor(mat, finalMat, ColorConversionCodes.BGR2GRAY);
                            else
                                mat.CopyTo(finalMat);

                            // 2. 统一转 16 位无符号（确保所有图都是 16位 灰度）
                            if (finalMat.Depth() != MatType.CV_16U)
                            {
                                if (finalMat.Depth() == MatType.CV_8U)
                                {
                                    // 8位图像需要放大 256 倍才能正确转换到16位
                                    finalMat.ConvertTo(finalMat, MatType.CV_16U, 256.0);
                                }
                                else
                                    finalMat.ConvertTo(finalMat, MatType.CV_16U);
                            }
                                

                            // 加入列表
                            images.Add(finalMat);
                            imagePaths.Add(f);

                            // 释放临时变量
                            mat.Dispose();
                        }
                        catch
                        {
                            mat?.Dispose();
                            continue;
                        }
                    }

                    // 加载成功后界面更新
                    if (images.Count > 0)
                    {
                        mainform.ProcessStateUpdate("原图", images);
                        imageform.InitFrameSlider(images.Count);

                        // 显示第一张图
                        if (mainform.GetLightEnhanceShowState())
                            imageform.LoadImage(ImageAlignment.AdjustBrightness(images[0], mainform.GetLightEnhanceValue()), true);
                        else
                            imageform.LoadImage(images[0], true);

                        string firstFileName = Path.GetFileName(imagePaths[0]);
                        imageform.SetTitleByFileName(firstFileName, "原图");

                        mainform.Invoke(() => {
                            mainform.SetTargetIndexLimit(images.Count);
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载出错：" + ex.Message);
                }
            });

            mainform.SetAllControlsEnabled(true);
            mainform.UpdateStatus($"已加载 {images.Count} 张太阳图像");
        }

        public static async Task SaveImages(List<Mat> images, List<string> names, string outFolder)
        {
            if (!Directory.Exists(outFolder))
                Directory.CreateDirectory(outFolder);

            string ext = ConfigManager.Config.SaveFormat.ToLower();

            for (int i = 0; i < images.Count; i++)
            {
                string name = Path.GetFileNameWithoutExtension(names[i]) + "." + ext;
                string savePath = Path.Combine(outFolder, name);
                Mat imgToSave = images[i];
                bool needDispose = false;

                try
                {
                    if (ext is "jpg" or "jpeg")
                    {
                        if (imgToSave.Depth() != MatType.CV_8U)
                        {
                            imgToSave = new Mat();
                            images[i].ConvertTo(imgToSave, MatType.CV_8U, 1.0 / 255.0);
                            needDispose = true;
                        }
                        Cv2.ImWrite(savePath, imgToSave);
                    }
                    else if (ext is "png")
                    {
                        if (imgToSave.Depth() != MatType.CV_16U)
                        {
                            imgToSave = new Mat();
                            images[i].ConvertTo(imgToSave, MatType.CV_16U, 256.0);
                            needDispose = true;
                        }
                        Cv2.ImWrite(savePath, imgToSave);
                    }
                    else if (ext is "tif" or "tiff")
                    {
                        if (imgToSave.Depth() != MatType.CV_16U)
                        {
                            imgToSave = new Mat();
                            images[i].ConvertTo(imgToSave, MatType.CV_16U, 256.0);
                            needDispose = true;
                        }

                        // ✅ 正确通用写法：TIFF 无压缩，解决兼容性报错
                        int[] tiffParams = { (int)ImwriteFlags.TiffCompression, 1 };
                        Cv2.ImWrite(savePath, imgToSave, tiffParams);
                    }
                    else if (ext is "fit" or "fits")
                    {
                        FitsIO.SaveMat(imgToSave, savePath);
                    }
                    else
                    {
                        Cv2.ImWrite(savePath, imgToSave);
                    }
                }
                finally
                {
                    if (needDispose && imgToSave != null)
                        imgToSave.Dispose();
                }
            }
        }

    }
}
