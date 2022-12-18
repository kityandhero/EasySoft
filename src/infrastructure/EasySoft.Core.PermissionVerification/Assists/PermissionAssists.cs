using EasySoft.Core.PermissionVerification.Attributes;
using EasySoft.Core.PermissionVerification.Detectors;
using EasySoft.Core.PermissionVerification.Entities;

namespace EasySoft.Core.PermissionVerification.Assists;

/// <summary>
/// PermissionAssists
/// </summary>
public static class PermissionAssists
{
    private static readonly ConcurrentQueue<AccessWayModel> AccessWayModelScanQuery;

    static PermissionAssists()
    {
        AccessWayModelScanQuery = new ConcurrentQueue<AccessWayModel>();
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

    internal static async Task StartSaveAsync()
    {
        if (!GetAccessWayModelScanQuery().Any()) return;

        var isDevelopment = EnvironmentAssist.IsDevelopment();

        if (isDevelopment)
            LogAssist.Prompt(
                $"Permission need sync {GetAccessWayModelScanQuery().Count}, start sync."
            );

        await ExecSaveAsync();

        if (isDevelopment) LogAssist.Prompt("Permission sync complete.");
    }

    private static async Task ExecSaveAsync()
    {
        while (AccessWayModelScanQuery.TryDequeue(out var accessWayModel))
        {
            var accessWayDetector = AutofacAssist.Instance.Resolve<IAccessWayDetector>();

            var accessWayModels = await accessWayDetector.Find(accessWayModel.GuidTag);

            if (accessWayModels.Count > 0) continue;

            await AutofacAssist.Instance.Resolve<IAccessWayProducer>().SendAsync(
                accessWayModel.GuidTag,
                accessWayModel.Name,
                accessWayModel.RelativePath,
                accessWayModel.Expand
            );

            if (EnvironmentAssist.IsDevelopment())
                LogAssist.Prompt($"Send {nameof(AccessWayModel)} -> {accessWayModel.BuildInfo()}.");
        }
    }
}