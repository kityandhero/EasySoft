using AutoFacTest.Interfaces;

namespace AutoFacTest.Implements;

public class Simple : ISimple
{
    private int _value = 100;

    public int GetValue()
    {
        return _value;
    }
}