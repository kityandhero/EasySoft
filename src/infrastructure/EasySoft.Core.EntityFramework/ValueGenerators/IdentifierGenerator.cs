using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace EasySoft.Core.EntityFramework.ValueGenerators;

public class IdentifierGenerator : ValueGenerator
{
    public override bool GeneratesTemporaryValues => false;

    protected override object NextValue(EntityEntry entry)
    {
        return IdentifierAssist.Create();
    }
}