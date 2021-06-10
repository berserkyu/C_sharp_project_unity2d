using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //移动参数
    private float walkingSpeed = 3, runningSpeed = 6, dodgingSpeed = 10, dodgeFrameCounter = 0;
    //当前Hp,耐力值
    private float hp, stamina;
    //最大hp,耐力值
    private static float maxStamina = 20, maxHp = 100;
    private Animator playerAnim;
    //当前是否正在躲闪
    public static bool isDodging = false;
    
<<<<<<< HEAD
    public GameObject myInventory;
    //Inventory是否打开
    private bool isInventoryOpen = false;
=======
>>>>>>> dea6bf7a8b7b86e94b84899d409d3a216a573fb1
    public Rigidbody2D rb;
    private float horiMove, vertiMove;
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
    public float getStamina()
    {
        return stamina;
    }
    public float getStaminaPercent()
    {
        return stamina / maxStamina;
    }
    public void doneDodge()
    {
        isDodging = false;
    }
    public void startDodge()
    {
        horiMove = Input.GetAxis("Horizontal");
        vertiMove = Input.GetAxis("Vertical");
        stamina -= 5;
        rb.velocity = new Vector2(dodgingSpeed * horiMove, dodgingSpeed * vertiMove);
        isDodging = true;
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
        if (isDodging)
        {
            curSpeed = dodgingSpeed;
        }
        rb.velocity = new Vector2(curSpeed * horiMove, curSpeed * vertiMove);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isDodging) manageMovement();
        openMyInventory();
    }

    //用按键打开Inventory
    private void openMyInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            //点击I键使Inventory可以反复打开或关闭
            isInventoryOpen = !isInventoryOpen;
            myInventory.SetActive(isInventoryOpen);
        }
    }

}
