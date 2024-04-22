namespace EasySoft.UtilityTools.Standard.Metas;

/// <summary>
/// 
/// </summary>
public abstract class EnumerationNumber : IComparable
{
    /// <summary>
    /// Value
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// EnumerationNumber
    /// </summary>
    /// <param name="value"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    protected EnumerationNumber(int value, string name = "", string description = "")
    {
        Value = value;
        Name = name;
        Description = description;
    }

    /// <summary>
    /// ToString
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Name;
    }

    /// <summary>
    /// GetAll
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> GetAll<T>() where T : EnumerationNumber
    {
        return typeof(T).GetFields(
                BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly
            )
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    /// <summary>
    /// Equals
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj is not EnumerationNumber otherValue)
        {
            return false;
        }

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    /// <summary>
    /// GetHashCode
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    /// CompareTo
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int CompareTo(object obj)
    {
        return Value.CompareTo(((EnumerationNumber)obj).Value);
    }

    /// <summary>
    /// equal
    /// </summary>
    /// <param name="competenceBefore"></param>
    /// <param name="competenceAfter"></param>
    /// <returns></returns>
    public static bool operator ==(EnumerationNumber competenceBefore, EnumerationNumber competenceAfter)
    {
        return competenceBefore.Value == competenceAfter.Value;
    }

    /// <summary>
    /// not equal
    /// </summary>
    /// <param name="competenceBefore"></param>
    /// <param name="competenceAfter"></param>
    /// <returns></returns>
    public static bool operator !=(EnumerationNumber competenceBefore, EnumerationNumber competenceAfter)
    {
        return competenceBefore.Value != competenceAfter.Value;
    }

    /// <summary>
    /// less than
    /// </summary>
    /// <param name="competenceBefore"></param>
    /// <param name="competenceAfter"></param>
    /// <returns></returns>
    public static bool operator <(EnumerationNumber competenceBefore, EnumerationNumber competenceAfter)
    {
        return competenceBefore.Value < competenceAfter.Value;
    }

    /// <summary>
    /// less equal than
    /// </summary>
    /// <param name="competenceBefore"></param>
    /// <param name="competenceAfter"></param>
    /// <returns></returns>
    public static bool operator <=(EnumerationNumber competenceBefore, EnumerationNumber competenceAfter)
    {
        return competenceBefore.Value <= competenceAfter.Value;
    }

    /// <summary>
    /// greater than
    /// </summary>
    /// <param name="competenceBefore"></param>
    /// <param name="competenceAfter"></param>
    /// <returns></returns>
    public static bool operator >(EnumerationNumber competenceBefore, EnumerationNumber competenceAfter)
    {
        return competenceBefore.Value > competenceAfter.Value;
    }

    /// <summary>
    /// greater equal than
    /// </summary>
    /// <param name="competenceBefore"></param>
    /// <param name="competenceAfter"></param>
    /// <returns></returns>
    public static bool operator >=(EnumerationNumber competenceBefore, EnumerationNumber competenceAfter)
    {
        return competenceBefore.Value >= competenceAfter.Value;
    }
}