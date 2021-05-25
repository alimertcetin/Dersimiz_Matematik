using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableEntity))]
public class PlayerInventory : MonoBehaviour, ISaveable
{
    [SerializeField] private VoidEventChannelSO OnInventoryLoaded;

    instance_TxtManager txt_Manager_Info;
    [SerializeField] int rakam_0 = 0, rakam_1 = 0, rakam_2 = 0, rakam_3 = 0, rakam_4 = 0,
                         rakam_5 = 0, rakam_6 = 0, rakam_7 = 0, rakam_8 = 0, rakam_9 = 0;

    public int Rakam_0 { get => rakam_0; private set => rakam_0 = value; }
    public int Rakam_1 { get => rakam_1; private set => rakam_1 = value; }
    public int Rakam_2 { get => rakam_2; private set => rakam_2 = value; }
    public int Rakam_3 { get => rakam_3; private set => rakam_3 = value; }
    public int Rakam_4 { get => rakam_4; private set => rakam_4 = value; }
    public int Rakam_5 { get => rakam_5; private set => rakam_5 = value; }
    public int Rakam_6 { get => rakam_6; private set => rakam_6 = value; }
    public int Rakam_7 { get => rakam_7; private set => rakam_7 = value; }
    public int Rakam_8 { get => rakam_8; private set => rakam_8 = value; }
    public int Rakam_9 { get => rakam_9; private set => rakam_9 = value; }

    public int _yesilKeycard { get; private set; }
    public int _sariKeycard { get; private set; }
    public int _kirmiziKeycard { get; private set; }

    [SerializeField] int YesilKeycard = 0;
    [SerializeField] int sariKeycard = 0;
    [SerializeField] int KirmiziKeycard = 0;
    [SerializeField] private int _inventory_Capacity = 5;
    public int Inventory_Capacity { get => _inventory_Capacity; private set => _inventory_Capacity = value; }

    private void Awake()
    {
        _yesilKeycard = YesilKeycard;
        _sariKeycard = sariKeycard;
        _kirmiziKeycard = KirmiziKeycard;
        txt_Manager_Info = FindObjectOfType<instance_TxtManager>();
        OnInventoryLoaded.RaiseEvent();
    }

    /// <summary>
    /// Keycard envantere eklendiyse true döndürür.
    /// </summary>
    public bool KeycardEkle(Door_and_Keycard_Level keycard)
    {
        if (keycard == Door_and_Keycard_Level.Yesil)
        {
            _yesilKeycard++;
            txt_Manager_Info.SetKeycardChildTexts();
            return true;
        }
        else if (keycard == Door_and_Keycard_Level.Sari)
        {
            _sariKeycard++;
            txt_Manager_Info.SetKeycardChildTexts();
            return true;
        }
        else if (keycard == Door_and_Keycard_Level.Kirmizi)
        {
            _kirmiziKeycard++;
            txt_Manager_Info.SetKeycardChildTexts();
            return true;
        }
        else
        {
            Debug.LogWarning("Keycard envantere eklenirken hata oluştu.");
            return false;
        }

    }

    /// <summary>
    /// Keycard'ı envanterden kaldırmayı dener. Başarılıysa true döndürür.
    /// </summary>
    /// <param name="KeycardColor">green, red, yellow</param>
    public bool KeycardCikar_Success(string KeycardColor)
    {
        KeycardColor = KeycardColor.ToLower();

        if (KeycardColor == "green" && _yesilKeycard > 0)
        {
            DecreaseKeycardAmount(KeycardColor); return true;
        }
        else if (KeycardColor == "yellow" && _sariKeycard > 0)
        {
            DecreaseKeycardAmount(KeycardColor); return true;
        }
        else if (KeycardColor == "red" && _kirmiziKeycard > 0)
        {
            DecreaseKeycardAmount(KeycardColor); return true;
        }
        else return false;
    }
    
    private void DecreaseKeycardAmount(string KeycardColor)
    {
        if (KeycardColor == "green") _yesilKeycard--;
        else if (KeycardColor == "yellow") _sariKeycard--;
        else if (KeycardColor == "red") _kirmiziKeycard--;

        txt_Manager_Info.SetKeycardChildTexts();
    }

    /// <summary>
    /// İşlem başarılıysa true döndürür.
    /// </summary>
    public bool Sayi_Ekle(int _eklenecekSayi, int _eklenecekMiktar)
    {
        if (Inventory_Capacity > 0 && Sayi_EnvantereEkle_Success(_eklenecekSayi, _eklenecekMiktar))
        {
            Inventory_Capacity -= _eklenecekMiktar;
            txt_Manager_Info.SetTheChildTexts();
            return true;
        }
        else return false;
    }
    /// <summary>
    /// İşlem başarılıysa true döndürür.
    /// </summary>
    public bool Sayi_Cikar(int _cikarilacakSayi, int _cikarilacakMiktar)
    {
        if (Sayi_EnvanterdenCikar_Success(_cikarilacakSayi, _cikarilacakMiktar))
        {
            Inventory_Capacity += _cikarilacakMiktar;
            txt_Manager_Info.SetTheChildTexts();
            return true;
        }
        else return false;
    }

