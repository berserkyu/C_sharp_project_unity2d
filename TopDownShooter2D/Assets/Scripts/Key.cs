using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    [SerializeField] private KeyDoor keyDoor;

    public enum KeyType
    {
        Silver,
        Gold
    }
    public KeyDoor GetKeyDoor()
    {
        return keyDoor;
    }
    public KeyType GetKeyType()
    {
        return keyType;
    }
}
