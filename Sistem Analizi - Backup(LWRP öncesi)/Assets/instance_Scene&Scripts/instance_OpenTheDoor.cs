using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class instance_OpenTheDoor : MonoBehaviour
{
    instance_LittlePeopleController LP_Controller;
    instance_btnManager btn_manager;

    //----Door Interaction Texts----\\
    public bool DoorIsLocked; //Kapı kilitli mi değil mi?
    public string DoorLockedQuestion = "Question"; //Kapıyı açmak için kullanılacak soru.
    public string DoorLockedPassword = "0"; //Kapının şifresi.
    public TMP_Text Txt_Notification, //Kapı kilitli mi değil mi? 
                    Txt_Soru; //Kilitli ise canvas açılmadan önce soruyu değiştir. Aynı textBox'ı diğer scriptlerde kullanıyor.
    [SerializeField]
    string Str_DoorIsOpen_Dialog = " ", Str_DoorIsClosed_Dialog = " ", Str_DoorIsLocked_Dialog = " ";
    //Buttons Script is turning this on or off
    public GameObject LockedDoor_UI;


    //----Door Opening And Closing Animation Variables----\\
    Animator anim;

    //Kapı kilitli ise trigger enterda
    //Kapının kilitli olduğu bildirimi verilecek.
    //Kapıdaki soru LockedDoor_UIdaki Soru textbox'ına aktarılacak.
    //F basılırsa canvas açılacak.
    //Cevap doğruysa Canvas kapatılacak ve Txt_Notification üzerindeki text değiştirilecek.
    //Sonrası kapı kilitli değilmiş gibi devam edecek.
    //F basılırsa kapı açılacak / kapanacak.

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
            Debug.LogError("Couldnt find Door animator. Doors are not gonna open!");

        //Başlangıçta text'i disable et.
        if (Txt_Notification != null)
            Txt_Notification.enabled = false;
        else
        {
            Debug.LogWarning("Kapı geri bildirim vermiyor. Bir Text eklemeyi unuttunuz.");
        }
    }

    bool TriggerEntered;
    private void Update()
    {
        if (TriggerEntered) //Eğer Player tarafından tetiklendiyse...
        {
            if (DoorIsLocked)
            {
                if (Input.GetKeyDown(KeyCode.F)) //Kapı kilitliyse ve F basıldıysa.
                {
                    LockedDoor_UI.SetActive(!LockedDoor_UI.activeSelf); //Canvas açıksa kapat,Kapalıysa aç.
                }
                LP_Controller.Allow_Input = LockedDoor_UI.activeSelf ? false : true; //Canvas açıksa karakterin girişlerini önle.
            }

            if (!DoorIsLocked && Input.GetKeyDown(KeyCode.F)) //Kapı kilitli "değilse" ve F basıldıysa.
            {
                DoorMovement();
            }
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
            if (DoorIsOpen) { Txt_Notification.text = Str_DoorIsOpen_Dialog; }
            else { Txt_Notification.text = Str_DoorIsClosed_Dialog; }
            Txt_Notification.enabled = true;
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
    [SerializeField]
    bool IsThisLeftSide; 
    bool DoorIsOpen, DoorIsClosed;
    void DoorMovement()
    {
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
