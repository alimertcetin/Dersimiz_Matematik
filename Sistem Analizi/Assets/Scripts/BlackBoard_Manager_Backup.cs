//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class BlackBoard_Manager_Backup : MonoBehaviour
//{
//    #region BlackBoard için buttonların kullanacağı methodlar
//    //1. Açılan pencerede SayiAl ve islemYap olarak 2 buton var.
    
//    instance_Player_Inventory inventory;
//    instance_LittlePeopleController Lp_Controller;

//    [SerializeField]
//    TMP_Text txt_BlackBoard_InputField = null, txt_Sayi1_info = null;

//    [SerializeField]
//    GameObject GO_BlackBoard_Warning = null, //The GameObject that has the txt_Waning on it.
//        Giris_UI = null,
//        islemYap_UI = null,
//        SayiAl_UI = null;

//    TMP_Text Txt_BlackBoard_Warning; //txt_Warning
    
//    [SerializeField]
//    float BeklenecekSure = 1.5f; //Uyarı için Coroutine bu değişkeni kullanıyor.

//    //----Sayı butonları için----\\
//    /// <summary>
//    /// Text'e girilebilecek maks karakter sayısı.
//    /// </summary>
//    public int Text_IstenenUzunluk = 3;
//    ///<summary>
//    ///Daha fazla sayı giremezsin.
//    ///</summary>
//    string Text_Warning1 = "Daha fazla sayı giremezsin.";
//    ///<summary>
//    ///Sayı envanterde bulunmuyor.
//    ///</summary>
//    string Text_Warning2 = "Bu sayı, envanterinde yok.";
    

//    //----Operatörler için----\\
//    int? sayi1, sayi2;
//    bool islem_Toplama, islem_Cikartma;

//    private void Awake()
//    {
//        SayiAl_UI.SetActive(false);
//        islemYap_UI.SetActive(false);
//        this.gameObject.SetActive(false);

//        inventory = FindObjectOfType<instance_Player_Inventory>();
//        Lp_Controller = FindObjectOfType<instance_LittlePeopleController>();

//        if (txt_Sayi1_info != null)
//            txt_Sayi1_info.text = "";
//        else Debug.LogError("Sayi1 için info texti eklemeyi unuttunuz.");

//        if (GO_BlackBoard_Warning != null) //If GO_BlackBoard_Warning is not null.
//        {
//            Txt_BlackBoard_Warning = GO_BlackBoard_Warning.GetComponentInChildren<TMP_Text>(); //Get text from it's children.
//        }
//        else
//        {
//            Debug.LogError("BlackBoard uyarı ekranı için Gameobject eklemeyi unuttunuz! -> BlackBoard_Manager.cs");
//        }
//    }

//    private void OnEnable()
//    {
//        if(Cursor.visible == false)
//        {
//            Cursor.lockState = CursorLockMode.None;
//            Cursor.visible = true;
//        }
//    }

//    private void OnDisable()
//    {
//        CancelOperation();
//        GO_BlackBoard_Warning.SetActive(false);
//    }

//    string FindTheTag(int silinenSayi)
//    {
//        if (silinenSayi == 0)
//        {
//            return "Sayı_0";
//        }
//        else if (silinenSayi == 1)
//        {
//            return "Sayı_1";
//        }
//        else if (silinenSayi == 2)
//        {
//            return "Sayı_2";
//        }
//        else if (silinenSayi == 3)
//        {
//            return "Sayı_3";
//        }
//        else if (silinenSayi == 4)
//        {
//            return "Sayı_4";
//        }
//        else if (silinenSayi == 5)
//        {
//            return "Sayı_5";
//        }
//        else if (silinenSayi == 6)
//        {
//            return "Sayı_6";
//        }
//        else if (silinenSayi == 7)
//        {
//            return "Sayı_7";
//        }
//        else if (silinenSayi == 8)
//        {
//            return "Sayı_8";
//        }
//        else if (silinenSayi == 9)
//        {
//            return "Sayı_9";
//        }
//        else
//            return null;
//    }


//    /// <summary>
//    /// Removes any number found in the txt_BlackBoard_InputField
//    /// And clears current operations of operators.
//    /// </summary>
//    void CancelOperation()
//    {
//        while (txt_BlackBoard_InputField.text.Length != 0)
//        {
//            //Son girilen yazıyı bul ve last input'a ata.
//            string last_input = txt_BlackBoard_InputField.text.Substring(txt_BlackBoard_InputField.text.Length - 1);
//            //Son girilen yazıyı sil.
//            txt_BlackBoard_InputField.text = txt_BlackBoard_InputField.text.Remove(txt_BlackBoard_InputField.text.Length - 1);

//            //Silinen yazının hangi sayı olduğunu bul
//            if (int.TryParse(last_input, out int _Last_input))
//            {
//                var _tag = FindTheTag(_Last_input);
//                inventory.CapacityHasChanged(_tag, 1); //Envanter kapasitesini düzenle
//            }
//            else
//                continue;
//        }

//        islemYap_UI.SetActive(false);
//        Giris_UI.SetActive(true);
//        islem_Toplama = false;
//        islem_Cikartma = false;

