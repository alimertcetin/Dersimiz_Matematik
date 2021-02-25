using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackBoard_UI_Management : MonoBehaviour
{
    instance_Player_Inventory inventory;
    instance_LittlePeopleController Lp_Controller;
    
    [SerializeField]
    GameObject Giris_UI = null, islemYap_UI = null, SayiAl_UI = null;

    private void Awake()
    {
        inventory = FindObjectOfType<instance_Player_Inventory>();
        Lp_Controller = FindObjectOfType<instance_LittlePeopleController>();
    }

    private void OnEnable()
    {
        Giris_UI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        islemYap_UI.SetActive(false);
        SayiAl_UI.SetActive(false);
    }

    #region UI Yönetimi için

    //btn_SayiAl
    public void SayiAl()
    {
        Giris_UI.SetActive(false);
        SayiAl_UI.SetActive(true);
    }

    //btn_islemYap
    public void islemYap()
    {
        Giris_UI.SetActive(false);
        islemYap_UI.SetActive(true);
    }
    
    //btn_Cikis
    public void Cikis()
    {
        this.gameObject.SetActive(false);
        if (!Lp_Controller.Allow_Input)
            Lp_Controller.Allow_Input = true;
    }

    #endregion
    
}
