using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


public class NOTE_SPAWN_PROPERTY
{
    public static float PositionX(int x)
    {   
        switch (x)
        {
            case 1:
                return -1.523f;
            case 2:
                return -1.05f;
            case 3:
                return -0.59f;
            case 4:
                return -0.05f;
            case 5:
                return 0.532f;
            case 6:
                return 1f;
            case 7:
                return 1.473f;
            default:
                return -0.05f;
        } 
    }
    public static string PrefabName(int x)
    {
        switch (x)
        {
            case 1:
                return "White";
            case 2:
                return "Blue";
            case 3:
                return "White";
            case 4:
                return "Yellow";
            case 5:
                return "White";
            case 6:
                return "Blue";
            case 7:
                return "White";
            default:
                return "Yellow";
        }
    }
}

public class NoteSpawner : MonoBehaviour
{
    [SerializeField]
    private float time;
    bool[] notesSpawned;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        ChartEntity.model = JsonConvert.DeserializeObject<JsonModel>(File.ReadAllText(@"Assets" + Path.DirectorySeparatorChar + "Chart"+Path.DirectorySeparatorChar + "chart.json"));
        notesSpawned = new bool[ChartEntity.model.Chart.Notes.Count];
        for(int i = 0; i < notesSpawned.Length; i++)
        {
            notesSpawned[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.timeSinceLevelLoad;
        foreach (NoteModel note in ChartEntity.model.Chart.Notes)
        {
            if (notesSpawned[note.NoteId - 1] == false)
            {
                if (time > note.NoteTime - 2)
                {
                    notesSpawned[note.NoteId - 1] = true;
                    var resource = Resources.Load(NOTE_SPAWN_PROPERTY.PrefabName(note.NotePosition), typeof(GameObject)) as GameObject;
                    var noteInstance = Instantiate(resource, new Vector3(NOTE_SPAWN_PROPERTY.PositionX(note.NotePosition), 6.05f, -1), new Quaternion()) as GameObject;
                    noteInstance.name = note.NoteId.ToString();
                    var runer = noteInstance.GetComponent<NoteRuner>();
                    runer.DestinationTime = note.NoteTime;
                    runer.LeastTime = note.NoteLeast;
                }
            }
        }
    }
}
