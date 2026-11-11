using EasySoft.Core.GeneralLogTransceiver.Producers;
using EasySoft.UtilityTools.Core.Results.Interfaces;
using EasySoft.UtilityTools.Standard.Producers;

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
    public IApiResult Test()
    {
        if (!GeneralConfigAssist.GetRemoteGeneralLogSwitch())
        {
            return this.Fail(ReturnCode.NoChange.ToMessage("RemoteGeneralLogEnable switch is not open"));
        }

        _generalLogProducer.SendAsync("Test");

        return this.Success(new { time = DateTime.Now.ToUnixTime() });
    }

    /// <summary>
    /// SubscribeMessage
    /// </summary>
    /// <param name="generalLogTransitiveMessage
    [CapSubscribe("EasySoft.GeneralLog")]
    public void SubscribeMessage(IGeneralLogTransitiveMessage generalLogTransitiveMessage)
    {
        Console.WriteLine("transitiveMessage is:" + generalLogTransitiveMessage.Message);
    }
}