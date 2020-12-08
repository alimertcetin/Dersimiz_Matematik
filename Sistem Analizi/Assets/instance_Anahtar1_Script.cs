using UnityEngine;

public class instance_Anahtar1_Script : MonoBehaviour
{
    instance_Player_Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<instance_Player_Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.Anahtar1++;
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //Play some particle
    }

}
