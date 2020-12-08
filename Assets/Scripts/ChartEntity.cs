using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteModel
{
    public int NoteId { get; set; }
    public int NotePosition { get; set; }
    public float NoteTime { get; set; }
    public float NoteLeast { get; set; }
}

public class ChartModel
{
    public float Bpm { get; set; }
    public int Clap { get; set; }
    public IList<NoteModel> Notes { get; set; }
}


public class ChartEntity
{
    public static ChartModel model;
}
