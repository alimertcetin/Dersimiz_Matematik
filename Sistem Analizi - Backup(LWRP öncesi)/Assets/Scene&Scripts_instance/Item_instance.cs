using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_instance
{
    public ItemType itemType;
    public int amount;


    public enum ItemType
    {
        Sayi_0,
        Sayi_1,
        Sayi_2,
        Sayi_3,
        Sayi_4,
        Sayi_5,
        Sayi_6,
        Sayi_7,
        Sayi_8,
        Sayi_9,
    }

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            //Sayı 0
            case ItemType.Sayi_0:
                return ItemAssets_instance.Instance.Spr_Sayi_0;
            //Sayı 1
            case ItemType.Sayi_1:
                return ItemAssets_instance.Instance.Spr_Sayi_1;
            //Sayı 2
            case ItemType.Sayi_2:
                return ItemAssets_instance.Instance.Spr_Sayi_2;
            //Sayı 3
            case ItemType.Sayi_3:
                return ItemAssets_instance.Instance.Spr_Sayi_3;
            //Sayı 4
            case ItemType.Sayi_4:
                return ItemAssets_instance.Instance.Spr_Sayi_4;
            //Sayı 5
            case ItemType.Sayi_5:
                return ItemAssets_instance.Instance.Spr_Sayi_5;
            //Sayı 6
            case ItemType.Sayi_6:
                return ItemAssets_instance.Instance.Spr_Sayi_6;
            //Sayı 7
            case ItemType.Sayi_7:
                return ItemAssets_instance.Instance.Spr_Sayi_7;
            //Sayı 8
            case ItemType.Sayi_8:
                return ItemAssets_instance.Instance.Spr_Sayi_8;
            //Sayı 9
            case ItemType.Sayi_9:
                return ItemAssets_instance.Instance.Spr_Sayi_9;
        }
    }

}
