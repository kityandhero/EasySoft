using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ExchangeRegulation.ExtensionMethods;
using EasySoft.Core.GeneralLogTransmitter.Producers;
using EasySoft.Core.Mvc.Framework.Controllers;
using EasySoft.Core.Mvc.Framework.ExtensionMethods;
using EasySoft.UtilityTools.Enums;
using EasySoft.UtilityTools.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers;

public class RemoteGeneralLogController : CustomControllerBase
{
    private readonly IGeneralLogProducer _generalLogProducer;

    public RemoteGeneralLogController(IGeneralLogProducer generalLogProducer)
    {
        _generalLogProducer = generalLogProducer;
    }

    // public DataController(DbContext context, IAuthorService authorService)
    // {
    //     context = context;
    //     _authorService = authorService;
    // }

    public IActionResult Test()
    {
        if (!GeneralConfigAssist.GetRemoteGeneralLogSwitch())
        {
            return this.Fail(ReturnCode.NoChange.ToMessage("RemoteGeneralLogEnable switch is not open"));
        }

        var log = _generalLogProducer.Send("Test");

        return this.Success(log.ToObject());
    }
}