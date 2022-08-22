using System;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.UtilityTools.Standard.Assists
{
    public static class VerifyAssist
    {
        /// <summary>
        /// 验证是否为null
        /// </summary>
        /// <param name="source">要检验的变量</param>
        /// <returns></returns>
        public static bool IsNull(this object? source)
        {
            return source == null;
        }

        /// <summary>
        /// 是否为数字（以double类型检验）
        /// </summary>
        /// <param name="source">要检验的变量</param>
        /// <returns></returns>
        public static bool IsBoolean(this object source)
        {
            if (source.IsNull())
            {
                return false;
            }

            var result = bool.TryParse(source.ToString(), out var _);
            return result;
        }

        /// <summary>
        /// 是否为数字（以double类型检验）
        /// </summary>
        /// <param name="source">要检验的变量</param>
        /// <returns></returns>
        public static bool IsNumber(this object source)
        {
            if (source.IsNull())
            {
                return false;
            }

            var result = double.TryParse(source.ToString(), out var _);
            return result;
        }

        /// <summary>
        /// 是否为 Int32 类型
        /// </summary>
        /// <param name="source">要检验的变量</param>
        /// <returns></returns>
        public static bool IsInt(this object source)
        {
            return source.IsInt32();
        }

        /// <summary>
        /// 是否为 Int32 类型
        /// </summary>
        /// <param name="source">要检验的变量</param>
        /// <returns></returns>
        public static bool IsInt32(this object source)
        {
            if (source.IsNull())
            {
                return false;
            }

            var result = int.TryParse(source.ToString(), out var _);

            return result;
        }

        /// <summary>
        /// 是否为 Int64 类型
        /// </summary>
        /// <param name="source">要检验的变量</param>
        /// <returns></returns>
        public static bool IsInt64(this object? source)
        {
            if (source == null)
            {
                return false;
            }

            var result = long.TryParse(source.ToString(), out _);

            return result;
        }

        /// <summary>
        /// 是否为Datetime
        /// </summary>
        /// <param name="source">要检验的变量</param>
        /// <returns></returns>
        public static bool IsDateTime(this object source)
        {
            if (source.IsNull())
            {
                return false;
            }

            var result = DateTime.TryParse(source.ToString(), out var _);

            return result;
        }

        #region Check Double

        /// <summary>
        /// Validate Double
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool CheckDouble(params string[] p)
        {
            var result = true;
            foreach (var one in p)
            {
                if (one == null)
                {
                    result = false;
                    break;
                }

                if (!double.TryParse(one, out var _))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        #endregion Check Double

        public static ExecutiveResult CheckPasswordCanUse(string initialPassword)
        {
            if (string.IsNullOrWhiteSpace(initialPassword))
            {
                return new ExecutiveResult(ReturnCode.ParamError.ToMessage("密码不能为空"));
            }

            var initialPasswordTrim = initialPassword.Remove(" ").Trim();

            if (initialPasswordTrim.Length < 6)
            {
                return new ExecutiveResult(ReturnCode.ParamError.ToMessage("密码长度不能小于6位"));
            }

            if (initialPasswordTrim.Length > 32)
            {
                return new ExecutiveResult(ReturnCode.ParamError.ToMessage("密码长度不能长于32位"));
            }

            return new ExecutiveResult(ReturnCode.Ok);
        }
    }
}