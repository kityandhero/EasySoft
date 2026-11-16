using EasySoft.Core.Infrastructure.Assists;
using EasySoft.Core.Infrastructure.Configures;
using EasySoft.Core.Infrastructure.Startup;

namespace EasySoft.Core.Web.Framework.Kernel.ExtensionMethods;

public static class MvcOptionsExtensions
{
    /// <summary>
    /// 织入扩展的 MvcOptions Action
    /// </summary>
    internal static void WeaveMvcOptionExtraAction(
        this MvcOptions option
    )
    {
        var extraActions = ApplicationConfigure.GetAllMvcOptionExtraActions().ToList();

        if (extraActions.Count <= 0)
        {
            return;
        }

        var startMessage = new StartupMessage()
            .SetLevel(LogLevel.Information)
            .SetMessage(
                UtilityTools.Standard.ConstCollection.ApplicationStartExtraMvcOptionMessageStartDivider
            );

        StartupMvcOptionExtraActionMessageAssist.Add(startMessage);

        var i = 1;

        extraActions.ForEach(extraAction =>
            {
                var action = extraAction.GetAction();

                if (action == null)
                {
                    return;
                }

                var name = extraAction.GetName();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    StartupMvcOptionExtraActionMessageAssist.Add(
                        new StartupMessage()
                            .SetLevel(LogLevel.Information)
                            .SetMessage(
                                $"{i}: {name}"
                            )
                    );

                    i += 1;
                }

                action(option);
            }
        );
    }
}