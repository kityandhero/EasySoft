using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.Core.IdentityVerification.Attributes
{
    /// <summary>
    /// 权限配置
    /// </summary>
    public class CompetenceConfigAttribute : Attribute
    {
        /// <summary>
        /// 配置项
        /// </summary>
        public List<string> Config { get; private set; }

        public CompetenceConfigAttribute() : this((new List<string>()).ToArray())
        {
        }

        public CompetenceConfigAttribute(params string[] jsonConfig)
        {
            Config = new List<string>();
            foreach (var c in jsonConfig)
            {
                if (c.Contains("|"))
                {
                    throw new Exception("扩展权限中不能含有|");
                }

                if (!c.IsTrimEmpty())
                {
                    Config.Add(c);
                }
            }
        }

        /// <summary>
        /// 转换权限值为字符串形式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = Config.Aggregate("", (current, c) => current + (c + "|"));
            result = result.TrimEnd('|');
            return result;
        }
    }
}