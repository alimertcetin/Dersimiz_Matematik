using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectListener_Test : MonoBehaviour
{
    public CanvasLockedDoor_Manager LockedDoor_Manager;

    #region
    int Sayi_0 = 0,
        Sayi_1 = 0,
        Sayi_2 = 0,
        Sayi_3 = 0,
        Sayi_4 = 0,
        Sayi_5 = 0,
        Sayi_6 = 0,
        Sayi_7 = 0,
        Sayi_8 = 0,
        Sayi_9 = 0;
    #endregion

    [SerializeField]
    int instance_Sayi_0 = 0,
        instance_Sayi_1 = 0,
        instance_Sayi_2 = 0,
        instance_Sayi_3 = 0,
        instance_Sayi_4 = 0,
        instance_Sayi_5 = 0,
        instance_Sayi_6 = 0,
        instance_Sayi_7 = 0,
        instance_Sayi_8 = 0,
        instance_Sayi_9 = 0;
    //Accesing from Script_Of_Numbers.cs
    public int GivenSpace = 0;


    public TMP_Text[] Txt_ChildSayilar;
    public GameObject[] Array_SayilarChild;


    private void Start()
    {
        if (Txt_ChildSayilar == null)
        {
            Debug.LogWarning("ObjectListener üzerindeki veya 'Txt_ChildSayilar' dizileri boş bırakılamaz.");
        }
    }

    public void InvantoryFull()
    {
        Debug.LogWarning("Invantory is Full!");
    }

    //List option for holding the numbers variables.
    //void HoldList()
    //{
    //List<int> Sayilar = new List<int>();
    //    for (int i = 0; i <= 10; i++)
    //    {
    //        Sayilar.Add(Sayi_0);
    //        Sayilar.Add(Sayi_1);
    //        Sayilar.Add(Sayi_2);
    //        Sayilar.Add(Sayi_3);
    //        Sayilar.Add(Sayi_4);
    //        Sayilar.Add(Sayi_5);
    //        Sayilar.Add(Sayi_6);
    //        Sayilar.Add(Sayi_7);
    //        Sayilar.Add(Sayi_8);
    //        Sayilar.Add(Sayi_9);
    //    }
    //}

    public void CheckTheTag_AndIncrease(string GO_tag)
    {
        if (GivenSpace > 0)
        {
            GivenSpace -= 1;
            if (GO_tag == "Sayı_0")
            {
                Sayi_0 += 1;
                Txt_ChildSayilar[0].SetText("x" + Sayi_0);
                //-----
                if (Txt_ChildSayilar[0].enabled != true)
                {
                    Txt_ChildSayilar[0].enabled = true;
                }
            }
            if (GO_tag == "Sayı_1")
            {
                Sayi_1 += 1;
                Txt_ChildSayilar[1].SetText("x" + Sayi_1);
                //-----
                if (Txt_ChildSayilar[1].enabled != true)
                {
                    Txt_ChildSayilar[1].enabled = true;
                }
            }
            if (GO_tag == "Sayı_2")
            {
                Sayi_2 += 1;
                Txt_ChildSayilar[2].SetText("x" + Sayi_2);
                //-----
                if (Txt_ChildSayilar[2].enabled != true)
                {
                    Txt_ChildSayilar[2].enabled = true;
                }
            }
            if (GO_tag == "Sayı_3")
            {
                Sayi_3 += 1;
                Txt_ChildSayilar[3].SetText("x" + Sayi_3);
                //-----
                if (Txt_ChildSayilar[3].enabled != true)
                {
                    Txt_ChildSayilar[3].enabled = true;
                }
            }
            if (GO_tag == "Sayı_4")
            {
                Sayi_4 += 1;
                Txt_ChildSayilar[4].SetText("x" + Sayi_4);
                //-----
                if (Txt_ChildSayilar[4].enabled != true)
                {
                    Txt_ChildSayilar[4].enabled = true;
                }
            }
            if (GO_tag == "Sayı_5")
            {
                Sayi_5 += 1;
                Txt_ChildSayilar[5].SetText("x" + Sayi_5);
                //-----
                if (Txt_ChildSayilar[5].enabled != true)
                {
                    Txt_ChildSayilar[5].enabled = true;
                }
            }
            if (GO_tag == "Sayı_6")
            {
                Sayi_6 += 1;
                Txt_ChildSayilar[6].SetText("x" + Sayi_6);
                //-----
                if (Txt_ChildSayilar[6].enabled != true)
                {
                    Txt_ChildSayilar[6].enabled = true;
                }
            }
            if (GO_tag == "Sayı_7")
            {
                Sayi_7 += 1;
                Txt_ChildSayilar[7].SetText("x" + Sayi_7);
                //-----
                if (Txt_ChildSayilar[7].enabled != true)
                    Txt_ChildSayilar[7].enabled = true;
            }
            if (GO_tag == "Sayı_8")
            {
                Sayi_8 += 1;
                Txt_ChildSayilar[8].SetText("x" + Sayi_8);
                //-----
                //-----
                if (Txt_ChildSayilar[8].enabled != true)
                    Txt_ChildSayilar[8].enabled = true;
            }
            if (GO_tag == "Sayı_9")
            {
                Sayi_9 += 1;
                Txt_ChildSayilar[9].SetText("x" + Sayi_9);
                //-----
                //-----
                if (Txt_ChildSayilar[9].enabled != true)
                    Txt_ChildSayilar[9].enabled = true;
            }
        }

        GreaterThan0();
        Instance_EqualsTo_Original();
    }

    public void CheckTheTag_AndDecrease(string GO_tag)
    {
        if (GO_tag == "Sayı_0")
        {
            instance_Sayi_0 -= 1;
        }
        if (GO_tag == "Sayı_1")
        {
            instance_Sayi_1 -= 1;
        }
        if (GO_tag == "Sayı_2")
        {
            instance_Sayi_2 -= 1;
        }
        if (GO_tag == "Sayı_3")
        {
            instance_Sayi_3 -= 1;
        }
        if (GO_tag == "Sayı_4")
        {
            instance_Sayi_4 -= 1;
        }
        if (GO_tag == "Sayı_5")
        {
            instance_Sayi_5 -= 1;
        }
        if (GO_tag == "Sayı_6")
        {
            instance_Sayi_6 -= 1;
        }
        if (GO_tag == "Sayı_7")
        {
            instance_Sayi_7 -= 1;
        }
        if (GO_tag == "Sayı_8")
        {
            instance_Sayi_8 -= 1;
        }
        if (GO_tag == "Sayı_9")
        {
            instance_Sayi_9 -= 1;
        }
    }

    void Instance_EqualsTo_Original()
    {
        instance_Sayi_0 = Sayi_0;
        instance_Sayi_1 = Sayi_1;
        instance_Sayi_2 = Sayi_2;
        instance_Sayi_3 = Sayi_3;
        instance_Sayi_4 = Sayi_4;
        instance_Sayi_5 = Sayi_5;
        instance_Sayi_6 = Sayi_6;
        instance_Sayi_7 = Sayi_7;
        instance_Sayi_8 = Sayi_8;
        instance_Sayi_9 = Sayi_9;
    }

    //When btn_cevapla is clicked this method is called by CanvasLockedDoor_Manager
    public void Original_EqualsTo_Instance()
    {
            Sayi_0 = instance_Sayi_0;
            Sayi_1 = instance_Sayi_1;
            Sayi_2 = instance_Sayi_2;
            Sayi_3 = instance_Sayi_3;
            Sayi_4 = instance_Sayi_4;
            Sayi_5 = instance_Sayi_5;
            Sayi_6 = instance_Sayi_6;
            Sayi_7 = instance_Sayi_7;
            Sayi_8 = instance_Sayi_8;
            Sayi_9 = instance_Sayi_9;

        setTheChildText();
    }

    void setTheChildText()
    {
        Txt_ChildSayilar[0].SetText("x" + Sayi_0);
        //-----
        Txt_ChildSayilar[1].SetText("x" + Sayi_1);
        //-----
        Txt_ChildSayilar[2].SetText("x" + Sayi_2);
        //-----
        Txt_ChildSayilar[3].SetText("x" + Sayi_3);
        //-----
        Txt_ChildSayilar[4].SetText("x" + Sayi_4);
        //-----
        Txt_ChildSayilar[5].SetText("x" + Sayi_5);
        //-----
        Txt_ChildSayilar[6].SetText("x" + Sayi_6);
        //-----
        Txt_ChildSayilar[7].SetText("x" + Sayi_7);
        //-----
        Txt_ChildSayilar[8].SetText("x" + Sayi_8);
        //-----
        Txt_ChildSayilar[9].SetText("x" + Sayi_9);
        //-----
    }

    void GreaterThan0()
    {
        if (Sayi_0 > 0)
            LockedDoor_Manager.sayi_0 = true;
        else
            LockedDoor_Manager.sayi_0 = false;

        if (Sayi_1 > 0)
            LockedDoor_Manager.sayi_1 = true;
        else
            LockedDoor_Manager.sayi_1 = false;

        if (Sayi_2 > 0)
            LockedDoor_Manager.sayi_2 = true;
        else
            LockedDoor_Manager.sayi_2 = false;

        if (Sayi_3 > 0)
            LockedDoor_Manager.sayi_3 = true;
        else
            LockedDoor_Manager.sayi_3 = false;

        if (Sayi_4 > 0)
            LockedDoor_Manager.sayi_4 = true;
        else
            LockedDoor_Manager.sayi_4 = false;

        if (Sayi_5 > 0)
            LockedDoor_Manager.sayi_5 = true;
        else
            LockedDoor_Manager.sayi_5 = false;

        if (Sayi_6 > 0)
            LockedDoor_Manager.sayi_6 = true;
        else
            LockedDoor_Manager.sayi_6 = false;

        if (Sayi_7 > 0)
            LockedDoor_Manager.sayi_7 = true;
        else
            LockedDoor_Manager.sayi_7 = false;

        if (Sayi_8 > 0)
            LockedDoor_Manager.sayi_8 = true;
        else
            LockedDoor_Manager.sayi_8 = false;

        if (Sayi_9 > 0)
            LockedDoor_Manager.sayi_9 = true;
        else
            LockedDoor_Manager.sayi_9 = false;
    }

}
