using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Core.PermissionServer.Core.Extensions;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Services.Implements;

/// <summary>
/// PermissionService
/// </summary>
public class PermissionRpcService : IPermissionRpcService
{
    private readonly IRepository<RoleGroup> _roleGroupRepository;

    private readonly IRepository<PresetRole> _presetRoleRepository;

    private readonly IRepository<CustomRole> _customRoleRepository;

    private readonly IRepository<AccessWay> _accessWayRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="customRoleRepository"></param>
    /// <param name="roleGroupRepository"></param>
    /// <param name="presetRoleRepository"></param>
    /// <param name="accessWayRepository"></param>
    public PermissionRpcService(
        IRepository<RoleGroup> roleGroupRepository,
        IRepository<PresetRole> presetRoleRepository,
        IRepository<CustomRole> customRoleRepository,
        IRepository<AccessWay> accessWayRepository
    )
    {
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
    public async Task MaintainSuper(int channel)
    {
        var resultGetPresetRole = await _presetRoleRepository.GetAsync(
            o => o.Channel == channel && o.WhetherSuper == Whether.Yes.ToInt()
        );

        PresetRole superRole;

        if (resultGetPresetRole.Success)
        {
            superRole = resultGetPresetRole.Data ?? throw new UnknownException("preset role is null, it is not allow.");
        }
        else
        {
            superRole = new PresetRole
            {
                Name = ConstCollection.SuperRoleName,
                WhetherSuper = Whether.Yes.ToInt(),
                Channel = channel
            };

            await _presetRoleRepository.AddAsync(superRole);
        }

        RoleGroup roleGroup;

        var resultGetRoleGroup = await _roleGroupRepository.GetAsync(
            o => o.Channel == channel && o.WhetherSuper == Whether.Yes.ToInt()
        );

        if (resultGetRoleGroup.Success)
        {
            roleGroup = resultGetRoleGroup.Data ?? throw new UnknownException("role group is null, it is not allow.");
        }
        else
        {
            roleGroup = new RoleGroup
            {
                Name = ConstCollection.SuperRoleGroupName,
                PresetRoleCollection = JsonConvert.SerializeObject(
                    new List<RoleItem>
                    {
                        new()
                        {
                            Id = superRole.Id
                        }
                    }
                ),
                WhetherSuper = Whether.Yes.ToInt(),
                Channel = channel
            };

            await _roleGroupRepository.AddAsync(roleGroup);
        }
    }

    #region private static methods

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

    #endregion
}