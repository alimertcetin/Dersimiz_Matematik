using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instance_GameManagament : MonoBehaviour
{
    static instance_GameManagament instance;

    bool isthisMenu = false;

    [SerializeField]
    GameObject PausedMenu = null, SettingsMenu = null, Hud = null;

    LP_AnimControlScript Lp_AnimControl;
    instance_LittlePeopleController LittlePeopleController;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        var s = SceneManager.GetActiveScene().buildIndex;
        if (s == 0)
        {
            isthisMenu = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (isthisMenu) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            PausedMenu.SetActive(!PausedMenu.activeSelf);
    }
    
}
