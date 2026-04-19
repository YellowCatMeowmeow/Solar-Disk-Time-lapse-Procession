using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SolarImageProcessionCsharp
{
    public class Astronomy
    {
        // 儒略历计算
        public static double JulianDay(DateTime utc)
        {
            double year = utc.Year;
            double month = utc.Month;
            double day = utc.Day +
                         utc.Hour / 24.0 +
                         utc.Minute / 1440.0 +
                         utc.Second / 86400.0;

            if (month < 3)
            {
                year--;
                month += 12;
            }

            double A = Math.Floor(year / 100);
            double B = 2 - A + Math.Floor(A / 4);
            return Math.Floor(365.25 * (year + 4716)) +
                   Math.Floor(30.6001 * (month + 1)) +
                   day + B - 1524.5;
        }

        // 格林威治恒星时计算
        public static double GreenwichSiderealTime(double jd)
        {
            double c = (jd - 2451545.0) / 36525.0;
            double gst = 280.46061837 + 360.98564736629 * (jd - 2451545.0) +
                         0.0003032 * c * c - 0.00000026 * c * c * c;
            gst %= 360;
            if (gst < 0) gst += 360;
            return gst * Math.PI / 180.0;
        }

        /// <summary>
        /// <summary>
        /// 【无依赖、不报错】获取本地经纬度（IP定位）
        /// </summary>
        public static async Task<(double lat, double lon)> GetLocalLongLat()
        {
            try
            {
                using var client = new HttpClient();

                var json = await client.GetStringAsync("http://ip-api.com/json");

                using var doc = JsonDocument.Parse(json);

                return (
                    doc.RootElement.GetProperty("lat").GetDouble(),
                    doc.RootElement.GetProperty("lon").GetDouble()
                );
            }
            catch
            {
                // 备用：比如你观测地
                return (0, 0);
            }
        }

        // 太阳赤道坐标函数
        public static (double RA, double Dec) SunEquatorialCoordinates(DateTime utc)
        {
            // 儒略日
            double jd = JulianDay(utc);

            // 从 J2000.0 起算的世纪数
            double n = (jd - 2451545.0) / 36525.0;

            // 太阳几何平黄经 L (deg)
            double L = (280.46646 + 36000.76983 * n + 0.0003032 * n * n) % 360;

            // 太阳平近点角 g (deg)
            double g = (357.52911 + 35999.05029 * n - 0.0001537 * n * n) % 360;

            // 太阳黄经 λ
            double g_rad = g * Math.PI / 180;
            double lambda = L +
                1.914602 * Math.Sin(g_rad) +
                0.019993 * Math.Sin(2 * g_rad) +
                0.000289 * Math.Sin(3 * g_rad);

            // 黄赤交角 ε
            double eps = 23.0 + (26.0 + (21.448 - 46.8150 * n - 0.00059 * n * n + 0.001813 * n * n * n) / 60) / 60;
            double eps_rad = eps * Math.PI / 180;
            double lambda_rad = lambda * Math.PI / 180;

            // 赤经 RA, 赤纬 Dec
            double ra_rad = Math.Atan2(Math.Cos(eps_rad) * Math.Sin(lambda_rad), Math.Cos(lambda_rad));
            double dec_rad = Math.Asin(Math.Sin(eps_rad) * Math.Sin(lambda_rad));

            double ra = ra_rad * 180.0 / Math.PI;
            double dec = dec_rad * 180.0 / Math.PI;

            if (ra < 0) ra += 360;
            return (ra, dec);
        }
    }

}
