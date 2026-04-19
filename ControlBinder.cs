using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolarImageProcessionCsharp
{
    /// <summary>
    /// 通用控件绑定器：简化控件与配置的双向绑定
    /// </summary>
    public static class ControlBinder
    {
        /// <summary>
        /// 绑定 CheckBox 到布尔值配置
        /// </summary>
        /// <param name="cb">复选框控件</param>
        /// <param name="getConfig">获取配置值的函数</param>
        /// <param name="setConfig">设置配置值的动作</param>
        public static void BindCheckBox(
            this CheckBox cb,
            Func<bool> getConfig,
            Action<bool> setConfig)
        {
            // 初始化：同步配置到界面
            cb.Checked = getConfig();
            // 绑定事件：界面变化同步到配置
            cb.CheckedChanged += (s, e) => setConfig(cb.Checked);
            // 🔥 按 Enter 也能切换勾选（和空格一样效果）
            cb.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cb.Checked = !cb.Checked;
                    e.Handled = true;
                }
            };
        }

        /// <summary>
        /// 绑定 NumericUpDown 到整数配置
        /// </summary>
        /// <param name="nud">数值输入框</param>
        /// <param name="getConfig">获取配置值的函数</param>
        /// <param name="setConfig">设置配置值的动作</param>
        /// <param name="afterValueChanged">值改变后的额外执行动作（可选）</param>
        public static void BindNumericUpDown(
            this NumericUpDown nud,
            Func<int> getConfig,
            Action<int> setConfig,
            Action<int> afterValueChanged = null) // 新增可选参数
        {
            nud.Value = getConfig();
            nud.ValueChanged += (s, e) =>
            {
                int value = (int)nud.Value;
                setConfig(value);

                // 如果有额外动作，就执行
                afterValueChanged?.Invoke(value);
            };
        }

        /// <summary>
        /// 绑定 RadioButton 到字符串配置（单选组）
        /// </summary>
        /// <param name="rb">单选按钮</param>
        /// <param name="groupValue">该按钮对应的配置值</param>
        /// <param name="getConfig">获取当前配置值</param>
        /// <param name="setConfig">设置配置值</param>
        public static void BindRadioButton(
            this RadioButton rb,
            string groupValue,
            Func<string> getConfig,
            Action<string> setConfig)
        {
            rb.TabStop = true;
            // 初始化：匹配当前配置值
            rb.Checked = getConfig() == groupValue;
            // 绑定事件：选中时设置对应值
            rb.CheckedChanged += (s, e) =>
            {
                if (rb.Checked) setConfig(groupValue);
                rb.TabStop = true;
            };
            // 🔥 核心：Tab 选中后，按 Enter 直接勾选
            rb.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    rb.Checked = true;
                    e.Handled = true;
                }
            };
        }

        /// <summary>
        /// 绑定 ComboBox 到字符串配置
        /// </summary>
        /// <param name="cb">下拉框</param>
        /// <param name="getConfig">获取配置值</param>
        /// <param name="setConfig">设置配置值</param>
        public static void BindComboBox(
            this ComboBox cb,
            Func<string> getConfig,
            Action<string> setConfig)
        {
            cb.SelectedItem = getConfig();
            cb.SelectedIndexChanged += (s, e) => setConfig(cb.SelectedItem.ToString());
        }

        /// <summary>
        /// 单选框选中时启用控件，未选中时禁用控件
        /// </summary>
        public static void BindRadioEnableControl(this RadioButton radio, Control targetControl)
        {
            // 初始状态
            targetControl.Enabled = radio.Checked;

            // 状态改变时联动
            radio.CheckedChanged += (s, e) =>
            {
                targetControl.Enabled = radio.Checked;
            };
        }

        /// <summary>
        /// 任意单选/复选框被选中时启用控件，全部未选时禁用
        /// </summary>
        public static void BindAnyCheckEnableControl(
            this Control targetControl,
            params Control[] checkControls)
        {
            void UpdateState()
            {
                bool anyChecked = checkControls.Any(c =>
                    (c is RadioButton rb && rb.Checked) ||
                    (c is CheckBox cb && cb.Checked));

                targetControl.Enabled = anyChecked;
            }

            // 初始状态
            UpdateState();

            // 绑定事件
            foreach (var c in checkControls)
            {
                if (c is RadioButton rb)
                    rb.CheckedChanged += (s, e) => UpdateState();
                else if (c is CheckBox cb)
                    cb.CheckedChanged += (s, e) => UpdateState();
            }
        }

        /// <summary>
        /// 复选框 控制 分组框启用禁用（自动联动）
        /// </summary>
        public static void BindCheckBoxEnableGroup(this CheckBox cb, Control targetControl)
        {
            // 初始状态同步
            targetControl.Enabled = cb.Checked;

            // 勾选变化时自动更新
            cb.CheckedChanged += (s, e) =>
            {
                targetControl.Enabled = cb.Checked;
            };
        }

        /// <summary>
        /// 范围选择联动：结束框 ≥ 开始框
        /// </summary>
        public static void BindRangeNumericUpDown(
            NumericUpDown nudStart,
            NumericUpDown nudEnd,
            Func<int> getMaxCount)
        {
            // 当 起始值 改变时 → 自动限制 结束值的最小值
            nudStart.ValueChanged += (s, e) =>
            {
                nudEnd.Minimum = nudStart.Value;

                // 防止结束值比新最小值还小
                if (nudEnd.Value < nudStart.Value)
                    nudEnd.Value = nudStart.Value;
            };

            // 当 结束值 改变时 → 不允许小于起始值
            nudEnd.ValueChanged += (s, e) =>
            {
                if (nudEnd.Value < nudStart.Value)
                    nudEnd.Value = nudStart.Value;
            };
        }

        /// <summary>
        /// 绑定 TextBox 为经纬度数字输入（支持范围限制）
        /// </summary>
        /// <param name="min">最小值（可选）</param>
        /// <param name="max">最大值（可选）</param>
        public static void BindCoordinateTextBox(
            this TextBox tb,
            Func<double> getConfig,
            Action<double> setConfig,
            double? min = null,
            double? max = null)
        {
            // 初始化
            tb.Text = getConfig().ToString();

            // 输入限制：数字 . -
            tb.KeyPress += (s, e) =>
            {
                char key = e.KeyChar;
                if (char.IsControl(key)) return;

                bool isDigit = char.IsDigit(key);
                bool isDot = key == '.';
                bool isMinus = key == '-';

                if (isDot && tb.Text.Contains(".")) e.Handled = true;
                if (isMinus && (tb.Text.Contains("-") || tb.SelectionStart != 0)) e.Handled = true;
                if (!isDigit && !isDot && !isMinus) e.Handled = true;
            };

            // 按下 Enter 键 → 主动触发 Leave（保存+校验）
            tb.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // 让下一个控件获得焦点，触发 Leave 校验保存
                    tb.Parent.SelectNextControl(tb, true, true, true, true);
                    e.Handled = true;
                }
            };

            // 失去焦点：验证 + 范围限制 + 同步配置
            tb.Leave += (s, e) =>
            {
                if (double.TryParse(tb.Text, out double val))
                {
                    // 范围限制
                    if (min.HasValue && val < min.Value) val = min.Value;
                    if (max.HasValue && val > max.Value) val = max.Value;

                    setConfig(val);
                    tb.Text = val.ToString();
                }
                else
                {
                    // 解析失败，恢复原值
                    tb.Text = getConfig().ToString();
                }
            };

        }

        /// <summary>
        /// 刷新界面上的经纬度文本框（你新增这个方法）
        /// </summary>
        public static void RefreshCoordinateTextBoxes(TextBox txtLat, TextBox txtLon)
        {
            txtLat.Text = ConfigManager.Config.Latitude.ToString();
            txtLon.Text = ConfigManager.Config.Longitude.ToString();
        }
    }
}
