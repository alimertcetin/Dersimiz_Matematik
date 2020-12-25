using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Door_and_Keycard_Level
{
    None,
    Yesil,
    Sari,
    Kirmizi
}

public class Keycard_Script : MonoBehaviour
{
    const string newLine = "\r\n";

    instance_Player_Inventory inventory;

    #region Tooltip
    [Tooltip("Kartın kaçıncı seviye kart olduğunu belirleyin." + newLine +
        "1.Seviye : Yeşil," + newLine +
        "2.Seviye : Sarı," + newLine +
        "3.Seviye : Kırmızı")]
    #endregion
    public Door_and_Keycard_Level Keycard;

    [HideInInspector]
    public bool Selected_Keycard1 = false,
    Selected_Keycard2 = false,
    Selected_Keycard3 = false;

    [Tooltip("Toplandığında oynatılacak particle efektini bu alana ekleyin.")]
    [SerializeField]
    ParticleSystem CollectedParticle = null;
    bool triggerEntered;
    [Header("Ayarları burada yaptıktan sonra ObjectBased üzerinden UyariUI eklemeyi unutmayın.")]
    [Tooltip("Keycard toplandığında ekrana uyarı verilecekse bu alanı işaretleyin.")]
    [SerializeField] bool UyariVerilecek;
    [SerializeField] float uyariSuresi = 2f;
    [SerializeField] string UyariText = "WARNING";
    ObjectBasedEvents _events;

    private void SetCurrentCardState()
    {
        Selected_Keycard1 = false;
        Selected_Keycard2 = false;
        Selected_Keycard3 = false;
        //(int)Keycard == (int)KeycardLevel.Seivye_1
        //Keycard'ın default değeri 0 olduğundan cast (int)Keycard VS tarafından gereksiz bulunacaktır.
        if (Keycard == Door_and_Keycard_Level.Yesil) //Cast etmeye de gerek yok zaten.
            Selected_Keycard1 = true;

        else if (Keycard == Door_and_Keycard_Level.Sari)
            Selected_Keycard2 = true;

        else if (Keycard == Door_and_Keycard_Level.Kirmizi)
            Selected_Keycard3 = true;

        else
            Debug.LogWarning("Kartlarda bi gariplik var. " + this.name);
    }

    private void Awake()
    {
        SetCurrentCardState();
        _events = GetComponent<ObjectBasedEvents>();
        inventory = FindObjectOfType<instance_Player_Inventory>();
    }

    private void Update()
    {
        if (triggerEntered && Input.GetKeyDown(KeyCode.F))
        {
            if (Selected_Keycard1)
                inventory.CollectedKeycard("green");

            else if (Selected_Keycard2)
                inventory.CollectedKeycard("Yellow");

            else if (Selected_Keycard3)
                inventory.CollectedKeycard("red");

            if (UyariVerilecek)
            {
                if (_events != null) _events.UyariVer(uyariSuresi, UyariText);
                else Debug.LogWarning("ObjectBasedEvents bulunamadı ancak " + this.name + " ona erişmeye çalışıyor.");
            }

            if(_events != null) if (_events._EventList != null) _events.HandleEvents();

            SpawnParicle();
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerEntered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        triggerEntered = false;
    }

    private void SpawnParicle()
    {
        GameObject go = Instantiate(CollectedParticle.gameObject); //Particle yarat.
        go.transform.position = this.transform.position; //Particle konumunu bu Gameobject olarak belirle
        Destroy(go, 5.0f); //Particle'ı 5 saniye sonra yok et.
    }
    

}
