using MathNet.Numerics.IntegralTransforms;
using OpenCvSharp;
using OpenCvSharp.Features2D;
using OpenCvSharp.Flann;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Complex = System.Numerics.Complex;

namespace SolarImageProcessionCsharp
{
    public partial class MainForm : Form
    {
        private int targetIndexInCode = 0;
        private List<string> _imagePaths = new List<string>();
        private List<Mat> _images = new List<Mat>();
        private ImageForm imageform;
        private bool _imageFormEventBound = false;
        public string process_state = "未加载图像";
        private List<Mat> process_state_images = new List<Mat>();
        public event Action<int> OnLightEnhanceChanged;
        public int LightEnhanceValue = 100;
        public bool isLightEnhanceShow = false;

        public MainForm()
        {
            InitializeComponent();
            ConfigManager.Load();
            InitializeImageForm();
            AllowDrop = true;
            DragEnter += Form1_DragEnter;
            DragDrop += Form1_DragDrop;
            trackBarLightEnhance.Minimum = 0;
            trackBarLightEnhance.Maximum = 100;
            trackBarLightEnhance.Value = 100;

            numericLightEnhance.Minimum = 0;
            numericLightEnhance.Maximum = 100;
            numericLightEnhance.Value = 100;

            // ===================== 核心：一行绑定所有控件 =====================
            // 复选框（布尔值）
            checkReadTif.BindCheckBox(() => ConfigManager.Config.EnableReadTif, v => ConfigManager.Config.EnableReadTif = v);
            checkReadJpg.BindCheckBox(() => ConfigManager.Config.EnableReadJpg, v => ConfigManager.Config.EnableReadJpg = v);
            checkReadPng.BindCheckBox(() => ConfigManager.Config.EnableReadPng, v => ConfigManager.Config.EnableReadPng = v);
            checkReadFit.BindCheckBox(() => ConfigManager.Config.EnableReadFit, v => ConfigManager.Config.EnableReadFit = v);

            chkLightNormaization.BindCheckBox(() => ConfigManager.Config.EnableLightNormalization, v => ConfigManager.Config.EnableLightNormalization = v);
            checkFlip.BindCheckBox(() => ConfigManager.Config.EnableFlip, v => ConfigManager.Config.EnableFlip = v);    
            checkScaleAlign.BindCheckBox(() => ConfigManager.Config.EnableScaleAlign, v => ConfigManager.Config.EnableScaleAlign = v);
            checkRotationAlign.BindCheckBox(() => ConfigManager.Config.EnableRotationAlign, v => ConfigManager.Config.EnableRotationAlign = v);
            chkAlign.BindCheckBox(() => ConfigManager.Config.EnableAlign, v => ConfigManager.Config.EnableAlign = v);
            checkScaleAlignMaxResolution.BindCheckBox(() => ConfigManager.Config.ScaleAlignMaxResolution, v => ConfigManager.Config.ScaleAlignMaxResolution = v);
            checkSolarPoleNorthUp.BindCheckBox(() => ConfigManager.Config.SolarPoleNorthUp, v => ConfigManager.Config.SolarPoleNorthUp = v);
            checkBoxECC.BindCheckBox(() => ConfigManager.Config.ECCAlign, v => ConfigManager.Config.ECCAlign = v);

            // 数值框（整数）
            numericTargetIndex.BindNumericUpDown(() => ConfigManager.Config.TargetIndex, v => ConfigManager.Config.TargetIndex = v, v => targetIndexInCode = v - 1);
            numericAlignTimes.BindNumericUpDown(() => ConfigManager.Config.AlignTimes, v => ConfigManager.Config.AlignTimes = v);
            numericRotationAlignTimes.BindNumericUpDown(() => ConfigManager.Config.RotationAlignTimes, v => ConfigManager.Config.RotationAlignTimes = v);

            // 单选框（字符串分组）
            radioFullDisk.BindRadioButton("FullDisk", () => ConfigManager.Config.ImageAlignmentObject, v => ConfigManager.Config.ImageAlignmentObject = v);
            radioLocalArea.BindRadioButton("LocalArea", () => ConfigManager.Config.ImageAlignmentObject, v => ConfigManager.Config.ImageAlignmentObject = v);
            radioMassCenter.BindRadioButton("MassCenter", () => ConfigManager.Config.ImageAlignmentMode, v => ConfigManager.Config.ImageAlignmentMode = v);
            radioPhaseCorrelate.BindRadioButton("PhaseCorrelate", () => ConfigManager.Config.ImageAlignmentMode, v => ConfigManager.Config.ImageAlignmentMode = v);
            radioAlignFeature.BindRadioButton("FeatureAlign", () => ConfigManager.Config.ImageAlignmentMode, v => ConfigManager.Config.ImageAlignmentMode = v);
            radioMiddleFlipAuto.BindRadioButton("Auto", () => ConfigManager.Config.MiddleFlipMode, v => ConfigManager.Config.MiddleFlipMode = v);
            radioMiddleFlipManual.BindRadioButton("Manual", () => ConfigManager.Config.MiddleFlipMode, v => ConfigManager.Config.MiddleFlipMode = v);
            radioScaleAlignPhaseCorrelate.BindRadioButton("PhaseCorrelate", () => ConfigManager.Config.ScaleAlignmentMode, v => ConfigManager.Config.ScaleAlignmentMode = v);
            radioScaleAlignFeature.BindRadioButton("FeatureAlign", () => ConfigManager.Config.ScaleAlignmentMode, v => ConfigManager.Config.ScaleAlignmentMode = v);
            radioRotationAlignNone.BindRadioButton("None", () => ConfigManager.Config.RotationAlignmentMode, v => ConfigManager.Config.RotationAlignmentMode = v);
            radioFieldRotation.BindRadioButton("FieldRotation", () => ConfigManager.Config.RotationAlignmentMode, v => ConfigManager.Config.RotationAlignmentMode = v);
            radioMiddleFlip.BindRadioButton("MiddleFlip", () => ConfigManager.Config.RotationAlignmentMode, v => ConfigManager.Config.RotationAlignmentMode = v);
            radioRotationAlignPhaseCorrelate.BindRadioButton("PhaseCorrelate", () => ConfigManager.Config.RotationAlignmentMode, v => ConfigManager.Config.RotationAlignmentMode = v);
            radioRotationAlignFeature.BindRadioButton("FeatureAlign", () => ConfigManager.Config.RotationAlignmentMode, v => ConfigManager.Config.RotationAlignmentMode = v); 
            radioSolarPoleNorthUpOnlyTarget.BindRadioButton("OnlyTarget", () => ConfigManager.Config.SolarPoleNorthUpMode, v => ConfigManager.Config.SolarPoleNorthUpMode = v);
            radioSolarPoleNorthUpAllImages.BindRadioButton("AllImages", () => ConfigManager.Config.SolarPoleNorthUpMode, v => ConfigManager.Config.SolarPoleNorthUpMode = v);

            // 保存格式单选（同理）
            radioSaveTif.BindRadioButton("tif", () => ConfigManager.Config.SaveFormat, v => ConfigManager.Config.SaveFormat = v);
            radioSaveJpg.BindRadioButton("jpg", () => ConfigManager.Config.SaveFormat, v => ConfigManager.Config.SaveFormat = v);
            radioSavePng.BindRadioButton("png", () => ConfigManager.Config.SaveFormat, v => ConfigManager.Config.SaveFormat = v);
            radioSaveFit.BindRadioButton("fit", () => ConfigManager.Config.SaveFormat, v => ConfigManager.Config.SaveFormat = v);

            // 单选/复选框 ↔ 分组框 启用禁用联动
            chkLightNormaization.BindCheckBoxEnableGroup(groupLightNormalization);
            checkScaleAlign.BindCheckBoxEnableGroup(groupBoxScaleAlign);
            checkRotationAlign.BindCheckBoxEnableGroup(groupBoxRotationAlign);
            chkAlign.BindCheckBoxEnableGroup(groupAlign);

            checkSolarPoleNorthUp.BindCheckBoxEnableGroup(groupBoxSolarPoleNorthUp);

            radioMiddleFlipManual.BindRadioEnableControl(groupBoxMiddleFlipManual);

            radioFieldRotation.BindRadioEnableControl(groupBoxFieldRotation);
            radioMiddleFlip.BindRadioEnableControl(groupBoxMiddleFlip);

           
            groupBoxFeatureAlign.BindAnyCheckEnableControl(radioScaleAlignFeature, radioRotationAlignFeature, radioAlignFeature, checkSolarPoleNorthUp);

            // 绑定联动
            ControlBinder.BindRangeNumericUpDown(numericMiddleFlipManual1, numericMiddleFlipManual2, () => _images.Count);

            textBoxLatitude.BindCoordinateTextBox(() => ConfigManager.Config.Latitude, val => ConfigManager.Config.Latitude = val, -90, 90);

            textBoxLongitude.BindCoordinateTextBox(() => ConfigManager.Config.Longitude, val => ConfigManager.Config.Longitude = val, -180, 180);
            // ================================================================
        }

