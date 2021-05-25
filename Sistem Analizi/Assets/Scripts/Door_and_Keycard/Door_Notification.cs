using System;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class Door_Notification : MonoBehaviour
{
    [SerializeField] private StringEventChannelSO notificationChannel = default;
    [SerializeField] private StringEventChannelSO WarningUIChannel = default;

    private Door_Is_Locked doorIsLocked;
    private DoorKeycard_Management keycardManager;
    private Door_Animation doorAnimation;
    private bool triggered, doorLocked, keycardsAreRemoved;

    private void Awake()
    {
        doorAnimation = GetComponent<Door_Animation>();

        if (TryGetComponent<Door_Is_Locked>(out doorIsLocked))
        {
            doorIsLocked.KapiAcildi += doorLocked_KapiAcildi;
            doorLocked = doorIsLocked.DoorLocked;
        }

        if (TryGetComponent<DoorKeycard_Management>(out keycardManager))
        {
            keycardManager.AllKeycardsRemoved += KeycardsRemovedFromDoor;
            keycardsAreRemoved = keycardManager.KeycardsAreRemoved;
        }
        else
        {
            keycardsAreRemoved = true;
        }
    }

    private void Update()
    {
        if (triggered)
        {
            if (keycardManager != null)
            {
                keycardsAreRemoved = keycardManager.KeycardsAreRemoved;
            }

            if (doorLocked)
            {
                notificationChannel.RaiseEvent("Kapı kilitli. Şifreyi görmek için F bas.");
            }
            else if (!doorLocked && !keycardsAreRemoved)
            {
                notificationChannel.RaiseEvent(keycardManager.Door_Keycard_NotificationText());
            }
            else if (!doorLocked && keycardsAreRemoved)
            {
                notificationChannel.RaiseEvent(GiveInfo_DoorIsOpen_OrNot(doorAnimation.DoorIsOpen));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = false;
            notificationChannel.RaiseEvent("", false);
        }
    }

    private void KeycardsRemovedFromDoor(object sender, EventArgs e)
    {
        keycardManager = null;
        keycardsAreRemoved = true;
    }

    private void doorLocked_KapiAcildi(Door_Is_Locked door)
    {
        doorIsLocked = null;
        doorLocked = false;
    }

    private string GiveInfo_DoorIsOpen_OrNot(bool isDoorOpen)
    {
        if (isDoorOpen)
        {
            return "Press " + InputManager.PlayerControls.Gameplay.Interact.name + " to Close";
        }
        else
        {
            return "Press " + InputManager.PlayerControls.Gameplay.Interact.name + " to Open";
        }
    }

    public void Warn(string text, bool value = true)
    {
        WarningUIChannel.RaiseEvent(text, value);
    }
}
