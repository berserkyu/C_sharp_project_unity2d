using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slot_Item;
    public Image slot_Image;
    public Text slot_Num;

    public void ItemOnClicked()
    {
        ItemUse.ItemClicked(slot_Item.item_Name);
        InventoryManager.UpdateItemInfo(slot_Item.item_Info);
    }
}
