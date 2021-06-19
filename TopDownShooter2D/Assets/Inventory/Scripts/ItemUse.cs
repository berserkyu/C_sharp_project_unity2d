using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    static Stack item_stack = new Stack();

    public static void ItemClicked(string itemName)
    {
        item_stack.Push(itemName);
    }

    public static string ItemUsed()
    {
        if (!isStackEmpty())
        {
            return item_stack.Pop().ToString();
        }
        return null;
    }

    public static void Clear()
    {
        if (isStackEmpty()) return;
        item_stack.Clear();
    }

    public static bool isStackEmpty()
    {
        if (item_stack.Count <= 0) return true;
        else return false;
    }
}
