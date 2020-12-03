using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instance_GameManagament : MonoBehaviour
{
    public bool Disable_AtStart = false;
    public bool Enable_AtStart = false;
    bool isthisMenu = false;
    [SerializeField]
    GameObject[] DisableAtStart = null;
    [SerializeField]
    GameObject[] KeepEnableAtStart = null;

    [Tooltip("PausedMenu : Canvaslar içerisinden bu alana atanacak." +
        "SettingsMenu : Canvaslar içerisinden bu alana atanacak." +
        "HUD kısmına ise InGame HUD atanacak.")]
    [SerializeField]
    GameObject PausedMenu = null,
        SettingsMenu = null,
        Hud = null,
        LockedDoor_UI = null;

    LP_AnimControlScript Lp_AnimControl;
    instance_LittlePeopleController LittlePeopleController;
    // Start is called before the first frame update
    void Awake()
    {
        Lp_AnimControl = FindObjectOfType<LP_AnimControlScript>();
        LittlePeopleController = FindObjectOfType<instance_LittlePeopleController>();
        if (PausedMenu != null)
            PausedMenu.SetActive(false);

        if (DisableAtStart != null && Disable_AtStart)
        {
            foreach (var item in DisableAtStart)
            {
                item.SetActive(false);
            }
        }
        if (KeepEnableAtStart != null && Enable_AtStart)
        {
            foreach (var item in KeepEnableAtStart)
            {
                item.SetActive(true);
            }
        }
    }

    private void Start()
    {

        var s = SceneManager.GetActiveScene().buildIndex;
        if (s == 0) isthisMenu = true;
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
    //escye bastı paused menu açıldı.
    //buttonla ayarlara girdi(PausedMenu kapandı) ve tekrar escye bastı.
    //Eğer settings menu açıksa PausedMenuye dokunma.

    private void Update()
    {
        if (!isthisMenu)
        {

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Escape)) // Escape basıldıysa
            {
                if (!LockedDoor_UI.activeSelf) // LockedDoor_UI active değilse
                {
                    if (SettingsMenu.activeSelf != true) // SettingsMenu active değilse
                    {
                        PausedMenu.SetActive(!PausedMenu.activeSelf); // PausedMenu'yu aktif veya deaktif et.

                        LittlePeopleController.Allow_Input = !LittlePeopleController.Allow_Input;
                        Time.timeScale = LittlePeopleController.Allow_Input ? 1 : 0;
                    }
                    else //SettingsMenu aktifse
                    {
                        SettingsMenu.SetActive(false); // SettingsMenu'yu deaktif et.
                        PausedMenu.SetActive(true); //PausedMenu'yu aktif et.
                    }
                }

                if (PausedMenu.activeSelf == true || SettingsMenu.activeSelf == true) //Paused veya SettingsMenu aktifse
                {
                    Cursor.lockState = CursorLockMode.None; //Cursor'ü serbest bırak
                    Cursor.visible = true; //Cursor'ü görünür yap.
                }
                if (PausedMenu.activeSelf == false && SettingsMenu.activeSelf == false)//Paused veya SettingsMenu aktif değilse
                {
                    Cursor.lockState = CursorLockMode.Locked; //Cursor'ü kilitle.
                    Cursor.visible = false; //Cursor'ü gizle.
                }
            }
        }

    }

    //btn_Back
    public void Back()
    {
        if(SettingsMenu.activeSelf && !PausedMenu.activeSelf)
        {
            SettingsMenu.SetActive(false);
            PausedMenu.SetActive(true);
        }
    }

    //btn_Settings
    public void GoToSettings()
    {
        PausedMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    //btn_Hud_Setting
    public void Hud_Setting_Method()
    {
        if (Hud != null)
        {
            Hud.SetActive(!Hud.activeSelf);
        }
    }

    //btn_Animation_Setting
    public void Lp_Anim_Setting_Method()
    {
        if (Lp_AnimControl != null)
        {
            Lp_AnimControl.enabled = !Lp_AnimControl.enabled;
        }
    }

    //btn_Start
    public void StartTheGame()
    {
        SceneManager.LoadScene(1);
    }

    //btn_Exit
    public void ExitGame()
    {
        Application.Quit();
    }
}
