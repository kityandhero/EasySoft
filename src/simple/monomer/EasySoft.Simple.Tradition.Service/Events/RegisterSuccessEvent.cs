namespace EasySoft.Simple.Tradition.Service.Events;

/// <summary>
/// RegisterSuccessEvent
/// </summary>
[Serializable]
public class RegisterSuccessEvent : EventEntity<RegisterSuccessEvent.EventData>
{
    /// <summary>
    /// RegisterSuccessEvent
    /// </summary>
    public RegisterSuccessEvent()
    {
    }

    /// <summary>
    /// RegisterSuccessEvent
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventData"></param>
    /// <param name="source"></param>
    public RegisterSuccessEvent(
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
        /// UserId
        /// </summary>
        public long UserId { get; set; }
    }
}