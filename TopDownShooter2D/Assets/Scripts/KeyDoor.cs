using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
    public void CloseDoor()
    {
        gameObject.SetActive(true);
    }
    public Transform getTrans()
    {
        return transform;
    }
}
