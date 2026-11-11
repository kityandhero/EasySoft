using EasySoft.Core.PermissionServer.Core.DataTransferObjects;
using EasySoft.Core.PermissionServer.Core.Entities;
using EasySoft.Core.PermissionServer.Core.Extensions;
using EasySoft.Core.PermissionServer.Core.Services.Interfaces;

namespace EasySoft.Core.PermissionServer.Core.Services.Implements;

/// <inheritdoc />
public class AccessWayService : IAccessWayService
{
    private readonly IRepository<AccessWay> _accessWayRepository;

    /// <summary>
    /// AccessWayService
    /// </summary>
    /// <param name="accessWayRepository"></param>
    public AccessWayService(
        IRepository<AccessWay> accessWayRepository
    )
    {
        _accessWayRepository = accessWayRepository;
    }

    /// <inheritdoc />
    public async Task<PageListResult<AccessWayDto>> PageListAsync(AccessWaySearchDto accessWaySearchDto)
    {
        var pageListResult = await _accessWayRepository.PageListAsync(
            accessWaySearchDto.PageNo,
            accessWaySearchDto.PageSize
        );

        return pageListResult.ToPageListResult(o => o.ToAccessWayDto());
    }

    /// <inheritdoc />
    public async Task SaveAccessWayAsync(IAccessWayTransitiveMessage accessWayTransitiveMessage)
    {
        if (string.IsNullOrWhiteSpace(accessWayTransitiveMessage.GuidTag))
        {
            return;
        }

        var resultGet =
            await _accessWayRepository.GetAsync(o => o.GuidTag == accessWayTransitiveMessage.GuidTag
            );

        if (resultGet.Success)
        {
            return;
        }

        var accessWay = new AccessWay
        {
            Name = accessWayTransitiveMessage.Name.ToLower(),
            GuidTag = accessWayTransitiveMessage.GuidTag.ToLower(),
            RelativePath = accessWayTransitiveMessage.RelativePath.ToLower(),
            Expand = accessWayTransitiveMessage.Expand.ToLower(),
            Group = accessWayTransitiveMessage.Group.ToLower(),
            TriggerChannel = accessWayTransitiveMessage.TriggerChannel,
            Channel = ChannelAssist.GetCurrentChannel().ToValue(),
            Status = 0,
            CreateTime = DateTimeOffset.Now.DateTime,
            ModifyTime = DateTimeOffset.Now.DateTime
        };

        var resultAdd = await _accessWayRepository.AddAsync(accessWay);

        if (!resultAdd.Success)
        {
            throw new UnknownException(resultAdd.Message);
        }
    }
}