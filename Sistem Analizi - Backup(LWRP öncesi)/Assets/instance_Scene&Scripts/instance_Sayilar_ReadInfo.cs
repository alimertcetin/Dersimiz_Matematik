using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class instance_Sayilar_ReadInfo : MonoBehaviour
{
    instance_Player_Inventory inventory;
    [SerializeField]
    TMP_Text txt_Notification = null;
    [SerializeField]
    string EnvanterDolu = "";
    public int CoveredArea = 1, 
        BeklenecekSure = 1; //Envanter dolduğunda çıkan uyarının ne kadar süre ekranda kalacağı.
    private void Start()
    {
        inventory = FindObjectOfType<instance_Player_Inventory>();
        if(txt_Notification == null) { Debug.LogError("instance_Sayilar_ReadInfo : Bildirim Text'ini(txt_Notification) eklemeyi unuttunuz!"); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (inventory.inventory_Capacity > 0)
            {
                this.gameObject.SetActive(false);
                //Toplanan sayıyı Envantere aktar ve Envanter Kapasitesini kaplanan alan kadar azalt.
                inventory.CapacityHasChanged(this.gameObject.tag, CoveredArea);
                //Toplanan sayıyı göstergeler ile göster.
                //Toplanan sayıdan kaç tane olduğunu göster.
            }
            else
            {
                txt_Notification.text = EnvanterDolu;
                //Show Notification. "Inventory is full!"
                StartCoroutine(waitGivenSecond(txt_Notification, BeklenecekSure));
            }

        }
    }

    IEnumerator waitGivenSecond(TMP_Text targetDisable, float _BeklenecekSure)
    {
        targetDisable.enabled = true;
        yield return new WaitForSeconds(_BeklenecekSure);
        targetDisable.enabled = false;
    }
}

