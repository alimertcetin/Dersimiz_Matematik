using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item_instance item)
    {
        Debug.LogWarning("SpawnItemWorld is Working!");
        Transform transform = Instantiate(ItemAssets_instance.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    private Item_instance item;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetItem(Item_instance item)
    {
        Debug.LogWarning("SetItem is Working!");
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item_instance GetItem()
    {
        Debug.LogWarning("GetItem is Working!");
        return item;
    }

    public void DestroySelf()
    {
        Debug.LogWarning("DestroySelf is Working!");
        Destroy(gameObject);
    }

}
