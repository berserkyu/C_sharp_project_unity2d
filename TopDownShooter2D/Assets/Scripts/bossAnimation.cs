using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimation : MonoBehaviour
{
    [SerializeField] private bossAim ba;
    [SerializeField] private Animator bossAnim;
    [SerializeField] private GameObject bossObject;
    [SerializeField] private soundManager sm;
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private GameObject endingScene;

    public void die()
    {
        if (ba.enabled == true)
        {
            sm.playSound(deadSound);
        }
        ba.enabled = false;
        StartCoroutine(fadesOut());
    }
    IEnumerator fadesOut()
    {
        SpriteRenderer spriteBody = GetComponent<SpriteRenderer>();
        SpriteRenderer spriteAim = ba.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        float frameCnt = 3f;
        while (frameCnt > 0)
        {
            spriteAim.color = new Color(frameCnt / 2, 1, frameCnt / 2, frameCnt / 2);
            spriteBody.color = spriteAim.color;
            frameCnt -= Time.deltaTime;
            yield return null;
        }
        bossObject.SetActive(false);
        endingScene.SetActive(true);
    }   

    // Update is called once per frame
    void Update()
    {
        
        float angle = ba.getAngle();
        if (angle > 45 && angle < 135)
        {
            bossAnim.Play("bossBodyIdleBack");
        }
        else if (angle > -135 && angle < -45)
        {
            bossAnim.Play("bossBodyIdleFront");
        }
        else
        {
            bossAnim.Play("bossBodyHorizontal");
            if(angle>135 || angle < -135)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
