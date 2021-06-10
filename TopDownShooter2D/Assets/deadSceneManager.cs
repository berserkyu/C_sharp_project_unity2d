using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadSceneManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Dodge"))
        {
            transform.parent.GetChild(0).gameObject.GetComponent<PlayerBehaviour>()?.respawn();
        }
    }
}
