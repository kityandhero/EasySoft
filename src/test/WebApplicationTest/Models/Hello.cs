using System.ComponentModel.DataAnnotations;

namespace WebApplicationTest.Models;

public class Hello
{
    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public Hello()
    {
        Name = "";
    }
}