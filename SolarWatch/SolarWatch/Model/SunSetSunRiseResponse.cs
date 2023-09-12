using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SolarWatch.Model;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class SunSetSunRiseResponse
{
    [Key]
    public int SunId { get; set; }
    
    public int CityId { get; set; }
    public string Sunrise { get; set; }
    
    public string Sunset { get; set; }
    
    [JsonIgnore]
    public City city { get; set; }
    
}