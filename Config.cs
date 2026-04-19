using System;
using System.IO;
using System.Reflection;

namespace SolarImageProcessionCsharp
{
    // 配置类
    public class AppConfig
    {
        public bool EnableReadTif { get; set; } = true;
        public bool EnableReadJpg { get; set; } = false;
        public bool EnableReadPng { get; set; } = false;
        public bool EnableReadFit { get; set; } = false;
        public bool EnableLightNormalization { get; set; } = true;
        public bool EnableFlip { get; set; } = false;
        public bool EnableScaleAlign { get; set; } = false;
        public bool EnableRotationAlign { get; set; } = false;
        public bool EnableAlign { get; set; } = true;
        public bool ScaleAlignMaxResolution { get; set; } = true;
        public bool SolarPoleNorthUp { get; set; } = true;
        public bool ECCAlign { get; set; } = false;
        public string ImageAlignmentObject { get; set; } = "FullDisk";
        public string ImageAlignmentMode { get; set; } = "MassCenter";
        public string ScaleAlignmentMode { get; set; } = "PhaseCorrelate";
        public string RotationAlignmentMode { get; set; } = "None";
        public string SolarPoleNorthUpMode { get; set; } = "OnlyTarget";
        public string MiddleFlipMode { get; set; } = "Auto";
        public string SaveFormat { get; set; } = "tif";
        public int TargetIndex { get; set; } = 1;
        public int AlignTimes { get; set; } = 1;
        public int RotationAlignTimes { get; set; } = 1;

        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;
    }

    // 配置管理器
    public static class ConfigManager
    {
        private static string ConfigPath = "config.ini";
        public static AppConfig Config { get; private set; } = new AppConfig();

        // 加载配置（不存在自动创建）
        public static void Load()
        {
            Config = new AppConfig();

            if (!File.Exists(ConfigPath))
            {
                Save(); // 不存在就创建默认配置
                return;
            }

            try
            {
                var lines = File.ReadAllLines(ConfigPath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";"))
                        continue;

                    var parts = line.Split('=');
                    if (parts.Length != 2)
                        continue;

                    var key = parts[0].Trim();
                    var value = parts[1].Trim();

                    // 反射自动赋值
                    var prop = typeof(AppConfig).GetProperty(key);
                    if (prop != null)
                    {
                        object convertedValue = Convert.ChangeType(value, prop.PropertyType);
                        prop.SetValue(Config, convertedValue);
                    }
                }
            }
            catch
            {
                Save();
            }
        }

        // 保存配置到 ini
        public static void Save()
        {
            using (var sw = new StreamWriter(ConfigPath))
            {
                sw.WriteLine("; 图像配置文件 - 自动生成");
                sw.WriteLine("; 请勿手动修改格式");
                sw.WriteLine();

                foreach (var prop in typeof(AppConfig).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var name = prop.Name;
                    var value = prop.GetValue(Config)?.ToString() ?? "";
                    sw.WriteLine($"{name}={value}");
                }
            }
        }
    }
}
