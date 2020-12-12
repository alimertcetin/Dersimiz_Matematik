using UnityEditor;
using UnityEngine;

public enum KeycardLevel
{
    Seivye_1,
    Seivye_2,
    Seivye_3
}

public class Keycard_Script : MonoBehaviour
{
    instance_Player_Inventory inventory;

    public KeycardLevel Keycard;

    [HideInInspector]
    public bool Selected_Keycard1 = false,
    Selected_Keycard2 = false,
    Selected_Keycard3 = false;

    [SerializeField]
    ParticleSystem CollectedParticle = null;

    private void SetCurrentCardState()
    {
        //Keycard'ın default değeri 0 olduğundan cast (int)Keycard VS tarafından gereksiz bulunacaktır.
        //(int)Keycard == (int)KeycardLevel.Seivye_1
        if (Keycard == KeycardLevel.Seivye_1) //Cast etmeye de gerek yok zaten.
        {
            Selected_Keycard1 = true;
            Selected_Keycard2 = false;
            Selected_Keycard3 = false;
        }
        else if (Keycard == KeycardLevel.Seivye_2)
        {
            Selected_Keycard1 = false;
            Selected_Keycard2 = true;
            Selected_Keycard3 = false;
        }
        else if (Keycard == KeycardLevel.Seivye_3)
        {
            Selected_Keycard1 = false;
            Selected_Keycard2 = false;
            Selected_Keycard3 = true;
        }
        else
        {
            Debug.LogWarning("Kartlarda bi gariplik var." + this.name);
        }
    }

    private void Awake()
    {
        SetCurrentCardState();

        inventory = FindObjectOfType<instance_Player_Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Selected_Keycard1)
            {
                inventory.CollectedKeycard("green");

                SpawnParicle();

                Destroy(this.gameObject);
            }
            else if (Selected_Keycard2)
            {
                inventory.CollectedKeycard("Yellow");

                SpawnParicle();

                Destroy(this.gameObject);
            }
            else if (Selected_Keycard3)
            {
                inventory.CollectedKeycard("red");

                SpawnParicle();

                Destroy(this.gameObject);
            }
        }
    }

    private void SpawnParicle()
    {
        GameObject go = Instantiate(CollectedParticle.gameObject); //Particle yarat.
        go.transform.position = this.transform.position; //Particle konumunu bu Gameobject olarak belirle
        Destroy(go, 5.0f); //Particle'ı 5 saniye sonra yok et.
    }
    

}
