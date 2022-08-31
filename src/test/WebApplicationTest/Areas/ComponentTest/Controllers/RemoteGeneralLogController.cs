using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.ExchangeRegulation.ExtensionMethods;
using EasySoft.Core.GeneralLogTransmitter.Producers;
using EasySoft.Core.Infrastructure.ExtensionMethods;
using EasySoft.Core.Web.Framework.ExtensionMethods;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class RemoteGeneralLogController : AreaControllerCore
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