using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //�ƶ�����
    [SerializeField] private float walkingSpeed, runningSpeed, dodgingSpeed;
    private float dodgeFrameCounter;
    //��ǰHp,����ֵ
    private float stamina;
    //���hp,����ֵ
    private static float maxStamina = 20, maxHp = 100;
    private Animator playerAnim;
    //��ǰ�Ƿ����ڶ���
    public static bool isDodging = false;
    public GameObject myGun;
    //Inventory��ʱGun�Ƿ���ʹ��
    private bool isGunEnabled = true;
    //inventory to show
    public GameObject myInventory;
    //Inventory�Ƿ��
    private bool isInventoryOpen = false;
    public Rigidbody2D rb;
    private float horiMove, vertiMove;
    //����ģʽ��ʵ��
    public static playerMovement instance;


    void Awake()
    {
        //����ģʽ��һ��������ͬʱֻ����һ��playerObject�Ĵ��ڣ����Ҵ�ʼ���ն���ͬһ��ʵ��
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
    }
    private void manageMovement()
    {
        //�����̽��յ��ƶ�����
        horiMove = Input.GetAxis("Horizontal");
        vertiMove = Input.GetAxis("Vertical");
        //��ǰ�ٶ�
        float curSpeed = walkingSpeed;
        //�������ڱ��ܣ�����٣���������
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

    //�ð�����Inventory
    private void openMyInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            
            //ÿ�δ򿪻�رն�����ջ
            ItemUse.Clear();
            //disable��ǹ
            isGunEnabled = !isGunEnabled;
            myGun.SetActive(isGunEnabled);
            //���I��ʹInventory���Է����򿪻�ر�
            isInventoryOpen = !isInventoryOpen;
            myInventory.SetActive(isInventoryOpen);
        }
    }
    public void startDodge()
    {
        horiMove = Input.GetAxis("Horizontal");
        vertiMove = Input.GetAxis("Vertical");
        stamina -= 5;
        rb.velocity = new Vector2(dodgingSpeed * horiMove, dodgingSpeed * vertiMove);
        isDodging = true;
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

}
