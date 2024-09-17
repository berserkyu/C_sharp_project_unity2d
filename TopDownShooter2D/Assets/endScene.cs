using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource sound;
    void OnEnable()
    {
        //play sound
        sound.Play();
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        //cant move player anymore
        GameObject.FindGameObjectWithTag("Player")?.SetActive(false);
        StartCoroutine(fadesIn());
    }
    IEnumerator fadesIn()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float frameCnt = 3f;
        while (frameCnt > 0)
        {
            sprite.color = new Color(1, 1, 1, 1-frameCnt / 3);
            frameCnt -= Time.deltaTime;
            yield return null;
        }

    }
}
