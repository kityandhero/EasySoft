namespace WebApplicationTest.Mapsters;

public class UserEntity
{
    public string Name { get; set; }

    public string Gender { get; set; }

    public int Age { get; set; }

    public string Address { get; set; }

    public UserEntity()
    {
        Name = "";
        Gender = "";
        Age = 0;
        Address = "";
    }
}