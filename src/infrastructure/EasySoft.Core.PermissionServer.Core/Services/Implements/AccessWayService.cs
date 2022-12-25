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
    public AccessWayService(IRepository<AccessWay> accessWayRepository
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
    public async Task SaveAccessWayAsync(AccessWayExchange accessWayExchange)
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
}