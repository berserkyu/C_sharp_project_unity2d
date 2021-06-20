using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private SceneFader sf;
    [SerializeField] private string nextScene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        sf = GameObject.Find("Black Image")?.GetComponent<SceneFader>();
        if(other.tag == "Player")
        {
            sf.FadeTo(nextScene);
            InventoryManager.getDoors();
        }
    }
}