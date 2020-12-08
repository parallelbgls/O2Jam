using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPressButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var pressObject = GameObject.Find("Press");
        for (int i=0; i< pressObject.transform.childCount; i++)
        {
            pressObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var pressObject = GameObject.Find("Press");
        var keyADown = Input.GetKeyDown(KeyCode.A);
        var keySDown = Input.GetKeyDown(KeyCode.S);
        var keyDDown = Input.GetKeyDown(KeyCode.D);
        var keySpaceDown = Input.GetKeyDown(KeyCode.Space);
        var keyJDown = Input.GetKeyDown(KeyCode.J);
        var keyKDown = Input.GetKeyDown(KeyCode.K);
        var keyLDown = Input.GetKeyDown(KeyCode.L);
        var keyAUp = Input.GetKeyUp(KeyCode.A);
        var keySUp = Input.GetKeyUp(KeyCode.S);
        var keyDUp = Input.GetKeyUp(KeyCode.D);
        var keySpaceUp = Input.GetKeyUp(KeyCode.Space);
        var keyJUp = Input.GetKeyUp(KeyCode.J);
        var keyKUp = Input.GetKeyUp(KeyCode.K);
        var keyLUp = Input.GetKeyUp(KeyCode.L);
        if (keyADown)
        {
            var press1Object = pressObject.transform.Find("KeyPress1");
            press1Object.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (keySDown)
        {
            var press2Object = pressObject.transform.Find("KeyPress2");
            press2Object.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (keyDDown)
        {
            var press3Object = pressObject.transform.Find("KeyPress3");
            press3Object.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (keySpaceDown)
        {
            var press4Object = pressObject.transform.Find("KeyPress4");
            press4Object.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (keyJDown)
        {
            var press5Object = pressObject.transform.Find("KeyPress5");
            press5Object.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (keyKDown)
        {
            var press6Object = pressObject.transform.Find("KeyPress6");
            press6Object.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (keyLDown)
        {
            var press7Object = pressObject.transform.Find("KeyPress7");
            press7Object.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (keyAUp)
        {
            var press1Object = pressObject.transform.Find("KeyPress1");
            press1Object.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (keySUp)
        {
            var press2Object = pressObject.transform.Find("KeyPress2");
            press2Object.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (keyDUp)
        {
            var press3Object = pressObject.transform.Find("KeyPress3");
            press3Object.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (keySpaceUp)
        {
            var press4Object = pressObject.transform.Find("KeyPress4");
            press4Object.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (keyJUp)
        {
            var press5Object = pressObject.transform.Find("KeyPress5");
            press5Object.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (keyKUp)
        {
            var press6Object = pressObject.transform.Find("KeyPress6");
            press6Object.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (keyLUp)
        {
            var press7Object = pressObject.transform.Find("KeyPress7");
            press7Object.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}