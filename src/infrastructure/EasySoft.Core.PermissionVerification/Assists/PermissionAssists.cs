using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Detectors;
using EasySoft.Core.PermissionVerification.Detectors.Interfaces;
using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.PermissionVerification.Assists;

/// <summary>
/// PermissionAssists
/// </summary>
public static class PermissionAssists
{
    private static readonly IDictionary<string, AccessWayModel> AccessWayModelScanHistory =
        new ConcurrentDictionary<string, AccessWayModel>();

    private static readonly ConcurrentQueue<AccessWayModel> AccessWayModelScanQuery = new();

    /// <summary>
    /// AddAccessWayModelScanHistory
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="guidTag"></param>
    /// <param name="accessWayModel"></param>
    public static async Task AddAccessWayModelScanHistory(
        IMediator mediator,
        string guidTag,
        AccessWayModel accessWayModel
    )
    {
        if (AccessWayModelScanHistory.Keys.Contains(guidTag))
            return;

        AccessWayModelScanHistory.Add(guidTag, accessWayModel);

        LogAssist.Prompt(
            $"Permission -> {accessWayModel.BuildInfo()} add to history."
        );

        await mediator.Publish(new PermissionAccessNotification(accessWayModel));
    }

    /// <summary>
    /// GetAccessWayModelScanQuery
    /// </summary>
    /// <returns></returns>
    private static ConcurrentQueue<AccessWayModel> GetAccessWayModelScanQuery()
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

            if (string.IsNullOrWhiteSpace(mainRoute)) return;

            var methodInfos = o
                .GetMethods()
                .Where(x => x.ExistAttribute<RouteAttribute>() && x.ExistAttribute<PermissionAttribute>())
                .ToList();

            methodInfos.ForEach(x =>
            {
                var childRoute = x.GetCustomAttribute<RouteAttribute>()?.Template ?? "";

                if (string.IsNullOrWhiteSpace(childRoute)) return;

                var permissionAttribute = x.GetCustomAttribute<PermissionAttribute>();

                if (permissionAttribute == null || string.IsNullOrWhiteSpace(permissionAttribute.GuidTag)) return;

                var relativePath = new List<string> { $"/{mainRoute}", childRoute }.Join("/");

                var accessWayModel = new AccessWayModel
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
                    LogAssist.Prompt(
                        $"Scan Permission: \"{assembly.GetName().Name}\" -> \"{accessWayModel.Name}, {accessWayModel.GuidTag} ({accessWayModel.RelativePath})\"."
                    );
            });
        });
    }

    internal static async Task StartSaveAsync(IAccessWayDetector accessWayDetector)
    {
        if (!GetAccessWayModelScanQuery().Any()) return;

        var isDevelopment = EnvironmentAssist.IsDevelopment();

        if (isDevelopment)
            LogAssist.Prompt(
                $"Permission need sync {GetAccessWayModelScanQuery().Count}, start sync."
            );

        await ExecSaveAsync(accessWayDetector);

        if (isDevelopment) LogAssist.Prompt("Permission sync complete.");
    }

    private static async Task ExecSaveAsync(IAccessWayDetector accessWayDetector)
    {
        var accessWayProducer = AutofacAssist.Instance.Resolve<IAccessWayProducer>();

        while (AccessWayModelScanQuery.TryDequeue(out var accessWayModel))
        {
            var accessWayModels = await accessWayDetector.Find(accessWayModel.GuidTag);

            if (accessWayModels.Count > 0) continue;

            await accessWayProducer.SendAsync(accessWayModel);

            if (EnvironmentAssist.IsDevelopment())
                LogAssist.Prompt($"Send {nameof(AccessWayModel)} -> {accessWayModel.BuildInfo()}.");
        }
    }
}