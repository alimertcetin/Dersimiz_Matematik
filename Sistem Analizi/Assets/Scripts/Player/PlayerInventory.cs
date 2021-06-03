using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableEntity))]
public class PlayerInventory : MonoBehaviour, ISaveable
{
    [SerializeField] private VoidEventChannelSO OnInventoryLoaded;

    instance_TxtManager txt_Manager_Info;
    [SerializeField] private PlayerInventorySO inventorySO;

    public int Inventory_Capacity { get => inventorySO.Capacity; }

    private void Awake()
    {
        txt_Manager_Info = FindObjectOfType<instance_TxtManager>();
        OnInventoryLoaded.RaiseEvent();
    }

    [System.Obsolete()]
    public bool InventoryControl_Sayi(int value)
    {
        return inventorySO.InventoryControl_Sayi(value);
    }

    /// <summary>
    /// Keycard envantere eklendiyse true döndürür.
    /// </summary>
    [System.Obsolete()]
    public bool KeycardEkle(Door_and_Keycard_Level keycard)
    {
        bool result = inventorySO.KeycardEkle(keycard);
        txt_Manager_Info.SetKeycardChildTexts();
        return result;
    }

    /// <summary>
    /// Keycard'ı envanterden kaldırmayı dener. Başarılıysa true döndürür.
    /// </summary>
    /// <param name="KeycardColor">green, red, yellow</param>
    [System.Obsolete()]
    public bool KeycardCikar_Success(string KeycardColor)
    {
        bool result = inventorySO.KeycardCikar_Success(KeycardColor);
        txt_Manager_Info.SetKeycardChildTexts();
        return result;
    }

    /// <summary>
    /// İşlem başarılıysa true döndürür.
    /// </summary>
    public bool Sayi_Ekle(int _eklenecekSayi, int _eklenecekMiktar)
    {
        bool result = inventorySO.Sayi_Ekle(_eklenecekSayi, _eklenecekMiktar);
        txt_Manager_Info.SetTheChildTexts();
        return result;
    }

    /// <summary>
    /// İşlem başarılıysa true döndürür.
    /// </summary>
    public bool Sayi_Cikar(int _cikarilacakSayi, int _cikarilacakMiktar)
    {
        bool result = inventorySO.Sayi_Cikar(_cikarilacakSayi, _cikarilacakMiktar);
        txt_Manager_Info.SetTheChildTexts();
        return result;
    }

    public object CaptureState()
    {
        return new SaveData
        {
            _rakam_0 = inventorySO.Rakam_0,
            _rakam_1 = inventorySO.Rakam_1,
            _rakam_2 = inventorySO.Rakam_2,
            _rakam_3 = inventorySO.Rakam_3,
            _rakam_4 = inventorySO.Rakam_4,
            _rakam_5 = inventorySO.Rakam_5,
            _rakam_6 = inventorySO.Rakam_6,
            _rakam_7 = inventorySO.Rakam_7,
            _rakam_8 = inventorySO.Rakam_8,
            _rakam_9 = inventorySO.Rakam_9,
            _yesilKeycard = inventorySO.yesilKeycard,
            _sariKeycard = inventorySO.sariKeycard,
            _kirmiziKeycard = inventorySO.kirmiziKeycard,
            _inventoryCapacity = inventorySO.Capacity
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        inventorySO.Rakam_0 = saveData._rakam_0;
        inventorySO.Rakam_1 = saveData._rakam_1;
        inventorySO.Rakam_2 = saveData._rakam_2;
        inventorySO.Rakam_3 = saveData._rakam_3;
        inventorySO.Rakam_4 = saveData._rakam_4;
        inventorySO.Rakam_5 = saveData._rakam_5;
        inventorySO.Rakam_6 = saveData._rakam_6;
        inventorySO.Rakam_7 = saveData._rakam_7;
        inventorySO.Rakam_8 = saveData._rakam_8;
        inventorySO.Rakam_9 = saveData._rakam_9;
        inventorySO.yesilKeycard = saveData._yesilKeycard;
        inventorySO.sariKeycard = saveData._sariKeycard;
        inventorySO.kirmiziKeycard = saveData._kirmiziKeycard;
        inventorySO.Capacity = saveData._inventoryCapacity;

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
