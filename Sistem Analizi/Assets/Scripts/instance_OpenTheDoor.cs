using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class instance_OpenTheDoor : MonoBehaviour
{
    public Door_and_Keycard_Level DoorLevel;
    [Header("Eğer bu bir ana kapı ise işaretleyin.")]
    [Tooltip("Ana kapılar default olarak Red Keycard gerektirir. İsteğe bağlı olarak yukarıdan iki aşamalı şifre eklenebilir.")]
    public bool IsThisMainDoor = false;

    instance_Player_Inventory inventory;
    instance_LittlePeopleController LP_Controller;
    instance_btnManager btn_manager;
    instance_OpenTheDoor ManageDoorScript;
    #region Header - Tooltip
    [Header("Kapı İkili bir Kapıysa bu alanı doldurun")]
    [Tooltip("Eğer bu Kapı bir İkili Kapı ise Sağ Kapı üzerindeki Scripte Sol Kapıyı taşıyın " +
        "ve Sağ Kapı üzerinde değişiklikleri yapın.")]
    #endregion
    [SerializeField] GameObject ManageThisDoor = null;
    public GameObject LockedDoor_UI;
    [Tooltip("Sadece 1 parent ve 1 child nesneden oluşan ve child nesnesi text olan Uyarı Gameobject'ini buraya yerleştirin.")]
    [SerializeField] GameObject UyariUI = null;
    float uyariSuresi = 3.5f;

    [Header("                                   //--- Kapı Özellikleri ---\\\\")]
    public bool DoorIsLocked;

    [Tooltip("Örnek Soru : 5 + ? = 8, Cevap = 3")]
    public string DoorLockedQuestion = "Question";

    [Tooltip("Sorunun cevabı olması gereken sayıyı buraya yazın. Örnek : 3")]
    public string DoorLockedPassword = "0";
    
    [Tooltip("Sahnede bulunan Canvaslar GO içinden bu kısımları doldurun.")]
    public TMP_Text Txt_Notification,
                    Txt_Soru;

    [SerializeField] string Str_DoorIsOpen_Dialog = " ", Str_DoorIsClosed_Dialog = " ", Str_DoorIsLocked_Dialog = " ";

    //----Door Opening And Closing Animation Variables----\\
    Animator anim;
    bool DoorIsOpen;

    // Start is called before the first frame update
    void Awake()
    {
        inventory = FindObjectOfType<instance_Player_Inventory>();

        if (DoorIsLocked)
        {
            btn_manager = FindObjectOfType<instance_btnManager>();
            LP_Controller = FindObjectOfType<instance_LittlePeopleController>();
        }

        anim = GetComponentInChildren<Animator>();
        if (anim == null)
            Debug.LogError("Couldnt find Door animator. Doors will not open or close!");

        //Başlangıçta text'i disable et.
        if (Txt_Notification != null)
            Txt_Notification.enabled = false;
        else
        {
            Debug.LogWarning("Kapı geri bildirim vermiyor. Bir Text eklemeyi unuttunuz.");
        }
    }

    private void Start()
    {
        //if(DoorLevel != Door_and_Keycard_Level.None && DoorIsLocked)
        //    Debug.LogError("Kapı hem kilitli hem de keycard ile açılma özelliğine sahip olamaz.");


        #region Bu kodlar ikili kapılar için geçerli.

        if (ManageThisDoor != null)
        {
            ManageDoorScript = ManageThisDoor.GetComponent<instance_OpenTheDoor>();
            if (ManageDoorScript != null)
            {
                var col = ManageThisDoor.gameObject.GetComponent<BoxCollider>();
                if (col.isTrigger)
                    Destroy(col);
                ManageDoorScript.DoorIsLocked = this.DoorIsLocked;
                ManageDoorScript.IsThisLeftSide = !this.IsThisLeftSide;
            }
            else
                Debug.LogWarning("OtherDoor is not empty but door has no instance_OpenTheDoor on it --> " + this.name);
        }

        #endregion
    }

    bool TriggerEntered;
    private void Update()
    {
        if (TriggerEntered && Time.timeScale != 0)
        {

            if (IsThisMainDoor || DoorLevel != Door_and_Keycard_Level.None)
            {
                ManageDoorLevel(IsThisMainDoor, DoorLevel != Door_and_Keycard_Level.None ? true : false);
            }

            else if (DoorIsLocked)
            {
                if (Input.GetKeyDown(KeyCode.F))
                    LockedDoor_UI.SetActive(!LockedDoor_UI.activeSelf);

                if (LockedDoor_UI.activeSelf)
                    LP_Controller.Allow_Input = false;
                else
                {
                    btn_manager.TextiTamamenTemizle();
                    LP_Controller.Allow_Input = true;
                }

                SetTheInfoTxt(Str_DoorIsLocked_Dialog);
            }

            else if (!DoorIsLocked && DoorLevel == Door_and_Keycard_Level.None && !IsThisMainDoor) //Kapı açık.
            {
                SetTheInfoTxt(DoorIsOpen ? Str_DoorIsOpen_Dialog : Str_DoorIsClosed_Dialog);

                if (Input.GetKeyDown(KeyCode.F))
                    DoorMovement();
            }

        }
    }

    private void ManageDoorLevel(bool mainDoor, bool StagedDoor)
    {
        bool yesil = false, sari = false, kirmizi = false;
        if (StagedDoor)
        {
            yesil = DoorLevel == Door_and_Keycard_Level.Yesil ? true : false;
            sari = DoorLevel == Door_and_Keycard_Level.Sari ? true : false;
            kirmizi = DoorLevel == Door_and_Keycard_Level.Kirmizi ? true : false;
            if (yesil)
                SetTheInfoTxt("Bu kapı için <color=red>Kırmızı</color> ve <color=green>Yeşil</color> anahtara ihtiyacın var.");
            else if (sari)
                SetTheInfoTxt("Bu kapı için <color=red>Kırmızı</color> ve <color=yellow>Sarı</color> anahtara ihtiyacın var.");
            else if (kirmizi)
                SetTheInfoTxt("Bu kapı için 2 tane <color=red>Kırmızı</color> ihtiyacın var.");
            else
                Debug.LogWarning("ManageDoorLevel/StagedDoor içerisinde bir terslik var ---> " + this.name);
        }

        if (mainDoor && StagedDoor && Input.GetKeyDown(KeyCode.F))
        {
            if (inventory.RemoveKeycardSuccess("red"))
                IsThisMainDoor = false;
            else
                UyariVer(uyariSuresi, "Gereken anahtarlar olmadan bu kapıyı açamazsın.");
        }

        else if (!mainDoor && StagedDoor)
        {
            if (yesil)
            {
                SetTheInfoTxt("Bu kapıyı açmak için <color=green>Yeşil</color> anahtara ihtiyacın var.");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (inventory.RemoveKeycardSuccess("green"))
                        DoorLevel = Door_and_Keycard_Level.None;
                    else
                        UyariVer(uyariSuresi, "Gereken <color=green>Yeşil</color> anahtar olmadan bu kapıyı açamazsın.");
                }
            }

            else if (sari)
            {
                SetTheInfoTxt("Bu kapıyı açmak için <color=yellow>Sarı</color> anahtara ihtiyacın var.");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (inventory.RemoveKeycardSuccess("yellow"))
                        DoorLevel = Door_and_Keycard_Level.None;
                    else
                        UyariVer(uyariSuresi, "Gereken <color=yellow>Sarı</color> anahtar olmadan bu kapıyı açamazsın.");
                }
            }

            else if (kirmizi)
            {
                SetTheInfoTxt("Bu kapıyı açmak için <color=red>Kırmızı</color> anahtara ihtiyacın var.");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (inventory.RemoveKeycardSuccess("red"))
                        DoorLevel = Door_and_Keycard_Level.None;
                    else
                        UyariVer(uyariSuresi, "Gereken <color=red>Kırmızı</color> anahtar olmadan bu kapıyı açamazsın.");
                }
            }
        }

        else if (mainDoor && !StagedDoor)
        {
            SetTheInfoTxt("Bu kapı için <color=red>Kırmızı</color> ihtiyacın var.");
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (inventory.RemoveKeycardSuccess("red"))
                    IsThisMainDoor = false;
                else
                    UyariVer(uyariSuresi, "Gereken <color=red>Kırmızı</color> anahtar olmadan bu kapıyı açamazsın.");
            }
        }

    }

    #region POOLING SYSTEM FOR WARNING SCREEN
    Queue<GameObject> UyariNesneleri = new Queue<GameObject>();
    bool CoroutineWorking = false;
    void UyariVer(float second,string text)
    {
        if (CoroutineWorking == false)
        {
            GameObject nesne = HavuzdanAl();
            TMP_Text txt = nesne.GetComponentInChildren<TMP_Text>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TriggerEntered = true;
            if (DoorIsLocked)
            {
                Txt_Soru.text = DoorLockedQuestion;
                btn_manager.GetTheScriptFromDoor(this.gameObject);
                SetTheInfoTxt(Str_DoorIsLocked_Dialog);
            }
            else if (!DoorIsLocked && !IsThisMainDoor && DoorLevel == Door_and_Keycard_Level.None)
                SetTheInfoTxt(DoorIsOpen ? Str_DoorIsOpen_Dialog : Str_DoorIsClosed_Dialog);
        }
    }

    private void SetTheInfoTxt(string text)
    {
        Txt_Notification.text = text;
        if(!Txt_Notification.enabled) Txt_Notification.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerEntered = false;
            Txt_Notification.enabled = false;
            if (LockedDoor_UI != null)
                LockedDoor_UI.SetActive(false);
        }
    }

    //---DoorMovement function Variables---\\
    [Tooltip("Eğer bu kapı sol taraftaysa işaretleyin. Not : Eğer bu ikili bir kapıysa" +
        " ve Sağ Kapı tarafından yönetiliyorsa işaretlemenize gerek yok.")]
    public bool IsThisLeftSide = false;
    float rnd_AnimSpeed = 0;
    void DoorMovement()
    {
        if (ManageDoorScript != null)
        {
            ManageDoorScript.DoorIsOpen = this.DoorIsOpen;
            ManageDoorScript.DoorMovement();
        }

        rnd_AnimSpeed = UnityEngine.Random.Range(0.5f, 1.5f);
        anim.SetFloat("rndAnimSpeed", rnd_AnimSpeed);
        if (IsThisLeftSide) //if true leftSide animations will work.
        {
            if (!DoorIsOpen)
            {
                anim.SetBool("LeftSide_Close", false);
                anim.SetBool("LeftSide_Open", true);
                DoorIsOpen = true;
            }
            else
            {
                anim.SetBool("LeftSide_Open", false);
                anim.SetBool("LeftSide_Close", true);
                DoorIsOpen = false;
            }
        }

        else //if IsThisLeftSide is not true; RightSide animations will work.
        {
            if (!DoorIsOpen)
            {
                anim.SetBool("RightSide_Close", false);
                anim.SetBool("RightSide_Open", true);
                DoorIsOpen = true;
            }
            else
            {
                anim.SetBool("RightSide_Open", false);
                anim.SetBool("RightSide_Close", true);
                DoorIsOpen = false;
            }
        }

    }
}
