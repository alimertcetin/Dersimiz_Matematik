using UnityEngine;
using System.Collections;
using TMPro;

public class Uyari_Ekrani_Management : MonoBehaviour
{
    public static Uyari_Ekrani_Management instance;
    TMP_Text txt_Uyari;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        txt_Uyari = GetComponentInChildren<TMP_Text>();
        this.gameObject.SetActive(false);
    }

    public void UyariyiAc(string text)
    {
        txt_Uyari.text = text;
        this.gameObject.SetActive(true);
    }
    public void UyariyiKapat() => this.gameObject.SetActive(false);

    public void UyariVer(float time,string text)
    {
        txt_Uyari.text = text;
        this.gameObject.SetActive(true);
        StartCoroutine(StopWarning(time));
    }

    IEnumerator StopWarning(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
