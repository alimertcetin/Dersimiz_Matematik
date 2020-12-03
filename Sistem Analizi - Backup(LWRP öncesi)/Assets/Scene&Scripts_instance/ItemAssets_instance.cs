using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets_instance : MonoBehaviour
{
    public static ItemAssets_instance Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public Transform pfItemWorld;

    public Sprite Spr_Sayi_0;
    public Sprite Spr_Sayi_1;
    public Sprite Spr_Sayi_2;
    public Sprite Spr_Sayi_3;
    public Sprite Spr_Sayi_4;
    public Sprite Spr_Sayi_5;
    public Sprite Spr_Sayi_6;
    public Sprite Spr_Sayi_7;
    public Sprite Spr_Sayi_8;
    public Sprite Spr_Sayi_9;
}
