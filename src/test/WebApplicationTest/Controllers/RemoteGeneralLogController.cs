using EasySoft.Core.AutoFac.IocAssists;
using EasySoft.Core.ExchangeRegulation.ExtensionMethods;
using EasySoft.Core.GeneralLogTransmitter.Interfaces;
using EasySoft.Core.GeneralLogTransmitter.Producers;
using EasySoft.Core.Mvc.Framework.Controllers;
using EasySoft.Core.Mvc.Framework.ExtensionMethods;
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
        var log = AutofacAssist.Instance.Resolve<IGeneralLogExchange>();

        _generalLogProducer.Send(log);

        return this.Success(log.ToObject());
    }
}