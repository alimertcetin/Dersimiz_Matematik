using UnityEngine;
using TMPro;
using System;

public class Door_Notification : MonoBehaviour
{
    [SerializeField] GameObject NotificationCanvas = null;
    TMP_Text OnTriggerText;

    Door_Is_Locked doorIsLocked;
    DoorKeycard_Management keycardManager;
    Door_Animation doorAnimation;
    bool triggered, _doorLocked, _keycardsAreRemoved;
    

    private void Awake()
    {
        if (NotificationCanvas != null)
            OnTriggerText = NotificationCanvas.GetComponentInChildren<TMP_Text>();
        else
            Debug.LogWarning("Canvas atanmamış veya canvas içinde text yok. " + this.name + " bildirim veremeyecek.");

        NotificationCanvas.SetActive(false);
        TryGetComponent<Door_Is_Locked>(out doorIsLocked);
        TryGetComponent<DoorKeycard_Management>(out keycardManager);
        TryGetComponent<Door_Animation>(out doorAnimation);
        if (doorIsLocked != null)
        {
            doorIsLocked.KapiAcildi += doorLocked_KapiAcildi;
            _doorLocked = doorIsLocked.DoorLocked;
        }

        if (keycardManager != null)
        {
            keycardManager.AllKeycardsRemoved += KeycardsRemovedFromDoor;
            _keycardsAreRemoved = keycardManager.KeycardsAreRemoved;
        }
        else _keycardsAreRemoved = true;
    }

    private void KeycardsRemovedFromDoor(object sender, EventArgs e)
    {
        keycardManager = null;
        _keycardsAreRemoved = true;
    }

    private void doorLocked_KapiAcildi(object sender, EventArgs e)
    {
        doorIsLocked = null;
        _doorLocked = false;
    }

    private void Update()
    {
        if (triggered)
        {
            if (keycardManager != null) _keycardsAreRemoved = keycardManager.KeycardsAreRemoved;
            if (_doorLocked)
                OnTriggerText.text = "Kapı kilitli. Şifreyi görmek için F bas.";

            else if (!_doorLocked && !_keycardsAreRemoved)
                OnTriggerText.text = keycardManager.Door_Keycard_NotificationText();

            else if (!_doorLocked && _keycardsAreRemoved)
                OnTriggerText.text = GiveInfo_DoorIsOpen_OrNot(doorAnimation);
        }
    }

    string GiveInfo_DoorIsOpen_OrNot(Door_Animation doorAnim)
    {
        if (doorAnim.DoorIsOpen)
            return "Press F to Close";
        else
            return "Press F to Open";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NotificationCanvas.SetActive(true);
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NotificationCanvas.SetActive(false);
            triggered = false;
        }
    }
}
