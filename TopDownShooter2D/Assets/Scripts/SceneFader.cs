using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image blackImage;
    [SerializeField] private float alpha;
    private static int cnt = 0;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string _sceneName)
    {
        //black out completely
        blackImage.color = new Color(0, 0, 0, 1);
        //load the scene
        //the scene is under the blackImage at this point
        SceneManager.LoadScene(_sceneName);
        //slowly fades in from the blackImage
        StartCoroutine(FadeIn());


        //fade out method
        //StartCoroutine(Fadeout(_sceneName));
    }

    IEnumerator FadeIn()
    {
        alpha = 1;
        cnt = 0;
        while (alpha > 0)
        {
            cnt++;
            alpha -= Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    IEnumerator Fadeout(string sceneName)
    {
        cnt = 0;
        alpha = 0;
        while (alpha < 1)
        {
            cnt++;
      
            alpha += Time.deltaTime;
            blackImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
         
        SceneManager.LoadScene(sceneName);
        blackImage.color = new Color(0, 0, 0, 0);
    }
}
