using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
//cur 
public class PlayerBehaviour : MonoBehaviour
{
    //objects for behaviours
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform healthTrans, staminaTrans;
    [SerializeField] private playerMovement movement;
    [SerializeField] private gunBehaviour gun;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private GameObject deadScene;
    [SerializeField] private Transform spawnPt;
    [SerializeField] private AudioSource soundEmitter;
    [SerializeField] private AudioClip damageSound, healSound;
    private Vector3 initStaminaScale;
    //public playerMovement player;
    private float dodgeCnt = 0f;
    private int maxHp;
    // private Animation curAnim;
    private float horiMove, vertiMove, faceDirection, angle;
    private float dodgeCoolDownCnt = 0f;
    private HealthSystem hpSys;
    // private float maxHp, curHp;
    private static bool isDodging;
    private bool dead;
    float dmgFrameCnt = 0;

    void Start()
    {
        initStaminaScale = staminaTrans.localScale;
        Application.targetFrameRate = 40;
        maxHp = 100;
        hpSys = new HealthSystem(maxHp);
        dead = false;
        damage(40);
    }

    private void manageAnimation()
    {
        if (hpSys.GetHealth() <= 0)
        {
            Debug.Log("dies");
            gun.die();
            playerAnim.Play("playerDead");
            movement.enabled = false;
            gun.enabled = false;
            dead = true;
        }
        //dodging animation
        if (isDodging)
        {
            if (!isPlaying(playerAnim, "dodgingHorizontal") && !isPlaying(playerAnim, "dodgingHorizontalReverse")
                && !isPlaying(playerAnim, "dodgingDown") && !isPlaying(playerAnim, "dodgingUp"))
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
        if (angle > 45 && angle < 135)
        {
            if (vertiMove == 0 && horiMove == 0) playerAnim.Play("IdleUp");
            else playerAnim.Play("runUp");

        }
        else if (angle > -135 && angle < -45)
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
        if (dead) return;
        //check if is dodging
        if (!isDodging && movement.getStamina() >= 5)
        {

            isDodging = Input.GetButtonDown("Dodge");
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
        staminaTrans.localScale = new Vector3(initStaminaScale.x * movement.getStaminaPercent(), initStaminaScale.y, initStaminaScale.z);

    }

    private bool isPlaying(Animator anim, string animationName)
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

    //variables of player's attribute/status
    public void heal(int val)
    {
        soundEmitter.PlayOneShot(healSound);
        playerSprite.color = new Color(0.411f, 1f, 0.411f, 1f);
        float dmgTimer = 0.3f;
        FunctionUpdater.Create(() =>
        {
            dmgTimer -= Time.deltaTime;
            if (dmgTimer <= 0)
            {
                playerSprite.color = new Color(1f, 1f, 1f, 1f);
                return true;
            }
            return false;
        });
        hpSys.Heal(val);
    }
    public void damage(int val)
    {
        soundEmitter.PlayOneShot(damageSound);
        playerSprite.color = new Color(1f, 0.411f, 0.411f, 1f);
        float dmgTimer = 0.3f;
        FunctionUpdater.Create(() =>
        {
            dmgTimer -= Time.deltaTime;
            if (dmgTimer <= 0)
            {
                playerSprite.color = new Color(1f, 1f, 1f, 1f);
                return true;
            }
            return false;

        });
        hpSys.Damage(val);
    }
    public void doneDying()
    {
        Debug.Log("done dying");
        deadScene.SetActive(true);
    }

    public void respawn()
    {
        dead = false;
        deadScene.SetActive(false);
        hpSys.Heal(maxHp);
        gun.enabled = true;
        movement.enabled = true;
        movement.transform.position = spawnPt.position;
       
        
    }
}
