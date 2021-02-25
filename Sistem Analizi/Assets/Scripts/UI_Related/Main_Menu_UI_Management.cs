using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_UI_Management : MonoBehaviour
{
    [SerializeField] GameObject Main = null, SettingsMenu = null;

    public void btn_Start() => SceneManager.LoadScene(1);

    public void btn_Settings()
    {
        Main.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void btn_Exit() => Application.Quit();

    //public void btn_Back()
    //{
    //    if (SettingsMenu.activeSelf && !Main.activeSelf)
    //    {
    //        SettingsMenu.SetActive(false);
    //        Main.SetActive(true);
    //    }
    //}

}
