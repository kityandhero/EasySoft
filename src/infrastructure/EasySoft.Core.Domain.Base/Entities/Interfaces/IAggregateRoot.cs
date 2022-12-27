using EasySoft.Core.Infrastructure.Entities.Interfaces;

namespace EasySoft.Core.Domain.Base.Entities.Interfaces;

public interface IAggregateRoot : IConcurrency
{
    Lazy<IEventPublisher> EventPublisher { get; }
}