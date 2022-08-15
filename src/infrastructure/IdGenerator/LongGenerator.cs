using Snowflake.Core;

namespace IdGenerator
{
    public class LongGenerator
    {
        #region 创建单例

        private IdWorker Worker { get; }

        public LongGenerator()
        {
            Worker = new IdWorker(1, 1);
        }

        public long Generate()
        {
            return Worker.NextId();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static LongGenerator GetInstance()
        {
            return SingleHelper.GetSingletonMonitor();
        }

        private class SingleHelper
        {
            private static readonly LongGenerator SingletonMonitor = new LongGenerator()
            {
            };

            public static LongGenerator GetSingletonMonitor()
            {
                return SingletonMonitor;
            }
        }

        private class SingleHelperImpl : SingleHelper
        {
        }

        #endregion 创建单例
    }
}