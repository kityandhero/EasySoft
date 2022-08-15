using AutoFacTest.Interfaces;

namespace AutoFacTest.Implementations;

public class Simple : ISimple
{
    private int _value = 100;

    public int GetValue()
    {
        return _value;
    }
}