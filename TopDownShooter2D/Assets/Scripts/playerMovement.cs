using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //道具的UI
    [SerializeField] private UI_Inventory uiInventory;
    //移动参数
    private float walkingSpeed = 5, runningSpeed = 60, dodgingSpeed = 10, dodgeFrameCounter = 0;
    //当前Hp,耐力值
    private float hp, stamina;
    //最大hp,耐力值
    private static float maxStamina = 100000, maxHp = 100;
    private Animator playerAnim;
    //当前是否正在躲闪
    public static bool isDodging = false;

    public Rigidbody2D rb;
    private float horiMove, vertiMove;
    private Inventory inventory;
    //单例模式的实例
    public static playerMovement instance;


    void Awake()
    {
        //单例模式：一个场景里同时只允许一个playerObject的存在，并且从始至终都是同一个实例
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
        //?
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        stamina = maxStamina;
        hp = maxHp;

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        
    }
    private void manageMovement()
    {
        //若键盘接收到移动命令
        horiMove = Input.GetAxis("Horizontal");
        vertiMove = Input.GetAxis("Vertical");
        //当前速度
        float curSpeed = walkingSpeed;
        //若是正在奔跑，则加速，减少耐力
        if (Input.GetButton("Run") && stamina > 1)
        {
            curSpeed = runningSpeed;
            stamina -= Time.deltaTime;
        }
        else
        {
            if (stamina <= maxStamina - Time.deltaTime) stamina += Time.deltaTime;
        }
        isDodging = PlayerBehaviour.getIsDodging();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

}
