namespace SolarImageProcessionCsharp
{
    partial class ImageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageForm));
            pictureBoxImageShow = new System.Windows.Forms.PictureBox();
            trackBarZoom = new System.Windows.Forms.TrackBar();
            trackBarFrame = new System.Windows.Forms.TrackBar();
            groupBoxFrame = new System.Windows.Forms.GroupBox();
            numericFrame = new System.Windows.Forms.NumericUpDown();
            groupBoxZoom = new System.Windows.Forms.GroupBox();
            numericZoom = new System.Windows.Forms.NumericUpDown();
            panelImage = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImageShow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarZoom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarFrame).BeginInit();
            groupBoxFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericFrame).BeginInit();
            groupBoxZoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericZoom).BeginInit();
            panelImage.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBoxImageShow
            // 
            pictureBoxImageShow.Location = new System.Drawing.Point(3, 3);
            pictureBoxImageShow.Name = "pictureBoxImageShow";
            pictureBoxImageShow.Size = new System.Drawing.Size(848, 516);
            pictureBoxImageShow.TabIndex = 0;
            pictureBoxImageShow.TabStop = false;
            // 
            // trackBarZoom
            // 
            trackBarZoom.Location = new System.Drawing.Point(6, 22);
            trackBarZoom.Maximum = 300;
            trackBarZoom.Minimum = 10;
            trackBarZoom.Name = "trackBarZoom";
            trackBarZoom.Size = new System.Drawing.Size(345, 45);
            trackBarZoom.TabIndex = 1;
            trackBarZoom.Value = 20;
            trackBarZoom.Scroll += trackBarZoom_Scroll;
            // 
            // trackBarFrame
            // 
            trackBarFrame.Location = new System.Drawing.Point(6, 22);
            trackBarFrame.Name = "trackBarFrame";
            trackBarFrame.Size = new System.Drawing.Size(345, 45);
            trackBarFrame.TabIndex = 2;
            trackBarFrame.Scroll += trackBarFrame_Scroll;
            // 
            // groupBoxFrame
            // 
            groupBoxFrame.Controls.Add(numericFrame);
            groupBoxFrame.Controls.Add(trackBarFrame);
            groupBoxFrame.Location = new System.Drawing.Point(12, 8);
            groupBoxFrame.Name = "groupBoxFrame";
            groupBoxFrame.Size = new System.Drawing.Size(480, 82);
            groupBoxFrame.TabIndex = 3;
            groupBoxFrame.TabStop = false;
            groupBoxFrame.Text = "帧选择";
            // 
            // numericFrame
            // 
            numericFrame.Location = new System.Drawing.Point(370, 22);
            numericFrame.Name = "numericFrame";
            numericFrame.Size = new System.Drawing.Size(97, 23);
            numericFrame.TabIndex = 3;
            // 
            // groupBoxZoom
            // 
            groupBoxZoom.Controls.Add(numericZoom);
            groupBoxZoom.Controls.Add(trackBarZoom);
            groupBoxZoom.Location = new System.Drawing.Point(498, 8);
            groupBoxZoom.Name = "groupBoxZoom";
            groupBoxZoom.Size = new System.Drawing.Size(480, 82);
            groupBoxZoom.TabIndex = 4;
            groupBoxZoom.TabStop = false;
            groupBoxZoom.Text = "图像缩放";
            // 
            // numericZoom
            // 
            numericZoom.Location = new System.Drawing.Point(370, 22);
            numericZoom.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numericZoom.Name = "numericZoom";
            numericZoom.Size = new System.Drawing.Size(97, 23);
            numericZoom.TabIndex = 2;
            // 
            // panelImage
            // 
            panelImage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelImage.AutoScroll = true;
            panelImage.Controls.Add(pictureBoxImageShow);
            panelImage.Location = new System.Drawing.Point(124, 94);
            panelImage.Name = "panelImage";
            panelImage.Size = new System.Drawing.Size(854, 522);
            panelImage.TabIndex = 5;
            // 
            // ImageForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1001, 649);
            ControlBox = false;
            Controls.Add(panelImage);
            Controls.Add(groupBoxZoom);
            Controls.Add(groupBoxFrame);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "ImageForm";
            Text = "图像显示";
            ((System.ComponentModel.ISupportInitialize)pictureBoxImageShow).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarZoom).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarFrame).EndInit();
            groupBoxFrame.ResumeLayout(false);
            groupBoxFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericFrame).EndInit();
            groupBoxZoom.ResumeLayout(false);
            groupBoxZoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericZoom).EndInit();
            panelImage.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxImageShow;
        private System.Windows.Forms.TrackBar trackBarZoom;
        private System.Windows.Forms.TrackBar trackBarFrame;
        private System.Windows.Forms.GroupBox groupBoxFrame;
        private System.Windows.Forms.NumericUpDown numericFrame;
        private System.Windows.Forms.GroupBox groupBoxZoom;
        private System.Windows.Forms.NumericUpDown numericZoom;
        private System.Windows.Forms.Panel panelImage;
    }
}