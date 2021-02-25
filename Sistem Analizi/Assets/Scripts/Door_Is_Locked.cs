using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(Door_Status))]
[RequireComponent(typeof(SaveableEntity))]
public class Door_Is_Locked : MonoBehaviour,ISaveable
{
    public EventHandler KapiAcildi;

    bool _doorLocked = true;
    public bool DoorLocked { get => _doorLocked; set => _doorLocked = value; }

    bool _triggered;
    public bool Triggered { get => _triggered; }

    [SerializeField] string _doorLockedQuestion = "Question";
    public string DoorLockedQuestion { get => _doorLockedQuestion; set => _doorLockedQuestion = value; }

    [SerializeField] int _doorLockedAnswer = 0;
    public int DoorLockedAnswer { get => _doorLockedAnswer; set => _doorLockedAnswer = value; }
    
    [SerializeField] LockedDoor_UI_Management LockedDoor_UI_Script;

    bool lockControl = false;
    private void Update()
    {
        if (Triggered && !DoorLocked && !lockControl)
        {
            KapiAcildi?.Invoke(this, EventArgs.Empty);
            lockControl = true;
        }
        if (!_doorLocked) return;
        if (Triggered && DoorLocked && !LockedDoor_UI_Script.gameObject.activeSelf && Input.GetKeyDown(KeyCode.F))
            LockedDoor_UI_Script.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LockedDoor_UI_Script.RecieveScriptFromDoor(this);
            _triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _triggered = false;
    }

    public object CaptureState()
    {
        return new SaveData
        {
            _DoorLocked = DoorLocked,
            _DoorLockedQuestion = DoorLockedQuestion,
            _DoorLockedAnswer = DoorLockedAnswer
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        DoorLocked = saveData._DoorLocked;
        DoorLockedQuestion = saveData._DoorLockedQuestion;
        DoorLockedAnswer = saveData._DoorLockedAnswer;
    }

    [System.Serializable]
    struct SaveData
    {
        public bool _DoorLocked;
        public string _DoorLockedQuestion;
        public int _DoorLockedAnswer;
    }
}
