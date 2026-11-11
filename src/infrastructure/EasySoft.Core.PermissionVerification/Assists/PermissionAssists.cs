using EasySoft.Core.DependencyInjection.IocAssists;
using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Detectors.Interfaces;
using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Standard.Messages;
using EasySoft.UtilityTools.Standard.Producers;

namespace EasySoft.Core.PermissionVerification.Assists;

/// <summary>
/// PermissionAssists
/// </summary>
public static class PermissionAssists
{
    private static readonly IDictionary<string, IAccessWayTransitiveMessage> AccessWayModelScanHistory =
        new ConcurrentDictionary<string, IAccessWayTransitiveMessage>();

    private static readonly ConcurrentQueue<IAccessWayTransitiveMessage> AccessWayModelScanQuery = new();

    /// <summary>
    /// AddAccessWayModelScanHistory
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="guidTag"></param>
    /// <param name="accessWayTransitiveMessage"></param>
    public static async Task AddAccessWayModelScanHistory(
        IMediator mediator,
        string guidTag,
        IAccessWayTransitiveMessage accessWayTransitiveMessage
    )
    {
        if (AccessWayModelScanHistory.Keys.Contains(guidTag))
        {
            return;
        }

        AccessWayModelScanHistory.Add(guidTag, accessWayTransitiveMessage);

        LogAssist.Prompt(
            $"Permission -> {accessWayTransitiveMessage.BuildInfo()} add to history."
        );

        await mediator.Publish(new PermissionAccessNotification(accessWayTransitiveMessage));
    }

    /// <summary>
    /// GetAccessWayModelScanQuery
    /// </summary>
    /// <returns></returns>
    private static ConcurrentQueue<IAccessWayTransitiveMessage> GetAccessWayModelScanQuery()
    {
        return AccessWayModelScanQuery;
    }

    /// <summary>
    /// ScanPermission
    /// </summary>
    internal static void ScanPermission(Assembly assembly)
    {
        var list = assembly
            .GetTypes()
            .Where(x => x.HasAttribute<RouteAttribute>() && x is { IsClass: true, IsAbstract: false })
            .ToList();

        list.ForEach(o =>
            {
                var mainRoute = o.GetCustomAttribute<RouteAttribute>()?.Template ?? "";

                if (string.IsNullOrWhiteSpace(mainRoute))
                {
                    return;
                }

                var methodInfos = o
                    .GetMethods()
                    .Where(x => x.ExistAttribute<RouteAttribute>() && x.ExistAttribute<PermissionAttribute>())
                    .ToList();

                methodInfos.ForEach(x =>
                    {
                        var childRoute = x.GetCustomAttribute<RouteAttribute>()?.Template ?? "";

                        if (string.IsNullOrWhiteSpace(childRoute))
                        {
                            return;
                        }

                        var permissionAttribute = x.GetCustomAttribute<PermissionAttribute>();

                        if (permissionAttribute == null || string.IsNullOrWhiteSpace(permissionAttribute.GuidTag))
                        {
                            return;
                        }

                        var relativePath = new List<string> { $"/{mainRoute}", childRoute }.Join("/");

                        var accessWayModel = new AccessWayTransitiveMessage
                        {
                            Name = string.IsNullOrWhiteSpace(permissionAttribute.Name)
                                ? relativePath
                                : permissionAttribute.Name,
                            GuidTag = permissionAttribute.GuidTag,
                            RelativePath = relativePath,
                            Group = permissionAttribute.Group,
                            Expand = permissionAttribute.AggregateExpandItems()
                        };

                        AccessWayModelScanQuery.Enqueue(accessWayModel);

                        if (EnvironmentAssist.IsDevelopment())
                        {
                            LogAssist.Prompt(
                                $"Scan Permission: \"{assembly.GetName().Name}\" -> \"{accessWayModel.Name}, {accessWayModel.GuidTag} ({accessWayModel.RelativePath})\"."
                            );
                        }
                    }
                );
            }
        );
    }

    internal static async Task StartSaveAsync(IAccessWayDetector accessWayDetector)
    {
        if (!GetAccessWayModelScanQuery().Any())
        {
            return;
        }

        var isDevelopment = EnvironmentAssist.IsDevelopment();

        if (isDevelopment)
        {
            LogAssist.Prompt(
                $"Permission need sync {GetAccessWayModelScanQuery().Count}, start sync."
            );
        }

        await ExecSaveAsync(accessWayDetector);

        if (isDevelopment)
        {
            LogAssist.Prompt("Permission sync complete.");
        }
    }

    private static async Task ExecSaveAsync(IAccessWayDetector accessWayDetector)
    {
        var accessWayProducer = ServiceProviderAssist.Instance.Resolve<IAccessWayProducer>();

        while (AccessWayModelScanQuery.TryDequeue(out var accessWayModel))
        {
            var result = await accessWayDetector.Find(accessWayModel.GuidTag);

            if (result.Success)
            {
                continue;
            }

            await accessWayProducer.SendAsync(accessWayModel);

            if (EnvironmentAssist.IsDevelopment())
            {
                LogAssist.Prompt($"Send {nameof(AccessWayModel)} -> {accessWayModel.BuildInfo()}.");
            }
        }
    }
}