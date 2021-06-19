using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory my_Inventory;
    public GameObject slot_Grid;
    public Slot slot_Prefab;
    public Text item_Info;

    void Awake()
    {
        if (instance != null) 
        {
            Destroy(this);
        }
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.item_Info.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.item_Info.text = itemDescription;
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slot_Prefab,
            instance.slot_Grid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slot_Grid.transform);
        newItem.gameObject.transform.localScale = new Vector3(1, 1, 1);

        newItem.slot_Item = item;
        newItem.slot_Image.sprite = item.item_Image;
        newItem.slot_Num.text = item.item_Held.ToString();
    }

    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slot_Grid.transform.childCount; i++) 
        {
            if (instance.slot_Grid.transform.childCount == 0) break;
            Destroy(instance.slot_Grid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.my_Inventory.itemList.Count; i++) 
        {
            CreateNewItem(instance.my_Inventory.itemList[i]);
        }
    }
    //使用道具后删除道具持有数
    public static void DeleteItem(string itemName)
    {
        for (int i = 0; i < instance.my_Inventory.itemList.Count; i++)
        {
            if (instance.my_Inventory.itemList[i].item_Name == itemName)
            {
                instance.my_Inventory.itemList[i].item_Held -= 1;
                if (instance.my_Inventory.itemList[i].item_Held <= 0)
                {
                    instance.my_Inventory.itemList[i].item_Held = 0;
                    instance.my_Inventory.itemList.RemoveAt(i);
                }
            }
        }
        RefreshItem();
    }
    //使用道具button
    public static void OnUseButtonClicked()
    {
        if (ItemUse.isStackEmpty()) return;

        string item_Use =  ItemUse.ItemUsed();

        if (item_Use != null) 
        {
            switch(item_Use)
            {
                case "Medkit":
                    playerMovement.hpAdded();
                    break;
                case "Key":
                    break;
                default: break;
            }
            DeleteItem(item_Use);

            //每次使用道具后若没有再点击其他道具则默认使用上一次用的道具
            ItemUse.Clear();
            ItemUse.ItemClicked(item_Use);
        }
    }
}