//        string str_sayi1 = "";
//        string str_sayi2 = "";
//        if (sayi1 != null)
//            str_sayi1 = sayi1.ToString();

//        if (sayi2 != null)
//            str_sayi2 = sayi2.ToString();

//        int Sayi1_i = 0;
//        if(sayi1 != null) //Sayi1
//        {
//            while (str_sayi1.Length != 0)
//            {
//                var last_number = str_sayi1.Substring(str_sayi1.Length - 1);
//                str_sayi1 = str_sayi1.Remove(str_sayi1.Length - 1);

//                if(!int.TryParse(last_number, out int sayi))
//                {
//                    Debug.LogError("sayi1 0 değil ve sayi1'in uzunluğu 0dam büyük ancak sayinin son basamağını alma girişimi başarısız oldu!");
//                    break;
//                }
//                var _tag = FindTheTag(int.Parse(last_number));
//                inventory.CapacityHasChanged(_tag, 1);
//                if(str_sayi1.Length == 0)
//                {
//                    sayi1 = null;
//                    break;
//                }

//                Sayi1_i++;
//                if (Sayi1_i == 10)
//                {
//                    Debug.Log("Sayi2 için while döngüsü kırıldı." + Sayi1_i + "kere tekrar etti.");
//                    break;
//                }
//            }
//        }

//        int Sayi2_i = 0;
//        if (sayi2 != null) //Sayi2
//        {
//            while (str_sayi2.Length != 0)
//            {
//                var last_number = str_sayi2.Substring(str_sayi2.Length - 1);
//                str_sayi2 = str_sayi2.Remove(str_sayi2.Length - 1);

//                if (!int.TryParse(last_number, out int sayi))
//                {
//                    Debug.LogError("sayi1 0 değil ve sayi1'in uzunluğu 0dam büyük ancak sayinin son basamağını alma girişimi başarısız oldu!");
//                    break;
//                }
//                var _tag = FindTheTag(int.Parse(last_number));
//                inventory.CapacityHasChanged(_tag, 1);
//                if (str_sayi2.Length == 0)
//                {
//                    sayi2 = null;
//                    break;
//                }

//                Sayi2_i++;
//                if (Sayi2_i == 10)
//                {
//                    Debug.Log("Sayi2 için while döngüsü kırıldı." + Sayi2_i + "kere tekrar etti.");
//                    break;
//                }

//            }
//        }

//    }



//    #region UI Yönetimi için

//    //btn_SayiAl
//    public void SayiAl()
//    {
//        Giris_UI.SetActive(false);
//        SayiAl_UI.SetActive(true);
//    }

//    //btn_islemYap
//    public void islemYap()
//    {
//        Giris_UI.SetActive(false);
//        islemYap_UI.SetActive(true);
//    }

//    //btn_Geri
//    public void GeriGel()
//    {
//        islemYap_UI.SetActive(false);
//        SayiAl_UI.SetActive(false);
//        CancelOperation();
//        Giris_UI.SetActive(true);
//    }

//    //btn_Cikis
//    public void Cikis()
//    {
//        CancelOperation();
//        islemYap_UI.SetActive(false);
//        this.gameObject.SetActive(false);
//        if (!Lp_Controller.Allow_Input)
//            Lp_Controller.Allow_Input = true;
//        if (Cursor.visible == true)
//        {
//            Cursor.lockState = CursorLockMode.Locked;
//            Cursor.visible = false;
//        }
//    }

//    #endregion


//    #region Operatörler için

//    //btn_Sil
//    public void Silme_islemi()
//    {
//        string str_last_input;
//        //Son karakteri al.
//        str_last_input = txt_BlackBoard_InputField.text.Substring(txt_BlackBoard_InputField.text.Length - 1);
//        //Son karakteri sil
//        txt_BlackBoard_InputField.text = txt_BlackBoard_InputField.text.Remove(txt_BlackBoard_InputField.text.Length - 1);
//        bool success = int.TryParse(str_last_input, out int int_last_input);
//        if (success)
//        {
//            var _tag = FindTheTag(int_last_input);
//            inventory.CapacityHasChanged(_tag, 1);
//        }
//        else
//        {
//            islem_Toplama = false;
//            islem_Cikartma = false;
//        }
//    }

//    //btn_Topla
//    public void Toplama_islemi()
//    {
//        var sonuc = int.TryParse(txt_BlackBoard_InputField.text, out int _sayi);
//        if (sonuc && !islem_Toplama && !islem_Cikartma)
//        {
//            sayi1 = _sayi;
//            Debug.Log("Sayi1 = " + sayi1);
//            txt_BlackBoard_InputField.text = "";

//            txt_Sayi1_info.text = sayi1.ToString() + " ve işlem : Toplama";
            
//            islem_Toplama = true;
//            islem_Cikartma = false;
//            sonuc = false;
//        }
//    }

