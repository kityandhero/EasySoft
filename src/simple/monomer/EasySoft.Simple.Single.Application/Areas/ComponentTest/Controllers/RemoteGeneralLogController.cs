using EasySoft.Core.ExchangeRegulation.Extensions;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

/// <summary>
/// RemoteGeneralLogController
/// </summary>
public class RemoteGeneralLogController : AreaControllerCore
{
    private readonly IGeneralLogProducer _generalLogProducer;

    /// <summary>
    /// RemoteGeneralLogController
    /// </summary>
    /// <param name="generalLogProducer"></param>
    public RemoteGeneralLogController(IGeneralLogProducer generalLogProducer)
    {
        _generalLogProducer = generalLogProducer;
    }

    // public DataController(DbContext context, IAuthorService authorService)
    // {
    //     context = context;
    //     _authorService = authorService;
    // }

    /// <summary>
    /// Test
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Test()
    {
        if (!GeneralConfigAssist.GetRemoteGeneralLogSwitch())
            return this.Fail(ReturnCode.NoChange.ToMessage("RemoteGeneralLogEnable switch is not open"));

        var log = await _generalLogProducer.SendAsync("Test");

        return this.Success(log.ToObject());
    }

    /// <summary>
    /// SubscribeMessage
    /// </summary>
    /// <param name="generalLogExchange"></param>
    [CapSubscribe("EasySoft.GeneralLog")]
    public void SubscribeMessage(GeneralLogExchange generalLogExchange)
    {
        Console.WriteLine("message is:" + generalLogExchange.Message);
    }
}