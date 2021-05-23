using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] 
public class Item
{
    public enum ItemType
    {
        HealthPotion,
        Coin,
        Medkit,
        Pistol,
        Shotgun,
        Rifle,
        Bullet
    }

    public ItemType item_Type;
    public int amount;

    public Sprite GetSprite()
    {
        switch(item_Type)
        {
            default:
            case ItemType.HealthPotion:return ItemAssets.Instance.healthPotionSprite;
            case ItemType.Coin: return ItemAssets.Instance.coinSprite;
            case ItemType.Medkit: return ItemAssets.Instance.medkitSprite;
            case ItemType.Pistol: return ItemAssets.Instance.pistolSprite;
            case ItemType.Shotgun: return ItemAssets.Instance.shotgunSprite;
            case ItemType.Bullet: return ItemAssets.Instance.bulletSprite;
            case ItemType.Rifle: return ItemAssets.Instance.rifleSprite;
        }
    }
    public ItemType getType()
    {
        return item_Type;
    }
}
