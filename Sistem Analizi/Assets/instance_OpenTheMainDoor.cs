using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class instance_OpenTheMainDoor : MonoBehaviour
{
    //---For those who has second door by side---\\
    instance_OpenTheMainDoor ManageDoorScript;

    [Header("Kapı İkili bir Kapıysa bu alanı doldurun")]

    [Tooltip("Eğer bu Kapı bir İkili Kapı ise Sağ Kapı üzerindeki Scripte Sol Kapıyı taşıyın " +
        "ve Sağ Kapı üzerinde değişiklikleri yapın.")]
    [SerializeField] GameObject ManageThisDoor = null;

    [Header("                                   //--- Kapı Özellikleri ---\\\\")]
    //---For defining door's current status And if its Locked these Variables will be used---\\
    public bool DoorIsLocked; //Kapı kilitli mi değil mi?

    //----Door Interaction Texts----\\
    public TMP_Text Txt_Notification; //Kapı kilitli mi değil mi? 

    [Tooltip("Kapının envanteri gibi düşünülebilir. Kapıya bu UI kullanarak anahtar ekleyip çıkartılacak.")]
    public GameObject LockedMainDoor_UI;

    public string Str_DoorIsOpen_Dialog = "Kapatmak için F";
    public string Str_DoorIsClosed_Dialog = "Açmak için F";
    public string Str_DoorIsLocked_Dialog = "Kapı Kilitli!";

    //----Door Opening And Closing Animation Variables----\\
    Animator anim;
    bool DoorIsOpen;

    // Start is called before the first frame update
    void Awake()
    {
        if (DoorIsLocked)
        {
            //Kapı kilitliyse Awakete yapılacak bir şey varsa...
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
            ManageDoorScript = ManageThisDoor.GetComponent<instance_OpenTheMainDoor>();
            if (ManageDoorScript != null)
            {
                var col = ManageThisDoor.gameObject.GetComponent<BoxCollider>();
                if (col.isTrigger)
                    Destroy(col);
                ManageDoorScript.DoorIsLocked = this.DoorIsLocked;
                ManageDoorScript.IsThisLeftSide = !this.IsThisLeftSide;
            }
            else
                Debug.LogWarning("OtherDoor is not empty but door has no instance_OpenTheMainDoor on it --> " + this.name);
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
                    LockedMainDoor_UI.SetActive(!LockedMainDoor_UI.activeSelf); //Canvas açıksa kapat,Kapalıysa aç.
                }
                CursorControl();
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

    /// <summary>
    /// Canvas'ın açık veya kapalı durumuna göre
    /// Cursor'ün görünürlük ve kilitlilik durumlarını kontrol eder
    /// </summary>
    private void CursorControl()
    {
        if (LockedMainDoor_UI.activeSelf) //Canvas açıksa 
        {
            if (Cursor.lockState == CursorLockMode.Locked) // Cursor Locked ise
            {
                Cursor.lockState = CursorLockMode.None; //Cursor'ü aktif hale getir
                Cursor.visible = true; //Cursor'ü görünür hale getir.
            }
        }
        else //Canvas kapalıysa
        {
            if (Cursor.lockState != CursorLockMode.Locked) //Cursor Locked değilse
            {
                Cursor.lockState = CursorLockMode.Locked; //Cursor'ü kilitle
                Cursor.visible = false; //ve görünmez yap.
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerEntered = true;
            if (DoorIsLocked)
            {
                Txt_Notification.text = Str_DoorIsLocked_Dialog;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerEntered = false;
            Txt_Notification.enabled = false;
            if (LockedMainDoor_UI != null)
                LockedMainDoor_UI.SetActive(false);
        }
    }

    //---DoorMovement function Variables---\\
    [SerializeField]
    [Tooltip("Eğer bu kapı sol taraftaysa işaretleyin. Not : Eğer bu ikili bir kapıysa" +
        " ve Sağ Kapı tarafından yönetiliyorsa işaretlemenize gerek yok.")]
    bool IsThisLeftSide = false;
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

        //if true leftSide animations will work.
        if (IsThisLeftSide)
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

        //if IsThisLeftSide is not true; RightSide animations will work.
        else
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
