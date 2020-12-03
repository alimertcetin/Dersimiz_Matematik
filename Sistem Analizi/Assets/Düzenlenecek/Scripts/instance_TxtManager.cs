using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class instance_TxtManager : MonoBehaviour
{
    instance_Player_Inventory _inventory;
    [SerializeField]
    TMP_Text[] Txt_Array_ChildSayilar = null;

    private void Start()
    {
        //---First find the inventory and then set the correct numbers for childTexts---\\
        _inventory = FindObjectOfType<instance_Player_Inventory>();
        SetTheChildTexts();
    }


    //Çalışması için
    //Diziye Child textler atanmış olmalı
    public void SetTheChildTexts()
    {
        //Sayi_0
        if (_inventory.Sayi_0 > 0)
        {
            Txt_Array_ChildSayilar[0].text = "x" + _inventory.Sayi_0.ToString();
            if (!Txt_Array_ChildSayilar[0].enabled)
                Txt_Array_ChildSayilar[0].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[0].enabled = false;
        }
        //Sayi_1
        if (_inventory.Sayi_1 > 0)
        {
            Txt_Array_ChildSayilar[1].text = "x" + _inventory.Sayi_1.ToString();
            if (!Txt_Array_ChildSayilar[1].enabled)
                Txt_Array_ChildSayilar[1].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[1].enabled = false;
        }
        //Sayi_2
        if (_inventory.Sayi_2 > 0)
        {
            Txt_Array_ChildSayilar[2].text = "x" + _inventory.Sayi_2.ToString();
            if (!Txt_Array_ChildSayilar[2].enabled)
                Txt_Array_ChildSayilar[2].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[2].enabled = false;
        }
        //Sayi_3
        if (_inventory.Sayi_3 > 0)
        {
            Txt_Array_ChildSayilar[3].text = "x" + _inventory.Sayi_3.ToString();
            if (!Txt_Array_ChildSayilar[3].enabled)
                Txt_Array_ChildSayilar[3].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[3].enabled = false;
        }
        //Sayi_4
        if (_inventory.Sayi_4 > 0)
        {
            Txt_Array_ChildSayilar[4].text = "x" + _inventory.Sayi_4.ToString();
            if (!Txt_Array_ChildSayilar[4].enabled)
                Txt_Array_ChildSayilar[4].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[4].enabled = false;
        }
        //Sayi_5
        if (_inventory.Sayi_5 > 0)
        {
            Txt_Array_ChildSayilar[5].text = "x" + _inventory.Sayi_5.ToString();
            if (!Txt_Array_ChildSayilar[5].enabled)
                Txt_Array_ChildSayilar[5].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[5].enabled = false;
        }
        //Sayi_6
        if (_inventory.Sayi_6 > 0)
        {
            Txt_Array_ChildSayilar[6].text = "x" + _inventory.Sayi_6.ToString();
            if (!Txt_Array_ChildSayilar[6].enabled)
                Txt_Array_ChildSayilar[6].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[6].enabled = false;
        }
        //Sayi_7
        if (_inventory.Sayi_7 > 0)
        {
            Txt_Array_ChildSayilar[7].text = "x" + _inventory.Sayi_7.ToString();
            if (!Txt_Array_ChildSayilar[7].enabled)
                Txt_Array_ChildSayilar[7].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[7].enabled = false;
        }
        //Sayi_8
        if (_inventory.Sayi_8 > 0)
        {
            Txt_Array_ChildSayilar[8].text = "x" + _inventory.Sayi_8.ToString();
            if (!Txt_Array_ChildSayilar[8].enabled)
                Txt_Array_ChildSayilar[8].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[8].enabled = false;
        }
        //Sayi_9
        if (_inventory.Sayi_9 > 0)
        {
            Txt_Array_ChildSayilar[9].text = "x" + _inventory.Sayi_9.ToString();
            if (!Txt_Array_ChildSayilar[9].enabled)
                Txt_Array_ChildSayilar[9].enabled = true;
        }
        else
        {
            Txt_Array_ChildSayilar[9].enabled = false;
        }
    }
}
