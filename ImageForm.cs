using OpenCvSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using OpenCvSharp.Extensions;

namespace SolarImageProcessionCsharp
{
    public partial class ImageForm : Form
    {
        // 🔥 关键：定义一个“委托”，滑条动了就通知主窗体
        public event Action<int> OnFrameChanged;

        public ImageForm()
        {
            InitializeComponent();
            pictureBoxImageShow.Location = new System.Drawing.Point(0, 0);
            pictureBoxImageShow.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            SetImageControlsEnabled(false);
            this.ControlBox = false;
        }
        // 启用/禁用所有缩放、帧控件
        public void SetImageControlsEnabled(bool enabled)
        {
            trackBarFrame.Enabled = enabled;
            numericFrame.Enabled = enabled;
            trackBarZoom.Enabled = enabled;
            numericZoom.Enabled = enabled;
        }

        // 设置窗口标题 = 文件名
        public void SetTitleByFileName(string fileName, string stateName)
        {
            if (InvokeRequired)
            {
                Invoke(() => SetTitleByFileName(fileName,stateName));
                return;
            }
            this.Text = stateName + " - " + fileName;
        }

        // 初始化帧滑块及数字框范围
        public void InitFrameSlider(int totalFrames)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => InitFrameSlider(totalFrames)));
                return;
            }

            if (totalFrames <= 1)
            {
                trackBarFrame.Visible = false;
                numericFrame.Visible = false;
                return;
            }

            trackBarFrame.Visible = true;
            numericFrame.Visible = true;

            trackBarFrame.Minimum = 0;
            trackBarFrame.Maximum = totalFrames - 1;
            trackBarFrame.Value = 0;

            numericFrame.Minimum = 0;
            numericFrame.Maximum = totalFrames - 1;
            numericFrame.Value = 0;
        }

        // 加载 OpenCV Mat 图像并显示
        public void LoadImage(Mat mat, bool isInitial = false)
        {
            // 跨线程安全
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => LoadImage(mat, isInitial)));
                return;
            }

            if (mat == null || mat.Empty())
                return;

            try
            {
                Mat mat8u = new Mat();
                mat.ConvertTo(mat8u, MatType.CV_8U, 255.0 / 65535.0);
                Bitmap bmp = mat8u.ToBitmap();

                // 释放旧图片
                if (pictureBoxImageShow.Image != null)
                    pictureBoxImageShow.Image.Dispose();

                pictureBoxImageShow.Image = bmp;
                pictureBoxImageShow.SizeMode = PictureBoxSizeMode.Zoom;
                if (isInitial)
                {
                    // 拿到显示区域（Panel 的客户区）
                    System.Drawing.Size containerSize = pictureBoxImageShow.Parent.ClientSize;
                    // 计算等比适配大小
                    System.Drawing.Size fitSize = GetFitProportionalSize(bmp.Size, containerSize);

                    // 直接赋值宽高 → 等比、左上对齐
                    pictureBoxImageShow.Size = fitSize;

                    // 计算真实百分比并同步到 UI
                    int scale = (int)((float)fitSize.Width / bmp.Width * 100);
                    trackBarZoom.Value = scale;
                    numericZoom.Value = scale;
                }
                
                SetImageControlsEnabled(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("图像显示失败：" + ex.Message);
                SetImageControlsEnabled(false);
            }
        }

        // 等比缩放计算：把原图 适配到 目标显示区域
        private System.Drawing.Size GetFitProportionalSize(System.Drawing.Size original, System.Drawing.Size container)
        {
            float ratioW = (float)container.Width / original.Width;
            float ratioH = (float)container.Height / original.Height;
            float ratio = Math.Min(ratioW, ratioH);

            return new System.Drawing.Size(
                (int)(original.Width * ratio),
                (int)(original.Height * ratio)
            );
        }

        // ===============================
        // 【帧 双向联动】
        // ===============================
        private void trackBarFrame_Scroll(object sender, EventArgs e)
        {
            int idx = trackBarFrame.Value;
            numericFrame.Value = idx;         // 同步到数字框
            OnFrameChanged?.Invoke(idx);
        }

        private void numericFrame_ValueChanged(object sender, EventArgs e)
        {
            int idx = (int)numericFrame.Value;
            trackBarFrame.Value = idx;        // 同步到滑块
            OnFrameChanged?.Invoke(idx);
        }

        // ===============================
        // 【缩放 双向联动】
        // ===============================
        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            int zoom = trackBarZoom.Value;
            numericZoom.Value = zoom;        // 同步到数字框
            ApplyZoom(zoom);
        }

        private void numericZoom_ValueChanged(object sender, EventArgs e)
        {
            int zoom = (int)numericZoom.Value;
            trackBarZoom.Value = zoom;       // 同步到滑块
            ApplyZoom(zoom);
        }

        // 给主窗体获取当前显示的帧
        public int GetCurrentFrameIndex()
        {
            return trackBarFrame.Value;
        }

        // 公开方法：刷新当前显示的帧
        public void RefreshCurrentFrame()
        {
            OnFrameChanged?.Invoke(trackBarFrame.Value);
        }

        // 统一执行缩放
        private void ApplyZoom(int percent)
        {
            if (pictureBoxImageShow.Image == null) return;

            float scale = percent / 100f;
            int w = (int)(pictureBoxImageShow.Image.Width * scale);
            int h = (int)(pictureBoxImageShow.Image.Height * scale);

            pictureBoxImageShow.Size = new System.Drawing.Size(w, h);
            pictureBoxImageShow.Refresh();
        }

    }
}