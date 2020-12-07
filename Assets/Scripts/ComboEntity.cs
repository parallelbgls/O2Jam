using UnityEditor;
using UnityEngine;

public class ComboEntity
{
    private static int combo = 0;

    public static int AddCombo()
    {
        combo += 1;
        var comboTextObject = GameObject.FindGameObjectWithTag("Combo");
        comboTextObject.GetComponent<SetCombo>().SetComboText(combo);
        return combo;
    }

    public static int ClearCombo()
    {
        combo = 0;
        var comboTextObject = GameObject.FindGameObjectWithTag("Combo");
        comboTextObject.GetComponent<SetCombo>().SetComboText(combo);
        return combo;
    }
}