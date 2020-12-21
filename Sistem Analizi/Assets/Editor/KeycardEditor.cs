using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Keycard_Script))] //Hangi nesnenin inspector görüntüsü değiştirilecek
public class KeycardEditor : Editor
{
    public bool seciliAnahtar1;
    public bool seciliAnahtar2;
    public bool seciliAnahtar3;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Keycard_Script anahtar = (Keycard_Script)target;
        seciliAnahtar1 = anahtar.Selected_Keycard1;
        seciliAnahtar2 = anahtar.Selected_Keycard2;
        seciliAnahtar3 = anahtar.Selected_Keycard2;

        if (GUI.changed)
        {
            if (anahtar.Keycard == Door_and_Keycard_Level.Yesil)
            {
                Debug.Log("Seviye 1 seçildi.");
                seciliAnahtar1 = true;
                seciliAnahtar2 = false;
                seciliAnahtar3 = false;
            }
            if (anahtar.Keycard == Door_and_Keycard_Level.Sari)
            {
                Debug.Log("Seviye 2 seçildi.");
                seciliAnahtar1 = false;
                seciliAnahtar2 = true;
                seciliAnahtar3 = false;
            }
            if (anahtar.Keycard == Door_and_Keycard_Level.Kirmizi)
            {
                Debug.Log("Seviye 3 seçildi.");
                seciliAnahtar1 = false;
                seciliAnahtar2 = false;
                seciliAnahtar3 = true;
            }

        }
    }
}