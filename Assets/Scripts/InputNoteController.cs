using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputNoteController : MonoBehaviour
{
    float[] pressTimeCount;
    int[] pressId;
    bool[] pressed;
    [SerializeField]
    float time;

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
            pressed = new bool[ChartEntity.model.Chart.Notes.Count];
        }
        time = Time.timeSinceLevelLoad;
        var keyA = Input.GetKey(KeyCode.A);
        var keyS = Input.GetKey(KeyCode.S);
        var keyD = Input.GetKey(KeyCode.D);
        var keySpace = Input.GetKey(KeyCode.Space);
        var keyJ = Input.GetKey(KeyCode.J);
        var keyK = Input.GetKey(KeyCode.K);
        var keyL = Input.GetKey(KeyCode.L);
        for (int i = 0; i < ChartEntity.model.Chart.Notes.Count; i++)
        {
            if (keyA && ChartEntity.model.Chart.Notes[i].NotePosition == 1
                || keyS && ChartEntity.model.Chart.Notes[i].NotePosition == 2
                || keyD && ChartEntity.model.Chart.Notes[i].NotePosition == 3
                || keySpace && ChartEntity.model.Chart.Notes[i].NotePosition == 4
                || keyJ && ChartEntity.model.Chart.Notes[i].NotePosition == 5
                || keyK && ChartEntity.model.Chart.Notes[i].NotePosition == 6
                || keyL && ChartEntity.model.Chart.Notes[i].NotePosition == 7)
            {
                var pressDistanceTime = Math.Abs(ChartEntity.model.Chart.Notes[i].NoteTime - time);
                if (pressDistanceTime < pressTimeCount[ChartEntity.model.Chart.Notes[i].NotePosition - 1] && !pressed[ChartEntity.model.Chart.Notes[i].NoteId - 1])
                {
                    pressTimeCount[ChartEntity.model.Chart.Notes[i].NotePosition - 1] = pressDistanceTime;
                    pressId[ChartEntity.model.Chart.Notes[i].NotePosition - 1] = ChartEntity.model.Chart.Notes[i].NoteId;
                    pressed[ChartEntity.model.Chart.Notes[i].NoteId - 1] = true;
                    var noteObject = GameObject.Find(ChartEntity.model.Chart.Notes[i].NoteId.ToString());
                    if (noteObject != null)
                    {
                        var gradeObjectsCount = GameObject.Find("Grade").transform.childCount;
                        for (int j = 0; j< gradeObjectsCount; j++)
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
        }
    }
}