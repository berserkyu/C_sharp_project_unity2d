using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //objects for behaviours
    public Rigidbody2D rb;
    public GameObject Bullet;
    public Transform aimTrans, FirePointTrans;
    public UnityEngine.UI.Text ammoCount,hpText,staminaTxt;
    //variables of player's attribute/status
    private float hp,stamina,maxHp,maxStamina;
    private float walkingSpeed = 5, runningSpeed = 15, dodgingSpeed = 30,  dodgeFrameCounter = 0;
    private float bulletSpeed = 200, playerAttackPoint = 10;
    private int curBullet, curMegazine;
    private List<int> megazines;
    private bool isDodging = false;
    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina = 20;
        hp = maxHp = 100;
        curMegazine = 0;
        curBullet = 10;
        megazines = new List<int>();
        megazines.Add(100);
    }

    // Update is called once per frame
    void Update()
    {
        //manage player movement
        //horizontal movement
        float horiMove = Input.GetAxis("Horizontal");
        //horizontal facing direction(left/right)
        float direction = Input.GetAxisRaw("Horizontal");
        //vertical movement
        float vertiMove = Input.GetAxis("Vertical");
        //determines moving speed
        float curSpeed = walkingSpeed;
        //if running then set speed to runningSpeed and decreases stamina
        if (Input.GetButton("Run") && stamina>1){
            curSpeed = runningSpeed;
            stamina -= Time.deltaTime;
        }
        else
        //not running
        {
            //recover stamina until maxStamina
            if (stamina < maxStamina ) stamina += Time.deltaTime;
        }
        //is starting to dodge (first frame of dodging)
        bool gonnaDodge = Input.GetButtonDown("Dodge");
        //if has enough stamina to dodge
        if (gonnaDodge && stamina>5)
        {
            stamina -= 5;
            isDodging = true;
        }
        //every frame of dodging
        if (isDodging)
        {
            //dodge for 1 sec so add deltaTime for every frame
            dodgeFrameCounter += Time.deltaTime;
            //doding is faster than running
            curSpeed = dodgingSpeed;
            if (dodgeFrameCounter > 0.25)
            {
                isDodging = false;
                dodgeFrameCounter = 0;
            }
        }
        //set moving speed
        rb.velocity = new Vector2(curSpeed * horiMove,curSpeed* vertiMove);
        
        //manage gun position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //vector of aiming direction from player to mouse cursor
        Vector3 aimDir = (mousePos - transform.position).normalized;
        //aiming angle of gun
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * 180 / Mathf.PI;
        //change gun angle
        aimTrans.eulerAngles = new Vector3(0, 0, angle);
        //control gun firing
        if (Input.GetMouseButtonDown(0) && curBullet>0) {
            //create a new bullet object
            GameObject new_bullet = Instantiate(Bullet, FirePointTrans.position, FirePointTrans.rotation);
            //compute shooting angle
            float shootingAngle = Mathf.Atan2(aimDir.y,aimDir.x);
            new_bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos(shootingAngle), bulletSpeed * Mathf.Sin(shootingAngle));
            curBullet--;
        }
        //reload
        if (Input.GetKeyDown(KeyCode.R)) {
            if (megazines[curMegazine] < 10)
            {
                curBullet = megazines[curMegazine];
                megazines[curMegazine] = 0;
            }
            else
            {
                curBullet = 10;
                megazines[curMegazine] -= 10;
            }
        }
        //renew status
        hpText.text = "HP: " + hp;
        ammoCount.text = ("currentAmmo : " + curBullet + "(" + megazines[curMegazine] + ")");
        staminaTxt.text = "stamina : " + (int)stamina;
    }
}
