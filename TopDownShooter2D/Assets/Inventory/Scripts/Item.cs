using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]
public class Item : ScriptableObject
{
    public string item_Name;
    public Sprite item_Image;
    public int item_Held;
    [TextArea]
    public string item_Info;
    public bool equip; 

}
