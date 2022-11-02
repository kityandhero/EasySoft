using EasySoft.Core.EventBus;
using EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

namespace EasySoft.Core.Domain.Base.Entities.Interfaces;

public interface IAggregateRoot : IConcurrency
{
    Lazy<IEventPublisher> EventPublisher { get; }
}