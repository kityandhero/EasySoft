using EasySoft.Core.EventBus.Events;

namespace EasySoft.Simple.DomainDrivenDesign.Shared.Events;

/// <summary>
/// 作者信息更新
/// </summary>
[Serializable]
public class AuthorUpdateEvent : EventEntity<AuthorUpdateEvent.EventData>
{
    /// <summary>
    /// AuthorUpdateEvent
    /// </summary>
    public AuthorUpdateEvent()
    {
    }

    /// <summary>
    /// AuthorUpdateEvent
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventData"></param>
    /// <param name="source"></param>
    public AuthorUpdateEvent(
        long id,
        EventData eventData,
        string source
    ) : base(id, eventData, source)
    {
    }

    /// <summary>
    /// EventData
    /// </summary>
    public class EventData
    {
        /// <summary>
        /// AuthorId
        /// </summary>
        public long AuthorId { get; set; }
    }
}