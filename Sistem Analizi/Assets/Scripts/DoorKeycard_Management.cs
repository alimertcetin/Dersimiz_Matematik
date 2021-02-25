using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Door_Status))]
public class DoorKeycard_Management : MonoBehaviour, ISaveable
{
    public System.EventHandler AllKeycardsRemoved;

    [SerializeField] List<Door_and_Keycard_Level> _gerekenKeycardlar = null;
    public List<Door_and_Keycard_Level> GerekenKeycardlar { get => _gerekenKeycardlar; set => _gerekenKeycardlar = value; }
    bool _keycardsAreRemoved;
    public bool KeycardsAreRemoved { get => _keycardsAreRemoved; set => _keycardsAreRemoved = value; }

    Door_Is_Locked Door_Is_Locked_Script;
    instance_Player_Inventory inventory;
    bool yesil, sari, kirmizi, 
        _triggered, _doorOpened;
    public bool DoorOpened { get => _doorOpened; set => _doorOpened = value; }

    [Tooltip("Sadece 1 parent ve 1 child nesneden oluşan ve child nesnesi text olan Uyarı Gameobject'ini buraya yerleştirin.")]
    [SerializeField] GameObject UyariUI = null;
    [SerializeField] string txt_Uyari = "Gereken Keycardlar olmadan bu kapıyı açamazsın.";
    [SerializeField] float uyariSuresi = 2f;
    [ContextMenu("Uyarı Süresini Sıfırla")]
    void SureyiSifirla() => uyariSuresi = 2f;

    const string Yesil_renkliYazdir = "<b><color=green>Yeşil</color></b>";
    const string Sari_renkliYazdir = "<b><color=yellow>Sarı</color></b>";
    const string Kirmizi_renkliYazdir = "<b><color=red>Kırmızı</color></b>";

    void Awake()
    {
        Door_Is_Locked_Script = GetComponent<Door_Is_Locked>();
        if(Door_Is_Locked_Script != null)
            Door_Is_Locked_Script.KapiAcildi += Door_Is_Locked_Kapıi_Acildi;

        inventory = FindObjectOfType<instance_Player_Inventory>();
    }

    void Door_Is_Locked_Kapıi_Acildi(object sender, EventArgs e)
    {
        var Door = (Door_Is_Locked)sender;
        _doorOpened = Door.DoorLocked ? false : true;
    }
    private void Update()
    {
        if (_gerekenKeycardlar == null || _gerekenKeycardlar.Count == 0 && !_keycardsAreRemoved)
        {
            AllKeycardsRemoved?.Invoke(this, EventArgs.Empty);
            _keycardsAreRemoved = true;
        }
        if (_triggered && !_keycardsAreRemoved && Input.GetKeyDown(KeyCode.F))
        {
            if (_doorOpened || Door_Is_Locked_Script == null)
                RemoveKeycardFromDoor();
        }
    }

    private void RemoveKeycardFromDoor()
    {
        RemoveKeycardFromDoor2(out Door_and_Keycard_Level _keycard, out string _typeName);
        if (inventory.KeycardCikar_Success(_typeName))
            _gerekenKeycardlar.Remove(_keycard);
        else UyariVer(uyariSuresi, txt_Uyari);
    }

    private void RemoveKeycardFromDoor2(out Door_and_Keycard_Level KeycardType, out string TypeName)
    {
        KeycardType = Door_and_Keycard_Level.None;
        TypeName = "None";
        foreach (var item in _gerekenKeycardlar)
        {
            if (item == Door_and_Keycard_Level.Yesil)
            {
                KeycardType = item;
                TypeName = "green";
                break;
            }
            else if (item == Door_and_Keycard_Level.Sari)
            {
                KeycardType = item;
                TypeName = "yellow";
                break;
            }
            else if (item == Door_and_Keycard_Level.Kirmizi)
            {
                KeycardType = item;
                TypeName = "red";
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _triggered = true;
    }

    public string Door_Keycard_NotificationText()
    {
        string str = "";
        str = "Bu kapıyı açmak için ";
        var Yesil_count = CountKeycards(Door_and_Keycard_Level.Yesil);
        var Sari_count = CountKeycards(Door_and_Keycard_Level.Sari);
        var Kirmizi_count = CountKeycards(Door_and_Keycard_Level.Kirmizi);

        if (Yesil_count != 0)
            str += $"{Yesil_count} tane {Yesil_renkliYazdir} ";
        if (Sari_count != 0)
            str += $"{Sari_count} tane {Sari_renkliYazdir} ";
        if (Kirmizi_count != 0)
            str += $"{Kirmizi_count} tane {Kirmizi_renkliYazdir} ";

        if (Yesil_count != 0 || Sari_count != 0 || Kirmizi_count != 0)
            return str += "Keycard gerekli.";
        else
            return "";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) _triggered = false;
    }

    private int CountKeycards(Door_and_Keycard_Level _keycard)
    {
        int count = 0;
        if (_keycard == Door_and_Keycard_Level.Yesil)
        {
            foreach (var item in _gerekenKeycardlar)
            {
                if (item == Door_and_Keycard_Level.Yesil) count += 1;
            }
        }
        else if (_keycard == Door_and_Keycard_Level.Sari)
        {
            foreach (var item in _gerekenKeycardlar)
            {
                if (item == Door_and_Keycard_Level.Sari) count += 1;
            }
        }
        else if (_keycard == Door_and_Keycard_Level.Kirmizi)
        {
            foreach (var item in _gerekenKeycardlar)
            {
                if (item == Door_and_Keycard_Level.Kirmizi) count += 1;
            }
        }

        return count;
    }

    void UyariVer(float second, string text)
    {
        Uyari_Ekrani_Management.instance.UyariVer(second, text);
    }

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
        var saveData = (SaveData)state;
        KeycardsAreRemoved = saveData._KeycardsAreRemoved;
        DoorOpened = saveData._DoorOpened;
        GerekenKeycardlar = saveData._GerekenKeycardlar;
    }

    [System.Serializable]
    struct SaveData
    {
        public bool _KeycardsAreRemoved;
        public bool _DoorOpened;
        public List<Door_and_Keycard_Level> _GerekenKeycardlar;
    }

}
