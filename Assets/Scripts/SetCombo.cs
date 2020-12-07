using UnityEngine;
using System.Collections;
using TMPro;

public class SetCombo : MonoBehaviour
{
    public void SetComboText(int combo)
    {
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        textmeshPro.SetText("{0}", combo);
    }
}
