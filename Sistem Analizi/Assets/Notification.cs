using System;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    private TMP_Text txt;

    private void Awake()
    {
        txt = GetComponentInChildren<TMP_Text>();
    }

    public void SetText(string text)
    {
        txt.text = text;
    }
}
