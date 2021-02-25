using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SayiAl_UI_Management : MonoBehaviour
{
    instance_Player_Inventory inventory;

    [SerializeField] GameObject Giris_UI = null;

    [SerializeField] TMP_Text txt_SayiAl_InputField = null, 
        txt_Soru = null;
    TMP_Text txt_Uyari_UI;

    [Tooltip("Input field'a maksimum bu kadar basamak girilebilir.")]
    [SerializeField] int InputFiedlMaxTextLenght = 7;
    [Tooltip("Soru üretilirken maksimum üretilecek sayı değeri")]
    [SerializeField] int MaxSayiDegeri = 999;
    [Tooltip("Uyari_Ekrani")]
    [SerializeField] GameObject Uyari_UI = null;
    [SerializeField] float UyariSuresi = 1.5f;
    int cevap;
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
            else if (Input.GetKeyDown(KeyCode.Space)) btn_SoruUret();
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) btn_Cevapla();
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

    private void OnEnable() => btn_SoruUret();
    private void OnDisable()
    {
        if (Uyari_UI != null)
            Uyari_UI.SetActive(false);
        txt_SayiAl_InputField.text = "";
    }

    //Sayı butonları için
    public void btn_OnClick(int btnDeger)
    {
        if (txt_SayiAl_InputField.text.Length < InputFiedlMaxTextLenght) txt_SayiAl_InputField.text += btnDeger;
        else StartCoroutine(UyariVer(Uyari_UI, BasamakAsildi, UyariSuresi));
    }

    //btn_Geri
    public void btn_Geri()
    {
        this.gameObject.SetActive(false);
        txt_SayiAl_InputField.text = "";
        Giris_UI.SetActive(true);
    }

    //btn_SoruUret
    public void btn_SoruUret()
    {
        int OperatorChance = Random.Range(0, 2);
        int sayi1 = Random.Range(0, MaxSayiDegeri);
        int sayi2 = Random.Range(0, MaxSayiDegeri);
        string Operator = OperatorChance == 0 ? "+" : "-";
        if (OperatorChance == 0)
            cevap = sayi1 + sayi2;
        else
        {
            while (sayi1 - sayi2 < 0)
            {
                sayi1 = Random.Range(0, MaxSayiDegeri);
                sayi2 = Random.Range(0, MaxSayiDegeri);
                if (sayi1 - sayi2 > 0)
                    break;
            }
            cevap = sayi1 - sayi2;
        }
        txt_Soru.text = $"Soru : {sayi1} {Operator} {sayi2}";
    }

    //btn_Sil
    public void btn_Sil()
    {
        if (txt_SayiAl_InputField.text != "")
            txt_SayiAl_InputField.text = txt_SayiAl_InputField.text.Remove(txt_SayiAl_InputField.text.Length - 1);
    }

    //btn_Cevapla
    public void btn_Cevapla()
    {
        if (inventory.Inventory_Capacity > 0)
        {
            if(txt_SayiAl_InputField.text == "")
                StartCoroutine(UyariVer(Uyari_UI, "Boş bırakmana gerek yok, puan kaybetmezsin :)", UyariSuresi));
            else if (txt_SayiAl_InputField.text == cevap.ToString())
            {
                int _rndNumber = Random.Range(0, 10);
                inventory.Sayi_Ekle(_rndNumber, 1);
                StartCoroutine(UyariVer(Uyari_UI, $"Kazandığın Sayı \"{_rndNumber}\"", UyariSuresi)); //Ekrana bildirimi ver.
                txt_SayiAl_InputField.text = ""; //Input alanındaki texti temizle.
                btn_SoruUret(); //Rastgele başka bir işlem üret.
            }
            else
                StartCoroutine(UyariVer(Uyari_UI, "Yanlış Cevap!", UyariSuresi));
        }
        else
        {
            txt_SayiAl_InputField.text = "";
            StartCoroutine(UyariVer(Uyari_UI, "Envanterin Dolu.", UyariSuresi));
        }
    }

    IEnumerator UyariVer(GameObject _target, string UyariText, float _GivenSeconds)
    {
        txt_Uyari_UI.text = UyariText;
        _target.SetActive(true);
        yield return new WaitForSeconds(_GivenSeconds);
        _target.SetActive(false);
    }
}
