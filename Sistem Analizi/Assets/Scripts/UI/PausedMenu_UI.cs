using UnityEngine;

public class PausedMenu_UI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject Main;

    [Header("Broadcasting To")]
    [SerializeField] private StringEventChannelSO WarningUIChannel = default;
    [SerializeField] private LoadEventChannelSO onExitPressed = default;

    [Header("Scenes To Load")]
    [SerializeField] private GameSceneSO mainMenu = default;

    private void OnEnable()
    {
        InputManager.GamePlay.Disable();
        Main.SetActive(true);
    }

    private void OnDisable()
    {
        Settings.SetActive(false);
        InputManager.GamePlay.Enable();
    }

    public void btn_Load()
    {
        SaveSystem.instance.Load();
    }

    public void btn_Save()
    {
        WarningUIChannel.RaiseEvent("Kayıt Edildi : " + Application.persistentDataPath);
        SaveSystem.instance.Save();
    }

    public void btn_Settings()
    {
        Settings.SetActive(true);
        Main.SetActive(false);
    }

    public void btn_Exit()
    {
        onExitPressed.RaiseEvent(mainMenu, true);
    }
}
