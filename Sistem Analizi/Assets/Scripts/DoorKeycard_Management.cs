using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(instance_OpenTheDoor))]
public class DoorKeycard_Management : MonoBehaviour
{
    instance_Player_Inventory inventory;
    instance_OpenTheDoor openTheDoor;
    [SerializeField] bool yesil, sari, kirmizi;

    [Tooltip("Sadece 1 parent ve 1 child nesneden oluşan ve child nesnesi text olan Uyarı Gameobject'ini buraya yerleştirin.")]
    [SerializeField] GameObject UyariUI = null;
    [SerializeField] string txt_Uyari = "Gereken Keycardlar olamadan bu kapıyı açamazsın.";
    float uyariSuresi = 3.5f;
    // Start is called before the first frame update
    void Awake()
    {
        inventory = FindObjectOfType<instance_Player_Inventory>();
        openTheDoor = GetComponent<instance_OpenTheDoor>();
        if (yesil || sari || kirmizi) { openTheDoor.AllowToOpen = false; }
        else openTheDoor.AllowToOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (openTheDoor.TriggerEntered && !openTheDoor.DoorIsLocked)
        {
            if (!yesil && !sari && !kirmizi)
            {
                openTheDoor.AllowToOpen = true;
                return;
            }
            openTheDoor.SetTheInfoTxt(Notification());
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!islemBasarili(yesil, sari, kirmizi)) UyariVer(uyariSuresi, txt_Uyari);
                openTheDoor.SetTheInfoTxt(Notification());
            }
        }
    }
    /// <summary>
    /// İşlem başarılı değilse false döner.
    /// </summary>
    private bool islemBasarili(bool _yesil, bool _sari, bool _kirmizi)
    {
        bool tempYesil = _yesil;
        bool tempSari = _sari;
        bool tempKirmizi = _kirmizi;

        if (_yesil && inventory.RemoveKeycardSuccess("green"))
            yesil = false;
        if (_sari && inventory.RemoveKeycardSuccess("yellow"))
            sari = false;
        if (_kirmizi && inventory.RemoveKeycardSuccess("red"))
            kirmizi = false;

        _yesil = yesil;
        _sari = sari;
        _kirmizi = kirmizi;

        if (_yesil == tempYesil && _sari == tempSari && _kirmizi == tempKirmizi)
            return false;
        else return true;

    }

    const string Yesil_renkliYazdir = "<b><color=green>Yeşil</color></b>";
    const string Sari_renkliYazdir = "<b><color=yellow>Sarı</color></b>";
    const string Kirmizi_renkliYazdir = "<b><color=red>Kırmızı</color></b>";

    private string Notification()
    {
        string str = null;
        if (yesil && sari && kirmizi)
            return str = $"Bu kapıyı açmak için {Yesil_renkliYazdir}, {Sari_renkliYazdir} ve {Kirmizi_renkliYazdir} anahtarlara ihtiyacın var.";

        else if (yesil && sari && !kirmizi)
            return str = $"Bu kapıyı açmak için {Yesil_renkliYazdir} ve {Sari_renkliYazdir} anahtarlara ihtiyacın var.";

        else if (yesil && !sari && kirmizi)
            return str = $"Bu kapıyı açmak için {Yesil_renkliYazdir} ve {Kirmizi_renkliYazdir} anahtarlara ihtiyacın var.";

        else if (!yesil && sari && kirmizi)
            return str = $"Bu kapıyı açmak için {Sari_renkliYazdir} ve {Kirmizi_renkliYazdir} anahtarlara ihtiyacın var.";

        else if (yesil && !sari && !kirmizi)
            return str = $"Bu kapıyı açmak için {Yesil_renkliYazdir} anahtara ihtiyacın var.";

        else if (!yesil && sari && !kirmizi)
            return str = $"Bu kapıyı açmak için {Sari_renkliYazdir} anahtara ihtiyacın var.";

        else if (!yesil && !sari && kirmizi)
            return str = $"Bu kapıyı açmak için {Kirmizi_renkliYazdir} anahtara ihtiyacın var.";

        else return str;
    }

    #region POOLING SYSTEM FOR WARNING SCREEN
    Queue<GameObject> UyariNesneleri = new Queue<GameObject>();
    bool CoroutineWorking = false;
    void UyariVer(float second, string text)
    {
        if (CoroutineWorking == false)
        {
            GameObject nesne = HavuzdanAl();
            TMPro.TMP_Text txt = nesne.GetComponentInChildren<TMPro.TMP_Text>();
            txt.text = text;
            StartCoroutine(UyariVerCoroutine(nesne, second));
        }
    }
    IEnumerator UyariVerCoroutine(GameObject nesne, float _second)
    {
        CoroutineWorking = true;
        nesne.SetActive(true);
        yield return new WaitForSeconds(_second);
        nesne.SetActive(false);
        UyariNesneleri.Enqueue(nesne);
        CoroutineWorking = false;
    }

    private GameObject HavuzdanAl()
    {
        if (UyariNesneleri.Count == 0)
            HavuzaEkle(1);
        return UyariNesneleri.Dequeue();
    }

    private void HavuzaEkle(int v)
    {
        for (int i = 0; i < v; i++)
        {
            var Uyari = Instantiate(UyariUI);
            Uyari.SetActive(false);
            UyariNesneleri.Enqueue(Uyari);
        }
    }

    #endregion
}
