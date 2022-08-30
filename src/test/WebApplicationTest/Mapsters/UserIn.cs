namespace WebApplicationTest.Mapsters;

public class UserIn
{
    public string Name { get; set; }

    public string Gender { get; set; }

    public int Age { get; set; }

    public UserIn()
    {
        Name = "";
        Gender = "";
        Age = 0;
    }
}