        //-----------------------------
        // 图像窗口相关
        //-----------------------------

        // 初始化图像窗口
        private void InitializeImageForm()
        {
            imageform = OpenImageForm();

        }

        // 创建并打开图像窗口
        public ImageForm OpenImageForm()
        {
            if (imageform != null && !imageform.IsDisposed)
            {
                return imageform;
            }

            imageform = new ImageForm();
            imageform.Show();

            // 图像窗口事件绑定
            imageform.OnFrameChanged += (index) =>
            {
                if (index >= 0 && index < process_state_images.Count)
                {
                    if (isLightEnhanceShow)
                        imageform.LoadImage(ImageAlignment.AdjustBrightness(process_state_images[index], trackBarLightEnhance.Value));
                    else
                        imageform.LoadImage(process_state_images[index]);
                    string fileName = Path.GetFileName(_imagePaths[index]);
                    imageform.SetTitleByFileName(fileName, process_state);
                }
            };
            _imageFormEventBound = true;

            return imageform;
        }

        //更新状态文本和当前图像列表
        public void ProcessStateUpdate(string newState, List<Mat> newImages)
        {
            process_state = newState;
            process_state_images = newImages.Select(mat => mat.Clone()).ToList();
        }

        // 在图像窗口显示图像
        public void ShowImageInImageForm(Mat targetImage, bool isInitial = false)
        {
            imageform.LoadImage(targetImage, isInitial); // ImageForm需同步改为Mat

        }

