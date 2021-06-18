using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit2 : MonoBehaviour
{
    [SerializeField] private SceneFader sf;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            sf.FadeTo("Scene3");
        }
    }
}