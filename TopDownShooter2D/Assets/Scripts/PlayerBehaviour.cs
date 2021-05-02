using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class PlayerBehaviour : MonoBehaviour
{
    //objects for behaviours
    [SerializeField] private Animator  playerAnim;
    private float horiMove, vertiMove, faceDirection, angle;
    //variables of player's attribute/status

    private bool isDodging = false;
    // Start is called before the first frame update
    
    private void manageAnimation()
    {
        //idle animation
        Debug.Log(angle);
        if(angle>45 && angle < 135)
        {
            if (vertiMove == 0 && horiMove == 0) playerAnim.Play("IdleUp");
            else playerAnim.Play("runUp");

        }else if(angle >-135 && angle < -45)
        {
            if (vertiMove == 0 && horiMove == 0) playerAnim.Play("Idle");
            else playerAnim.Play("runDown");
        }
        else
        {
            if (vertiMove == 0 && horiMove == 0) playerAnim.Play("IdleHorizontal");
            else playerAnim.Play("runHorizontal");
            //manage facing direction
            if (faceDirection == -1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (faceDirection == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //manage player movement
        horiMove = Input.GetAxis("Horizontal");
        vertiMove = Input.GetAxis("Vertical");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 aimDir = (mousePos - transform.position).normalized;
        angle = Mathf.Atan2(aimDir.y, aimDir.x) * 180 / Mathf.PI;
        faceDirection = ((angle > 90 || angle < -90) ? -1 : 1);


        manageAnimation();
        
    }
    
}
