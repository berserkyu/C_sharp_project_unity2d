using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;
    public Sprite rifleSprite;
    public Sprite pistolSprite;
    public Sprite healthPotionSprite;
    public Sprite coinSprite;
    public Sprite medkitSprite;
    public Sprite shotgunSprite;
    public Sprite bulletSprite;
}
