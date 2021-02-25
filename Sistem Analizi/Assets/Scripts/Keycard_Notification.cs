using UnityEngine;
using TMPro;
using System;

public class Keycard_Notification : MonoBehaviour
{
    [SerializeField] GameObject NotificationCanvas = null;
    TMP_Text OnTriggerText;
    Keycard_Script _keycardScript;
    string KeycardTuru = "";

    private void Awake()
    {
        if (NotificationCanvas != null)
            OnTriggerText = NotificationCanvas.GetComponentInChildren<TMP_Text>();
        else
            Debug.LogWarning("Canvas atanmamış. " + this.name + " bildirim veremeyecek.");

        NotificationCanvas.SetActive(false);
        _keycardScript = GetComponent<Keycard_Script>();
        _keycardScript.KeycardCollected += _keycardScript_KeycardCollected;
        SetKeycardText();
    }

    private void _keycardScript_KeycardCollected(object sender, EventArgs e)
    {
        if (NotificationCanvas != null)
            NotificationCanvas.SetActive(false);
    }

    private void SetKeycardText()
    {
        if (_keycardScript.Keycard == Door_and_Keycard_Level.Yesil) KeycardTuru = "<color=green><b>Yeşil</b></color>";
        else if (_keycardScript.Keycard == Door_and_Keycard_Level.Sari) KeycardTuru = "<color=yellow><b>Sarı</b></color>";
        else if (_keycardScript.Keycard == Door_and_Keycard_Level.Kirmizi) KeycardTuru = "<color=red><b>Kırmızı</b></color>";
        else Debug.LogWarning(this.name + " Keycard türüne ulaşamadı.");

        OnTriggerText.text = KeycardTuru + " Keycard'ı toplamak için F'ye bas.";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetKeycardText();
            if (NotificationCanvas != null)
                NotificationCanvas.SetActive(true);
            else Debug.LogWarning("Canvas bulunamadı.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (NotificationCanvas != null)
                NotificationCanvas.SetActive(false);
        }
    }

    void OnDisable()
    {
        if (NotificationCanvas != null)
            NotificationCanvas.SetActive(false);
    }
}
