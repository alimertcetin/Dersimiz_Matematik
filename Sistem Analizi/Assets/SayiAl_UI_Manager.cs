using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SayiAl_UI_Manager : MonoBehaviour
{

    [SerializeField]
    TMP_Text txt_SayiAl_InputField = null, txt_Soru = null;

    [SerializeField]
    GameObject SayiAl_Warning_GO = null;
    TMP_Text Txt_SayiAl_Warning, Txt_BlackBoard_Warning_instance;

    instance_Player_Inventory inventory;

    [SerializeField]
    GameObject Giris_UI = null,
        SayiAl_UI = null;

    public int Text_MaxLenght = 7;

    private void Awake()
    {
        Txt_SayiAl_Warning = SayiAl_Warning_GO.GetComponentInChildren<TMP_Text>();
        Txt_BlackBoard_Warning_instance = Txt_SayiAl_Warning;
        inventory = FindObjectOfType<instance_Player_Inventory>();
    }

    private void OnEnable()
    {
        RastgeleUret();
    }

    #region UI yönetimi için

    //btn_RastgeleUret
    int maxDongu = 0;
    int cevap;
    public int MaxSayiDegeri = 999;
    public void RastgeleUret()
    {
        //Eğer cooldown dolmadıysa.
        RastgeleUretMethodu();
        //Eğer dolduysa
        //Ekrana mesaj gönder.
        //Pencereyi kapat.
    }

    public void RastgeleUretMethodu()
    {
        int OperatorChance = Random.Range(0, 2); //max value dahil değil.
        string Operator;
        if (OperatorChance == 0)
            Operator = "+";
        else
            Operator = "-";

        int sayi1 = Random.Range(0, MaxSayiDegeri);
        int sayi2 = Random.Range(0, MaxSayiDegeri);
        if (Operator == "-")
        {
            while (sayi1 - sayi2 < 0)
            {
                sayi1 = Random.Range(0, MaxSayiDegeri);
                sayi2 = Random.Range(0, MaxSayiDegeri);
                maxDongu++;
                if (maxDongu == 100 || sayi1 - sayi2 > 0)
                {
                    maxDongu = 0;
                    break;
                }
            }
            cevap = sayi1 - sayi2;
        }
        else
        {
            cevap = sayi1 + sayi2;
        }

        //Her üretilen sayıdan sonra cooldown için süreyi ayarla.
        Debug.LogError($"while döngüsü {maxDongu} kere döndü");
        Debug.LogWarning($"İhtimal : {OperatorChance} --> 0 ise + değil ise -");
        Debug.LogError($"İşlem {Operator} olarak seçildi.");
        txt_Soru.text = $"Soru : {sayi1} {Operator} {sayi2}";
    }

    //btn_Sil
    public void Sil_islemi()
    {
        if (txt_SayiAl_InputField.text != "")
            txt_SayiAl_InputField.text = txt_SayiAl_InputField.text.Remove(txt_SayiAl_InputField.text.Length - 1);
    }

    //btn_Geri
    public void GeriGel()
    {
        SayiAl_UI.SetActive(false);
        txt_SayiAl_InputField.text = "";
        Giris_UI.SetActive(true);
    }

    //btn_Cevapla
    public void Cevapla()
    {
        if(txt_SayiAl_InputField.text == cevap.ToString()) //Eğer cevap doğruysa
        {
            int rndNumber = Random.Range(0, 10);
            var _tag = inventory.FindTheTag(rndNumber);
            if (inventory.inventory_Capacity > 0) //Eğer envanter kapasitesi yeterliyse
            {
                Txt_SayiAl_Warning.text = $"Kazandığın Sayı \"{rndNumber}\""; //Ekrana bildirim için text'i ayarla.
                inventory.CapacityHasChanged(_tag, 1); //Sayıyı envantere ekle
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 2.5f)); //Ekrana bildirimi ver.
                txt_SayiAl_InputField.text = ""; //Input alanındaki texti temizle.
                RastgeleUretMethodu(); //Rastgele başka bir işlem üret.
            }
            else //Eğer envanter kapasitesi yeterli değilse
            {
                Txt_SayiAl_Warning.text = "Envanterin Dolu."; //Ekrana bildirim için text'i ayarla.
                txt_SayiAl_InputField.text = ""; //Input alanındaki texti temizle.
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f)); //Ekrana bildirimi ver.
                RastgeleUretMethodu(); //Rastgele başka bir işlem üret.
            }
            //cooldown'ı kontrol et.
            //Eğer cooldown bittiyse ekranı kapat.
            //ve bunu giris ekranındaki sayı al butonuna bildir.
            //böylece süre dolmadan ekranı açamaz.
            //cooldown bitmemişse devamke..
        }
        else
        {
            Txt_SayiAl_Warning.text = "Yanlış Cevap!";
            txt_SayiAl_InputField.text = "";
            RastgeleUretMethodu();
            StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            //Soru yenilenirken mi cooldown arttırılmalı yoksa burada mı?

        }
    }

    #endregion

        
    //Sayıyı al.
    //Eğer ekranda yeterli alan yoksa uyarı ver ve sayıyı yazma.
    //Sayıyı ekrana yazdır.
    //Cevabı kontrol et.
    //Eğer cevap doğru değilse uyarı ver.
    //Cevap doğruysa rastgele bir sayı ver.
    //-----Sayıyı yazdırmak için-----\\
    public void Sayiyi_Yazdir(string _YazilacakSayi_Tag)
    {
        //---Sayi_0
        if (_YazilacakSayi_Tag == "Sayı_0") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 0; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }
        //---Sayi_1
        if (_YazilacakSayi_Tag == "Sayı_1") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 1; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }
        //---Sayi_2
        if (_YazilacakSayi_Tag == "Sayı_2") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 2; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }
        //---Sayi_3
        if (_YazilacakSayi_Tag == "Sayı_3") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 3; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }
        //---Sayi_4
        if (_YazilacakSayi_Tag == "Sayı_4") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 4; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }
        //---Sayi_5
        if (_YazilacakSayi_Tag == "Sayı_5") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 5; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }
        //---Sayi_6
        if (_YazilacakSayi_Tag == "Sayı_6") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 6; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }
        //---Sayi_7
        if (_YazilacakSayi_Tag == "Sayı_7") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 7; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }
        //---Sayi_8
        if (_YazilacakSayi_Tag == "Sayı_8") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 8; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }
        //---Sayi_9
        if (_YazilacakSayi_Tag == "Sayı_9") //---Envantere bildirilecek sayı bu ise
        {
            if (txt_SayiAl_InputField.text.Length < Text_MaxLenght)
                txt_SayiAl_InputField.text += 9; //---Sayıyı InputField'a yazdır.
            else
            {
                Txt_SayiAl_Warning.text = "Daha fazla basamak giremezsin.";
                StartCoroutine(WaitAndDisable(SayiAl_Warning_GO, 1.5f));
            }
        }

    }

    IEnumerator WaitAndDisable(GameObject _target, float _GivenSeconds)
    {
        _target.SetActive(true);
        yield return new WaitForSeconds(_GivenSeconds);
        _target.SetActive(false);
        Txt_SayiAl_Warning = Txt_BlackBoard_Warning_instance;
    }
}
