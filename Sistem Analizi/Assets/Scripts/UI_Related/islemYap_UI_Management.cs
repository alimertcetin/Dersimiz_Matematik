using System.Collections;
using UnityEngine;
using TMPro;

public class islemYap_UI_Management : MonoBehaviour
{
    instance_Player_Inventory inventory;
    [SerializeField] TMP_Text txt_islemYap_InputField = null, txt_ReviewInput = null;

    TMP_Text txt_Uyari_UI;
    [Tooltip("Uyari_Ekrani")]
    [SerializeField] GameObject Uyari_UI = null, Giris_UI = null;
    [SerializeField] float UyariSuresi = 1.5f;
    [Tooltip("Input field'a maksimum bu kadar basamak girilebilir.")]
    [SerializeField] int InputFiedlMaxTextLenght = 7;

    int? sayi1;
    bool toplama = false, cikarma = false;
    const string BasamakAsildi = "Daha fazla basamak giremezsin.";

    private void Awake()
    {
        if (Uyari_UI == null)
            Debug.LogError(this.name + " üzerine Uyarı UI atanmamış.");
        inventory = FindObjectOfType<instance_Player_Inventory>();
        txt_Uyari_UI = Uyari_UI.GetComponentInChildren<TMP_Text>();
    }

    float Maintimer;
    float timer;
    float controlEachDeleteTime = .2f;
    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                btn_Geri();
                return;
            }
            if (Input.GetKeyDown(KeyCode.Backspace)) btn_Sil();
            else if (Input.GetKey(KeyCode.Backspace))
            {
                timer += Time.deltaTime;
                Maintimer += Time.deltaTime;
                if (timer > controlEachDeleteTime)
                {
                    btn_Sil();
                    timer = 0;
                }
                if (Maintimer > controlEachDeleteTime * 5)
                {
                    btn_Sil();
                    timer = 0;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Backspace))
            {
                timer = 0;
                Maintimer = 0;
            }
            if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus)) btn_Topla();
            else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) btn_Cikar();
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) btn_SonucuGoster();
            else if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0)) btn_OnClick(0);
            else if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) btn_OnClick(1);
            else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) btn_OnClick(2);
            else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) btn_OnClick(3);
            else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) btn_OnClick(4);
            else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) btn_OnClick(5);
            else if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) btn_OnClick(6);
            else if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7)) btn_OnClick(7);
            else if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8)) btn_OnClick(8);
            else if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9)) btn_OnClick(9);
        }
    }

    private void OnDisable()
    {
        if (Uyari_UI != null)
            Uyari_UI.SetActive(false);
        if (sayi1 != null || txt_islemYap_InputField.text != null) CancelOperation(ref sayi1);
        txt_islemYap_InputField.text = "";
    }

    //btn_OnClick
    public void btn_OnClick(int btnDeger)
    {
        if (inventory.InventoryControl_Sayi(btnDeger))
        {
            if (txt_islemYap_InputField.text.Length < InputFiedlMaxTextLenght)
            {
                txt_islemYap_InputField.text += btnDeger;
                inventory.Sayi_Cikar(btnDeger, 1);
            }
            else
                StartCoroutine(UyariVer(Uyari_UI, BasamakAsildi, UyariSuresi));
        }
        else
            StartCoroutine(UyariVer(Uyari_UI, "Bu rakam envanterinde yok!", UyariSuresi));
    }

    //btn_Geri
    public void btn_Geri()
    {
        this.gameObject.SetActive(false);
        Giris_UI.SetActive(true);
    }

    //btn_Sil
    public void btn_Sil()
    {
        if (txt_islemYap_InputField.text.Length == 0)
            return;
        else
        {
            //Son girilen yazıyı bul ve last input'a ata.
            string last_input = txt_islemYap_InputField.text.Substring(txt_islemYap_InputField.text.Length - 1);
            //Son girilen yazıyı sil.
            txt_islemYap_InputField.text = txt_islemYap_InputField.text.Remove(txt_islemYap_InputField.text.Length - 1);
            inventory.Sayi_Ekle(int.Parse(last_input), 1);
        }
    }

    //btn_Topla
    public void btn_Topla()
    {
        var sonuc = int.TryParse(txt_islemYap_InputField.text, out int _sayi);
        if (sonuc && !cikarma && !toplama)
        {
            sayi1 = _sayi;
            txt_islemYap_InputField.text = "";
            txt_ReviewInput.text = "Girdiğiniz sayı \"" + sayi1.ToString() + "\" ve işlem Toplama.";
            cikarma = false;
            toplama = true;
            sonuc = false;
        }
        else if(!sonuc && toplama || cikarma)
            StartCoroutine(UyariVer(Uyari_UI, "Önce seçtiğin işlemi tamamla veya iptal et!", UyariSuresi));
        else
            StartCoroutine(UyariVer(Uyari_UI, "Önce sayı girmelisin!", UyariSuresi));
    }

    //btn_Çıkar
    public void btn_Cikar()
    {
        var sonuc = int.TryParse(txt_islemYap_InputField.text, out int _sayi);
        if (sonuc && !cikarma && !toplama)
        {
            sayi1 = _sayi;
            txt_islemYap_InputField.text = "";
            txt_ReviewInput.text = "Girdiğiniz sayı \"" + sayi1.ToString() + "\" ve işlem Çıkarma.";
            cikarma = true;
            toplama = false;
            sonuc = false;
        }
        else if (!sonuc && toplama || cikarma)
            StartCoroutine(UyariVer(Uyari_UI, "Önce seçtiğin işlemi tamamla veya iptal et!", UyariSuresi));
        else
            StartCoroutine(UyariVer(Uyari_UI, "Önce sayı girmelisin!", UyariSuresi));
    }

    //btn_SonucuGoster
    public void btn_SonucuGoster()
    {
        if (toplama || cikarma)
        {
            int.TryParse(txt_islemYap_InputField.text, out int _sayi2);
            var sonuc = islemiYap(sayi1, _sayi2, out int? islemSonucu);
            if (sonuc)
            {
                txt_islemYap_InputField.text = islemSonucu.ToString();
                txt_ReviewInput.text = "";
                toplama = false;
                cikarma = false;
                sayi1 = null;
            }
            else if((int)islemSonucu < 0)
                StartCoroutine(UyariVer(Uyari_UI, "İşlem sonucu 0'dan küçük olamaz!", UyariSuresi));
            else if(txt_islemYap_InputField.text == "")
                StartCoroutine(UyariVer(Uyari_UI, "Bir \"Sayı\" girmelisin!", UyariSuresi));
            else
                StartCoroutine(UyariVer(Uyari_UI, "Bir terslik oldu. işlem seçip sayı girdiğinden emin misin?", UyariSuresi));
        }
        else StartCoroutine(UyariVer(Uyari_UI, "Öncelikle işlem seçip sayı girmelisin!", UyariSuresi));

    }

    /// <summary>
    /// İşlem sonucu 0dan küçükse false döner.
    /// </summary>
    bool islemiYap(int? _sayi1, int? _sayi2, out int? sonuc)
    {
        if (toplama)
        {
            sonuc = (int)_sayi1 + (int)_sayi2;
            return true;
        }
        else
        {
            if (_sayi1 - _sayi2 >= 0)
            {
                sonuc = _sayi1 - _sayi2;
                return true;
            }
            else
            {
                sonuc = null;
                return false;
            }
        }
    }
    /// <summary>
    /// Removes any number found in the txt_islemYap_InputField
    /// And clears current operations of operators.
    /// </summary>
    void CancelOperation(ref int? _sayi1)
    {
        string str_sayi1 = _sayi1.ToString();
        while (str_sayi1.Length != 0)
        {
            var last_number = str_sayi1.Substring(str_sayi1.Length - 1);
            str_sayi1 = str_sayi1.Remove(str_sayi1.Length - 1);
            inventory.Sayi_Ekle(int.Parse(last_number), 1);
            if (str_sayi1.Length == 0) break;
        }
        //sayi2 direkt inputField üzerinden alınıyor. Değişkene aktarılmıyor.
        while (txt_islemYap_InputField.text.Length != 0 || !string.IsNullOrEmpty(txt_islemYap_InputField.text))
        {
            string last_input = txt_islemYap_InputField.text.Substring(txt_islemYap_InputField.text.Length - 1);
            txt_islemYap_InputField.text = txt_islemYap_InputField.text.Remove(txt_islemYap_InputField.text.Length - 1);
            inventory.Sayi_Ekle(int.Parse(last_input), 1);
            if (txt_islemYap_InputField.text.Length == 0) break;
        }
        _sayi1 = null;
        txt_ReviewInput.text = "";
        toplama = false;
        cikarma = false;
    }

    IEnumerator UyariVer(GameObject _target, string UyariText, float _GivenSeconds)
    {
        txt_Uyari_UI.text = UyariText;
        _target.SetActive(true);
        yield return new WaitForSeconds(_GivenSeconds);
        _target.SetActive(false);
    }
}
