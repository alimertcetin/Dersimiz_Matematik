using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Door_Status))]
[DisallowMultipleComponent]
public class DoorKeycard_Management : MonoBehaviour, ISaveable
{
    private Door_Notification DoorNotification = default;

    public EventHandler AllKeycardsRemoved;

    [SerializeField]
    private List<Door_and_Keycard_Level> gerekenKeycardlar = null;
    public List<Door_and_Keycard_Level> GerekenKeycardlar { get => gerekenKeycardlar; set => gerekenKeycardlar = value; }

    private Door_Is_Locked Door_Is_Locked_Script;
    private PlayerInventory inventory;

    private bool keycardsAreRemoved;
    public bool KeycardsAreRemoved { get => keycardsAreRemoved; set => keycardsAreRemoved = value; }
    private bool yesil;
    private bool sari;
    private bool kirmizi;
    private bool triggered;
    private bool doorOpened;
    public bool DoorOpened { get => doorOpened; set => doorOpened = value; }

    private const string WARNING_TEXT = "Gereken Keycardlar olmadan bu kapıyı açamazsın.";
    private const string YESIL_RENKLIYAZDIR = "<b><color=green>Yeşil</color></b>";
    private const string SARI_RENKLIYAZDIR = "<b><color=yellow>Sarı</color></b>";
    private const string KIRMIZI_RENKLIYAZDIR = "<b><color=red>Kırmızı</color></b>";

    private void OnEnable()
    {
        InputManager.PlayerControls.Gameplay.Interact.performed += Interact_performed;
    }

    private void OnDisable()
    {
        InputManager.PlayerControls.Gameplay.Interact.performed -= Interact_performed;
    }

    private void Awake()
    {
        DoorNotification = GetComponent<Door_Notification>();

        inventory = FindObjectOfType<PlayerInventory>();
        Door_Is_Locked_Script = GetComponent<Door_Is_Locked>();
        if (Door_Is_Locked_Script != null)
        {
            Door_Is_Locked_Script.KapiAcildi += Door_Is_Locked_Kapi_Acildi;
        }
    }

    private void Update()
    {
        if (gerekenKeycardlar == null || gerekenKeycardlar.Count == 0 && !keycardsAreRemoved)
        {
            AllKeycardsRemoved?.Invoke(this, EventArgs.Empty);
            keycardsAreRemoved = true;
        }
    }

    private void Door_Is_Locked_Kapi_Acildi(Door_Is_Locked door)
    {
        Door_Is_Locked Door = door;
        doorOpened = Door.DoorLocked ? false : true;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if (triggered && !keycardsAreRemoved)
        {
            if (doorOpened || Door_Is_Locked_Script == null)
            {
                RemoveKeycardFromDoor();
            }
        }
    }

    private void RemoveKeycardFromDoor()
    {
        GetKeycardName(out Door_and_Keycard_Level _keycard, out string _typeName);

        if (inventory.KeycardCikar_Success(_typeName))
        {
            gerekenKeycardlar.Remove(_keycard);
        }
        else
        {
            DoorNotification.Warn(WARNING_TEXT);
        }
    }

    private void GetKeycardName(out Door_and_Keycard_Level KeycardType, out string TypeName)
    {
        KeycardType = Door_and_Keycard_Level.None;
        TypeName = "None";
        foreach (Door_and_Keycard_Level item in gerekenKeycardlar)
        {
            switch (item)
            {
                case Door_and_Keycard_Level.Yesil:
                    KeycardType = item;
                    TypeName = "green";
                    break;
                case Door_and_Keycard_Level.Sari:
                    KeycardType = item;
                    TypeName = "yellow";
                    break;
                case Door_and_Keycard_Level.Kirmizi:
                    KeycardType = item;
                    TypeName = "red";
                    break;
                default:
                    break;
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
        }
    }

    public string Door_Keycard_NotificationText()
    {
        string str = "";
        str = "Bu kapıyı açmak için ";
        int Yesil_count = CountKeycards(Door_and_Keycard_Level.Yesil);
        int Sari_count = CountKeycards(Door_and_Keycard_Level.Sari);
        int Kirmizi_count = CountKeycards(Door_and_Keycard_Level.Kirmizi);

        if (Yesil_count != 0)
        {
            str += $"{Yesil_count} tane {YESIL_RENKLIYAZDIR} ";
        }

        if (Sari_count != 0)
        {
            str += $"{Sari_count} tane {SARI_RENKLIYAZDIR} ";
        }

        if (Kirmizi_count != 0)
        {
            str += $"{Kirmizi_count} tane {KIRMIZI_RENKLIYAZDIR} ";
        }

        if (Yesil_count != 0 || Sari_count != 0 || Kirmizi_count != 0)
        {
            return str += "Keycard gerekli.";
        }
        else
        {
            return "";
        }
    }

    private int CountKeycards(Door_and_Keycard_Level _keycard)
    {
        int count = 0;
        if (_keycard == Door_and_Keycard_Level.Yesil)
        {
            foreach (Door_and_Keycard_Level item in gerekenKeycardlar)
            {
                if (item == Door_and_Keycard_Level.Yesil)
                {
                    count += 1;
                }
            }
        }
        else if (_keycard == Door_and_Keycard_Level.Sari)
        {
            foreach (Door_and_Keycard_Level item in gerekenKeycardlar)
            {
                if (item == Door_and_Keycard_Level.Sari)
                {
                    count += 1;
                }
            }
        }
        else if (_keycard == Door_and_Keycard_Level.Kirmizi)
        {
            foreach (Door_and_Keycard_Level item in gerekenKeycardlar)
            {
                if (item == Door_and_Keycard_Level.Kirmizi)
                {
                    count += 1;
                }
            }
        }

        return count;
    }

    #region -_- Save -_-

    public object CaptureState()
    {
        return new SaveData
        {
            _KeycardsAreRemoved = KeycardsAreRemoved,
            _DoorOpened = DoorOpened,
            _GerekenKeycardlar = GerekenKeycardlar
        };
    }

    public void RestoreState(object state)
    {
        SaveData saveData = (SaveData)state;
        KeycardsAreRemoved = saveData._KeycardsAreRemoved;
        DoorOpened = saveData._DoorOpened;
        GerekenKeycardlar = saveData._GerekenKeycardlar;
    }

    [System.Serializable]
    private struct SaveData
    {
        public bool _KeycardsAreRemoved;
        public bool _DoorOpened;
        public List<Door_and_Keycard_Level> _GerekenKeycardlar;
    }

    #endregion

}
