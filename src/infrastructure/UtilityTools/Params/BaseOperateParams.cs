namespace UtilityTools.Params
{
    public abstract class BaseOperateParams : IOperateParams
    {
        public long OperatorId { get; set; }

        public string OperatorName { get; set; }

        protected BaseOperateParams()
        {
            OperatorName = "";
        }
    }
}