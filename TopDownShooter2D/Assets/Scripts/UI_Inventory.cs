using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform container;
    private Transform slot;
    
    private void Awake()
    {
        container = transform.Find("ItemSlotContainer");
        slot = container.Find("Slot");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender,System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in container)
        {
            if (child == slot) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 34f;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransfrom = Instantiate(slot, container).GetComponent<RectTransform>();
            itemSlotRectTransfrom.gameObject.SetActive(true);
            itemSlotRectTransfrom.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransfrom.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x += 3;
            if (x > 15) 
            {
                x = 0;
                y -= 3;
            }
        }
    }
}
