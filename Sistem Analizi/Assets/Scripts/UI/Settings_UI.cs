using UnityEngine;
using TMPro;

public class Settings_UI : MonoBehaviour
{
    [SerializeField] GameObject[] Hud;
    [SerializeField] GameObject Main;
    [SerializeField] TMP_Text Hud_Button_Text, Animation_Button_Text;
    Player_AnimController LP_AnimControl;
    private void Awake()
    {
        LP_AnimControl = FindObjectOfType<Player_AnimController>();
    }

    public void btn_Hud()
    {
        foreach (var item in Hud)
        {
            item.SetActive(!item.activeSelf);
            Hud_Button_Text.text = item.activeSelf ? "Hud Açık" : "Hud Kapalı";
        }
    }
    public void btn_Animation()
    {
        LP_AnimControl.enabled = !LP_AnimControl.enabled;
        Animation_Button_Text.text = LP_AnimControl.enabled ? "Anim Açık" : "Anim Kapalı";
    }
    public void btn_Back()
    {
        Main.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
