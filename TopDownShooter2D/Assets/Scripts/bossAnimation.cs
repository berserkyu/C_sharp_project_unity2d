using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimation : MonoBehaviour
{
    [SerializeField] private bossAim ba;
    [SerializeField] private Animator bossAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void die()
    {

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
