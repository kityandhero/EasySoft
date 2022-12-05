using EasySoft.Simple.Tradition.Data.Entities;
using EasySoft.Simple.Tradition.Service.Services.Interfaces;

namespace EasySoft.Simple.Tradition.Service.Services.Implementations;

/// <summary>
/// SecurityService
/// </summary>
public class SecurityService : ISecurityService
{
    private readonly IEventPublisher _eventPublisher;

    private readonly IRepository<User> _userRepository;

    private readonly IRepository<RoleGroup> _roleGroupRepository;

    private readonly IRepository<PresetRole> _presetRoleRepository;

    private readonly IRepository<CustomRole> _customRoleRepository;

    private readonly IRepository<AccessWay> _accessWayRepository;

    /// <summary>
    /// UserService
    /// </summary>
    /// <param name="eventPublisher"></param>
    /// <param name="userRepository"></param>
    /// <param name="customRoleRepository"></param>
    /// <param name="roleGroupRepository"></param>
    /// <param name="presetRoleRepository"></param>
    /// <param name="accessWayRepository"></param>
    public SecurityService(
        IEventPublisher eventPublisher,
        IRepository<User> userRepository,
        IRepository<RoleGroup> roleGroupRepository,
        IRepository<PresetRole> presetRoleRepository,
        IRepository<CustomRole> customRoleRepository,
        IRepository<AccessWay> accessWayRepository
    )
    {
        _eventPublisher = eventPublisher;

        _userRepository = userRepository;
        _roleGroupRepository = roleGroupRepository;
        _presetRoleRepository = presetRoleRepository;
        _customRoleRepository = customRoleRepository;
        _accessWayRepository = accessWayRepository;
    }

    /// <inheritdoc />
    public async Task<List<CompetenceEntity>> GetCompetenceEntityCollectionAsync(long userId)
    {
        var ceList = new List<CompetenceEntity>();

        var userResult = await _userRepository.GetAsync(userId);

        if (!userResult.Success || userResult.Data == null) return ceList;

        var user = userResult.Data;

        if (user.RoleGroup == null) return ceList;

        var roelGroupResult = await _roleGroupRepository.GetAsync(user.RoleGroup.Id);

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