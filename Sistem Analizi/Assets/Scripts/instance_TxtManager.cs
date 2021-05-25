using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class instance_TxtManager : MonoBehaviour
{
    PlayerInventory _inventory;
    [Header("HUD üzerindeki sayıların child textlerini sırasıyla ekleyin.")]
    [SerializeField] TMP_Text[] Txt_Array_ChildSayilar = null;

    [Header("Keycardlar textlerini sırasıyla ekleyin.")]
    [Tooltip("Yeşil, sarı, kırmızı - 0,1,2 şeklinde.")]
    [SerializeField] GameObject[] KeycardArray = new GameObject[3];
    TMP_Text Txt_Seviye1;
    TMP_Text Txt_Seviye2;
    TMP_Text Txt_Seviye3;

    private void Start()
    {
        Txt_Seviye1 = KeycardArray[0].GetComponentInChildren<TMP_Text>();
        Txt_Seviye2 = KeycardArray[1].GetComponentInChildren<TMP_Text>();
        Txt_Seviye3 = KeycardArray[2].GetComponentInChildren<TMP_Text>();

        //---First find the inventory and then set the correct numbers for childTexts---\\
        _inventory = FindObjectOfType<PlayerInventory>();
        SetTheChildTexts();
        SetKeycardChildTexts();
    }

    public void SetKeycardChildTexts()
    {
        //Keycard Seviye 1
        if (_inventory._yesilKeycard > 0)
        {
            Txt_Seviye1.text = "x" + _inventory._yesilKeycard.ToString();
            Txt_Seviye1.enabled = true;
            KeycardArray[0].SetActive(true);
        }
        else
            KeycardArray[0].SetActive(false);

        //Keycard Seviye 2
        if (_inventory._sariKeycard > 0)
        {
            Txt_Seviye2.text = "x" + _inventory._sariKeycard.ToString();
            Txt_Seviye2.enabled = true;
            KeycardArray[1].SetActive(true);
        }
        else
            KeycardArray[1].SetActive(false);

        //Keycard Seviye 3
        if (_inventory._kirmiziKeycard > 0)
        {
            Txt_Seviye3.text = "x" + _inventory._kirmiziKeycard.ToString();
            Txt_Seviye3.enabled = true;
            KeycardArray[2].SetActive(true);
        }
        else
            KeycardArray[2].SetActive(false);
    }

    //Çalışması için
    //Diziye Child textler atanmış olmalı
    public void SetTheChildTexts()
    {
        //Sayi_0
        if (_inventory.Rakam_0 > 0)
        {
            Txt_Array_ChildSayilar[0].text = "x" + _inventory.Rakam_0.ToString();
            if (!Txt_Array_ChildSayilar[0].enabled)
                Txt_Array_ChildSayilar[0].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[0].enabled = false;
        }
        //Sayi_1
        if (_inventory.Rakam_1 > 0)
        {
            Txt_Array_ChildSayilar[1].text = "x" + _inventory.Rakam_1.ToString();
            if (!Txt_Array_ChildSayilar[1].enabled)
                Txt_Array_ChildSayilar[1].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[1].enabled = false;
        }
        //Sayi_2
        if (_inventory.Rakam_2 > 0)
        {
            Txt_Array_ChildSayilar[2].text = "x" + _inventory.Rakam_2.ToString();
            if (!Txt_Array_ChildSayilar[2].enabled)
                Txt_Array_ChildSayilar[2].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[2].enabled = false;
        }
        //Sayi_3
        if (_inventory.Rakam_3 > 0)
        {
            Txt_Array_ChildSayilar[3].text = "x" + _inventory.Rakam_3.ToString();
            if (!Txt_Array_ChildSayilar[3].enabled)
                Txt_Array_ChildSayilar[3].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[3].enabled = false;
        }
        //Sayi_4
        if (_inventory.Rakam_4 > 0)
        {
            Txt_Array_ChildSayilar[4].text = "x" + _inventory.Rakam_4.ToString();
            if (!Txt_Array_ChildSayilar[4].enabled)
                Txt_Array_ChildSayilar[4].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[4].enabled = false;
        }
        //Sayi_5
        if (_inventory.Rakam_5 > 0)
        {
            Txt_Array_ChildSayilar[5].text = "x" + _inventory.Rakam_5.ToString();
            if (!Txt_Array_ChildSayilar[5].enabled)
                Txt_Array_ChildSayilar[5].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[5].enabled = false;
        }
        //Sayi_6
        if (_inventory.Rakam_6 > 0)
        {
            Txt_Array_ChildSayilar[6].text = "x" + _inventory.Rakam_6.ToString();
            if (!Txt_Array_ChildSayilar[6].enabled)
                Txt_Array_ChildSayilar[6].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[6].enabled = false;
        }
        //Sayi_7
        if (_inventory.Rakam_7 > 0)
        {
            Txt_Array_ChildSayilar[7].text = "x" + _inventory.Rakam_7.ToString();
            if (!Txt_Array_ChildSayilar[7].enabled)
                Txt_Array_ChildSayilar[7].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[7].enabled = false;
        }
        //Sayi_8
        if (_inventory.Rakam_8 > 0)
        {
            Txt_Array_ChildSayilar[8].text = "x" + _inventory.Rakam_8.ToString();
            if (!Txt_Array_ChildSayilar[8].enabled)
                Txt_Array_ChildSayilar[8].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[8].enabled = false;
        }
        //Sayi_9
        if (_inventory.Rakam_9 > 0)
        {
            Txt_Array_ChildSayilar[9].text = "x" + _inventory.Rakam_9.ToString();
            if (!Txt_Array_ChildSayilar[9].enabled)
                Txt_Array_ChildSayilar[9].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[9].enabled = false;
        }
    }
}
