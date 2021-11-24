using Newtonsoft.Json;
using System.Collections.Generic;

public class Geometry
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("coordinates")]
    public float[] Coordinates { get; set; }
}

public class Feature
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("geometry")]
    public Geometry Geometry { get; set; }

    [JsonProperty("properties")]
    public Dictionary<string, object> Properties { get; set; }
}

public class GeoJson
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("features")]
    public Feature[] Features { get; set; }
}