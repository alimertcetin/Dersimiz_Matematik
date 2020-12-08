using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartingCanvas_Manager : MonoBehaviour
{
    [SerializeField]
    GameObject[] Textler = new GameObject[5];
    [SerializeField]
    TMP_Text NextEnd_btn_Text = null;
    string orj_Text;

    [SerializeField]
    GameObject btn_Prev;

    int tracker = 0;
    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.5)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
    
    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    private void Start()
    {
        orj_Text = NextEnd_btn_Text.text;
        btn_Prev.SetActive(false);
    }

    //btn_Next
    public void btn_NEXT()
    {
        tracker++;
        if (tracker > 0)
        {
            btn_Prev.SetActive(true);
            if (tracker > Textler.Length - 1)
                Destroy(this.gameObject);
            else
            {
                foreach (var item in Textler)
                {
                    item.SetActive(false);
                }

                Textler[tracker].SetActive(true);
            }
        }

        if (tracker == Textler.Length - 1)
            NextEnd_btn_Text.text = "Son";
        else
            NextEnd_btn_Text.text = orj_Text;

    }

    //btn_Previous
    public void btn_Prev_Method()
    {
        tracker--;
        if (tracker < 0) tracker = 0;

        foreach (var item in Textler)
        {
            item.SetActive(false);
        }

        Textler[tracker].SetActive(true);

        if (tracker < Textler.Length - 1)
            NextEnd_btn_Text.text = orj_Text;

        if (tracker == 0) btn_Prev.SetActive(false);
    }
}
