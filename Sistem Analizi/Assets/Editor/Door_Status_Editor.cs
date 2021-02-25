using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Door_Status))] //Hangi nesnenin inspector görüntüsü değiştirilecek
public class Door_Status_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        Door_Status _status = (Door_Status)target;

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            if (!_status.Door_Status_List.Contains(DoorStatus.KeycardRequired))
            {
                var keycardScripts = _status.gameObject.GetComponents<DoorKeycard_Management>();
                if (keycardScripts != null)
                {
                    foreach (var item in keycardScripts)
                    {
                        DestroyImmediate(item);
                    }
                }
            }
            if (!_status.Door_Status_List.Contains(DoorStatus.Locked))
            {
                var keycardScripts = _status.gameObject.GetComponents<Door_Is_Locked>();
                if (keycardScripts != null)
                {
                    foreach (var item in keycardScripts)
                    {
                        DestroyImmediate(item);
                    }
                }
            }

            foreach (var item in _status.Door_Status_List)
            {
                if (item == DoorStatus.Locked)
                {
                    if (_status.gameObject.GetComponent<Door_Is_Locked>() == null)
                        _status.gameObject.AddComponent<Door_Is_Locked>();
                }
                else if (item == DoorStatus.KeycardRequired)
                {
                    if (_status.gameObject.GetComponent<DoorKeycard_Management>() == null)
                    _status.gameObject.AddComponent<DoorKeycard_Management>();
                }
                else if (item == DoorStatus.JustOpenAndClose)
                {
                    if (_status.gameObject.GetComponent<Door_Animation>() == null)
                    _status.gameObject.AddComponent<Door_Animation>();
                }
            }
        }
    }
}