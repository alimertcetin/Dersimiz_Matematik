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
    string EnvanterDolu = "Envanter Dolu", CollectText = "Toplamak için F'ye bas";
    public int CoveredArea = 1, 
        BeklenecekSure = 1; //Envanter dolduğunda çıkan uyarının ne kadar süre ekranda kalacağı.
    bool AllowToCollect;

    [SerializeField]
    ParticleSystem CollectedParticle = null;
    private void Start()
    {
        inventory = FindObjectOfType<instance_Player_Inventory>();
        if(txt_Notification == null) { Debug.LogError("instance_Sayilar_ReadInfo : Bildirim Text'ini(txt_Notification) eklemeyi unuttunuz!"); }
    }

    private void Update()
    {
        if (AllowToCollect && Input.GetKeyDown(KeyCode.F)) //Eğer toplanmaya izin verildiyse ve F basıldıysa
        {
            inventory.CapacityHasChanged(this.gameObject.tag, CoveredArea); //Toplanan sayıyı Envantere aktar ve Envanter Kapasitesini kaplanan alan kadar azalt.

            GameObject go = Instantiate(CollectedParticle.gameObject); //Particle yarat.
            go.transform.position = this.transform.position; //Particle konumunu bu Gameobject olarak belirle
            Destroy(go, 5.0f); //Particle'ı 5 saniye sonra yok et.

            txt_Notification.enabled = false; //text'i kapat.
            Destroy(this.gameObject); //Bu gameobject'i anında yok et.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (inventory.inventory_Capacity > 0) //Player'ın envanter kapasitesi 0dan büyükse
            {
                AllowToCollect = true; //Toplamaya izin ver.
                txt_Notification.text = CollectText; //Notification Text'i CollectText ile değiştir.
            }
            else //Player envanter kapasitesi yetersizse
            {
                AllowToCollect = false;
                txt_Notification.text = EnvanterDolu; //Notification Text'i EnvanterDolu ile değiştir.
            }

            txt_Notification.enabled = true; //Notification Text'i aktif et.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            AllowToCollect = false; //Toplamaya izin verme.
            txt_Notification.enabled = false; //Notification Text'i deaktif et.
        }
    }
    
}

