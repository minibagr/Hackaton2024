using System;
using TMPro;
using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public TMP_Text text;
    public String number;

    public void ChangeText()
    {
        text.text += number;
    }
}
