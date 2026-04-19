using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SolarImageProcessionCsharp
{
    // 更新日志
    public class UpdateInfo
    {
        public string Version { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public List<string> Changes { get; set; } = new();
    }

    // 更新日志助手
    public static class UpdateLogHelper
    {
        // 读取更新日志
        public static List<UpdateInfo> LoadUpdateLog()
        {
            try
            {
                string path = Path.Combine(AppContext.BaseDirectory, "UpdateLog.json");
                string json = File.ReadAllText(path);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<List<UpdateInfo>>(json, options) ?? new();
            }
            catch
            {
                return new List<UpdateInfo>();
            }
        }
    }
}
