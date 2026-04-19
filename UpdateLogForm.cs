using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolarImageProcessionCsharp
{
    public partial class UpdateLogForm : Form
    {
        public UpdateLogForm()
        {
            InitializeComponent();
            LoadUpdateLogContent();
        }

        private void LoadUpdateLogContent()
        {
            var logs = UpdateLogHelper.LoadUpdateLog();

            richTextBoxUpdateLog.Clear();

            foreach (var log in logs)
            {
                richTextBoxUpdateLog.AppendText($"【版本 {log.Version}】{log.Date}\n");
                foreach (var change in log.Changes)
                {
                    richTextBoxUpdateLog.AppendText($"• {change}\n");
                }
                richTextBoxUpdateLog.AppendText("\n");
            }

            // 回到顶部
            richTextBoxUpdateLog.SelectionStart = 0;
            richTextBoxUpdateLog.ScrollToCaret();
        }
    }
}
