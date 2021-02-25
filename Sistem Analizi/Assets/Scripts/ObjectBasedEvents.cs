using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveableEntity))]
public class ObjectBasedEvents : MonoBehaviour, ISaveable
{
    public enum Event_Enum { None, Enable, Disable, ColliderTrigger_True, ColliderTrigger_False, DestroyThisAfterEvents }

    [Tooltip("Triggerlandığında tanımlanan eventleri yerine getirir ve uyarı verilecek işaretliyse uyarı verir.")]
    [SerializeField] bool _useTriggerEnter = false;
    [Tooltip("İliştirilen gameobject bir trigger collider'a sahip olmalı ve bu collider character tarafından" +
        " triggerlanmış ve o süre içerisinde F basılmış olmalıdır.")]
    [SerializeField] bool _performWhen_F_Pressed = false;
    [Header("Create an event. If you will call DestroyThisAfterEvents assign it to the last index.")]
    [SerializeField] List<Event_Enum> _eventList = null;
    [Header("Add objects that will get effected by the event.")]
    [SerializeField] List<GameObject> Enable_Objects = null;
    [SerializeField] List<GameObject> Disable_Objects = null;
    [SerializeField] List<GameObject> Collider_Trigger_Objects = null;
    [Header("Not : Destroy seçildiği zaman nesne yok edilmez. Deaktif hale getirilir.")]
    [SerializeField] bool _uyariVerilecek = false;
    public bool PerformWhen_F_Pressed { get => _performWhen_F_Pressed; set => _performWhen_F_Pressed = value; }
    public bool UseTriggerEnter { get => _useTriggerEnter; set => _useTriggerEnter = value; }
    public List<Event_Enum> EventList { get => _eventList; }

    bool TriggerEntered;
    [SerializeField] float uyariSuresi = 2f;
    [SerializeField] string UyariText = "WARNING";

    private void Update()
    {
        if (TriggerEntered && _useTriggerEnter)
        {
            if (_uyariVerilecek) { UyariVer(uyariSuresi, UyariText); }
            if (_eventList != null) HandleEvents();
        }
        else if (TriggerEntered && _performWhen_F_Pressed && Input.GetKeyDown(KeyCode.F))
        {
            if (_uyariVerilecek) { UyariVer(uyariSuresi, UyariText); }
            if (_eventList != null) HandleEvents();
        }
    }

    private void OnDisable() => TriggerEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) TriggerEntered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) TriggerEntered = false;
    }

    public void HandleEvents()
    {
        foreach (var item in _eventList)
        {
            if (item == Event_Enum.Enable) { EnableObjects(); }
            else if (item == Event_Enum.Disable) { DisableObjects(); }
            else if (item == Event_Enum.ColliderTrigger_True) { ColliderIsTrigger(true); }
            else if (item == Event_Enum.ColliderTrigger_False) { ColliderIsTrigger(false); }

            else if (item == Event_Enum.DestroyThisAfterEvents) { this.gameObject.SetActive(false); }
        }
    }

    private void ColliderIsTrigger(bool triggered)
    {
        foreach (var item in Collider_Trigger_Objects)
        {
            Collider col = item.GetComponent<Collider>();
            if (col != null) col.isTrigger = triggered;
            for (int i = 0; i < item.transform.childCount; i++)
            {
                var childCol = item.GetComponentInChildren<Collider>();
                if (childCol != null) childCol.isTrigger = triggered;
            }
        }
    }

    private void DisableObjects()
    {
        foreach (var item in Disable_Objects)
        {
            item.SetActive(false);
        }
    }

    private void EnableObjects()
    {
        foreach (var item in Enable_Objects)
        {
            item.SetActive(true);
        }
    }

    [SerializeField] GameObject UyariUI = null;
    public void UyariVer(float second, string text)
    {
        Uyari_Ekrani_Management tat = UyariUI.GetComponent<Uyari_Ekrani_Management>();
        tat.UyariVer(second, text);
    }

    public object CaptureState()
    {
        bool ColTriggerState = false;
        TryGetComponent<Collider>(out Collider col);
        if (col != null)
            ColTriggerState = col.isTrigger;
        return new SaveData
        {
            _isActive = gameObject.activeSelf,
            _isTriggered = ColTriggerState,
        };
    }

    public void RestoreState(object state)
    {
        TryGetComponent<Collider>(out Collider col);
        var saveData = (SaveData)state;
        gameObject.SetActive(saveData._isActive);
        if (col != null)
            col.isTrigger = saveData._isTriggered;
    }

    [System.Serializable]
    struct SaveData
    {
        public bool _isActive;
        public bool _isTriggered;
    }
}
