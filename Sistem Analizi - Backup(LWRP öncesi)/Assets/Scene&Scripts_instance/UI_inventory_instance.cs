using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_inventory_instance : MonoBehaviour
{
    private inventory_instance inventory_instance;

    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;

    private void Awake()
    {
        if (itemSlotContainer == null)
            Debug.LogWarning("ItemSlotContainer is null");
        if(itemSlotTemplate == null)
        {
            Debug.Log("ItemSlotTemplate is null");
        }
        //itemSlotContainer = transform.Find("ItemSlotContainer");
        //itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetInventory(inventory_instance inventory)
    {
        //this.inventory_instance is a field
        this.inventory_instance = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender,System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    
    [SerializeField]
    float itemSlotCellSize = 30f, CellOffset = 10;
    void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0, y = 0;
        foreach(Item_instance item in inventory_instance.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2((x * itemSlotCellSize) + CellOffset, (y * itemSlotCellSize) + CellOffset);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if (x > 5)
            {
                x = 0;
                y--;
            }
        }
    }
}
