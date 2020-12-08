using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputNoteController : MonoBehaviour
{
    float[] pressTimeCount;
    int[] pressId;
    bool[] pressed;
    bool[] up;
    int[] beatCount;
    [SerializeField]
    float time;

    void NoteLeastHold(NoteModel noteModel)
    {
        var beat = 60.0f / ChartEntity.model.Clap / ChartEntity.model.Bpm;
        var currentLeastTime = time - noteModel.NoteTime;
        if (currentLeastTime > 0)
        {
            var currentBeat = (int)Math.Floor(currentLeastTime / beat);
            while (currentBeat > beatCount[noteModel.NoteId-1] && currentBeat < noteModel.NoteLeast / beat)
            {
                beatCount[noteModel.NoteId - 1]++;
                var gradeObjectsCount = GameObject.Find("Grade").transform.childCount;
                for (int j = 0; j < gradeObjectsCount; j++)
                {
                    Destroy(GameObject.Find("Grade").transform.GetChild(j).gameObject);
                }
                var gradeObject = Instantiate(Resources.Load("Cool", typeof(GameObject)) as GameObject);
                gradeObject.transform.parent = GameObject.Find("Grade").transform;
                ComboEntity.AddCombo();
            }
        }
    }

    void NoteLeastUp(NoteModel noteModel)
    {
        bool keyUp = false;
        if (noteModel.NotePosition == 1)
        {
            keyUp = Input.GetKeyUp(KeyCode.A);
        }
        else if (noteModel.NotePosition == 2)
        {
            keyUp = Input.GetKeyUp(KeyCode.S);
        }
        else if (noteModel.NotePosition == 3)
        {
            keyUp = Input.GetKeyUp(KeyCode.D);
        }
        else if (noteModel.NotePosition == 4)
        {
            keyUp = Input.GetKeyUp(KeyCode.Space);
        }
        else if (noteModel.NotePosition == 5)
        {
            keyUp = Input.GetKeyUp(KeyCode.J);
        }
        else if (noteModel.NotePosition == 6)
        {
            keyUp = Input.GetKeyUp(KeyCode.K);
        }
        else if (noteModel.NotePosition == 7)
        {
            keyUp = Input.GetKeyUp(KeyCode.L);
        }
        if (keyUp)
        {
            var gradeObjectsCount = GameObject.Find("Grade").transform.childCount;
            for (int j = 0; j < gradeObjectsCount; j++)
            {
                Destroy(GameObject.Find("Grade").transform.GetChild(j).gameObject);
            }
            var upDistanceTime = Math.Abs(noteModel.NoteTime + noteModel.NoteLeast - time);
            if (upDistanceTime < 0.1f)
            {
                var gradeObject = Instantiate(Resources.Load("Cool", typeof(GameObject)) as GameObject);
                gradeObject.transform.parent = GameObject.Find("Grade").transform;
                ComboEntity.AddCombo();
            }
            else if (upDistanceTime < 0.15f)
            {
                var gradeObject = Instantiate(Resources.Load("Good", typeof(GameObject)) as GameObject);
                gradeObject.transform.parent = GameObject.Find("Grade").transform;
                ComboEntity.AddCombo();
            }
            else
            {
                var gradeObject = Instantiate(Resources.Load("Miss", typeof(GameObject)) as GameObject);
                gradeObject.transform.parent = GameObject.Find("Grade").transform;
                ComboEntity.ClearCombo();
            }
            up[noteModel.NoteId - 1] = true;
        }  
    }

    // Start is called before the first frame update
    void Start()
    {
        pressTimeCount = new float[7];
        pressId = new int[7];
        for (int i = 0; i < 7; i++)
        {
            pressTimeCount[i] = 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed == null && ChartEntity.model != null)
        {
            pressed = new bool[ChartEntity.model.Notes.Count];
        }
        if (up == null && ChartEntity.model != null)
        {
            up = new bool[ChartEntity.model.Notes.Count];
        }
        if (beatCount == null && ChartEntity.model != null)
        {
            beatCount = new int[ChartEntity.model.Notes.Count];
        }
        time = Time.timeSinceLevelLoad;
        var keyA  = Input.GetKeyDown(KeyCode.A);
        var keyS = Input.GetKeyDown(KeyCode.S);
        var keyD = Input.GetKeyDown(KeyCode.D);
        var keySpace = Input.GetKeyDown(KeyCode.Space);
        var keyJ = Input.GetKeyDown(KeyCode.J);
        var keyK = Input.GetKeyDown(KeyCode.K);
        var keyL = Input.GetKeyDown(KeyCode.L);
        for (int i = 0; i < ChartEntity.model.Notes.Count; i++)
        {
            if (keyA && ChartEntity.model.Notes[i].NotePosition == 1
                || keyS && ChartEntity.model.Notes[i].NotePosition == 2
                || keyD && ChartEntity.model.Notes[i].NotePosition == 3
                || keySpace && ChartEntity.model.Notes[i].NotePosition == 4
                || keyJ && ChartEntity.model.Notes[i].NotePosition == 5
                || keyK && ChartEntity.model.Notes[i].NotePosition == 6
                || keyL && ChartEntity.model.Notes[i].NotePosition == 7)
            {
                var pressDistanceTime = Math.Abs(ChartEntity.model.Notes[i].NoteTime - time);
                if (pressDistanceTime < pressTimeCount[ChartEntity.model.Notes[i].NotePosition - 1] && !pressed[ChartEntity.model.Notes[i].NoteId - 1])
                {
                    pressTimeCount[ChartEntity.model.Notes[i].NotePosition - 1] = pressDistanceTime;
                    pressId[ChartEntity.model.Notes[i].NotePosition - 1] = ChartEntity.model.Notes[i].NoteId;
                    pressed[ChartEntity.model.Notes[i].NoteId - 1] = true;
                    var noteObject = GameObject.Find(ChartEntity.model.Notes[i].NoteId.ToString());
                    if (noteObject != null)
                    {
                        var gradeObjectsCount = GameObject.Find("Grade").transform.childCount;
                        for (int j = 0; j < gradeObjectsCount; j++)
                        {
                            Destroy(GameObject.Find("Grade").transform.GetChild(j).gameObject);
                        }
                        if (pressDistanceTime < 0.1f)
                        {
                            var gradeObject = Instantiate(Resources.Load("Cool", typeof(GameObject)) as GameObject);
                            gradeObject.transform.parent = GameObject.Find("Grade").transform;
                            ComboEntity.AddCombo();

                        }
                        else if (pressDistanceTime < 0.15f)
                        {
                            var gradeObject = Instantiate(Resources.Load("Good", typeof(GameObject)) as GameObject);
                            gradeObject.transform.parent = GameObject.Find("Grade").transform;
                            ComboEntity.AddCombo();
                        }
                        else
                        {
                            var gradeObject = Instantiate(Resources.Load("Miss", typeof(GameObject)) as GameObject);
                            gradeObject.transform.parent = GameObject.Find("Grade").transform;
                            ComboEntity.ClearCombo();
                        }
                        Destroy(noteObject);
                    }
                }
            }
            if (ChartEntity.model.Notes[i].NoteLeast > 0.001f && pressed[ChartEntity.model.Notes[i].NoteId - 1] && !up[ChartEntity.model.Notes[i].NoteId - 1])
            {
                NoteLeastHold(ChartEntity.model.Notes[i]);
                NoteLeastUp(ChartEntity.model.Notes[i]);
            }
        }
    }
}