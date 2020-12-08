using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        ChartEntity.model = new ChartModel();
        ChartEntity.model.Notes = new List<NoteModel>();
        ChartEntity.model.Bpm = 180;
        ChartEntity.model.Clap = 4;
        using (var reader = new StreamReader(@"Assets" + Path.DirectorySeparatorChar + "Chart" + Path.DirectorySeparatorChar + "chart.txt"))
        {
            int i = 1;
            while (!reader.EndOfStream)
            {
                var noteMessage = reader.ReadLine();
                var messages = noteMessage.Split(',');
                var noteModel = new NoteModel();
                noteModel.NoteId = i;
                noteModel.NotePosition = int.Parse(messages[0]) / 73 + 1;
                if (messages[3]=="1" || messages[3]=="5")
                {
                    noteModel.NoteTime = int.Parse(messages[2]) / 1000f;
                    noteModel.NoteLeast = 0;
                }
                else if (messages[3]=="128")
                {
                    noteModel.NoteTime = int.Parse(messages[2]) / 1000f;
                    var noteEnd = int.Parse(messages[5].Split(':')[0]);
                    noteModel.NoteLeast = noteEnd / 1000f - noteModel.NoteTime;
                }
                ChartEntity.model.Notes.Add(noteModel);
                i++;
            }

        }
        notesSpawned = new bool[ChartEntity.model.Notes.Count];
        for(int i = 0; i < notesSpawned.Length; i++)
        {
            notesSpawned[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.timeSinceLevelLoad;
        foreach (NoteModel note in ChartEntity.model.Notes)
        {
            if (notesSpawned[note.NoteId - 1] == false)
            {
                if (time > note.NoteTime - 2)
                {
                    notesSpawned[note.NoteId - 1] = true;
                    var resourceNote = Resources.Load(NOTE_SPAWN_PROPERTY.PrefabName(note.NotePosition), typeof(GameObject)) as GameObject;
                    var noteInstance = Instantiate(resourceNote, new Vector3(NOTE_SPAWN_PROPERTY.PositionX(note.NotePosition), 6.051f, -1), new Quaternion());
                    noteInstance.name = note.NoteId.ToString();
                    noteInstance.tag = "note";
                    var runer = noteInstance.GetComponent<NoteRuner>();
                    runer.DestinationTime = note.NoteTime;
                    runer.LeastTime = 0;
                    runer.startY = 6.051f;
                    if (note.NoteLeast > 0.001f)
                    {
                        var resourceSquare = Resources.Load("Square" + NOTE_SPAWN_PROPERTY.PrefabName(note.NotePosition), typeof(GameObject)) as GameObject;
                        var squareInstance = Instantiate(resourceSquare, new Vector3(NOTE_SPAWN_PROPERTY.PositionX(note.NotePosition), 2.5f * note.NoteLeast + 6.051f, -1), new Quaternion());
                        squareInstance.transform.localScale = new Vector3(squareInstance.transform.localScale.x, squareInstance.transform.localScale.y * note.NoteLeast, squareInstance.transform.localScale.z);
                        squareInstance.name = "Square" + note.NoteId.ToString();
                        squareInstance.tag = "square";
                        var runerSqaure = squareInstance.GetComponent<NoteRuner>();
                        runerSqaure.DestinationTime = note.NoteTime + note.NoteLeast / 2;
                        runerSqaure.LeastTime = note.NoteLeast / 2;
                        runerSqaure.startY = 6.051f + 2.5f * note.NoteLeast;
                        var resourceNote2 = Resources.Load(NOTE_SPAWN_PROPERTY.PrefabName(note.NotePosition), typeof(GameObject)) as GameObject;
                        var noteInstance2 = Instantiate(resourceNote2, new Vector3(NOTE_SPAWN_PROPERTY.PositionX(note.NotePosition), 6.051f + 5 * note.NoteLeast, -1), new Quaternion());
                        noteInstance2.name = "End"+note.NoteId.ToString();
                        noteInstance2.tag = "note_end";
                        var runer2 = noteInstance2.GetComponent<NoteRuner>();
                        runer2.DestinationTime = note.NoteTime + note.NoteLeast;
                        runer2.LeastTime = note.NoteLeast;
                        runer2.startY = 6.051f + 5 * note.NoteLeast;
                    }
                }
            }
        }
    }
}
