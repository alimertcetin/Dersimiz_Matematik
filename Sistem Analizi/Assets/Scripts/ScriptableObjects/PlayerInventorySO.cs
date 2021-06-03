using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData/Inventory")]
public class PlayerInventorySO : ScriptableObject
{
    public int Capacity = 5;

    public int Rakam_0 = 0;
    public int Rakam_1 = 0;
    public int Rakam_2 = 0;
    public int Rakam_3 = 0;
    public int Rakam_4 = 0;
    public int Rakam_5 = 0;
    public int Rakam_6 = 0;
    public int Rakam_7 = 0;
    public int Rakam_8 = 0;
    public int Rakam_9 = 0;
    
    public int yesilKeycard = 0;
    public int sariKeycard = 0;
    public int kirmiziKeycard = 0;

    [ContextMenu("Reset All")]
    public void ResetAll()
    {
        ResetNumbers();
        ResetKeycards();
    }

    [ContextMenu("Reset Numbers")]
    public void ResetNumbers()
    {
        Rakam_0 = 50;
        Rakam_1 = 50;
        Rakam_2 = 50;
        Rakam_3 = 50;
        Rakam_4 = 50;
        Rakam_5 = 50;
        Rakam_6 = 50;
        Rakam_7 = 50;
        Rakam_8 = 50;
        Rakam_9 = 50;
    }

    [ContextMenu("Reset Keycards")]
    public void ResetKeycards()
    {
        yesilKeycard = 50;
        sariKeycard = 50;
        kirmiziKeycard = 50;
    }

    public bool Sayi_EnvantereEkle_Success(int Sayi, int Amount)
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

    public bool Sayi_EnvanterdenCikar_Success(int Sayi, int Amount)
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

    /// <summary>
    /// Keycard envantere eklendiyse true döndürür.
    /// </summary>
    public bool KeycardEkle(Door_and_Keycard_Level keycard)
    {
        if (keycard == Door_and_Keycard_Level.Yesil)
        {
            yesilKeycard++;

            return true;
        }
        else if (keycard == Door_and_Keycard_Level.Sari)
        {
            sariKeycard++;
            //txt_Manager_Info.SetKeycardChildTexts();
            return true;
        }
        else if (keycard == Door_and_Keycard_Level.Kirmizi)
        {
            kirmiziKeycard++;
            //txt_Manager_Info.SetKeycardChildTexts();
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

        if (KeycardColor == "green" && yesilKeycard > 0)
        {
            DecreaseKeycardAmount(KeycardColor);
            return true;
        }
        else if (KeycardColor == "yellow" && sariKeycard > 0)
        {
            DecreaseKeycardAmount(KeycardColor);
            return true;
        }
        else if (KeycardColor == "red" && kirmiziKeycard > 0)
        {
            DecreaseKeycardAmount(KeycardColor);
            return true;
        }
        else return false;
    }

    private void DecreaseKeycardAmount(string KeycardColor)
    {
        if (KeycardColor == "green")
        {
            yesilKeycard--;
        }
        else if (KeycardColor == "yellow")
        {
            sariKeycard--;
        }
        else if (KeycardColor == "red")
        {
            kirmiziKeycard--;
        }
    }

    /// <summary>
    /// İşlem başarılıysa true döndürür.
    /// </summary>
    public bool Sayi_Ekle(int _eklenecekSayi, int _eklenecekMiktar)
    {
        if (Capacity > 0 && Sayi_EnvantereEkle_Success(_eklenecekSayi, _eklenecekMiktar))
        {
            Capacity -= _eklenecekMiktar;
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
            Capacity += _cikarilacakMiktar;
            return true;
        }
        else return false;
    }
}
