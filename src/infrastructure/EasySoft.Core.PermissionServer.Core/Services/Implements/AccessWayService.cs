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
    public async Task SaveAccessWayAsync(IAccessWayMessage accessWayMessage)
    {
        if (string.IsNullOrWhiteSpace(accessWayMessage.GuidTag))
        {
            return;
        }

        var resultGet = await _accessWayRepository.GetAsync(
            o => o.GuidTag == accessWayMessage.GuidTag
        );

        if (resultGet.Success)
        {
            return;
        }

        var accessWay = new AccessWay
        {
            Name = accessWayMessage.Name.ToLower(),
            GuidTag = accessWayMessage.GuidTag.ToLower(),
            RelativePath = accessWayMessage.RelativePath.ToLower(),
            Expand = accessWayMessage.Expand.ToLower(),
            Group = accessWayMessage.Group.ToLower(),
            TriggerChannel = accessWayMessage.TriggerChannel,
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