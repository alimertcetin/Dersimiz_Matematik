using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class instance_OpenTheDoor : MonoBehaviour
{
    instance_LittlePeopleController LP_Controller;

    //---For those who has second door by side---\\
    instance_OpenTheDoor ManageDoorScript;
    [Header("Kapı İkili bir Kapıysa bu alanı doldurun")]
    [SerializeField]
    [Tooltip("Eğer bu Kapı bir İkili Kapı ise Sağ Kapı üzerindeki Scripte Sol Kapıyı taşıyın " +
        "ve Sağ Kapı üzerinde değişiklikleri yapın.")]
    GameObject ManageThisDoor = null;

    [Header("                                   //--- Kapı Özellikleri ---\\\\")]
    //---For defining door's current status And if its Locked these Variables will be used---\\
    public bool DoorIsLocked; //Kapı kilitli mi değil mi?
    [Tooltip("Örnek Soru : 5 + ? = 8, Cevap = 3")]
    public string DoorLockedQuestion = "Question"; //Kapıyı açmak için kullanılacak soru.
    [Tooltip("Sorunun cevabı olması gereken sayıyı buraya yazın. Örnek : 3")]
    public string DoorLockedPassword = "0"; //Kapının şifresi.
    instance_btnManager btn_manager;

    //----Door Interaction Texts----\\
    public TMP_Text Txt_Notification, //Kapı kilitli mi değil mi? 
                    Txt_Soru; //Kilitli ise canvas açılmadan önce soruyu değiştir. Not : Aynı textBox'ı diğer scriptlerde kullanıyor.
    [SerializeField]
    string Str_DoorIsOpen_Dialog = " ", Str_DoorIsClosed_Dialog = " ", Str_DoorIsLocked_Dialog = " ";
    //Script of Buttons turning this on or off
    public GameObject LockedDoor_UI;

    //----Door Opening And Closing Animation Variables----\\
    Animator anim;
    bool DoorIsOpen;

    // Start is called before the first frame update
    void Awake()
    {
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
        if (TriggerEntered && Time.timeScale != 0) //Eğer Player tarafından tetiklendiyse...
        {
            if (DoorIsLocked) //Kapı kilitliyse...
            {
                if (Input.GetKeyDown(KeyCode.F)) //Kapı kilitliyse ve F basıldıysa.
                {
                    LockedDoor_UI.SetActive(!LockedDoor_UI.activeSelf); //Canvas açıksa kapat,Kapalıysa aç.
                }
                if (LockedDoor_UI.activeSelf) //Canvas açıksa 
                {
                    LP_Controller.Allow_Input = false; //karakterin girişlerini önle.
                    if (Cursor.lockState == CursorLockMode.Locked) // Cursor Locked ise
                    {
                        Cursor.lockState = CursorLockMode.None; //Cursor'ü aktif hale getir
                        Cursor.visible = true; //Cursor'ü görünür hale getir.
                    }
                }
                else //Canvas kapalıysa
                {
                    btn_manager.TextiTamamenTemizle();
                    LP_Controller.Allow_Input = true; //Karakterin girişlerine izin ver.
                    if (Cursor.lockState != CursorLockMode.Locked) //Cursor Locked değilse
                    {
                        Cursor.lockState = CursorLockMode.Locked; //Cursor'ü kilitle
                        Cursor.visible = false; //ve görünmez yap.
                    }
                }
            }

            if (!DoorIsLocked) //Kapı kilitli "değilse" 
            {
                if (Input.GetKeyDown(KeyCode.F)) //ve F basıldıysa.
                    DoorMovement(); //Kapıyı aç veya kapat

                if (Cursor.lockState != CursorLockMode.Locked) // Cursor Locked değilse
                {
                    Cursor.lockState = CursorLockMode.Locked; //Cursor'ü kilitle
                    Cursor.visible = false; //ve görünmez yap.
                }
            }

            //Managing Txt_Notification
            if (DoorIsOpen && !DoorIsLocked) { Txt_Notification.text = Str_DoorIsOpen_Dialog; }
            if (!DoorIsOpen && !DoorIsLocked) { Txt_Notification.text = Str_DoorIsClosed_Dialog; }
            if (Txt_Notification.enabled == false) { Txt_Notification.enabled = true; }

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TriggerEntered = true;
            if (DoorIsLocked)
            {
                Txt_Soru.text = DoorLockedQuestion;
                btn_manager.GetTheScriptFromDoor(this.gameObject);
                Txt_Notification.text = Str_DoorIsLocked_Dialog;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
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

        rnd_AnimSpeed = Random.Range(0.5f, 1.5f);
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
