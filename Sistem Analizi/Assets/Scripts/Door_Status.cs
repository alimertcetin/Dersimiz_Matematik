using System.Collections.Generic;
using UnityEngine;

public enum DoorStatus
{
    Locked,
    JustOpenAndClose,
    KeycardRequired
}

public class Door_Status : MonoBehaviour
{
    [Header("OpenTheDoor scripti güncelleniyor.")]
    [Tooltip("Bu değişken aslında instance_OpenTheDoor classına ait.")]
    public List<DoorStatus> Door_Status_List;
}
