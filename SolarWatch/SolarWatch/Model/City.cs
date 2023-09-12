using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SolarWatch.Model;

public class City
{ 
    [Key]
    public int CityId { get; set; }
    
    //public int SunId { get; set; }
    public string Name { get; set; }
     public string Country { get; set; }
     public string State { get; set; }
     public double Lat { get; set; }
    public double Lon { get; set; }
    
    public SunSetSunRiseResponse SunRiseResponse { get; set; }
}