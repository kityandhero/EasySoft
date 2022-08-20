﻿using System.ComponentModel;
using EasySoft.Core.AccessWayTransmitter.Interfaces;
using EasySoft.Core.ExchangeRegulation.Entities;

namespace EasySoft.Core.AccessWayTransmitter.Entities;

public class AccessWayExchange : BaseExchange, IAccessWayExchange
{
    [Description("名称")]
    public string Name { get; set; }

    [Description("识别标识")]
    public string GuidTag { get; set; }

    [Description("相对路径")]
    public string RelativePath { get; set; }

    [Description("扩展权限")]
    public string Expand { get; set; }

    public AccessWayExchange()
    {
        Name = "";
        GuidTag = "";
        RelativePath = "";
        Expand = "";
    }
}