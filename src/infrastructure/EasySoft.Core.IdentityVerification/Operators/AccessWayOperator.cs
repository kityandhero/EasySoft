using EasySoft.UtilityTools;

namespace EasySoft.Core.IdentityVerification.Operators;

public class AccessWayOperator
{
    private TimeSpan GuidTagIdMappingCacheExpireTime => new TimeSpan(
        TimeSpan.TicksPerSecond * 12 * RandomEx.ThreadSafeNext(3600, 7200)
    );

    public long GetGuidTagIdMapping(
        string guidTag
    )
    {
        var result = 0L;

        if (string.IsNullOrWhiteSpace(guidTag))
        {
            var db = RedisConfigCollection.LocalRedisDatabase();

            var key = GetGuidTagIdMappingCacheKey(
                guidTag
            );

            var reSet = false;

            var name = db.StringGet(key);

            if (!name.IsNullOrEmpty)
            {
                result = Convert.ToInt64(name, CultureInfo.InvariantCulture);
            }
            else
            {
                reSet = true;
            }

            if (reSet)
            {
                result = GetGuidTagIdMappingCore(
                    guidTag
                );

                db.StringSet(key, result, GuidTagIdMappingCacheExpireTime);
            }
        }

        return result;
    }

    private long GetGuidTagIdMappingCore(
        string guidTag
    )
    {
        var result = 0L;

        if (string.IsNullOrWhiteSpace(guidTag))
        {
            var expressInvoice = EntityAssist.GetEntity(
                new List<Condition<AccessWay>>
                {
                    new Condition<AccessWay>
                    {
                        ConditionType = ConditionType.Eq,
                        Expression = o => o.GuidTag,
                        Value = guidTag
                    }
                }
            );

            if (expressInvoice != null)
            {
                result = expressInvoice.Id;
            }
        }

        return result;
    }

    private string GetGuidTagIdMappingCacheKey(
        string guidTag
    )
    {
        if (string.IsNullOrWhiteSpace(guidTag))
        {
            throw new Exception("GuidTag无效");
        }

        var v = guidTag.Remove("-").ToLower();

        return RedisAssist.BuildKey(
            nameof(AccessWay).ToLowerFirst(),
            "guidTag",
            v
        );
    }
}