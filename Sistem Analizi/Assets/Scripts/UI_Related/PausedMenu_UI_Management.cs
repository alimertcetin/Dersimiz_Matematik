using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu_UI_Management : MonoBehaviour
{
    [SerializeField] GameObject Settings, Main;

    instance_LittlePeopleController LpController;

    private void Awake() => LpController = FindObjectOfType<instance_LittlePeopleController>();
    private void OnEnable()
    {
        LpController.Allow_Input = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        Settings.SetActive(false);
        LpController.Allow_Input = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void btn_Load()
    {
        SaveSystem.instance.Load();
    }
    public void btn_Save()
    {
        var UyariText = "Kayıt Edildi : " + Application.persistentDataPath;
        Uyari_Ekrani_Management.instance.UyariVer(8, UyariText);
        SaveSystem.instance.Save();
    }

    public void btn_Settings()
    {
        Settings.SetActive(true);
        Main.SetActive(false);
    }

    public void btn_Exit()
    {
        SceneManager.LoadScene(0);
    }
}
