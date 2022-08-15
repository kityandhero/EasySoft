using System;

namespace UtilityTools.Assists
{
    public static class GeoHashAssist
    {
        /// <summary>
        /// 计算GeoHash字符串表示
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static string CalculateGeoHash(double latitude, double longitude)
        {
            return NGeoHash.GeoHash.Encode(latitude, longitude);
        }

        /// <summary>
        /// 计算GeoHash字符串表示
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static string[] CalculateNeighbors(string hash)
        {
            return NGeoHash.GeoHash.Neighbors(hash);
        }

        /// <summary>
        /// 计算GeoHash数字表示
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static long CalculateGeoHashInt(double latitude, double longitude)
        {
            return NGeoHash.GeoHash.EncodeInt(latitude, longitude);
        }

        private const double EARTH_RADIUS = 6378137;

        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double CalculateDistance(double lat1, double lng1, double lat2, double lng2)
        {
            var radLat1 = Rad(lat1);
            var radLng1 = Rad(lng1);
            var radLat2 = Rad(lat2);
            var radLng2 = Rad(lng2);
            var a = radLat1 - radLat2;
            var b = radLng1 - radLng2;
            var result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                                                 Math.Cos(radLat1) * Math.Cos(radLat2) *
                                                 Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return d * Math.PI / 180d;
        }
    }
}