using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class instance_OpenTheDoor : MonoBehaviour
{
    public bool DoorIsLocked
    {
        get => _doorIsLocked;
        set
        {
            if (value != _doorIsLocked) SetTheInfoTxt(DoorIsOpen ? Str_DoorIsOpen_Dialog : Str_DoorIsClosed_Dialog);
            SetUpManagedDoor();
            _doorIsLocked = value;
        }
    }
    [Tooltip("Kapı kilitliyse bu alanı işaretleyin.")]
    [SerializeField] private bool _doorIsLocked;

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

    [Tooltip("Örnek Soru : 5 + ? = 8, Cevap = 3")]
    public string DoorLockedQuestion = "Question";

    [Tooltip("Sorunun cevabı olması gereken sayıyı buraya yazın. Örnek : 3")]
    public string DoorLockedPassword = "0";
    
    [Tooltip("Sahnede bulunan Canvaslar GO içinden bu kısımları doldurun.")]
    public TMP_Text Txt_Notification,
                    Txt_Soru;

    [SerializeField] string Str_DoorIsOpen_Dialog = " ", Str_DoorIsClosed_Dialog = " ", Str__doorIsLocked_Dialog = " ";

    //----Door Opening And Closing Animation Variables----\\
    Animator anim;
    bool DoorIsOpen;

    public bool AllowToOpen = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (_doorIsLocked)
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
            Debug.LogWarning("Kapı geri bildirim vermiyor. Bir Text eklemeyi unuttunuz.");
    }

    private void Start()
    {
        //if(DoorLevel != Door_and_Keycard_Level.None && _doorIsLocked)
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
                SetUpManagedDoor();
            }
            else
                Debug.LogWarning("OtherDoor is not empty but door has no instance_OpenTheDoor on it --> " + this.name);
        }

        #endregion
    }

    public bool TriggerEntered;
    private void Update()
    {
        if (TriggerEntered && Time.timeScale != 0)
        {
            if (_doorIsLocked)
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
            }
            else if (!_doorIsLocked && AllowToOpen && Input.GetKeyDown(KeyCode.F))
            {
                DoorMovement();
                SetUpManagedDoor();
                if (ManageDoorScript != null)
                {
                    ManageDoorScript.DoorMovement();
                }
            }
        }
    }

    private void SetUpManagedDoor()
    {
        if (ManageDoorScript != null)
        {
            ManageDoorScript.AllowToOpen = this.AllowToOpen;
            ManageDoorScript.DoorIsLocked = this.DoorIsLocked;
            ManageDoorScript.IsThisLeftSide = !this.IsThisLeftSide;
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
            if (_doorIsLocked)
            {
                Txt_Soru.text = DoorLockedQuestion;
                btn_manager.GetTheScriptFromDoor(this.gameObject);
                SetTheInfoTxt(Str__doorIsLocked_Dialog);
            }
            else if(!_doorIsLocked && AllowToOpen) { SetTheInfoTxt(DoorIsOpen ? Str_DoorIsOpen_Dialog : Str_DoorIsClosed_Dialog); }
        }
    }

    public void SetTheInfoTxt(string text)
    {
        if(text == null)
            text = DoorIsOpen ? Str_DoorIsOpen_Dialog : Str_DoorIsClosed_Dialog;
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
        if (!AllowToOpen) return;

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

        SetTheInfoTxt(null);

    }
}
