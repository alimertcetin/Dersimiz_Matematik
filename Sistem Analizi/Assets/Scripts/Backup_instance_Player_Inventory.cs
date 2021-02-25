using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Backup_instance_Player_Inventory : MonoBehaviour
{
    instance_TxtManager txt_Manager_Info;
    [SerializeField] int _sayi_0 = 0,
        _sayi_1 = 0,
        _sayi_2 = 0,
        _sayi_3 = 0,
        _sayi_4 = 0,
        _sayi_5 = 0,
        _sayi_6 = 0,
        _sayi_7 = 0,
        _sayi_8 = 0,
        _sayi_9 = 0;

    public int Sayi_0 { get => _sayi_0; }
    public int Sayi_1 { get => _sayi_1; }
    public int Sayi_2 { get => _sayi_2; }
    public int Sayi_3 { get => _sayi_3; }
    public int Sayi_4 { get => _sayi_4; }
    public int Sayi_5 { get => _sayi_5; }
    public int Sayi_6 { get => _sayi_6; }
    public int Sayi_7 { get => _sayi_7; }
    public int Sayi_8 { get => _sayi_8; }
    public int Sayi_9 { get => _sayi_9; }

    public int Anahtar_Seviye1 { get; private set; }
    public int Anahtar_Seviye2 { get; private set; }
    public int Anahtar_Seviye3 { get; private set; }

    [SerializeField] int YesilKeycard = 0;
    [SerializeField] int sariKeycard = 0;
    [SerializeField] int KirmiziKeycard = 0;

    private int _inventory_Capacity = 5;
    public int Inventory_Capacity { get => _inventory_Capacity; set => _inventory_Capacity = value; }

    private void Awake()
    {
        Anahtar_Seviye1 = YesilKeycard;
        Anahtar_Seviye2 = sariKeycard;
        Anahtar_Seviye3 = KirmiziKeycard;
        txt_Manager_Info = FindObjectOfType<instance_TxtManager>();
    }

    
    public void CapacityHasChanged(string _tagOfNumber, int CoveredArea)
    {
        //Envanter kapasitesini değiştir.
        _inventory_Capacity -= CoveredArea;
        //Toplanan sayının miktarını değiştir.
        CollectedNumber(_tagOfNumber, CoveredArea);
        txt_Manager_Info.SetTheChildTexts();
    }


    /// <summary>
    /// Verilen sayıyı envanterden eksiltir ve envanter kapasitesini arttırır.
    /// </summary>
    /// <param name="Sayi">Azaltılacak Sayı</param>
    /// <param name="amount">Azaltılacak miktar</param>
    public void Sayi_Cikar(int Sayi, int amount)
    {
        if (amount > 0 && _inventory_Capacity > 0 && Sayi_EnvanterdenCikar_Success(Sayi, amount))
            _inventory_Capacity += amount;
        else
        {
            Debug.LogWarning("Kapasite azaltılırken bir terslik oldu.");
            return;
        }

        txt_Manager_Info.SetTheChildTexts();
    }

    //CapacityHasChanged'e gönderilen değerlere göre sayıyı bul ve kapladığı alan kadar miktarını arttır.
    bool Sayi_EnvanterdenCikar_Success(int _Sayi, int _amount)
    {
        if (_Sayi == 0)
        {
            _sayi_0 -= _amount;
            return true;
        }
        else if (_Sayi == 1)
        {
            _sayi_1 -= _amount;
            return true;
        }
        else if (_Sayi == 2)
        {
            _sayi_2 -= _amount;
            return true;
        }
        else if (_Sayi == 3)
        {
            _sayi_3 -= _amount;
            return true;
        }
        else if (_Sayi == 4)
        {
            _sayi_4 -= _amount;
            return true;
        }
        else if (_Sayi == 5)
        {
            _sayi_5 -= _amount;
            return true;
        }
        else if (_Sayi == 6)
        {
            _sayi_6 -= _amount;
            return true;
        }
        else if (_Sayi == 7)
        {
            _sayi_7 -= _amount;
            return true;
        }
        else if (_Sayi == 8)
        {
            _sayi_8 -= _amount;
            return true;
        }
        else if (_Sayi == 9)
        {
            _sayi_9 -= _amount;
            return true;
        }
        else
            return false;
    }


    /// <summary>
    /// Verilen sayıyı envantere ekler ve envanter kapasitesini azaltır.
    /// </summary>
    /// <param name="Sayi">Azaltılacak Sayı</param>
    /// <param name="amount">Azaltılacak miktar</param>
    public void Sayi_Ekle(int Sayi, int amount)
    {
        Sayi_EnvantereEkle_Success(Sayi, amount);
            _inventory_Capacity -= amount;

        txt_Manager_Info.SetTheChildTexts();
    }

    //CapacityHasChanged'e gönderilen değerlere göre sayıyı bul ve kapladığı alan kadar miktarını arttır.
    bool Sayi_EnvantereEkle_Success(int _Sayi, int _amount)
    {
        if (_Sayi == 0)
        {
            _sayi_0 += _amount;
            return true;
        }
        else if (_Sayi == 1)
        {
            _sayi_1 += _amount;
            return true;
        }
        else if (_Sayi == 2)
        {
            _sayi_2 += _amount;
            return true;
        }
        else if (_Sayi == 3)
        {
            _sayi_3 += _amount;
            return true;
        }
        else if (_Sayi == 4)
        {
            _sayi_4 += _amount;
            return true;
        }
        else if (_Sayi == 5)
        {
            _sayi_5 += _amount;
            return true;
        }
        else if (_Sayi == 6)
        {
            _sayi_6 += _amount;
            return true;
        }
        else if (_Sayi == 7)
        {
            _sayi_7 += _amount;
            return true;
        }
        else if (_Sayi == 8)
        {
            _sayi_8 += _amount;
            return true;
        }
        else if (_Sayi == 9)
        {
            _sayi_9 += _amount;
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Gonderilen değerdeki sayı envanterde varsa true döndürür.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Sayi_InventoryControl(int value)
    {
        if (value == 0 && _sayi_0 > 0) return true;
        else if (value == 1 && _sayi_1 > 0) return true;
        else if (value == 2 && _sayi_2 > 0) return true;
        else if (value == 3 && _sayi_3 > 0) return true;
        else if (value == 4 && _sayi_4 > 0) return true;
        else if (value == 5 && _sayi_5 > 0) return true;
        else if (value == 6 && _sayi_6 > 0) return true;
        else if (value == 7 && _sayi_7 > 0) return true;
        else if (value == 8 && _sayi_8 > 0) return true;
        else if (value == 9 && _sayi_9 > 0) return true;
        else return false;
    }

    //CapacityHasChanged'e gönderilen değerlere göre sayıyı bul ve kapladığı alan kadar miktarını arttır.
    public void CollectedNumber(string tagOfNumber, int amount)
    {
        //Tagler Türkçe karakter kullanarak verildiği için string ifadelerde "ı" kullanıldı.
        //Sayi_0
        if (tagOfNumber == "Sayı_0")
        {
            _sayi_0 += amount;
        }
        //Sayi_1
        if (tagOfNumber == "Sayı_1")
        {
            _sayi_1 += amount;
        }
        //Sayi_2
        if (tagOfNumber == "Sayı_2")
        {
            _sayi_2 += amount;
        }
        //Sayi_3
        if (tagOfNumber == "Sayı_3")
        {
            _sayi_3 += amount;
        }
        //Sayi_4
        if (tagOfNumber == "Sayı_4")
        {
            _sayi_4 += amount;
        }
        //Sayi_5
        if (tagOfNumber == "Sayı_5")
        {
            _sayi_5 += amount;
        }
        //Sayi_6
        if (tagOfNumber == "Sayı_6")
        {
            _sayi_6 += amount;
        }
        //Sayi_7
        if (tagOfNumber == "Sayı_7")
        {
            _sayi_7 += amount;
        }
        //Sayi_8
        if (tagOfNumber == "Sayı_8")
        {
            _sayi_8 += amount;
        }
        //Sayi_9
        if (tagOfNumber == "Sayı_9")
        {
            _sayi_9 += amount;
        }
    }

    public void CollectedKeycard(string KeycardColor)
    {
        KeycardColor = KeycardColor.ToLower();
        if (KeycardColor == "green")
            Anahtar_Seviye1++;

        else if (KeycardColor == "yellow")
            Anahtar_Seviye2++;

        else if (KeycardColor == "red")
            Anahtar_Seviye3++;
        else
            Debug.LogWarning("Keycard bulunamadı. ---->" + this.name);

        txt_Manager_Info.SetKeycardChildTexts();
    }

    /// <summary>
    /// Keycard'ı envanterden kaldırmayı dener. Başarılıysa true döndürür.
    /// </summary>
    /// <param name="KeycardColor">green, red, yellow</param>
    public bool RemoveKeycardSuccess (string KeycardColor)
    {
        KeycardColor = KeycardColor.ToLower();
        if (KeycardColor == "green" && Anahtar_Seviye1 > 0)
        {
            DecreaseKeycardAmount(KeycardColor);
            return true;
        }
        else if(KeycardColor == "yellow" && Anahtar_Seviye2 > 0)
        {
            DecreaseKeycardAmount(KeycardColor);
            return true;
        }
        else if(KeycardColor == "red" && Anahtar_Seviye3 > 0)
        {
            DecreaseKeycardAmount(KeycardColor);
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Removes the keycard from inventory, depends on KeycardColor
    /// </summary>
    /// <param name="KeycardColor"></param>
    private void DecreaseKeycardAmount(string KeycardColor)
    {
        if (KeycardColor == "green")
            Anahtar_Seviye1--;

        else if (KeycardColor == "yellow")
            Anahtar_Seviye2--;

        else if (KeycardColor == "red")
            Anahtar_Seviye3--;
        else
            Debug.LogWarning("Keycard bulunamadı. ---->" + this.name);

        txt_Manager_Info.SetKeycardChildTexts();
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
