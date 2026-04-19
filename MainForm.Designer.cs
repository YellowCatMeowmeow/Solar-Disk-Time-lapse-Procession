using System;

namespace SolarImageProcessionCsharp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            updatelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            groupFileSelect = new System.Windows.Forms.GroupBox();
            txtFolderPath = new System.Windows.Forms.TextBox();
            btnLoadPath = new System.Windows.Forms.Button();
            btnSelectFolder = new System.Windows.Forms.Button();
            groupProcess = new System.Windows.Forms.GroupBox();
            checkFlip = new System.Windows.Forms.CheckBox();
            checkRotationAlign = new System.Windows.Forms.CheckBox();
            checkScaleAlign = new System.Windows.Forms.CheckBox();
            numericTargetIndex = new System.Windows.Forms.NumericUpDown();
            labelTargetIndex = new System.Windows.Forms.Label();
            chkLightNormaization = new System.Windows.Forms.CheckBox();
            lblStatus = new System.Windows.Forms.Label();
            btnProcess = new System.Windows.Forms.Button();
            chkAlign = new System.Windows.Forms.CheckBox();
            groupLightNormalization = new System.Windows.Forms.GroupBox();
            groupAlign = new System.Windows.Forms.GroupBox();
            groupBoxLightEnhance = new System.Windows.Forms.GroupBox();
            trackBarLightEnhance = new System.Windows.Forms.TrackBar();
            buttonLightEnhance = new System.Windows.Forms.Button();
            numericLightEnhance = new System.Windows.Forms.NumericUpDown();
            groupAlignTimes = new System.Windows.Forms.GroupBox();
            numericAlignTimes = new System.Windows.Forms.NumericUpDown();
            groupAlignObject = new System.Windows.Forms.GroupBox();
            radioFullDisk = new System.Windows.Forms.RadioButton();
            radioLocalArea = new System.Windows.Forms.RadioButton();
            groupAlignMode = new System.Windows.Forms.GroupBox();
            radioAlignFeature = new System.Windows.Forms.RadioButton();
            radioMassCenter = new System.Windows.Forms.RadioButton();
            radioPhaseCorrelate = new System.Windows.Forms.RadioButton();
            groupFileSave = new System.Windows.Forms.GroupBox();
            groupOutput = new System.Windows.Forms.GroupBox();
            radioSaveFit = new System.Windows.Forms.RadioButton();
            radioSaveTif = new System.Windows.Forms.RadioButton();
            radioSaveJpg = new System.Windows.Forms.RadioButton();
            radioSavePng = new System.Windows.Forms.RadioButton();
            groupLogText = new System.Windows.Forms.GroupBox();
            rtbLog = new System.Windows.Forms.RichTextBox();
            splitContainerMainForm = new System.Windows.Forms.SplitContainer();
            groupBoxScaleAlign = new System.Windows.Forms.GroupBox();
            radioScaleAlignFeature = new System.Windows.Forms.RadioButton();
            radioScaleAlignPhaseCorrelate = new System.Windows.Forms.RadioButton();
            checkScaleAlignMaxResolution = new System.Windows.Forms.CheckBox();
            groupBoxFeatureAlign = new System.Windows.Forms.GroupBox();
            checkBoxECC = new System.Windows.Forms.CheckBox();
            groupBoxRotationAlign = new System.Windows.Forms.GroupBox();
            radioRotationAlignFeature = new System.Windows.Forms.RadioButton();
            radioRotationAlignNone = new System.Windows.Forms.RadioButton();
            groupBoxRotationAlignTimes = new System.Windows.Forms.GroupBox();
            numericRotationAlignTimes = new System.Windows.Forms.NumericUpDown();
            groupBoxSolarPoleNorthUp = new System.Windows.Forms.GroupBox();
            radioSolarPoleNorthUpAllImages = new System.Windows.Forms.RadioButton();
            radioSolarPoleNorthUpOnlyTarget = new System.Windows.Forms.RadioButton();
            checkSolarPoleNorthUp = new System.Windows.Forms.CheckBox();
            groupBoxFieldRotation = new System.Windows.Forms.GroupBox();
            groupBoxGeoCoord = new System.Windows.Forms.GroupBox();
            buttonGetGeoCoord = new System.Windows.Forms.Button();
            textBoxLatitude = new System.Windows.Forms.TextBox();
            textBoxLongitude = new System.Windows.Forms.TextBox();
            labelLatitude = new System.Windows.Forms.Label();
            labelLongitude = new System.Windows.Forms.Label();
            groupBoxMiddleFlip = new System.Windows.Forms.GroupBox();
            radioMiddleFlipManual = new System.Windows.Forms.RadioButton();
            radioMiddleFlipAuto = new System.Windows.Forms.RadioButton();
            groupBoxMiddleFlipManual = new System.Windows.Forms.GroupBox();
            numericMiddleFlipManual2 = new System.Windows.Forms.NumericUpDown();
            numericMiddleFlipManual1 = new System.Windows.Forms.NumericUpDown();
            labelMiddleFlipManual = new System.Windows.Forms.Label();
            radioRotationAlignPhaseCorrelate = new System.Windows.Forms.RadioButton();
            radioMiddleFlip = new System.Windows.Forms.RadioButton();
            radioFieldRotation = new System.Windows.Forms.RadioButton();
            checkReadTif = new System.Windows.Forms.CheckBox();
            checkReadJpg = new System.Windows.Forms.CheckBox();
            checkReadPng = new System.Windows.Forms.CheckBox();
            checkReadFit = new System.Windows.Forms.CheckBox();
            menuStrip1.SuspendLayout();
            groupFileSelect.SuspendLayout();
            groupProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericTargetIndex).BeginInit();
            groupAlign.SuspendLayout();
            groupBoxLightEnhance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLightEnhance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericLightEnhance).BeginInit();
            groupAlignTimes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericAlignTimes).BeginInit();
            groupAlignObject.SuspendLayout();
            groupAlignMode.SuspendLayout();
            groupFileSave.SuspendLayout();
            groupOutput.SuspendLayout();
            groupLogText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMainForm).BeginInit();
            splitContainerMainForm.Panel1.SuspendLayout();
            splitContainerMainForm.Panel2.SuspendLayout();
            splitContainerMainForm.SuspendLayout();
            groupBoxScaleAlign.SuspendLayout();
            groupBoxFeatureAlign.SuspendLayout();
            groupBoxRotationAlign.SuspendLayout();
            groupBoxRotationAlignTimes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericRotationAlignTimes).BeginInit();
            groupBoxSolarPoleNorthUp.SuspendLayout();
            groupBoxFieldRotation.SuspendLayout();
            groupBoxGeoCoord.SuspendLayout();
            groupBoxMiddleFlip.SuspendLayout();
            groupBoxMiddleFlipManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericMiddleFlipManual2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMiddleFlipManual1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(896, 25);
            menuStrip1.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openFolderToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            fileToolStripMenuItem.Text = "文件";
            // 
            // openFolderToolStripMenuItem
            // 
            openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            openFolderToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            openFolderToolStripMenuItem.Text = "打开文件夹";
            openFolderToolStripMenuItem.Click += openFolderToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            exitToolStripMenuItem.Text = "退出";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { aboutToolStripMenuItem, updatelogToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            helpToolStripMenuItem.Text = "帮助";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            aboutToolStripMenuItem.Text = "关于";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // updatelogToolStripMenuItem
            // 
            updatelogToolStripMenuItem.Name = "updatelogToolStripMenuItem";
            updatelogToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            updatelogToolStripMenuItem.Text = "更新日志";
            updatelogToolStripMenuItem.Click += updatelogToolStripMenuItem_Click;
            // 
            // groupFileSelect
            // 
            groupFileSelect.Controls.Add(checkReadFit);
            groupFileSelect.Controls.Add(checkReadPng);
            groupFileSelect.Controls.Add(checkReadJpg);
            groupFileSelect.Controls.Add(checkReadTif);
            groupFileSelect.Controls.Add(txtFolderPath);
            groupFileSelect.Controls.Add(btnLoadPath);
            groupFileSelect.Controls.Add(btnSelectFolder);
            groupFileSelect.Location = new System.Drawing.Point(15, 13);
            groupFileSelect.Name = "groupFileSelect";
            groupFileSelect.Size = new System.Drawing.Size(421, 113);
            groupFileSelect.TabIndex = 0;
            groupFileSelect.TabStop = false;
            groupFileSelect.Text = "文件选择";
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new System.Drawing.Point(13, 48);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new System.Drawing.Size(396, 23);
            txtFolderPath.TabIndex = 0;
            txtFolderPath.KeyDown += txtFolderPath_KeyDown;
            // 
            // btnLoadPath
            // 
            btnLoadPath.Location = new System.Drawing.Point(106, 77);
            btnLoadPath.Name = "btnLoadPath";
            btnLoadPath.Size = new System.Drawing.Size(80, 25);
            btnLoadPath.TabIndex = 2;
            btnLoadPath.Text = "加载";
            btnLoadPath.Click += btnLoadPath_Click;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new System.Drawing.Point(13, 77);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new System.Drawing.Size(80, 25);
            btnSelectFolder.TabIndex = 1;
            btnSelectFolder.Text = "打开文件夹";
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // groupProcess
            // 
            groupProcess.Controls.Add(checkFlip);
            groupProcess.Controls.Add(checkRotationAlign);
            groupProcess.Controls.Add(checkScaleAlign);
            groupProcess.Controls.Add(numericTargetIndex);
            groupProcess.Controls.Add(labelTargetIndex);
            groupProcess.Controls.Add(chkLightNormaization);
            groupProcess.Controls.Add(lblStatus);
            groupProcess.Controls.Add(btnProcess);
            groupProcess.Controls.Add(chkAlign);
            groupProcess.Location = new System.Drawing.Point(15, 136);
            groupProcess.Name = "groupProcess";
            groupProcess.Size = new System.Drawing.Size(421, 90);
            groupProcess.TabIndex = 1;
            groupProcess.TabStop = false;
            groupProcess.Text = "处理流程";
            // 
            // checkFlip
            // 
            checkFlip.AutoSize = true;
            checkFlip.Location = new System.Drawing.Point(78, 22);
            checkFlip.Name = "checkFlip";
            checkFlip.Size = new System.Drawing.Size(75, 21);
            checkFlip.TabIndex = 13;
            checkFlip.Text = "翻转图像";
            checkFlip.UseVisualStyleBackColor = true;
            // 
            // checkRotationAlign
            // 
            checkRotationAlign.AutoSize = true;
            checkRotationAlign.Location = new System.Drawing.Point(228, 22);
            checkRotationAlign.Name = "checkRotationAlign";
            checkRotationAlign.Size = new System.Drawing.Size(111, 21);
            checkRotationAlign.TabIndex = 12;
            checkRotationAlign.Text = "旋转对齐或指北";
            checkRotationAlign.UseVisualStyleBackColor = true;
            // 
            // checkScaleAlign
            // 
            checkScaleAlign.AutoSize = true;
            checkScaleAlign.Location = new System.Drawing.Point(154, 22);
            checkScaleAlign.Name = "checkScaleAlign";
            checkScaleAlign.Size = new System.Drawing.Size(75, 21);
            checkScaleAlign.TabIndex = 11;
            checkScaleAlign.Text = "放缩对齐";
            checkScaleAlign.UseVisualStyleBackColor = true;
            // 
            // numericTargetIndex
            // 
            numericTargetIndex.Location = new System.Drawing.Point(359, 53);
            numericTargetIndex.Name = "numericTargetIndex";
            numericTargetIndex.Size = new System.Drawing.Size(56, 23);
            numericTargetIndex.TabIndex = 5;
            // 
            // labelTargetIndex
            // 
            labelTargetIndex.AutoSize = true;
            labelTargetIndex.Location = new System.Drawing.Point(273, 55);
            labelTargetIndex.Name = "labelTargetIndex";
            labelTargetIndex.Size = new System.Drawing.Size(92, 17);
            labelTargetIndex.TabIndex = 10;
            labelTargetIndex.Text = "参考图像序号：";
            // 
            // chkLightNormaization
            // 
            chkLightNormaization.AutoSize = true;
            chkLightNormaization.Checked = true;
            chkLightNormaization.CheckState = System.Windows.Forms.CheckState.Checked;
            chkLightNormaization.Location = new System.Drawing.Point(6, 22);
            chkLightNormaization.Name = "chkLightNormaization";
            chkLightNormaization.Size = new System.Drawing.Size(75, 21);
            chkLightNormaization.TabIndex = 0;
            chkLightNormaization.Text = "亮度统一";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new System.Drawing.Point(132, 56);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(128, 17);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "请选择太阳图像文件夹";
            // 
            // btnProcess
            // 
            btnProcess.Location = new System.Drawing.Point(6, 44);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new System.Drawing.Size(120, 40);
            btnProcess.TabIndex = 4;
            btnProcess.Text = "开始处理";
            btnProcess.Click += btnProcess_Click;
            // 
            // chkAlign
            // 
            chkAlign.AutoSize = true;
            chkAlign.Checked = true;
            chkAlign.CheckState = System.Windows.Forms.CheckState.Checked;
            chkAlign.Location = new System.Drawing.Point(345, 22);
            chkAlign.Name = "chkAlign";
            chkAlign.Size = new System.Drawing.Size(75, 21);
            chkAlign.TabIndex = 3;
            chkAlign.Text = "平移对齐";
            // 
            // groupLightNormalization
            // 
            groupLightNormalization.Location = new System.Drawing.Point(14, 233);
            groupLightNormalization.Name = "groupLightNormalization";
            groupLightNormalization.Size = new System.Drawing.Size(421, 35);
            groupLightNormalization.TabIndex = 2;
            groupLightNormalization.TabStop = false;
            groupLightNormalization.Text = "亮度统一";
            // 
            // groupAlign
            // 
            groupAlign.Controls.Add(groupBoxLightEnhance);
            groupAlign.Controls.Add(groupAlignTimes);
            groupAlign.Controls.Add(groupAlignObject);
            groupAlign.Controls.Add(groupAlignMode);
            groupAlign.Location = new System.Drawing.Point(15, 365);
            groupAlign.Name = "groupAlign";
            groupAlign.Size = new System.Drawing.Size(421, 212);
            groupAlign.TabIndex = 0;
            groupAlign.TabStop = false;
            groupAlign.Text = "平移对齐";
            // 
            // groupBoxLightEnhance
            // 
            groupBoxLightEnhance.Controls.Add(trackBarLightEnhance);
            groupBoxLightEnhance.Controls.Add(buttonLightEnhance);
            groupBoxLightEnhance.Controls.Add(numericLightEnhance);
            groupBoxLightEnhance.Location = new System.Drawing.Point(13, 134);
            groupBoxLightEnhance.Name = "groupBoxLightEnhance";
            groupBoxLightEnhance.Size = new System.Drawing.Size(396, 70);
            groupBoxLightEnhance.TabIndex = 3;
            groupBoxLightEnhance.TabStop = false;
            groupBoxLightEnhance.Text = "亮度增强（全日面且有明显自转时用）";
            // 
            // trackBarLightEnhance
            // 
            trackBarLightEnhance.Location = new System.Drawing.Point(65, 17);
            trackBarLightEnhance.Name = "trackBarLightEnhance";
            trackBarLightEnhance.Size = new System.Drawing.Size(203, 45);
            trackBarLightEnhance.TabIndex = 1;
            trackBarLightEnhance.Scroll += trackBarLightEnhance_Scroll;
            // 
            // buttonLightEnhance
            // 
            buttonLightEnhance.Location = new System.Drawing.Point(274, 17);
            buttonLightEnhance.Name = "buttonLightEnhance";
            buttonLightEnhance.Size = new System.Drawing.Size(61, 23);
            buttonLightEnhance.TabIndex = 2;
            buttonLightEnhance.Text = "应用";
            buttonLightEnhance.UseVisualStyleBackColor = true;
            buttonLightEnhance.Click += buttonLightEnhance_Click;
            // 
            // numericLightEnhance
            // 
            numericLightEnhance.Location = new System.Drawing.Point(6, 18);
            numericLightEnhance.Name = "numericLightEnhance";
            numericLightEnhance.Size = new System.Drawing.Size(53, 23);
            numericLightEnhance.TabIndex = 0;
            numericLightEnhance.ValueChanged += numericLightEnhance_ValueChanged;
            // 
            // groupAlignTimes
            // 
            groupAlignTimes.Controls.Add(numericAlignTimes);
            groupAlignTimes.Location = new System.Drawing.Point(270, 22);
            groupAlignTimes.Name = "groupAlignTimes";
            groupAlignTimes.Size = new System.Drawing.Size(139, 50);
            groupAlignTimes.TabIndex = 2;
            groupAlignTimes.TabStop = false;
            groupAlignTimes.Text = "对齐迭代次数";
            // 
            // numericAlignTimes
            // 
            numericAlignTimes.Location = new System.Drawing.Point(6, 18);
            numericAlignTimes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericAlignTimes.Name = "numericAlignTimes";
            numericAlignTimes.Size = new System.Drawing.Size(43, 23);
            numericAlignTimes.TabIndex = 0;
            numericAlignTimes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // groupAlignObject
            // 
            groupAlignObject.Controls.Add(radioFullDisk);
            groupAlignObject.Controls.Add(radioLocalArea);
            groupAlignObject.Location = new System.Drawing.Point(13, 22);
            groupAlignObject.Name = "groupAlignObject";
            groupAlignObject.Size = new System.Drawing.Size(242, 50);
            groupAlignObject.TabIndex = 0;
            groupAlignObject.TabStop = false;
            groupAlignObject.Text = "目标类型";
            // 
            // radioFullDisk
            // 
            radioFullDisk.AutoSize = true;
            radioFullDisk.Checked = true;
            radioFullDisk.Location = new System.Drawing.Point(6, 20);
            radioFullDisk.Name = "radioFullDisk";
            radioFullDisk.Size = new System.Drawing.Size(108, 21);
            radioFullDisk.TabIndex = 0;
            radioFullDisk.TabStop = true;
            radioFullDisk.Text = "全日/月/行星面";
            // 
            // radioLocalArea
            // 
            radioLocalArea.AutoSize = true;
            radioLocalArea.Location = new System.Drawing.Point(120, 20);
            radioLocalArea.Name = "radioLocalArea";
            radioLocalArea.Size = new System.Drawing.Size(74, 21);
            radioLocalArea.TabIndex = 1;
            radioLocalArea.TabStop = true;
            radioLocalArea.Text = "局部区域";
            // 
            // groupAlignMode
            // 
            groupAlignMode.Controls.Add(radioAlignFeature);
            groupAlignMode.Controls.Add(radioMassCenter);
            groupAlignMode.Controls.Add(radioPhaseCorrelate);
            groupAlignMode.Location = new System.Drawing.Point(13, 78);
            groupAlignMode.Name = "groupAlignMode";
            groupAlignMode.Size = new System.Drawing.Size(396, 50);
            groupAlignMode.TabIndex = 1;
            groupAlignMode.TabStop = false;
            groupAlignMode.Text = "对齐方式";
            // 
            // radioAlignFeature
            // 
            radioAlignFeature.AutoSize = true;
            radioAlignFeature.Enabled = false;
            radioAlignFeature.Location = new System.Drawing.Point(249, 20);
            radioAlignFeature.Name = "radioAlignFeature";
            radioAlignFeature.Size = new System.Drawing.Size(134, 21);
            radioAlignFeature.TabIndex = 2;
            radioAlignFeature.Text = "特征点对齐（停用）";
            radioAlignFeature.UseVisualStyleBackColor = true;
            // 
            // radioMassCenter
            // 
            radioMassCenter.AutoSize = true;
            radioMassCenter.Checked = true;
            radioMassCenter.Location = new System.Drawing.Point(6, 20);
            radioMassCenter.Name = "radioMassCenter";
            radioMassCenter.Size = new System.Drawing.Size(74, 21);
            radioMassCenter.TabIndex = 0;
            radioMassCenter.TabStop = true;
            radioMassCenter.Text = "质心对齐";
            // 
            // radioPhaseCorrelate
            // 
            radioPhaseCorrelate.AutoSize = true;
            radioPhaseCorrelate.Location = new System.Drawing.Point(120, 20);
            radioPhaseCorrelate.Name = "radioPhaseCorrelate";
            radioPhaseCorrelate.Size = new System.Drawing.Size(110, 21);
            radioPhaseCorrelate.TabIndex = 1;
            radioPhaseCorrelate.Text = "相位相关法对齐";
            // 
            // groupFileSave
            // 
            groupFileSave.Controls.Add(groupOutput);
            groupFileSave.Location = new System.Drawing.Point(12, 414);
            groupFileSave.Name = "groupFileSave";
            groupFileSave.Size = new System.Drawing.Size(420, 89);
            groupFileSave.TabIndex = 1;
            groupFileSave.TabStop = false;
            groupFileSave.Text = "保存格式";
            // 
            // groupOutput
            // 
            groupOutput.Controls.Add(radioSaveFit);
            groupOutput.Controls.Add(radioSaveTif);
            groupOutput.Controls.Add(radioSaveJpg);
            groupOutput.Controls.Add(radioSavePng);
            groupOutput.Location = new System.Drawing.Point(6, 22);
            groupOutput.Name = "groupOutput";
            groupOutput.Size = new System.Drawing.Size(324, 50);
            groupOutput.TabIndex = 0;
            groupOutput.TabStop = false;
            groupOutput.Text = "输出格式";
            // 
            // radioSaveFit
            // 
            radioSaveFit.AutoSize = true;
            radioSaveFit.Location = new System.Drawing.Point(232, 22);
            radioSaveFit.Name = "radioSaveFit";
            radioSaveFit.Size = new System.Drawing.Size(43, 21);
            radioSaveFit.TabIndex = 3;
            radioSaveFit.TabStop = true;
            radioSaveFit.Text = "FIT";
            radioSaveFit.UseVisualStyleBackColor = true;
            // 
            // radioSaveTif
            // 
            radioSaveTif.AutoSize = true;
            radioSaveTif.Checked = true;
            radioSaveTif.Location = new System.Drawing.Point(12, 23);
            radioSaveTif.Name = "radioSaveTif";
            radioSaveTif.Size = new System.Drawing.Size(43, 21);
            radioSaveTif.TabIndex = 0;
            radioSaveTif.TabStop = true;
            radioSaveTif.Text = "TIF";
            // 
            // radioSaveJpg
            // 
            radioSaveJpg.AutoSize = true;
            radioSaveJpg.Location = new System.Drawing.Point(82, 22);
            radioSaveJpg.Name = "radioSaveJpg";
            radioSaveJpg.Size = new System.Drawing.Size(47, 21);
            radioSaveJpg.TabIndex = 1;
            radioSaveJpg.TabStop = true;
            radioSaveJpg.Text = "JPG";
            // 
            // radioSavePng
            // 
            radioSavePng.AutoSize = true;
            radioSavePng.Location = new System.Drawing.Point(157, 22);
            radioSavePng.Name = "radioSavePng";
            radioSavePng.Size = new System.Drawing.Size(52, 21);
            radioSavePng.TabIndex = 2;
            radioSavePng.TabStop = true;
            radioSavePng.Text = "PNG";
            // 
            // groupLogText
            // 
            groupLogText.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupLogText.Controls.Add(rtbLog);
            groupLogText.Location = new System.Drawing.Point(1, 626);
            groupLogText.Name = "groupLogText";
            groupLogText.Size = new System.Drawing.Size(889, 111);
            groupLogText.TabIndex = 2;
            groupLogText.TabStop = false;
            groupLogText.Text = "日志输出";
            // 
            // rtbLog
            // 
            rtbLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rtbLog.BackColor = System.Drawing.Color.White;
            rtbLog.Location = new System.Drawing.Point(12, 21);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            rtbLog.Size = new System.Drawing.Size(871, 83);
            rtbLog.TabIndex = 0;
            rtbLog.Text = "";
            // 
            // splitContainerMainForm
            // 
            splitContainerMainForm.IsSplitterFixed = true;
            splitContainerMainForm.Location = new System.Drawing.Point(0, 28);
            splitContainerMainForm.Name = "splitContainerMainForm";
            // 
            // splitContainerMainForm.Panel1
            // 
            splitContainerMainForm.Panel1.Controls.Add(groupBoxScaleAlign);
            splitContainerMainForm.Panel1.Controls.Add(groupFileSelect);
            splitContainerMainForm.Panel1.Controls.Add(groupAlign);
            splitContainerMainForm.Panel1.Controls.Add(groupProcess);
            splitContainerMainForm.Panel1.Controls.Add(groupLightNormalization);
            // 
            // splitContainerMainForm.Panel2
            // 
            splitContainerMainForm.Panel2.Controls.Add(groupBoxFeatureAlign);
            splitContainerMainForm.Panel2.Controls.Add(groupBoxRotationAlign);
            splitContainerMainForm.Panel2.Controls.Add(groupFileSave);
            splitContainerMainForm.Size = new System.Drawing.Size(896, 592);
            splitContainerMainForm.SplitterDistance = 448;
            splitContainerMainForm.TabIndex = 1;
            splitContainerMainForm.TabStop = false;
            // 
            // groupBoxScaleAlign
            // 
            groupBoxScaleAlign.Controls.Add(radioScaleAlignFeature);
            groupBoxScaleAlign.Controls.Add(radioScaleAlignPhaseCorrelate);
            groupBoxScaleAlign.Controls.Add(checkScaleAlignMaxResolution);
            groupBoxScaleAlign.Location = new System.Drawing.Point(15, 274);
            groupBoxScaleAlign.Name = "groupBoxScaleAlign";
            groupBoxScaleAlign.Size = new System.Drawing.Size(420, 85);
            groupBoxScaleAlign.TabIndex = 3;
            groupBoxScaleAlign.TabStop = false;
            groupBoxScaleAlign.Text = "放缩对齐";
            // 
            // radioScaleAlignFeature
            // 
            radioScaleAlignFeature.AutoSize = true;
            radioScaleAlignFeature.Location = new System.Drawing.Point(171, 49);
            radioScaleAlignFeature.Name = "radioScaleAlignFeature";
            radioScaleAlignFeature.Size = new System.Drawing.Size(110, 21);
            radioScaleAlignFeature.TabIndex = 2;
            radioScaleAlignFeature.TabStop = true;
            radioScaleAlignFeature.Text = "特征点放缩对齐";
            radioScaleAlignFeature.UseVisualStyleBackColor = true;
            // 
            // radioScaleAlignPhaseCorrelate
            // 
            radioScaleAlignPhaseCorrelate.AutoSize = true;
            radioScaleAlignPhaseCorrelate.Location = new System.Drawing.Point(13, 49);
            radioScaleAlignPhaseCorrelate.Name = "radioScaleAlignPhaseCorrelate";
            radioScaleAlignPhaseCorrelate.Size = new System.Drawing.Size(134, 21);
            radioScaleAlignPhaseCorrelate.TabIndex = 1;
            radioScaleAlignPhaseCorrelate.TabStop = true;
            radioScaleAlignPhaseCorrelate.Text = "相位相关法放缩对齐";
            radioScaleAlignPhaseCorrelate.UseVisualStyleBackColor = true;
            // 
            // checkScaleAlignMaxResolution
            // 
            checkScaleAlignMaxResolution.AutoSize = true;
            checkScaleAlignMaxResolution.Location = new System.Drawing.Point(13, 22);
            checkScaleAlignMaxResolution.Name = "checkScaleAlignMaxResolution";
            checkScaleAlignMaxResolution.Size = new System.Drawing.Size(231, 21);
            checkScaleAlignMaxResolution.TabIndex = 0;
            checkScaleAlignMaxResolution.Text = "以最高分辨率图像为放缩对齐参考图像";
            checkScaleAlignMaxResolution.UseVisualStyleBackColor = true;
            // 
            // groupBoxFeatureAlign
            // 
            groupBoxFeatureAlign.Controls.Add(checkBoxECC);
            groupBoxFeatureAlign.Location = new System.Drawing.Point(12, 509);
            groupBoxFeatureAlign.Name = "groupBoxFeatureAlign";
            groupBoxFeatureAlign.Size = new System.Drawing.Size(420, 68);
            groupBoxFeatureAlign.TabIndex = 3;
            groupBoxFeatureAlign.TabStop = false;
            groupBoxFeatureAlign.Text = "特征点对齐选项";
            // 
            // checkBoxECC
            // 
            checkBoxECC.AutoSize = true;
            checkBoxECC.Location = new System.Drawing.Point(11, 22);
            checkBoxECC.Name = "checkBoxECC";
            checkBoxECC.Size = new System.Drawing.Size(206, 21);
            checkBoxECC.TabIndex = 0;
            checkBoxECC.Text = "是否使用ECC精对齐（比较耗时）";
            checkBoxECC.UseVisualStyleBackColor = true;
            // 
            // groupBoxRotationAlign
            // 
            groupBoxRotationAlign.Controls.Add(radioRotationAlignFeature);
            groupBoxRotationAlign.Controls.Add(radioRotationAlignNone);
            groupBoxRotationAlign.Controls.Add(groupBoxRotationAlignTimes);
            groupBoxRotationAlign.Controls.Add(groupBoxSolarPoleNorthUp);
            groupBoxRotationAlign.Controls.Add(checkSolarPoleNorthUp);
            groupBoxRotationAlign.Controls.Add(groupBoxFieldRotation);
            groupBoxRotationAlign.Controls.Add(groupBoxMiddleFlip);
            groupBoxRotationAlign.Controls.Add(radioRotationAlignPhaseCorrelate);
            groupBoxRotationAlign.Controls.Add(radioMiddleFlip);
            groupBoxRotationAlign.Controls.Add(radioFieldRotation);
            groupBoxRotationAlign.Location = new System.Drawing.Point(12, 13);
            groupBoxRotationAlign.Name = "groupBoxRotationAlign";
            groupBoxRotationAlign.Size = new System.Drawing.Size(421, 397);
            groupBoxRotationAlign.TabIndex = 2;
            groupBoxRotationAlign.TabStop = false;
            groupBoxRotationAlign.Text = "旋转对齐";
            // 
            // radioRotationAlignFeature
            // 
            radioRotationAlignFeature.AutoSize = true;
            radioRotationAlignFeature.Location = new System.Drawing.Point(209, 42);
            radioRotationAlignFeature.Name = "radioRotationAlignFeature";
            radioRotationAlignFeature.Size = new System.Drawing.Size(110, 21);
            radioRotationAlignFeature.TabIndex = 9;
            radioRotationAlignFeature.TabStop = true;
            radioRotationAlignFeature.Text = "特征点旋转对齐";
            radioRotationAlignFeature.UseVisualStyleBackColor = true;
            // 
            // radioRotationAlignNone
            // 
            radioRotationAlignNone.AutoSize = true;
            radioRotationAlignNone.Checked = true;
            radioRotationAlignNone.Location = new System.Drawing.Point(13, 20);
            radioRotationAlignNone.Name = "radioRotationAlignNone";
            radioRotationAlignNone.Size = new System.Drawing.Size(86, 21);
            radioRotationAlignNone.TabIndex = 8;
            radioRotationAlignNone.TabStop = true;
            radioRotationAlignNone.Text = "不旋转对齐";
            radioRotationAlignNone.UseVisualStyleBackColor = true;
            // 
            // groupBoxRotationAlignTimes
            // 
            groupBoxRotationAlignTimes.Controls.Add(numericRotationAlignTimes);
            groupBoxRotationAlignTimes.Location = new System.Drawing.Point(232, 69);
            groupBoxRotationAlignTimes.Name = "groupBoxRotationAlignTimes";
            groupBoxRotationAlignTimes.Size = new System.Drawing.Size(126, 52);
            groupBoxRotationAlignTimes.TabIndex = 7;
            groupBoxRotationAlignTimes.TabStop = false;
            groupBoxRotationAlignTimes.Text = "旋转对齐迭代次数";
            // 
            // numericRotationAlignTimes
            // 
            numericRotationAlignTimes.Location = new System.Drawing.Point(6, 21);
            numericRotationAlignTimes.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericRotationAlignTimes.Name = "numericRotationAlignTimes";
            numericRotationAlignTimes.Size = new System.Drawing.Size(47, 23);
            numericRotationAlignTimes.TabIndex = 0;
            numericRotationAlignTimes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // groupBoxSolarPoleNorthUp
            // 
            groupBoxSolarPoleNorthUp.Controls.Add(radioSolarPoleNorthUpAllImages);
            groupBoxSolarPoleNorthUp.Controls.Add(radioSolarPoleNorthUpOnlyTarget);
            groupBoxSolarPoleNorthUp.Location = new System.Drawing.Point(13, 305);
            groupBoxSolarPoleNorthUp.Name = "groupBoxSolarPoleNorthUp";
            groupBoxSolarPoleNorthUp.Size = new System.Drawing.Size(396, 79);
            groupBoxSolarPoleNorthUp.TabIndex = 6;
            groupBoxSolarPoleNorthUp.TabStop = false;
            groupBoxSolarPoleNorthUp.Text = "校准太阳自转轴方向";
            // 
            // radioSolarPoleNorthUpAllImages
            // 
            radioSolarPoleNorthUpAllImages.AutoSize = true;
            radioSolarPoleNorthUpAllImages.Location = new System.Drawing.Point(150, 30);
            radioSolarPoleNorthUpAllImages.Name = "radioSolarPoleNorthUpAllImages";
            radioSolarPoleNorthUpAllImages.Size = new System.Drawing.Size(218, 21);
            radioSolarPoleNorthUpAllImages.TabIndex = 1;
            radioSolarPoleNorthUpAllImages.TabStop = true;
            radioSolarPoleNorthUpAllImages.Text = "所有图像都计算旋转（非常耗时！）";
            radioSolarPoleNorthUpAllImages.UseVisualStyleBackColor = true;
            // 
            // radioSolarPoleNorthUpOnlyTarget
            // 
            radioSolarPoleNorthUpOnlyTarget.AutoSize = true;
            radioSolarPoleNorthUpOnlyTarget.Location = new System.Drawing.Point(15, 30);
            radioSolarPoleNorthUpOnlyTarget.Name = "radioSolarPoleNorthUpOnlyTarget";
            radioSolarPoleNorthUpOnlyTarget.Size = new System.Drawing.Size(122, 21);
            radioSolarPoleNorthUpOnlyTarget.TabIndex = 0;
            radioSolarPoleNorthUpOnlyTarget.TabStop = true;
            radioSolarPoleNorthUpOnlyTarget.Text = "仅计算参考帧旋转";
            radioSolarPoleNorthUpOnlyTarget.UseVisualStyleBackColor = true;
            // 
            // checkSolarPoleNorthUp
            // 
            checkSolarPoleNorthUp.AutoSize = true;
            checkSolarPoleNorthUp.Location = new System.Drawing.Point(13, 84);
            checkSolarPoleNorthUp.Name = "checkSolarPoleNorthUp";
            checkSolarPoleNorthUp.Size = new System.Drawing.Size(195, 21);
            checkSolarPoleNorthUp.TabIndex = 5;
            checkSolarPoleNorthUp.Text = "校准太阳自转轴方向（北朝上）";
            checkSolarPoleNorthUp.UseVisualStyleBackColor = true;
            // 
            // groupBoxFieldRotation
            // 
            groupBoxFieldRotation.Controls.Add(groupBoxGeoCoord);
            groupBoxFieldRotation.Location = new System.Drawing.Point(11, 126);
            groupBoxFieldRotation.Name = "groupBoxFieldRotation";
            groupBoxFieldRotation.Size = new System.Drawing.Size(398, 88);
            groupBoxFieldRotation.TabIndex = 3;
            groupBoxFieldRotation.TabStop = false;
            groupBoxFieldRotation.Text = "经纬仪场旋校准";
            // 
            // groupBoxGeoCoord
            // 
            groupBoxGeoCoord.Controls.Add(buttonGetGeoCoord);
            groupBoxGeoCoord.Controls.Add(textBoxLatitude);
            groupBoxGeoCoord.Controls.Add(textBoxLongitude);
            groupBoxGeoCoord.Controls.Add(labelLatitude);
            groupBoxGeoCoord.Controls.Add(labelLongitude);
            groupBoxGeoCoord.Location = new System.Drawing.Point(13, 22);
            groupBoxGeoCoord.Name = "groupBoxGeoCoord";
            groupBoxGeoCoord.Size = new System.Drawing.Size(373, 56);
            groupBoxGeoCoord.TabIndex = 1;
            groupBoxGeoCoord.TabStop = false;
            groupBoxGeoCoord.Text = "拍摄地经纬度坐标";
            // 
            // buttonGetGeoCoord
            // 
            buttonGetGeoCoord.Location = new System.Drawing.Point(262, 22);
            buttonGetGeoCoord.Name = "buttonGetGeoCoord";
            buttonGetGeoCoord.Size = new System.Drawing.Size(86, 23);
            buttonGetGeoCoord.TabIndex = 2;
            buttonGetGeoCoord.Text = "获取经纬度";
            buttonGetGeoCoord.UseVisualStyleBackColor = true;
            buttonGetGeoCoord.Click += buttonGetGeoCoord_Click;
            // 
            // textBoxLatitude
            // 
            textBoxLatitude.Location = new System.Drawing.Point(167, 22);
            textBoxLatitude.Name = "textBoxLatitude";
            textBoxLatitude.Size = new System.Drawing.Size(60, 23);
            textBoxLatitude.TabIndex = 1;
            // 
            // textBoxLongitude
            // 
            textBoxLongitude.Location = new System.Drawing.Point(45, 22);
            textBoxLongitude.Name = "textBoxLongitude";
            textBoxLongitude.Size = new System.Drawing.Size(60, 23);
            textBoxLongitude.TabIndex = 0;
            // 
            // labelLatitude
            // 
            labelLatitude.AutoSize = true;
            labelLatitude.Location = new System.Drawing.Point(128, 25);
            labelLatitude.Name = "labelLatitude";
            labelLatitude.Size = new System.Drawing.Size(113, 17);
            labelLatitude.TabIndex = 3;
            labelLatitude.Text = "纬度：                °";
            // 
            // labelLongitude
            // 
            labelLongitude.AutoSize = true;
            labelLongitude.Location = new System.Drawing.Point(6, 25);
            labelLongitude.Name = "labelLongitude";
            labelLongitude.Size = new System.Drawing.Size(113, 17);
            labelLongitude.TabIndex = 2;
            labelLongitude.Text = "经度：                °";
            // 
            // groupBoxMiddleFlip
            // 
            groupBoxMiddleFlip.Controls.Add(radioMiddleFlipManual);
            groupBoxMiddleFlip.Controls.Add(radioMiddleFlipAuto);
            groupBoxMiddleFlip.Controls.Add(groupBoxMiddleFlipManual);
            groupBoxMiddleFlip.Location = new System.Drawing.Point(13, 220);
            groupBoxMiddleFlip.Name = "groupBoxMiddleFlip";
            groupBoxMiddleFlip.Size = new System.Drawing.Size(396, 83);
            groupBoxMiddleFlip.TabIndex = 4;
            groupBoxMiddleFlip.TabStop = false;
            groupBoxMiddleFlip.Text = "中天翻转校准";
            // 
            // radioMiddleFlipManual
            // 
            radioMiddleFlipManual.AutoSize = true;
            radioMiddleFlipManual.Location = new System.Drawing.Point(6, 46);
            radioMiddleFlipManual.Name = "radioMiddleFlipManual";
            radioMiddleFlipManual.Size = new System.Drawing.Size(50, 21);
            radioMiddleFlipManual.TabIndex = 1;
            radioMiddleFlipManual.TabStop = true;
            radioMiddleFlipManual.Text = "手动";
            radioMiddleFlipManual.UseVisualStyleBackColor = true;
            // 
            // radioMiddleFlipAuto
            // 
            radioMiddleFlipAuto.AutoSize = true;
            radioMiddleFlipAuto.Checked = true;
            radioMiddleFlipAuto.Location = new System.Drawing.Point(6, 22);
            radioMiddleFlipAuto.Name = "radioMiddleFlipAuto";
            radioMiddleFlipAuto.Size = new System.Drawing.Size(50, 21);
            radioMiddleFlipAuto.TabIndex = 0;
            radioMiddleFlipAuto.TabStop = true;
            radioMiddleFlipAuto.Text = "自动";
            radioMiddleFlipAuto.UseVisualStyleBackColor = true;
            // 
            // groupBoxMiddleFlipManual
            // 
            groupBoxMiddleFlipManual.Controls.Add(numericMiddleFlipManual2);
            groupBoxMiddleFlipManual.Controls.Add(numericMiddleFlipManual1);
            groupBoxMiddleFlipManual.Controls.Add(labelMiddleFlipManual);
            groupBoxMiddleFlipManual.Location = new System.Drawing.Point(87, 17);
            groupBoxMiddleFlipManual.Name = "groupBoxMiddleFlipManual";
            groupBoxMiddleFlipManual.Size = new System.Drawing.Size(297, 60);
            groupBoxMiddleFlipManual.TabIndex = 2;
            groupBoxMiddleFlipManual.TabStop = false;
            groupBoxMiddleFlipManual.Text = "手动翻转图像选择";
            // 
            // numericMiddleFlipManual2
            // 
            numericMiddleFlipManual2.Location = new System.Drawing.Point(162, 24);
            numericMiddleFlipManual2.Name = "numericMiddleFlipManual2";
            numericMiddleFlipManual2.Size = new System.Drawing.Size(60, 23);
            numericMiddleFlipManual2.TabIndex = 1;
            // 
            // numericMiddleFlipManual1
            // 
            numericMiddleFlipManual1.Location = new System.Drawing.Point(39, 24);
            numericMiddleFlipManual1.Name = "numericMiddleFlipManual1";
            numericMiddleFlipManual1.Size = new System.Drawing.Size(60, 23);
            numericMiddleFlipManual1.TabIndex = 0;
            // 
            // labelMiddleFlipManual
            // 
            labelMiddleFlipManual.AutoSize = true;
            labelMiddleFlipManual.Location = new System.Drawing.Point(19, 26);
            labelMiddleFlipManual.Name = "labelMiddleFlipManual";
            labelMiddleFlipManual.Size = new System.Drawing.Size(226, 17);
            labelMiddleFlipManual.TabIndex = 2;
            labelMiddleFlipManual.Text = "第                  张——第                  张";
            // 
            // radioRotationAlignPhaseCorrelate
            // 
            radioRotationAlignPhaseCorrelate.AutoSize = true;
            radioRotationAlignPhaseCorrelate.Location = new System.Drawing.Point(13, 42);
            radioRotationAlignPhaseCorrelate.Name = "radioRotationAlignPhaseCorrelate";
            radioRotationAlignPhaseCorrelate.Size = new System.Drawing.Size(134, 21);
            radioRotationAlignPhaseCorrelate.TabIndex = 2;
            radioRotationAlignPhaseCorrelate.Text = "相位相关法旋转对齐";
            radioRotationAlignPhaseCorrelate.UseVisualStyleBackColor = true;
            // 
            // radioMiddleFlip
            // 
            radioMiddleFlip.AutoSize = true;
            radioMiddleFlip.Location = new System.Drawing.Point(221, 20);
            radioMiddleFlip.Name = "radioMiddleFlip";
            radioMiddleFlip.Size = new System.Drawing.Size(98, 21);
            radioMiddleFlip.TabIndex = 1;
            radioMiddleFlip.Text = "中天翻转校准";
            radioMiddleFlip.UseVisualStyleBackColor = true;
            // 
            // radioFieldRotation
            // 
            radioFieldRotation.AutoSize = true;
            radioFieldRotation.Location = new System.Drawing.Point(105, 20);
            radioFieldRotation.Name = "radioFieldRotation";
            radioFieldRotation.Size = new System.Drawing.Size(110, 21);
            radioFieldRotation.TabIndex = 0;
            radioFieldRotation.Text = "经纬仪场旋校准";
            radioFieldRotation.UseVisualStyleBackColor = true;
            // 
            // checkReadTif
            // 
            checkReadTif.AutoSize = true;
            checkReadTif.Location = new System.Drawing.Point(19, 20);
            checkReadTif.Name = "checkReadTif";
            checkReadTif.Size = new System.Drawing.Size(72, 21);
            checkReadTif.TabIndex = 3;
            checkReadTif.Text = "TIF/TIFF";
            checkReadTif.UseVisualStyleBackColor = true;
            // 
            // checkReadJpg
            // 
            checkReadJpg.AutoSize = true;
            checkReadJpg.Location = new System.Drawing.Point(118, 20);
            checkReadJpg.Name = "checkReadJpg";
            checkReadJpg.Size = new System.Drawing.Size(81, 21);
            checkReadJpg.TabIndex = 4;
            checkReadJpg.Text = "JPG/JPEG";
            checkReadJpg.UseVisualStyleBackColor = true;
            // 
            // checkReadPng
            // 
            checkReadPng.AutoSize = true;
            checkReadPng.Location = new System.Drawing.Point(213, 20);
            checkReadPng.Name = "checkReadPng";
            checkReadPng.Size = new System.Drawing.Size(53, 21);
            checkReadPng.TabIndex = 5;
            checkReadPng.Text = "PNG";
            checkReadPng.UseVisualStyleBackColor = true;
            // 
            // checkReadFit
            // 
            checkReadFit.AutoSize = true;
            checkReadFit.Location = new System.Drawing.Point(287, 20);
            checkReadFit.Name = "checkReadFit";
            checkReadFit.Size = new System.Drawing.Size(73, 21);
            checkReadFit.TabIndex = 6;
            checkReadFit.Text = "FIT/FITS";
            checkReadFit.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            ClientSize = new System.Drawing.Size(896, 742);
            Controls.Add(splitContainerMainForm);
            Controls.Add(menuStrip1);
            Controls.Add(groupLogText);
            Name = "MainForm";
            Text = "太阳图像对齐工具";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupFileSelect.ResumeLayout(false);
            groupFileSelect.PerformLayout();
            groupProcess.ResumeLayout(false);
            groupProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericTargetIndex).EndInit();
            groupAlign.ResumeLayout(false);
            groupBoxLightEnhance.ResumeLayout(false);
            groupBoxLightEnhance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLightEnhance).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericLightEnhance).EndInit();
            groupAlignTimes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericAlignTimes).EndInit();
            groupAlignObject.ResumeLayout(false);
            groupAlignObject.PerformLayout();
            groupAlignMode.ResumeLayout(false);
            groupAlignMode.PerformLayout();
            groupFileSave.ResumeLayout(false);
            groupOutput.ResumeLayout(false);
            groupOutput.PerformLayout();
            groupLogText.ResumeLayout(false);
            splitContainerMainForm.Panel1.ResumeLayout(false);
            splitContainerMainForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMainForm).EndInit();
            splitContainerMainForm.ResumeLayout(false);
            groupBoxScaleAlign.ResumeLayout(false);
            groupBoxScaleAlign.PerformLayout();
            groupBoxFeatureAlign.ResumeLayout(false);
            groupBoxFeatureAlign.PerformLayout();
            groupBoxRotationAlign.ResumeLayout(false);
            groupBoxRotationAlign.PerformLayout();
            groupBoxRotationAlignTimes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericRotationAlignTimes).EndInit();
            groupBoxSolarPoleNorthUp.ResumeLayout(false);
            groupBoxSolarPoleNorthUp.PerformLayout();
            groupBoxFieldRotation.ResumeLayout(false);
            groupBoxGeoCoord.ResumeLayout(false);
            groupBoxGeoCoord.PerformLayout();
            groupBoxMiddleFlip.ResumeLayout(false);
            groupBoxMiddleFlip.PerformLayout();
            groupBoxMiddleFlipManual.ResumeLayout(false);
            groupBoxMiddleFlipManual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericMiddleFlipManual2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMiddleFlipManual1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        // 窗体组件
        private System.Windows.Forms.SplitContainer splitContainerMainForm;

        // 菜单栏
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatelogToolStripMenuItem;

        // 文件选择
        private System.Windows.Forms.GroupBox groupFileSelect;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnLoadPath;

        // 处理流程
        private System.Windows.Forms.GroupBox groupProcess;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.NumericUpDown numericTargetIndex;
        private System.Windows.Forms.Label labelTargetIndex;
        // 流程选择
        private System.Windows.Forms.CheckBox chkLightNormaization;
        private System.Windows.Forms.CheckBox checkScaleAlign;
        private System.Windows.Forms.CheckBox checkRotationAlign;
        private System.Windows.Forms.CheckBox chkAlign;

        // 亮度统一
        private System.Windows.Forms.GroupBox groupLightNormalization;

        // 场旋校准
        private System.Windows.Forms.GroupBox groupBoxFieldRotation;
        private System.Windows.Forms.GroupBox groupBoxGeoCoord;
        private System.Windows.Forms.TextBox textBoxLongitude;
        private System.Windows.Forms.TextBox textBoxLatitude;
        private System.Windows.Forms.Label labelLatitude;
        private System.Windows.Forms.Label labelLongitude;
        private System.Windows.Forms.Button buttonGetGeoCoord;

        // 中天翻转校准
        private System.Windows.Forms.GroupBox groupBoxMiddleFlip;
        private System.Windows.Forms.RadioButton radioMiddleFlipManual;
        private System.Windows.Forms.RadioButton radioMiddleFlipAuto;
        // 中天翻转手动设置
        private System.Windows.Forms.GroupBox groupBoxMiddleFlipManual;
        private System.Windows.Forms.NumericUpDown numericMiddleFlipManual2;
        private System.Windows.Forms.NumericUpDown numericMiddleFlipManual1;
        private System.Windows.Forms.Label labelMiddleFlipManual;

        // 图像对齐
        private System.Windows.Forms.GroupBox groupAlign;
        // 第一组：对齐目标
        private System.Windows.Forms.GroupBox groupAlignObject;
        private System.Windows.Forms.RadioButton radioFullDisk;
        private System.Windows.Forms.RadioButton radioLocalArea;
        // 第二组：对齐模式
        private System.Windows.Forms.GroupBox groupAlignMode;
        private System.Windows.Forms.RadioButton radioMassCenter;
        private System.Windows.Forms.RadioButton radioPhaseCorrelate;
        // 第三组：对齐次数
        private System.Windows.Forms.GroupBox groupAlignTimes;
        private System.Windows.Forms.NumericUpDown numericAlignTimes;
        // 亮度增强（全日面且有明显自转时用）
        private System.Windows.Forms.GroupBox groupBoxLightEnhance;
        private System.Windows.Forms.Button buttonLightEnhance;
        private System.Windows.Forms.NumericUpDown numericLightEnhance;
        private System.Windows.Forms.TrackBar trackBarLightEnhance;

        // 保存格式
        private System.Windows.Forms.GroupBox groupFileSave;
        // 第一组：图像格式
        private System.Windows.Forms.GroupBox groupOutput;
        private System.Windows.Forms.RadioButton radioSaveTif;
        private System.Windows.Forms.RadioButton radioSaveJpg;
        private System.Windows.Forms.RadioButton radioSavePng;

        // 日志框
        private System.Windows.Forms.GroupBox groupLogText;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.GroupBox groupBoxRotationAlign;
        private System.Windows.Forms.RadioButton radioRotationAlignPhaseCorrelate;
        private System.Windows.Forms.RadioButton radioMiddleFlip;
        private System.Windows.Forms.RadioButton radioFieldRotation;
        private System.Windows.Forms.GroupBox groupBoxScaleAlign;
        private System.Windows.Forms.CheckBox checkScaleAlignMaxResolution;
        private System.Windows.Forms.GroupBox groupBoxRotationAlignTimes;
        private System.Windows.Forms.NumericUpDown numericRotationAlignTimes;
        private System.Windows.Forms.GroupBox groupBoxSolarPoleNorthUp;
        private System.Windows.Forms.CheckBox checkSolarPoleNorthUp;
        private System.Windows.Forms.RadioButton radioSolarPoleNorthUpAllImages;
        private System.Windows.Forms.RadioButton radioSolarPoleNorthUpOnlyTarget;
        private System.Windows.Forms.RadioButton radioRotationAlignNone;
        private System.Windows.Forms.CheckBox checkFlip;
        private System.Windows.Forms.RadioButton radioRotationAlignFeature;
        private System.Windows.Forms.RadioButton radioAlignFeature;
        private System.Windows.Forms.RadioButton radioScaleAlignFeature;
        private System.Windows.Forms.RadioButton radioScaleAlignPhaseCorrelate;
        private System.Windows.Forms.GroupBox groupBoxFeatureAlign;
        private System.Windows.Forms.CheckBox checkBoxECC;
        private System.Windows.Forms.RadioButton radioSaveFit;
        private System.Windows.Forms.CheckBox checkReadFit;
        private System.Windows.Forms.CheckBox checkReadPng;
        private System.Windows.Forms.CheckBox checkReadJpg;
        private System.Windows.Forms.CheckBox checkReadTif;
    }
}