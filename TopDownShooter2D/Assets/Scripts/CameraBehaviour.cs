using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform playerTrans;
    public static CameraBehaviour instance;
    [SerializeField] private playerMovement player;
    // private Image health;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
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
