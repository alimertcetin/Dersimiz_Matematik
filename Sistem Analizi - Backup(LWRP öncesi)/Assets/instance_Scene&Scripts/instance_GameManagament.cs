using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instance_GameManagament : MonoBehaviour
{
    public bool Disable_AtStart = false;
    public bool Enable_AtStart = false;
    [SerializeField]
    GameObject[] DisableAtStart = null;
    [SerializeField]
    GameObject[] KeepEnableAtStart = null;
    // Start is called before the first frame update
    void Start()
    {
        if (DisableAtStart != null && Disable_AtStart)
        {
            foreach (var item in DisableAtStart)
            {
                item.SetActive(false);
            }
        }
        if (KeepEnableAtStart != null && Enable_AtStart)
        {
            foreach (var item in KeepEnableAtStart)
            {
                item.SetActive(true);
            }
        }
    }


}
