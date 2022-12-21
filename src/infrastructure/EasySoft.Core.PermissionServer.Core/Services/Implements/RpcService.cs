using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Core.PermissionServer.Core.Extensions;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.Core.PermissionServer.Core.Services.Implements;

/// <summary>
/// PermissionService
/// </summary>
public class RpcService : IRpcService
{
    private readonly IEventPublisher _eventPublisher;

    private readonly IRepository<RoleGroup> _roleGroupRepository;

    private readonly IRepository<PresetRole> _presetRoleRepository;

    private readonly IRepository<CustomRole> _customRoleRepository;

    private readonly IRepository<AccessWay> _accessWayRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="eventPublisher"></param>
    /// <param name="customRoleRepository"></param>
    /// <param name="roleGroupRepository"></param>
    /// <param name="presetRoleRepository"></param>
    /// <param name="accessWayRepository"></param>
    public RpcService(
        IEventPublisher eventPublisher,
        IRepository<RoleGroup> roleGroupRepository,
        IRepository<PresetRole> presetRoleRepository,
        IRepository<CustomRole> customRoleRepository,
        IRepository<AccessWay> accessWayRepository
    )
    {
        _eventPublisher = eventPublisher;

        _roleGroupRepository = roleGroupRepository;
        _presetRoleRepository = presetRoleRepository;
        _customRoleRepository = customRoleRepository;
        _accessWayRepository = accessWayRepository;
    }

    /// <inheritdoc />
    public async Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync(long roleGroupId)
    {
        var ceList = new List<CompetenceEntity>();

        var roelGroupResult = await _roleGroupRepository.GetAsync(roleGroupId);

        if (!roelGroupResult.Success || roelGroupResult.Data == null) return ceList;

        var roleGroup = roelGroupResult.Data;

        var customRoles = new List<CustomRole>();
        var presetRoles = new List<PresetRole>();

        var customRoleItemList = GetCustomRoleItemList(roleGroup);

        if (customRoleItemList.Count > 0)
        {
            IList<CustomRole> list = new List<CustomRole>();

            async void GetCustomRole(IRoleItem o)
            {
                var r = await _customRoleRepository.GetAsync(o.Id);

                if (r.Success && r.Data != null) list.Add(r.Data);
            }

            customRoleItemList.ForEach(GetCustomRole);

            customRoles.AddRange(list);
        }

        var presetRoleItemList = GetPresetRoleItemList(roleGroup);

        if (presetRoleItemList.Count > 0)
        {
            IList<PresetRole> list = new List<PresetRole>();

            async void GetPresetRole(IRoleItem o)
            {
                var r = await _presetRoleRepository.GetAsync(o.Id);

                if (r.Success && r.Data != null) list.Add(r.Data);
            }

            presetRoleItemList.ForEach(GetPresetRole);

            presetRoles.AddRange(list);
        }

        ceList = await RoleAssist.MergeCompetenceCollectionAsync(presetRoles, ceList, GetAccessWayPersistenceList);
        ceList = await RoleAssist.MergeCompetenceCollectionAsync(customRoles, ceList, GetAccessWayPersistenceList);

        return ceList;
    }

    /// <inheritdoc />
    public async Task<ExecutiveResult<AccessWayModel>> FindAccessWayAsync(string guidTag)
    {
        var result = await _accessWayRepository.GetAsync(
            o => o.GuidTag == guidTag
        );

        return result.ToExecutiveResult(result.Data?.ToAccessWayModel());
    }

    /// <inheritdoc />
    public async Task MaintainSuperRole(int channel)
    {
        var result = await _presetRoleRepository.GetAsync(
            o => o.Channel == channel && o.WhetherSuper == Whether.Yes.ToInt()
        );

        if (result.Success) return;

        throw new NotImplementedException();
    }

    private static List<IRoleItem> GetCustomRoleItemList(RoleGroup roleGroup)
    {
        var list = new List<IRoleItem>();

        try
        {
            list = (JsonConvert.DeserializeObject<List<IRoleItem>>(
                    roleGroup.CustomRoleCollection
                ) ?? new List<IRoleItem>())
                .ToListFilterNullable();
        }
        catch (Exception)
        {
            // ignored
        }

        return list;
    }

    private static IList<IRoleItem> GetPresetRoleItemList(RoleGroup roleGroup)
    {
        var list = new List<IRoleItem>();

        try
        {
            list = (JsonConvert.DeserializeObject<List<IRoleItem>>(
                    roleGroup.PresetRoleCollection
                ) ?? new List<IRoleItem>())
                .ToListFilterNullable();
        }
        catch (Exception)
        {
            // ignored
        }

        return list;
    }

    private async Task<IEnumerable<IAccessWay>> GetAccessWayPersistenceList(
        IEnumerable<CompetenceEntity> competenceEntities
    )
    {
        return await _accessWayRepository.SingleListAsync(
            o => o.GuidTag.In(competenceEntities.Select(c => c.GuidTag).ToArray())
        );
    }
}