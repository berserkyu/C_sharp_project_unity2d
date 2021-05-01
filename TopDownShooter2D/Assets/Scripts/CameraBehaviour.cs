using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform playerTrans;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTrans.position.x, playerTrans.position.y,-10);
    }
}
