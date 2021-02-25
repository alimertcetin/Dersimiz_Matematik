using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Main_Menu_Settings_Manager : MonoBehaviour
{
    [SerializeField] GameObject btn_Vsync = null, btn_FrameRate = null, //Hepsinin button olması ve childlarında bir tane tmp_Text olması bekleniyor.
                                btn_QualitySetting, Main = null;
    TMP_Text txt_Vsync, txt_FrameRate, txt_QualitySetting;

    private void Start()
    {
        txt_QualitySetting = btn_QualitySetting.GetComponentInChildren<TMP_Text>();
        txt_QualitySetting.text = SetQualityText();

        txt_Vsync = btn_Vsync.GetComponentInChildren<TMP_Text>();
        txt_Vsync.text = SetVsyncText();

        txt_FrameRate = btn_FrameRate.GetComponentInChildren<TMP_Text>();
        txt_FrameRate.text = SetTargetFrameRateTxt();
    }

    private string SetQualityText()
    {
        if (QualitySettings.GetQualityLevel() == 1) return "Grafik Kalitesi : düşük";
        else if (QualitySettings.GetQualityLevel() == 2) return "Grafik Kalitesi : orta";
        else if (QualitySettings.GetQualityLevel() == 3) return "Grafik Kalitesi : yüksek";
        else return "Grafik Kalitesi : idare eder";
    }

    private string SetVsyncText()
    {
        if (FrameController.instance.VsyncCount == 0) return "Vsync Kapalı";
        else return "Vsync Açık";
    }

    private string SetTargetFrameRateTxt()
    {
        if (FrameController.instance.FrameRate == 30) return "Hedeflenen Fps : 30";
        else return "Hedeflenen Fps : 60";
    }

    public void VsycnOnclick()
    {
        FrameController.instance.VsyncCount++;
        txt_Vsync.text = SetVsyncText();
    }

    public void FrameRateOnclick()
    {
        if (FrameController.instance.FrameRate == 30) FrameController.instance.FrameRate = 60;
        else FrameController.instance.FrameRate = 30;
        txt_FrameRate.text = SetTargetFrameRateTxt();
    }

    public void BackOnClick()
    {
        Main.SetActive(true);
        this.gameObject.SetActive(false);
    }


    bool a = false;
    public void btn_Quality()
    {
        if (QualitySettings.GetQualityLevel() <= 3 && a)
        {
            QualitySettings.IncreaseLevel();
            if (QualitySettings.GetQualityLevel() == 3)
                a = false;
        }
        else if(QualitySettings.GetQualityLevel() >=3 && !a)
        {
            QualitySettings.DecreaseLevel();
            if (QualitySettings.GetQualityLevel() == 1)
                a = true;
        }
        txt_QualitySetting.text = SetQualityText();
    }

}
