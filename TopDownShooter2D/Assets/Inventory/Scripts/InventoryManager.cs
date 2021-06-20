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
   
    [SerializeField] private PlayerBehaviour player;
    [SerializeField] private KeyDoor SilverKeyDoor;
    [SerializeField] private KeyDoor GoldKeyDoor;
    [SerializeField] private KeyDoor SpecialKeyDoor;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        getDoors();
    }
    public static Inventory getMyInventory()
    {
        return instance.my_Inventory;
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
            instance.slot_Grid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slot_Grid.transform);
        newItem.gameObject.transform.localScale = new Vector3(1, 1, 1);

        newItem.slot_Item = item;
        newItem.slot_Image.sprite = item.item_Image;
        newItem.slot_Num.text = item.item_Held.ToString();
    }

    public static void RefreshItem()
    {
        if (instance == null)
        {
            Debug.Log("instance is null");
        }else if (instance.slot_Grid == null)
        {
            Debug.Log("instance slot grid is null");
        }
        
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
    bool validDist(KeyDoor k,float dist)
    {
        if (k == null) return false;
        Transform playerTrans = player.getTrans();
        Transform doorTrans = k.getTrans();
        Vector2 pv = new Vector2(playerTrans.position.x, playerTrans.position.y);
        Vector2 kv = new Vector2(doorTrans.position.x, doorTrans.position.y);
        return Vector2.Distance(pv, kv) < dist;
    }
    //使用道具button
    public void OnUseButtonClicked()
    {
        Debug.Log("use button clicked");
        if (ItemUse.isStackEmpty())
        {
            Debug.Log("item stack is empty");
            return;
        }

        string item_Use = ItemUse.ItemUsed();

        if (item_Use != null)
        {
            switch (item_Use)
            {
                case "Medkit":
                    player.heal(30);
                    break;
                case "SilverKey":

                    SilverKeyDoor?.OpenDoor();
                    break;
                case "GoldKey":

                    GoldKeyDoor?.OpenDoor();
                    break;
                case "SpecialKey":
   
                    SpecialKeyDoor?.OpenDoor();
                    break;
                default: break;
            }
            DeleteItem(item_Use);

            //每次使用道具后若没有再点击其他道具则默认使用上一次用的道具
            ItemUse.Clear();
            ItemUse.ItemClicked(item_Use);
        }
    }
    public static void getDoors()
    {
        instance.SilverKeyDoor = GameObject.Find("SilverDoor")?.GetComponent<KeyDoor>();
        instance.GoldKeyDoor = GameObject.Find("GoldDoor")?.GetComponent<KeyDoor>();
        instance.SpecialKeyDoor = GameObject.Find("SpecialDoor")?.GetComponent<KeyDoor>();
    }
}
