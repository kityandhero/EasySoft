﻿using EasySoft.UtilityTools.Core.Securities.Interfaces;

namespace EasySoft.UtilityTools.Core.Securities.implementations;

internal class HashGenerator : IHashGenerator
{
    public HashConsistentGenerator ConsistentGenerator => HashConsistentGenerator.Instance;

    internal HashGenerator()
    {
    }
}