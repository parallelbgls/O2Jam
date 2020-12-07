using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteRuner : MonoBehaviour
{
    [SerializeField]
    private float time;
    public float DestinationTime;
    public float LeastTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.timeSinceLevelLoad;
        var percentage = (DestinationTime - time) / 2;
        var positionY = percentage * 9 - 3.95f;
        Vector3 position = this.GetComponent<Transform>().position;
        position.y = positionY;
        this.GetComponent<Transform>().position = position;
        if (position.y < - 4.5)
        {
            Destroy(gameObject);
            var gradeObjectsCount = GameObject.Find("Grade").transform.childCount;
            for (int j = 0; j < gradeObjectsCount; j++)
            {
                Destroy(GameObject.Find("Grade").transform.GetChild(j).gameObject);
            }
            var gradeObject = Instantiate(Resources.Load("Miss", typeof(GameObject)) as GameObject);
            gradeObject.transform.parent = GameObject.Find("Grade").transform;
            ComboEntity.ClearCombo();
        }
    }
}
