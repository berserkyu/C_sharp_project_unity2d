using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class PlayerBehaviour : MonoBehaviour
{
    //objects for behaviours
    [SerializeField] private Animator  playerAnim;
    [SerializeField] private Transform healthTrans;
    [SerializeField] private playerMovement movement;
    [SerializeField] private gunBehaviour gun;
    [SerializeField] private SpriteRenderer playerSprite;
    //public playerMovement player;
    private float dodgeCnt = 0f;
    private int maxHp;
   // private Animation curAnim;
    private float horiMove, vertiMove, faceDirection, angle;
    private float dodgeCoolDownCnt = 0f;
    private HealthSystem hpSys;
    // private float maxHp, curHp;
    private static bool isDodging;
    private  bool dead;
    float dmgFrameCnt = 0;
    void Start()
    {
        Application.targetFrameRate = 40;
        maxHp = 1000;
        hpSys = new HealthSystem(maxHp);
    }
    //variables of player's attribute/status
    public void heal(int val)
    {
        hpSys.Heal(val);
    }
    public void damage(int val)
    {
        playerSprite.color = new Color(1f, 0.411f, 0.411f, 1f);
        float dmgTimer = 0.3f;
        FunctionUpdater.Create(() =>
        {
            dmgTimer -= Time.deltaTime;
            if (dmgTimer <= 0)
            {
                playerSprite.color = new Color(1f,1f, 1f, 1f);
                return true;
            }
            return false;

        });
        hpSys.Damage(val);
    }
    private bool isPlaying(Animator anim,string animationName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
    public void doneDodging()
    {
        dodgeCoolDownCnt = 0;
        isDodging = false;
        movement.doneDodge();
    }
    public void startDodging()
    {
        isDodging = true;
        movement.startDodge();
    }
    private void manageAnimation()
    {
        if (hpSys.GetHealth() <= 0 && !dead)
        {
            gun.die();
            movement.enabled = false;
            gun.enabled = false;
            playerAnim.Play("playerDead");
            dead = true;
        }
        //dodging animation
        if (isDodging)
        {
            if (!isPlaying(playerAnim, "dodgingHorizontal") && !isPlaying(playerAnim, "dodgingHorizontalReverse") 
                && !isPlaying(playerAnim, "dodgingDown") && !isPlaying(playerAnim, "dodgingUp") )
            {
                float face = Input.GetAxisRaw("Horizontal");
                float ang = gun.getAngle();
                if (face == -1)
                {
                    if (ang > 90 || ang < -90)
                    {
                        playerAnim.Play("dodgingHorizontal");
                    }
                    else
                    {
                        playerAnim.Play("dodgingHorizontalReverse");
                    }
                }
                else if (face == 1)
                {
                    if (ang > 90 || ang < -90)
                    {
                        playerAnim.Play("dodgingHorizontalReverse");
                    }
                    else
                    {
                        playerAnim.Play("dodgingHorizontal");
                    }
                }
                else
                {
                    if (ang < 0) playerAnim.Play("dodgingDown");
                    else playerAnim.Play("dodgingUp");
                }
            }
            return;
        }
        

            // idle animation
            if (angle>45 && angle < 135)
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
        //check if is dodging
        if (!isDodging && movement.getStamina() >= 5)
        {
            
            isDodging = Input.GetButtonDown("Dodge");
            Debug.Log("getting input" + isDodging);
        }
        //manage gun position
        horiMove = Input.GetAxis("Horizontal");
        vertiMove = Input.GetAxis("Vertical");
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 aimDir = (mousePos - transform.position).normalized;
        angle = Mathf.Atan2(aimDir.y, aimDir.x) * 180 / Mathf.PI;
        faceDirection = ((angle > 90 || angle < -90) ? -1 : 1);
        manageAnimation();
        //manage health bar
        dmgFrameCnt += Time.deltaTime;
        if (dmgFrameCnt > 1)
        {
            dmgFrameCnt = 0;
        }
        healthTrans.localScale = new Vector3(hpSys.GetHealthPercent(), 1, 1);

    }
    
}
