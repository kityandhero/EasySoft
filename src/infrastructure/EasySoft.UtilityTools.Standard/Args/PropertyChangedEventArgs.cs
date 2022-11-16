namespace EasySoft.UtilityTools.Standard.Args;

/// <summary>
/// PropertyModifyEventArgs
/// </summary>
public class PropertyModifyEventArgs : EventArgs
{
    /// <summary>
    /// PropertyChangedEventArgs
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="oldValue">    </param>
    /// <param name="newValue">    </param>
    public PropertyModifyEventArgs(string propertyName, object oldValue, object newValue)
    {
        PropertyName = propertyName;
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    /// Cancel
    /// </summary>
    public bool Cancel { get; set; }

    /// <summary>
    /// PropertyName
    /// </summary>
    public string PropertyName { get; private set; }

    /// <summary>
    /// OldValue
    /// </summary>
    public object OldValue { get; private set; }

    /// <summary>
    /// NewValue
    /// </summary>
    public object NewValue { get; set; }
}