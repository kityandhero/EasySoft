using EasySoft.Core.Data.Repositories;
using EasySoft.IdGenerator.Assists;
using EasySoft.Simple.WebLog.Application.Contracts.DataTransferObjects;
using EasySoft.Simple.WebLog.Application.Contracts.ExtensionMethods;
using EasySoft.Simple.WebLog.Application.Contracts.Services;
using EasySoft.Simple.Shared.Events;
using EasySoft.Simple.WebLog.Domain.Aggregates.AuthorAggregate;
using EasySoft.UtilityTools.Standard.Enums;
using EasySoft.UtilityTools.Standard.Result;

namespace EasySoft.Simple.WebLog.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IRepository<Author> _authorRepository;

    public AuthorService(IRepository<Author> repository)
    {
        _authorRepository = repository;
    }

    public async Task<ExecutiveResult<AuthorDto>> GetAuthorDtoSync(int authorId)
    {
        var result = await _authorRepository.GetAsync(authorId);

        if (!result.Success) return new ExecutiveResult<AuthorDto>(result.Code);

        if (result.Data != null)
            return new ExecutiveResult<AuthorDto>(result.Code)
            {
                Data = result.Data.ToAuthorDto()
            };

        return new ExecutiveResult<AuthorDto>(ReturnCode.NoData);
    }

    public async Task<ExecutiveResult<Author>> UpdateFirstAuthor()
    {
        var enumerable = await _authorRepository.SingleListAsync();

        var list = enumerable.ToList();

        if (!list.Any()) return new ExecutiveResult<Author>(ReturnCode.NoData);

        var first = list.First();

        // var result = await _authorRepository.GetAsync(first.Id);

        first.Pseudonym = Guid.NewGuid().ToString();

        var resultUpdate = await _authorRepository.UpdateAsync(first);

        if (resultUpdate.Success)
        {
            //发布领域事件，通知客户中心扣款(Demo是从余额中扣款)
            var eventId = IdentifierAssist.Create();
            var eventData = new AuthorUpdateEvent.EventData
            {
                AuthorId = first.Id
            };

            const string eventSource = nameof(UpdateFirstAuthor);

            await first.EventPublisher.Value.PublishAsync(new AuthorUpdateEvent(eventId, eventData, eventSource));

            return new ExecutiveResult<Author>(ReturnCode.Ok)
            {
                Data = resultUpdate.Data
            };
        }

        return resultUpdate;
    }
}