//    //btn_Cikart
//    public void Cikartma_islemi()
//    {
//        var sonuc = int.TryParse(txt_BlackBoard_InputField.text, out int _sayi);
//        if (sonuc && !islem_Cikartma && !islem_Toplama)
//        {
//            sayi1 = _sayi;
//            Debug.Log("Sayi1 = " + sayi1);
//            txt_BlackBoard_InputField.text = "";

//            txt_Sayi1_info.text = sayi1.ToString() + " ve işlem : Çıkartma";

//            islem_Cikartma = true;
//            islem_Toplama = false;
//            sonuc = false;
//        }
//    }


//    // Not : text'i komple sayıya dönüştür.

//    //Sayıyı aldıktan ve storeladıktan sonra
//    //sonucu göstere tıklayınca sayıyı temizle

//    //btn_SonucuGoster
//    public void SonucuGoster()
//    {
//        if (islem_Toplama || islem_Cikartma)
//        {
//            var sonuc = int.TryParse(txt_BlackBoard_InputField.text, out int _sayi2);
//            if (sonuc)
//            {
//                sayi2 = _sayi2;
//                txt_BlackBoard_InputField.text = islemiYap(sayi1, sayi2).ToString();


//                txt_Sayi1_info.text = "";
//                islem_Toplama = false;
//                islem_Cikartma = false;
//                sayi1 = null;
//                sayi2 = null;
//            }
//            else Debug.LogError("Sonucu Göster üzerindeki sonuc " + sonuc + ", yani inputField geçerli sayı değerine sahip değil.");
//        }

//    }

//    int? islemiYap(int? _sayi1, int? _sayi2)
//    {
//        if (islem_Toplama)
//            return _sayi1 + _sayi2;

//        else if (islem_Cikartma)
//        {
//            if (_sayi1 - _sayi2 >= 0)
//                return _sayi1 - sayi2;
//            else
//            {
//                Debug.LogError("Sonuc 0dan küçük olamaz");
//                sayi2 = null;
//                Txt_BlackBoard_Warning.text = "Sayı 0dan küçük olamaz!";
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//                CancelOperation();
//                return null;
//            }
//        }
//        else
//        {
//            Debug.LogError("İşlem bulunamadı.");
//            return null;
//        }
//    }

//    #endregion


//    public void Sayiyi_Yazdir(string _GonderilecekTag)
//    {
//        //---Sayi_0
//        if (_GonderilecekTag == "Sayı_0") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_0 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 0; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }

//        //---Sayi_1
//        else if (_GonderilecekTag == "Sayı_1") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_1 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 1; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }

//        //---Sayi_2
//        else if (_GonderilecekTag == "Sayı_2") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_2 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 2; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }

//        //---Sayi_3
//        else if (_GonderilecekTag == "Sayı_3") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_3 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 3; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }

//        //---Sayi_4
//        else if (_GonderilecekTag == "Sayı_4") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_4 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 4; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }

//        //---Sayi_5
//        else if (_GonderilecekTag == "Sayı_5") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_5 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 5; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }

//        //---Sayi_6
//        else if (_GonderilecekTag == "Sayı_6") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_6 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 6; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }

//        //---Sayi_7
//        else if (_GonderilecekTag == "Sayı_7") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_7 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 7; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }

//        //---Sayi_8
//        else if (_GonderilecekTag == "Sayı_8") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_8 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 8; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }

//        //---Sayi_9
//        else if (_GonderilecekTag == "Sayı_9") //---Envantere bildirilecek sayı bu ise
//        {
//            if (inventory.Sayi_9 > 0) //---Ve eğer envanterde bu sayı varsa
//            {
//                if (txt_BlackBoard_InputField.text.Length <= Text_IstenenUzunluk) //ve txt_BlackBoard_InputField üzerinde yer varsa
//                {
//                    inventory.CapacityHasChanged(_GonderilecekTag, -1); //Kapasiteyi değiştir ve sayıyı eksilt.
//                    txt_BlackBoard_InputField.text += 9; //---Sayıyı InputField'a yazdır.
//                }
//                else //InputField üzerinde yer yoksa
//                {
//                    Txt_BlackBoard_Warning.text = Text_Warning1; //Daha fazla sayı giremeyeceğine dair uyarı.
//                    StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure)); //Uyarı GameObject'i BeklenecekSure boyunca çalıştır.
//                }
//            }
//            else //Sayı envanterde yoksa
//            {
//                Txt_BlackBoard_Warning.text = Text_Warning2; //Daha fazla sayı giremeyeceğine dair uyarı.
//                StartCoroutine(WaitAndDisable(GO_BlackBoard_Warning, BeklenecekSure));
//            }
//        }
        
//    }

//    /// <summary>
//    /// This will enable the object. And when givenSeconds end, object will be disabled.
//    /// </summary>
//    /// <param name="_target">The GameObject that will disable after enabled.</param>
//    /// <param name="_givenSeconds">Determine how many seconds later object will be disabled</param>
//    IEnumerator WaitAndDisable(GameObject _target,float _givenSeconds)
//    {
//        _target.SetActive(true);
//        yield return new WaitForSeconds(_givenSeconds);
//        _target.SetActive(false);
//    }

//    #endregion Sayı butonları için


//}
