using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform playerTrans;
    public static CameraBehaviour instance;

    void Awake()
    {

        if (instance == null)
        {
            Debug.Log("Created Camera");
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Debug.Log("Destroyed camera\n");
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTrans.position.x, playerTrans.position.y,-10);
    }
}
