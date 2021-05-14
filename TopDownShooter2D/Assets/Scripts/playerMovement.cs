using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public UnityEngine.UI.Text hpText, staminaTxt;
    private float walkingSpeed = 5, runningSpeed = 60, dodgingSpeed = 10, dodgeFrameCounter = 0;
    private float bulletSpeed = 200, playerAttackPoint = 10;
    private float hp, stamina;
    private static float maxStamina = 100000, maxHp = 100;
    private Animator playerAnim;
    public static bool isDodging = false;
    public Rigidbody2D rb;
    private float horiMove, vertiMove;

    public static playerMovement instance;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        stamina = maxStamina;
        hp = maxHp;
    }
    private void manageMovement()
    {
        horiMove = Input.GetAxis("Horizontal");
        vertiMove = Input.GetAxis("Vertical");

        float curSpeed = walkingSpeed;
        if (Input.GetButton("Run") && stamina > 1)
        {
           
            curSpeed = runningSpeed;
            stamina -= Time.deltaTime;
        }
        else
        {
            if (stamina <= maxStamina - Time.deltaTime) stamina += Time.deltaTime;
        }


        bool gonnaDodge = Input.GetButtonDown("Dodge");

        if (gonnaDodge && stamina > 5)
        {
            stamina -= 5;
            isDodging = true;
        }
        if (isDodging)
        {
            dodgeFrameCounter += Time.deltaTime;
            curSpeed = dodgingSpeed;
            if (dodgeFrameCounter > 0.3)
            {
                isDodging = false;
                dodgeFrameCounter = 0;
            }
        }
        rb.velocity = new Vector2(curSpeed * horiMove, curSpeed * vertiMove);
    }
    // Update is called once per frame
    void Update()
    {
        manageMovement();
        //hpText.text = "HP: " + hp;
       // staminaTxt.text = "stamina : " + (int)stamina;
    }
}
