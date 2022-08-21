namespace EasySoft.Core.IdentityVerification.Tokens;

public class Token : IToken
{
    private readonly string _token;

    public Token(string token)
    {
        _token = token;
    }

    public string GetValue()
    {
        return _token;
    }
}