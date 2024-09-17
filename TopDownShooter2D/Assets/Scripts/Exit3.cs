using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit3 : MonoBehaviour
{
    [SerializeField] private SceneFader sf;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("exit : " + other.gameObject.name);
        if (other.tag == "Player")
        {
            Debug.Log("touchedPlayer");
            sf.FadeTo("Scene4");
        }
    }
}