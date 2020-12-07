using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[JsonObject("note")]
public class NoteModel
{
    [JsonProperty("note_id")]
    public int NoteId { get; set; }
    [JsonProperty("note_position")]
    public int NotePosition { get; set; }
    [JsonProperty("note_time")]
    public float NoteTime { get; set; }
    [JsonProperty("note_least")]
    public float NoteLeast { get; set; }
}

[JsonObject("chart")]
public class ChartModel
{
    [JsonProperty("note")]
    public IList<NoteModel> Notes { get; set; }
}

public class JsonModel
{
    [JsonProperty("chart")]
    public ChartModel Chart { get; set; }
}

public class ChartEntity
{
    public static JsonModel model;
}
