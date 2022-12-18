using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Core.PermissionServer.Core.Extensions;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;
using EasySoft.Core.PermissionVerification.Entities;
using EasySoft.UtilityTools.Standard.Entities;

namespace EasySoft.Core.PermissionServer.Core.Services.Implementations;

/// <summary>
/// PermissionService
/// </summary>
public class PermissionService : IPermissionService
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
    public PermissionService(
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

            async void GetCustomRole(RoleItem o)
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

            async void GetPresetRole(RoleItem o)
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
    public async Task<IList<AccessWayModel>> FindAccessWayModelAsync(string guidTag)
    {
        var result = await _accessWayRepository.GetAsync(
            o => o.GuidTag == guidTag
        );

        if (!result.Success || result.Data == null) return new List<AccessWayModel>();

        return new List<AccessWayModel>
        {
            result.Data.ToAccessWayModel()
        };
    }

    /// <inheritdoc />
    public async Task SaveAccessWayModelAsync(AccessWayExchange accessWayExchange)
    {
        if (string.IsNullOrWhiteSpace(accessWayExchange.GuidTag)) return;

        var resultGet = await _accessWayRepository.GetAsync(
            o => o.GuidTag == accessWayExchange.GuidTag
        );

        if (resultGet.Success) return;

        var accessWay = new AccessWay
        {
            Name = accessWayExchange.Name.ToLower(),
            GuidTag = accessWayExchange.GuidTag.ToLower(),
            RelativePath = accessWayExchange.RelativePath.ToLower(),
            Expand = accessWayExchange.Expand.ToLower(),
            Group = accessWayExchange.Group.ToLower(),
            Channel = accessWayExchange.Channel,
            Status = 0,
            Ip = accessWayExchange.Ip.ToLower(),
            CreateTime = DateTimeOffset.Now.DateTime,
            ModifyTime = DateTimeOffset.Now.DateTime
        };

        var resultAdd = await _accessWayRepository.AddAsync(accessWay);

        if (!resultAdd.Success) throw new UnknownException(resultAdd.Message);
    }

    private static List<RoleItem> GetCustomRoleItemList(RoleGroup roleGroup)
    {
        var list = new List<RoleItem>();

        try
        {
            list = (JsonConvert.DeserializeObject<List<RoleItem>>(
                    roleGroup.CustomRoleCollection
                ) ?? new List<RoleItem>())
                .ToListFilterNullable();
        }
        catch (Exception)
        {
            // ignored
        }

        return list;
    }

    private static List<RoleItem> GetPresetRoleItemList(RoleGroup roleGroup)
    {
        var list = new List<RoleItem>();

        try
        {
            list = (JsonConvert.DeserializeObject<List<RoleItem>>(
                    roleGroup.PresetRoleCollection
                ) ?? new List<RoleItem>())
                .ToListFilterNullable();
        }
        catch (Exception)
        {
            // ignored
        }

        return list;
    }

    private async Task<IEnumerable<IAccessWayPersistence>> GetAccessWayPersistenceList(
        IEnumerable<CompetenceEntity> competenceEntities
    )
    {
        return await _accessWayRepository.SingleListAsync(
            o => o.GuidTag.In(competenceEntities.Select(c => c.GuidTag).ToArray())
        );
    }
}