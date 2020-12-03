using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasLockedDoor_Manager : MonoBehaviour
{
    public OpenTheDoor _openTheDoor;
    
    public TMP_Text txt_Input_answer;
    int Input_Answer;
    [SerializeField]
    int RealAnswer = 0;

    public ObjectListener_Test Listener;
    public GameObject[] Buttons;
    [HideInInspector]
    public bool sayi_0, sayi_1, sayi_2, sayi_3, sayi_4, sayi_5, sayi_6, sayi_7, sayi_8, sayi_9;

    private void Start()
    {
        this.gameObject.SetActive(false);
        txt_Input_answer.text = "";
    }

    private void Update()
    {
        //this.gameObject.SetActive(_openTheDoor.OpenThe_LockedDoorCanvas);
        SetactiveByListener();
    }

    void SetactiveByListener()
    {
        Buttons[0].SetActive(sayi_0);
        Buttons[1].SetActive(sayi_1);
        Buttons[2].SetActive(sayi_2);
        Buttons[3].SetActive(sayi_3);
        Buttons[4].SetActive(sayi_4);
        Buttons[5].SetActive(sayi_5);
        Buttons[6].SetActive(sayi_6);
        Buttons[7].SetActive(sayi_7);
        Buttons[8].SetActive(sayi_8);
        Buttons[9].SetActive(sayi_9);
    }

    public void CheckTheAnswer()
    {
        //This variable is for managing ObjectListener
        Listener.Original_EqualsTo_Instance();
        Input_Answer = int.Parse(txt_Input_answer.text);
        if (RealAnswer == Input_Answer)
        {
            _openTheDoor.DoorIsLocked = false;
            _openTheDoor.KeyPressed_F = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Wrong Answer!!!");
            txt_Input_answer.text = "";
        }
    }



    public void DegeriYazdır(string YazdirilacakDeger)
    {
        txt_Input_answer.text += YazdirilacakDeger;
    }

    public void DegeriSil()
    {
        txt_Input_answer.text = txt_Input_answer.text.Remove(txt_Input_answer.text.Length - 1);
    }


}
