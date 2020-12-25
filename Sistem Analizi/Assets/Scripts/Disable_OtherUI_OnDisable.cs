using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable_OtherUI_OnDisable : MonoBehaviour
{
    [SerializeField] List<GameObject> DisableObjectList;
    private void OnDisable()
    {
        foreach (var item in DisableObjectList)
        {
            item.SetActive(false);
            if (item.CompareTag("Giris_UI")) item.SetActive(true);
        }
    }

    private void OnEnable()
    {
        foreach (var item in DisableObjectList)
        {
            item.SetActive(false);
            if (item.CompareTag("Giris_UI")) item.SetActive(true);
        }
    }
}