        //-----------------------------
        // 组件操作相关
        //-----------------------------

        // 日志输出
        public void Log(string msg)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => Log(msg)));
                return;
            }

            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
            rtbLog.ScrollToCaret();
        }

        // 提示文本刷新
        public void UpdateStatus(string msg)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => UpdateStatus(msg)));
                return;
            }
            lblStatus.Text = msg;
        }

        // 统一启用/禁用所有界面控件
        private void SetControlsEnabled(bool enabled)
        {
            txtFolderPath.Enabled = enabled;
            btnSelectFolder.Enabled = enabled;
            btnLoadPath.Enabled = enabled;
            btnProcess.Enabled = enabled;
        }

        // 递归启用/禁用所有控件
        public void SetAllControlsEnabled(bool enabled)
        {
            foreach (System.Windows.Forms.Control control in this.Controls)
            {
                // 排除：日志分组、日志框、标签
                if (control is Label
                    || control == groupLogText
                    || control == rtbLog)
                {
                    continue;
                }

                control.Enabled = enabled;
            }
            this.Refresh();
        }

        // Debug图片查看器 (CV2版本)
        public void DebugShowImage(Mat mat, string title = "Debug")
        {
            if (mat == null) return;
            string tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            Cv2.ImWrite(tempPath, mat);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(tempPath) { UseShellExecute = true });
        }

        // 重载：float[,] → Mat
        public void DebugShowImage(float[,] floatImage, string title = "Debug")
        {
            Mat mat = new Mat(floatImage.GetLength(0), floatImage.GetLength(1), MatType.CV_32FC1);
            for (int y = 0; y < mat.Height; y++)
                for (int x = 0; x < mat.Width; x++)
                    mat.Set<float>(y, x, floatImage[y, x]);

            // 归一化到0-1
            Cv2.Normalize(mat, mat, 0, 1, NormTypes.MinMax);
            DebugShowImage(mat, title);
        }

        //------------------------------
        // 菜单栏
        //------------------------------

        // 菜单：文件 → 打开文件夹
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSelectFolder.PerformClick();
        }
        // 菜单：文件 → 退出
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // 菜单：帮助 → 关于
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("太阳图像对齐工具 v0.1.0  by 黄喵", "关于");
        }
        // 菜单：帮助 → 更新日志
        private void updatelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UpdateLogForm().ShowDialog();
        }

        //------------------------------
        // 图像加载
        //------------------------------

        // 浏览文件夹
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.ValidateNames = false;
                ofd.CheckFileExists = false;
                ofd.CheckPathExists = true;
                ofd.FileName = "选择文件夹";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string folder = Path.GetDirectoryName(ofd.FileName);
                    if (!string.IsNullOrEmpty(folder))
                    {
                        txtFolderPath.Text = folder;
                        ImageIO.LoadImages(this, imageform, folder, _images, _imagePaths, process_state);
                    }
                }
            }
        }

        // 加载路径
        private void btnLoadPath_Click(object sender, EventArgs e)
        {
            string folder = txtFolderPath.Text.Trim();
            if (string.IsNullOrEmpty(folder))
            {
                MessageBox.Show("请输入文件夹路径或使用浏览按钮选择。");
                return;
            }
            if (!Directory.Exists(folder))
            {
                MessageBox.Show("路径不存在，请检查。");
                return;
            }
            ImageIO.LoadImages(this, imageform, folder, _images, _imagePaths, process_state);
        }

        // 回车加载
        private void txtFolderPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLoadPath.PerformClick();
            }
        }

        // 拖放加载进入
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (items != null && items.Length > 0 && Directory.Exists(items[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        // 拖放加载放下
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (items != null && items.Length > 0)
            {
                string path = items[0];
                if (Directory.Exists(path))
                {
                    txtFolderPath.Text = path;
                    ImageIO.LoadImages(this, imageform, path, _images, _imagePaths, process_state);
                }
                else
                {
                    MessageBox.Show("请拖放文件夹（不是文件）。");
                }
            }
        }

        //------------------------------
        // 图像处理
        //------------------------------
        private async void btnProcess_Click(object sender, EventArgs e)
        {
            if (_images.Count == 0)
            {
                MessageBox.Show("请先加载图像");
                return;
            }

            try
            {
                SetAllControlsEnabled(false);
                string root = Path.GetDirectoryName(_imagePaths[0]);

                List<Mat> normalized = null;
                List<Mat> fliped = null;
                List<Mat> scaled = null;
                List<Mat> rotationcalibrated = null;
                List<Mat> middleflipcalibrated = null;
                List<Mat> aligned = null;

                
                List<double> solarPoleNorthUpRotationAngles = null;
                List<double> fieldRotationAngles = null;
                List<double> mainRotationAngles = null;


                // 亮度归一化
                if (ConfigManager.Config.EnableLightNormalization)
                {
                    lblStatus.Text = "亮度归一化中...";
                    Log("亮度归一化中...");
                    await Task.Delay(10);
                    await Task.Run(() => normalized = ImageLightNormalization.NormalizeLight(this, _images, targetIndex: targetIndexInCode));
                    ImageIO.SaveImages(normalized, _imagePaths, Path.Combine(root, "lightNormalizationed"));
                    process_state = "亮度统一";
                }
                else
                {
                    normalized = _images.Select(mat => mat.Clone()).ToList();
                }
                process_state_images = normalized.Select(mat => mat.Clone()).ToList();
                if (normalized != null) foreach (var m in normalized) m.Dispose();

                // 翻转
                if (ConfigManager.Config.EnableFlip)
                {
                    lblStatus.Text = "翻转图像中...";
                    Log("翻转图像中...");
                    await Task.Delay(10);
                    await Task.Run(() => fliped = ImageAlignment.ImageFlip(process_state_images));
                    ImageIO.SaveImages(fliped, _imagePaths, Path.Combine(root, "flipped"));
                    process_state = "翻转";
                }
                else
                {
                    fliped = process_state_images.Select(mat => mat.Clone()).ToList();
                }
                process_state_images = fliped.Select(mat => mat.Clone()).ToList();
                if (fliped != null) foreach (var m in fliped) m.Dispose();


                // 相位相关法放缩旋转校准
                if ((ConfigManager.Config.EnableScaleAlign && ConfigManager.Config.ScaleAlignmentMode == "PhaseCorrelate") || 
                    ConfigManager.Config.RotationAlignmentMode == "PhaseCorrelate")
                {
                    lblStatus.Text = "相位相关法放缩校准中...";
                    Log("相位相关法放缩旋转校准中...");
                    await Task.Delay(10);
                    await Task.Run(() => (scaled, mainRotationAngles) = ImageAlignment.AlignScaleAndRotation(this, process_state_images,
                        targetIndex: targetIndexInCode,
                        isScale: ConfigManager.Config.ScaleAlignmentMode == "PhaseCorrelate",
                        isRotate: ConfigManager.Config.RotationAlignmentMode == "PhaseCorrelate",
                        isLog: true));
                    ImageIO.SaveImages(scaled, _imagePaths, Path.Combine(root, "scaleAligned"));
                    process_state = "放缩对齐";
                }
                if ((ConfigManager.Config.EnableScaleAlign && ConfigManager.Config.ScaleAlignmentMode == "FeatureAlign") || 
                    ConfigManager.Config.RotationAlignmentMode == "FeatureAlign")
                {
                    lblStatus.Text = "特征点放缩校准中...";
                    Log("特征点放缩旋转校准中...");
                    await Task.Delay(10);
                    await Task.Run(() => (scaled, mainRotationAngles) = ImageAlignment.AlignByPCAGroup(this, process_state_images,
                        targetIndex: targetIndexInCode,
                        isScale: ConfigManager.Config.ScaleAlignmentMode == "FeatureAlign",
                        isRotate: ConfigManager.Config.RotationAlignmentMode == "FeatureAlign",
                        isTranslate: false,
                        isLog: true));
                    ImageIO.SaveImages(scaled, _imagePaths, Path.Combine(root, "scaleAligned"));
                    process_state = "放缩对齐";
                }
                        
                if (!ConfigManager.Config.EnableScaleAlign)
                {
                    scaled = process_state_images.Select(mat => mat.Clone()).ToList();
                }
                process_state_images = scaled.Select(mat => mat.Clone()).ToList();
                if (scaled != null) foreach (var m in scaled) m.Dispose();


                // 旋转对齐
                if (ConfigManager.Config.EnableRotationAlign)
                {
                    // 场旋校准
                    if (ConfigManager.Config.RotationAlignmentMode == "FieldRotation")
                    {
                        List<double> rotationAngles = null;
                        lblStatus.Text = "场旋校准中...";
                        Log("场旋校准中...");
                        await Task.Delay(10);
                        await Task.Run(() => rotationAngles = ImageAlignment.CalculateFieldRotationAngles(this, process_state_images, _imagePaths, ConfigManager.Config.Latitude, ConfigManager.Config.Longitude, targetIndex: targetIndexInCode));
                        process_state = "场旋校准";
                    }

                    // 中天翻转校准
                    if (ConfigManager.Config.RotationAlignmentMode == "MiddleFlip")
                    {
                        int firstRotatedIndex = -1;
                        int lastRotatedIndex = -1;
                        int index1 = -1;
                        if (ConfigManager.Config.MiddleFlipMode == "Auto")
                        {
                            // 自动找旋转180°的第一张图像序号
                            lblStatus.Text = "自动中天翻转校准中...";
                            Log("自动中天翻转校准中...");
                            await Task.Delay(10);
                            await Task.Run(() => index1 = ImageAlignment.FindFirstRotated180Index(this, process_state_images));
                            if (index1 > targetIndexInCode)
                            {

                                firstRotatedIndex = index1;
                                lastRotatedIndex = _images.Count - 1;
                            }
                            else
                            {
                                firstRotatedIndex = 0;
                                lastRotatedIndex = index1 - 1;
                            }
                            if (firstRotatedIndex >= 0)
                                Log($"自动检测到旋转180°起始位置：第 {index1 + 1} 张（索引：{index1}）");
                            else
                                Log("未检测到旋转180°的图像段！");
                        }
                        else if (ConfigManager.Config.MiddleFlipMode == "Manual")
                        {
                            lblStatus.Text = "手动中天翻转校准中...";
                            await Task.Delay(10);
                            await Task.Run(() => firstRotatedIndex = (int)numericMiddleFlipManual1.Value - 1);
                            lastRotatedIndex = (int)numericMiddleFlipManual2.Value - 1;
                        }
                        // 翻转图像
                        if (firstRotatedIndex >= 0 && lastRotatedIndex >= 0 && lastRotatedIndex > firstRotatedIndex)
                        {
                            Log($"翻转图像：第 {firstRotatedIndex + 1} 张至第 {lastRotatedIndex + 1} 张");
                            await Task.Delay(10);
                            await Task.Run(() => middleflipcalibrated = ImageAlignment.FlipImages180InRange(process_state_images, firstRotatedIndex, lastRotatedIndex));
                            ImageIO.SaveImages(middleflipcalibrated, _imagePaths, Path.Combine(root, "middleFlipCalibrated"));
                            process_state = "中天翻转校准";
                        }
                        else
                        {
                            Log($"取消中天翻转校准！");
                            middleflipcalibrated = process_state_images.Select(mat => mat.Clone()).ToList();
                        }

                    }
                    else
                    {
                        middleflipcalibrated = process_state_images.Select(mat => mat.Clone()).ToList();
                    }
                    process_state_images = middleflipcalibrated.Select(mat => mat.Clone()).ToList();
                    if (middleflipcalibrated != null) foreach (var m in middleflipcalibrated) m.Dispose();

                    // 太阳北极朝上计算
                    if (ConfigManager.Config.SolarPoleNorthUp)
                    {
                        lblStatus.Text = "太阳北极朝上校准中...";
                        Log("太阳北极朝上校准中...");
                        await Task.Delay(10);
                        await Task.Run(() => solarPoleNorthUpRotationAngles = ImageAlignment.SolarPoleNorthUpCalc(this, process_state_images, _imagePaths, targetIndex: targetIndexInCode));
                        process_state = "太阳北极朝上";
                    }

                    // 未选择相位相关法或者特征点旋转校准时，清空相位相关法旋转角度列表，避免后续误用
                    if (!(ConfigManager.Config.RotationAlignmentMode == "PhaseCorrelate" && ConfigManager.Config.RotationAlignmentMode == "FeatureAlign"))
                        mainRotationAngles = null;

                    // 自动合并所有不为 null 的旋转角度（有几个加几个）
                    if (solarPoleNorthUpRotationAngles == null &&
                        fieldRotationAngles == null &&
                        mainRotationAngles == null)
                    {
                        rotationcalibrated = process_state_images.Select(mat => mat.Clone()).ToList();
                    }
                    else
                    {
                        List<double> totalRotationAngles = new List<double>();
                        int count = process_state_images.Count;
                        for (int i = 0; i < count; i++)
                        {
                            double total = 0;

                            if (solarPoleNorthUpRotationAngles != null)
                                total += solarPoleNorthUpRotationAngles[i];

                            if (fieldRotationAngles != null)
                                total += fieldRotationAngles[i];

                            if (mainRotationAngles != null)
                                total += mainRotationAngles[i];

                            totalRotationAngles.Add(total);
                        }

                        if (totalRotationAngles.All(angle => Math.Abs(angle) < 1e-9))
                        {
                            Log("所有图像旋转角度均为0°，跳过旋转校准！");
                            rotationcalibrated = process_state_images.Select(mat => mat.Clone()).ToList();
                        }
                        else
                        {
                            for (int i = 0; i < count; i++)
                            {
                                Log($"执行旋转校准 → 第{i + 1}张图旋总转角度: {totalRotationAngles[i]:F2}°");
                            }
                            if (ConfigManager.Config.RotationAlignmentMode == "PhaseCorrelate")
                            {
                                lblStatus.Text = "相位相关法旋转校准中...";
                                Log("相位相关法旋转校准中...");
                            }
                            else if (ConfigManager.Config.RotationAlignmentMode == "FeatureAlign")
                            {
                                lblStatus.Text = "特征点法旋转校准中...";
                                Log("特征点法旋转校准中...");
                            }

                            await Task.Delay(10);
                            await Task.Run(() => rotationcalibrated = ImageAlignment.CorrectRotationAndScaleForImages(process_state_images, angles: totalRotationAngles));
                            ImageIO.SaveImages(rotationcalibrated, _imagePaths, Path.Combine(root, "rotationCalibrated"));
                            process_state = "旋转校准";
                        }
                    }
                    process_state_images = rotationcalibrated.Select(mat => mat.Clone()).ToList();
                    if (rotationcalibrated != null) foreach (var m in rotationcalibrated) m.Dispose();


                }

                // 平移对齐
                int alignTimes = (int)numericAlignTimes.Value;
                if (ConfigManager.Config.EnableAlign)
                {
                    if (ConfigManager.Config.ImageAlignmentMode == "FeatureAlign")
                    {
                        lblStatus.Text = "特征点对齐中...";
                        Log("特征点对齐中...");
                        await Task.Delay(10);
                        await Task.Run(() => (aligned, mainRotationAngles) = ImageAlignment.AlignByPCAGroup(this, process_state_images,
                            targetIndex: targetIndexInCode,
                            isScale: false,
                            isRotate: false,
                            isTranslate: true,
                            isLog: true));
                    }
                    else if (ConfigManager.Config.ImageAlignmentMode == "MassCenter" || ConfigManager.Config.ImageAlignmentMode == "PhaseCorrelate")
                    {
                        if (ConfigManager.Config.ImageAlignmentMode == "MassCenter")
                            lblStatus.Text = "质心法对齐中...";
                        else if (ConfigManager.Config.ImageAlignmentMode == "PhaseCorrelate")
                            lblStatus.Text = "相位相关法对齐中...";

                        for (int i = 0; i < alignTimes; i++)
                        {
                            await Task.Delay(10);
                            Log($"第{i + 1}遍对齐");
                            if (i == 0)
                                await Task.Run(() => aligned = ImageAlignment.AlignImages(this, process_state_images, targetIndex: targetIndexInCode));
                            else
                                await Task.Run(() => aligned = ImageAlignment.AlignImages(this, aligned, targetIndex: targetIndexInCode));
                            
                        }
                    }
                    ImageIO.SaveImages(aligned, _imagePaths, Path.Combine(root, "aligned"));
                    process_state = "对齐";
                }
                else
                {
                    aligned = process_state_images.Select(mat => mat.Clone()).ToList();
                }
                process_state_images = aligned.Select(mat => mat.Clone()).ToList();
                if (aligned != null) foreach (var m in aligned) m.Dispose();

                lblStatus.Text = "✅ 处理完成！输出到对应文件夹";
                MessageBox.Show("处理完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("处理错误：" + ex.Message);
            }
            finally
            {
                SetAllControlsEnabled(true);
            }
        }

        //------------------------------
        // 参数设置
        //------------------------------

        // 【亮度增强 双向联动】
        private void trackBarLightEnhance_Scroll(object sender, EventArgs e)
        {
            SyncBrightnessControls();
        }

        private void numericLightEnhance_ValueChanged(object sender, EventArgs e)
        {
            SyncBrightnessControls();
        }

        // 新增：统一同步
        private void SyncBrightnessControls()
        {
            int val = trackBarLightEnhance.Value;
            numericLightEnhance.Value = val;
            UpdateBrightnessPreview();
        }

        //  实时更新亮度预览
        private void UpdateBrightnessPreview()
        {
            // 没有加载图像 或 没打开窗口 → 不执行
            if (_images == null || _images.Count == 0) return;
            if (imageform == null || imageform.IsDisposed) return;
            if (!isLightEnhanceShow) return;

            // 获取当前显示的是第几帧
            int currentIndex = (int)imageform.GetCurrentFrameIndex();

            if (currentIndex < 0 || currentIndex >= _images.Count) return;

            // 拿原图 → 调整亮度 → 显示
            Mat original = _images[currentIndex];
            Mat adjusted = ImageAlignment.AdjustBrightness(original, trackBarLightEnhance.Value);
            LightEnhanceValue = trackBarLightEnhance.Value;

            imageform.LoadImage(adjusted);

            adjusted.Dispose(); // 用完释放
        }

        // 亮度增强应用
        private void buttonLightEnhance_Click(object sender, EventArgs e)
        {
            // 切换开关（简化写法，和你原来效果一样）
            isLightEnhanceShow = !isLightEnhanceShow;

            // 触发当前帧刷新 → 自动切换亮度增强/原图
            if (imageform != null && !imageform.IsDisposed)
            {
                imageform.RefreshCurrentFrame();
            }
        }

        public int GetLightEnhanceValue()
        {
            return LightEnhanceValue;
        }

        public bool GetLightEnhanceShowState()
        {
            return isLightEnhanceShow;
        }

        public void SetTargetIndexLimit(int uplimit)
        {
            numericTargetIndex.Minimum = 1;
            numericTargetIndex.Maximum = uplimit;
            numericMiddleFlipManual1.Minimum = 1;
            numericMiddleFlipManual1.Maximum = uplimit;
            numericMiddleFlipManual2.Minimum = 1;
            numericMiddleFlipManual2.Maximum = uplimit;
        }

        private async void buttonGetGeoCoord_Click(object sender, EventArgs e)
        {
            (ConfigManager.Config.Latitude, ConfigManager.Config.Longitude) = await Astronomy.GetLocalLongLat();
            ControlBinder.RefreshCoordinateTextBoxes(textBoxLatitude, textBoxLongitude);
        }

    }
}