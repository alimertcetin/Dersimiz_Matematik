using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpdateInputField_TestingScene : MonoBehaviour
{

    public TMP_Text txt_wall;
    private TMP_InputField txt_inputField;

    // Start is called before the first frame update
    void Start()
    {
        txt_inputField = GetComponent<TMP_InputField>();
    }
    bool activate;
    // Update is called once per frame
    void Update()
    {
        if (activate == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                txt_inputField.enabled = true;
                txt_inputField.text = txt_wall.text;
                activate = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                txt_inputField.enabled = false;
                activate = false;
            }
        }
        if (txt_inputField.enabled == true)
        {
            txt_wall.text = txt_inputField.text;
        }
    }
}
