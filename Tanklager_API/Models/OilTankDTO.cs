using System.Text.Json.Serialization;

namespace Tanklager_API.Models;

public class OilTankDTO
{
    public int id { get;set; }
    public string name { get; set; }
    public  int capacity { get; set; }
    
}