    bool Sayi_EnvantereEkle_Success(int Sayi, int Amount)
    {
        if (Sayi == 0)
        {
            Rakam_0 += Amount; return true;
        }
        else if (Sayi == 1)
        {
            Rakam_1 += Amount; return true;
        }
        else if (Sayi == 2)
        {
            Rakam_2 += Amount; return true;
        }
        else if (Sayi == 3)
        {
            Rakam_3 += Amount; return true;
        }
        else if (Sayi == 4)
        {
            Rakam_4 += Amount; return true;
        }
        else if (Sayi == 5)
        {
            Rakam_5 += Amount; return true;
        }
        else if (Sayi == 6)
        {
            Rakam_6 += Amount; return true;
        }
        else if (Sayi == 7)
        {
            Rakam_7 += Amount; return true;
        }
        else if (Sayi == 8)
        {
            Rakam_8 += Amount; return true;
        }
        else if (Sayi == 9)
        {
            Rakam_9 += Amount; return true;
        }
        else return false;
    }
    bool Sayi_EnvanterdenCikar_Success(int Sayi, int Amount)
    {
        if (Sayi == 0)
        {
            Rakam_0 -= Amount; return true;
        }
        else if (Sayi == 1)
        {
            Rakam_1 -= Amount; return true;
        }
        else if (Sayi == 2)
        {
            Rakam_2 -= Amount; return true;
        }
        else if (Sayi == 3)
        {
            Rakam_3 -= Amount; return true;
        }
        else if (Sayi == 4)
        {
            Rakam_4 -= Amount; return true;
        }
        else if (Sayi == 5)
        {
            Rakam_5 -= Amount; return true;
        }
        else if (Sayi == 6)
        {
            Rakam_6 -= Amount; return true;
        }
        else if (Sayi == 7)
        {
            Rakam_7 -= Amount; return true;
        }
        else if (Sayi == 8)
        {
            Rakam_8 -= Amount; return true;
        }
        else if (Sayi == 9)
        {
            Rakam_9 -= Amount; return true;
        }
        else return false;
    }

    /// <summary>
    /// Gonderilen değerdeki sayı envanterde varsa true döndürür.
    /// </summary>
    public bool InventoryControl_Sayi(int value)
    {
        if (value == 0 && Rakam_0 > 0) return true;
        else if (value == 1 && Rakam_1 > 0) return true;
        else if (value == 2 && Rakam_2 > 0) return true;
        else if (value == 3 && Rakam_3 > 0) return true;
        else if (value == 4 && Rakam_4 > 0) return true;
        else if (value == 5 && Rakam_5 > 0) return true;
        else if (value == 6 && Rakam_6 > 0) return true;
        else if (value == 7 && Rakam_7 > 0) return true;
        else if (value == 8 && Rakam_8 > 0) return true;
        else if (value == 9 && Rakam_9 > 0) return true;
        else return false;
    }
    

    public object CaptureState()
    {
        return new SaveData
        {
            _rakam_0 = Rakam_0,
            _rakam_1 = Rakam_1,
            _rakam_2 = Rakam_2,
            _rakam_3 = Rakam_3,
            _rakam_4 = Rakam_4,
            _rakam_5 = Rakam_5,
            _rakam_6 = Rakam_6,
            _rakam_7 = Rakam_7,
            _rakam_8 = Rakam_8,
            _rakam_9 = Rakam_9,
            _yesilKeycard = _yesilKeycard,
            _sariKeycard = _sariKeycard,
            _kirmiziKeycard = _kirmiziKeycard,
            _inventoryCapacity = Inventory_Capacity
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        Rakam_0 = rakam_0;
        Rakam_1 = saveData._rakam_1;
        Rakam_2 = saveData._rakam_2;
        Rakam_3 = saveData._rakam_3;
        Rakam_4 = saveData._rakam_4;
        Rakam_5 = saveData._rakam_5;
        Rakam_6 = saveData._rakam_6;
        Rakam_7 = saveData._rakam_7;
        Rakam_8 = saveData._rakam_8;
        Rakam_9 = saveData._rakam_9;
        _yesilKeycard = saveData._yesilKeycard;
        _sariKeycard = saveData._sariKeycard;
        _kirmiziKeycard = saveData._kirmiziKeycard;
        Inventory_Capacity = saveData._inventoryCapacity;

        txt_Manager_Info.SetTheChildTexts();
        txt_Manager_Info.SetKeycardChildTexts();
    }

    [System.Serializable]
    struct SaveData
    {
        public int _rakam_0, _rakam_1, _rakam_2, _rakam_3, _rakam_4,
                   _rakam_5, _rakam_6, _rakam_7, _rakam_8, _rakam_9;
        public int _yesilKeycard, _sariKeycard, _kirmiziKeycard;
        public int _inventoryCapacity;
    }
}
