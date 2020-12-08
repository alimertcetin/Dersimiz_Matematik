using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class instance_Player_Inventory : MonoBehaviour
{
    instance_TxtManager txt_Manager_Info;
    public int Sayi_0 = 0,
        Sayi_1 = 0,
        Sayi_2 = 0,
        Sayi_3 = 0,
        Sayi_4 = 0,
        Sayi_5 = 0,
        Sayi_6 = 0,
        Sayi_7 = 0,
        Sayi_8 = 0,
        Sayi_9 = 0;
    public int Anahtar1 = 0;

    public int inventory_Capacity = 5;

    private void Awake()
    {
        txt_Manager_Info = FindObjectOfType<instance_TxtManager>();
    }


    //Sayilar_ReadInfo tarafından kullanılıyor.
    /// <summary>
    /// Envanter kapasitesini değiştirir.
    /// </summary>
    /// <param name="_tagOfNumber">Envanterden azaltılacak veya Envantere eklenecek sayıyı bulur.</param>
    /// <param name="CoveredArea">Değer pozitifse kapasiteyi azaltır ve sayıyı envantere ekler.
    /// Negatifse kapasiteyi arttırır ve sayıyı envanterden çıkartır.</param>
    public void CapacityHasChanged(string _tagOfNumber, int CoveredArea)
    {
        //Envanter kapasitesini değiştir.
        inventory_Capacity -= CoveredArea;
        //Toplanan sayının miktarını değiştir.
        CollectedNumber(_tagOfNumber, CoveredArea);
        txt_Manager_Info.SetTheChildTexts();
    }

    //CapacityHasChanged'e gönderilen değerlere göre sayıyı bul ve kapladığı alan kadar miktarını arttır.
    public void CollectedNumber(string tagOfNumber, int amount)
    {
        //Tagler Türkçe karakter kullanarak verildiği için string ifadelerde "ı" kullanıldı.
        //Sayi_0
        if (tagOfNumber == "Sayı_0")
        {
            Sayi_0 += amount;
        }
        //Sayi_1
        if (tagOfNumber == "Sayı_1")
        {
            Sayi_1 += amount;
        }
        //Sayi_2
        if (tagOfNumber == "Sayı_2")
        {
            Sayi_2 += amount;
        }
        //Sayi_3
        if (tagOfNumber == "Sayı_3")
        {
            Sayi_3 += amount;
        }
        //Sayi_4
        if (tagOfNumber == "Sayı_4")
        {
            Sayi_4 += amount;
        }
        //Sayi_5
        if (tagOfNumber == "Sayı_5")
        {
            Sayi_5 += amount;
        }
        //Sayi_6
        if (tagOfNumber == "Sayı_6")
        {
            Sayi_6 += amount;
        }
        //Sayi_7
        if (tagOfNumber == "Sayı_7")
        {
            Sayi_7 += amount;
        }
        //Sayi_8
        if (tagOfNumber == "Sayı_8")
        {
            Sayi_8 += amount;
        }
        //Sayi_9
        if (tagOfNumber == "Sayı_9")
        {
            Sayi_9 += amount;
        }
    }

    /// <summary>
    /// Returns the tag of integer number.
    /// </summary>
    /// <param name="_receviedNumber">Gönderilecek rakam.</param>
    /// <returns>Tag of number.</returns>
    public string FindTheTag(int _receviedNumber)
    {
        if (_receviedNumber == 0)
        {
            return "Sayı_0";
        }
        else if (_receviedNumber == 1)
        {
            return "Sayı_1";
        }
        else if (_receviedNumber == 2)
        {
            return "Sayı_2";
        }
        else if (_receviedNumber == 3)
        {
            return "Sayı_3";
        }
        else if (_receviedNumber == 4)
        {
            return "Sayı_4";
        }
        else if (_receviedNumber == 5)
        {
            return "Sayı_5";
        }
        else if (_receviedNumber == 6)
        {
            return "Sayı_6";
        }
        else if (_receviedNumber == 7)
        {
            return "Sayı_7";
        }
        else if (_receviedNumber == 8)
        {
            return "Sayı_8";
        }
        else if (_receviedNumber == 9)
        {
            return "Sayı_9";
        }
        else
            return null;
    }

}
