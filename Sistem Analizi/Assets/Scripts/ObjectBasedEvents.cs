using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBasedEvents : MonoBehaviour
{
    public enum Event_Enum { None, Enable, Disable, ColliderTrigger_True, ColliderTrigger_False, DestroyThisAfterEvents }

    [Tooltip("Triggerlandığında tanımlanan eventleri yerine getirir ve uyarı verilecek işaretliyse uyarı verir.")]
    [SerializeField] bool UseTriggerEnter;
    [Tooltip("İliştirilen gameobject bir trigger collider'a sahip olmalı ve bu collider character tarafından" +
        " triggerlanmış ve o süre içerisinde F basılmış olmalıdır.")]
    [SerializeField] bool PerformWhen_F_Pressed;
    [Header("Create an event. If you will call DestroyThisAfterEvents assign it to the last index.")]
    public List<Event_Enum> _EventList;
    [Header("Add objects that will get effected by the event.")]
    [SerializeField] List<GameObject> Enable_Objects;
    [SerializeField] List<GameObject> Disable_Objects;
    [SerializeField] List<GameObject> Collider_Trigger_Objects;
    [Header("Eğer bu script bir Keycard'ın üzerindeyse bu alanı Keycard üzerinden doldurun.")]
    [SerializeField] bool UyariVerilecek;
    [SerializeField] float uyariSuresi = 2f;
    [SerializeField] string UyariText = "WARNING";

    bool TriggerEntered;

    private void Update()
    {
        if (TriggerEntered && UseTriggerEnter)
        {
            if (UyariVerilecek) { UyariVer(uyariSuresi, UyariText); }
            if (_EventList != null) HandleEvents();
        }
        if (TriggerEntered && PerformWhen_F_Pressed && Input.GetKeyDown(KeyCode.F))
        {
            if (UyariVerilecek) { UyariVer(uyariSuresi, UyariText); }
            if (_EventList != null) HandleEvents();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerEntered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        TriggerEntered = false;
    }

    private void OnDestroy()
    {
        Destroy(nesne,uyariSuresi);
    }

    public void HandleEvents()
    {
        foreach (var item in _EventList)
        {
            if (item == Event_Enum.Enable) { EnableObjects(); }
            else if (item == Event_Enum.Disable) { DisableObjects(); }
            else if (item == Event_Enum.ColliderTrigger_True) { ColliderIsTrigger(true); }
            else if (item == Event_Enum.ColliderTrigger_False) { ColliderIsTrigger(false); }

            else if (item == Event_Enum.DestroyThisAfterEvents) { Destroy(this.gameObject); }
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

    #region POOLING SYSTEM FOR WARNING SCREEN
    [SerializeField] GameObject UyariUI;
    GameObject nesne;
    Queue<GameObject> UyariNesneleri = new Queue<GameObject>();
    bool CoroutineWorking = false;

    public void UyariVer(float second, string text)
    {
        if (CoroutineWorking == false)
        {
            nesne = HavuzdanAl();
            TMPro.TMP_Text txt = nesne.GetComponentInChildren<TMPro.TMP_Text>();
            txt.text = text;
            StartCoroutine(UyariVerCoroutine(nesne, second));
        }
    }
    IEnumerator UyariVerCoroutine(GameObject nesne, float _second)
    {
        CoroutineWorking = true;
        nesne.SetActive(true);
        yield return new WaitForSeconds(_second);
        nesne.SetActive(false);
        UyariNesneleri.Enqueue(nesne);
        CoroutineWorking = false;
    }

    private GameObject HavuzdanAl()
    {
        if (UyariNesneleri.Count == 0)
            HavuzaEkle(1);
        return UyariNesneleri.Dequeue();
    }

    private void HavuzaEkle(int v)
    {
        for (int i = 0; i < v; i++)
        {
            var Uyari = Instantiate(UyariUI);
            Uyari.SetActive(false);
            UyariNesneleri.Enqueue(Uyari);
        }
    }

    #endregion
}
