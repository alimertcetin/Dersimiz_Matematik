using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Of_Numbers : MonoBehaviour
{
    ObjectListener_Test Test_objectListener;
    // Start is called before the first frame update
    void Start()
    {
        Test_objectListener = FindObjectOfType<ObjectListener_Test>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Test_objectListener.GivenSpace == 0)
        {
            Test_objectListener.InvantoryFull();
        }
        else if (other.gameObject.tag == "Player" && Test_objectListener.GivenSpace > 0)
        {
            Debug.LogWarning(this.gameObject.tag + " Collected!");
            gameObject.SetActive(false);
            Test_objectListener.CheckTheTag_AndIncrease(gameObject.tag); //Nesnenin tagini kontrol et ve Listenerda toplanan nesneye göre değişkeni arttır.
        }
    }
}
