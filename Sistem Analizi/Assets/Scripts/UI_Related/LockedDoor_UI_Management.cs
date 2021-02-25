using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class LockedDoor_UI_Management : MonoBehaviour
{
    public EventHandler SoruCevaplandi;

    instance_Player_Inventory inventory;
    instance_LittlePeopleController LP_Controller;
    Door_Is_Locked LockedDoor_Script;
    
    [SerializeField] TMP_Text txt_InputField = null;
    [SerializeField] TMP_Text txt_Soru = null;
    [Header("ekrana verilecek uyarıların süresi")]
    [SerializeField] int UyariSuresi = 2;

    private void Awake()
    {
        LP_Controller = FindObjectOfType<instance_LittlePeopleController>();
        inventory = FindObjectOfType<instance_Player_Inventory>();
    }

    float Maintimer;
    float timer;
    float controlEachDeleteTime = .2f;
    private void Update()
    {
        if (!this.gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Backspace)) SilMethodu();
        else if (Input.GetKey(KeyCode.Backspace))
        {
            timer += Time.deltaTime;
            Maintimer += Time.deltaTime;
            if (timer > controlEachDeleteTime)
            {
                SilMethodu();
                timer = 0;
            }
            if (Maintimer > controlEachDeleteTime * 5)
            {
                SilMethodu();
                timer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            timer = 0;
            Maintimer = 0;
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter)) CevaplaMethodu();
        else if (Input.GetKeyDown(KeyCode.Return)) CevaplaMethodu();
        else if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0)) NumberOnClick(0);
        else if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) NumberOnClick(1);
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) NumberOnClick(2);
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) NumberOnClick(3);
        else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) NumberOnClick(4);
        else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) NumberOnClick(5);
        else if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) NumberOnClick(6);
        else if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7)) NumberOnClick(7);
        else if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8)) NumberOnClick(8);
        else if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9)) NumberOnClick(9);
    }

    private void OnEnable()
    {
        if (LockedDoor_Script != null)
        {
            txt_Soru.text = LockedDoor_Script.DoorLockedQuestion;
            LP_Controller.Allow_Input = false;
        }
        else
        {
            Debug.LogError("LockedDoor_UI kapıdan soruyu alamadı!");
            return;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnDisable()
    {
        LP_Controller.Allow_Input = true;
        TextiTamamenTemizle();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TextiTamamenTemizle()
    {
        if (txt_InputField.text.Length == 0) return;
        else
        {
            while (txt_InputField.text.Length != 0)
            {
                string last_input = txt_InputField.text.Substring(txt_InputField.text.Length - 1);
                txt_InputField.text = txt_InputField.text.Remove(txt_InputField.text.Length - 1);

                inventory.Sayi_Ekle(int.Parse(last_input), 1);
                if (txt_InputField.text.Length == 0) break;
            }
        }
    }

    //OpenTheDoor scripti hangi kapıya bağlı olduğunu bu methodla buraya bildiriyor
    public void RecieveScriptFromDoor(Door_Is_Locked Door) => LockedDoor_Script = Door;

    public void NumberOnClick(int value)
    {
        //Eğer değer envanterde varsa
        if (inventory.InventoryControl_Sayi(value))
        {
            txt_InputField.text += value.ToString();
            inventory.Sayi_Cikar(value, 1);
        }
        else UyariVer(UyariSuresi, "Bu rakam envanterinde yok!");
    }

    public void SilMethodu()
    {
        if (txt_InputField.text.Length == 0) return;
        else
        {
            //Son girilen yazıyı bul ve last input'a ata.
            string last_input = txt_InputField.text.Substring(txt_InputField.text.Length - 1);
            //Son girilen yazıyı sil.
            txt_InputField.text = txt_InputField.text.Remove(txt_InputField.text.Length - 1);
            inventory.Sayi_Ekle(int.Parse(last_input), 1);
        }
    }

    public void CevaplaMethodu()
    {
        if (txt_InputField.text == LockedDoor_Script.DoorLockedAnswer.ToString())
        {
            LockedDoor_Script.DoorLocked = false;
            txt_InputField.text = "";
            SoruCevaplandi?.Invoke(this, EventArgs.Empty);
            this.gameObject.SetActive(false);
        }
        else UyariVer(UyariSuresi, "Şifre Yanlış");
    }

    void UyariVer(float time, string text) => Uyari_Ekrani_Management.instance.UyariVer(time, text);
}
