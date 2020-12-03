using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OpenTheDoor : MonoBehaviour
{
    //----Door Rotation Variables----\\
    Transform parent;
    [SerializeField]
    float DoorOpenSpeed = 1, parentStartRot_Y;
    [SerializeField]
    bool TRIGGERED, Rotate_ToOtherSide, AllowToTrigger = true, ReversedRotation;
        public bool KeyPressed_F;
    [SerializeField]
    //DesiredRotaion has to be decimal number not an integer. //Decimal = Ondalık
    float DesiredRotation = 0;

    //----Door Interaction text----\\
    public bool DoorIsLocked;
    public TMP_Text Txt_Door_PressF;
    [SerializeField]
    bool dialog_first, dialog_DoorIs_Open, dialog_DoorIs_Close;
    

    [SerializeField]
    string Str_firstDialog = " ", Str_DoorIsOpen_Dialog = " ", Str_DoorIsClosed_Dialog = " ", Str_DoorIsLocked_Dialog = " ";

    public GameObject LockedCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //Parent objenin transform'u parent'a aktar.
        //Parent'ın başlangıç dönüş değişkenini sakla.
        //parent'ı child objeden ayır.
        parent = this.transform.parent;
        parentStartRot_Y = parent.transform.rotation.y;
        parent.transform.DetachChildren();

        //Başlangıçta text'i disable et.
        if (Txt_Door_PressF != null)
            Txt_Door_PressF.enabled = false;
        else
        {
            Debug.LogWarning("Kapı geri bildirim vermiyor. Bir Text eklemeyi unuttunuz.");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!DoorIsLocked)
            {
                dialog_first = true;
                Txt_Door_PressF.enabled = true;
            }
            else
            {
                Txt_Door_PressF.enabled = true;
            }
        }
    }


    void Char_TriggerStay()
    {
        KeyPressed_F = Input.GetKey(KeyCode.F);
        if (AllowToTrigger && KeyPressed_F && !DoorIsLocked)
        {
            //Arzulanan konum kapının konumundan büyük.
            if (parentStartRot_Y < DesiredRotation)
            {
                ReversedRotation = false;
                if (parent.transform.rotation.y < DesiredRotation)
                {
                    TRIGGERED = true;
                    AllowToTrigger = false;
                }
                else
                {
                    Rotate_ToOtherSide = true;
                    AllowToTrigger = false;
                }
            }
            else
            {
                ReversedRotation = true;
                //Arzulanan konum kapının konumundan küçük.
                if (parent.transform.rotation.y > DesiredRotation)
                {
                    TRIGGERED = true;
                    AllowToTrigger = false;
                }
                else
                {
                    Rotate_ToOtherSide = true;
                    AllowToTrigger = false;
                }
            }
        }
        
        if(DoorIsLocked && KeyPressed_F)
        {
            LockedCanvas.SetActive(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (KeyPressed_F && dialog_first)
            {
                dialog_first = false;
            }
            Char_TriggerStay();
            Text_ViewUpdated();
            Door_OpenMethod();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Txt_Door_PressF.enabled = false;
            LockedCanvas.SetActive(false);
        }
    }

    void Door_OpenMethod()
    {
        if (!ReversedRotation)
        {
            if (TRIGGERED)
            {
                dialog_DoorIs_Close = false;
                dialog_DoorIs_Open = true;
                parent.transform.Rotate(Vector3.up * DoorOpenSpeed * Time.deltaTime);
                if (parent.transform.rotation.y >= DesiredRotation)
                {
                    AllowToTrigger = true;
                    TRIGGERED = false;
                }
            }

            if (Rotate_ToOtherSide)
            {
                dialog_DoorIs_Open = false;
                dialog_DoorIs_Close = true;
                parent.transform.Rotate(Vector3.down * DoorOpenSpeed * Time.deltaTime);
                if (parent.transform.rotation.y <= parentStartRot_Y)
                {
                    AllowToTrigger = true;
                    Rotate_ToOtherSide = false;
                }
            }
        }
        else       //Arzulanan konum kapının konumundan küçük.
        {
            if (TRIGGERED)
            {
                dialog_DoorIs_Close = false;
                dialog_DoorIs_Open = true;
                parent.transform.Rotate(Vector3.up * DoorOpenSpeed * Time.deltaTime);
                Debug.Log("Parent dönüş dereceleri" + parent.transform.rotation);
                if (parent.transform.rotation.y <= DesiredRotation)
                {
                    AllowToTrigger = true;
                    TRIGGERED = false;
                }
            }

            if (Rotate_ToOtherSide)
            {
                dialog_DoorIs_Open = false;
                dialog_DoorIs_Close = true;
                parent.transform.Rotate(Vector3.down * DoorOpenSpeed * Time.deltaTime);
                if (parent.transform.rotation.y >= parentStartRot_Y)
                {
                    AllowToTrigger = true;
                    Rotate_ToOtherSide = false;
                }
            }
        }

    }
    void Text_ViewUpdated()
    {
        if(Str_DoorIsClosed_Dialog == null && Str_DoorIsOpen_Dialog == null && Str_firstDialog == null)
        {
            Debug.LogError("Door Interaction Dialogs cant be null!");
        }
        if(dialog_first)
        {
            Txt_Door_PressF.text = Str_firstDialog;
        }
        if(dialog_DoorIs_Open)
        {
            Txt_Door_PressF.text = Str_DoorIsOpen_Dialog;
        }
        if(dialog_DoorIs_Close)
        {
            Txt_Door_PressF.text = Str_DoorIsClosed_Dialog;
        }
        if(DoorIsLocked)
        {
            Txt_Door_PressF.text = Str_DoorIsLocked_Dialog;
        }
        if(!DoorIsLocked)
        {
            dialog_first = true;
        }
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
