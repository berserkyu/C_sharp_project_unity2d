using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class gunBehaviour : MonoBehaviour
{
    [SerializeField] private Material weaponTracerMaterial;
    [SerializeField] private Transform aimTrans, FirePointTrans;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private int weaponType , megazinesNo, singleMegazineBullet;
    public UnityEngine.UI.Text ammoCount;
    private bool canShootThisFrame = true;
    private Animator gunAnim;
    private Vector3 fromPos;
    private int curBullet;
    private float bulletSpeed = 600;
    private float autoWeaponCounter = 0f;

    public void die()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        aimTrans.eulerAngles = new Vector3(0, 0, -90);
    }
    public void canShoot(bool val)
    {
        canShootThisFrame = val;
    }
    // Update is called once per frame
    private void Start()
    {
        gunAnim = transform.GetChild(0).GetComponent<Animator>();
        gunAnim.Play("gunHolding");
        curBullet = singleMegazineBullet;
    }
    private void handGunFiring(Vector3 aimDir)
    {
        GameObject new_bullet = Instantiate(Bullet, FirePointTrans.position, FirePointTrans.rotation);
        float shootingAngle = Mathf.Atan2(aimDir.y, aimDir.x);
        new_bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos(shootingAngle), bulletSpeed * Mathf.Sin(shootingAngle));
        new_bullet.GetComponent<bulletBehaviour>()?.setGunBehaviour(this);
    }
    private void shotGunFiring(Vector3 aimDir)
    {
        GameObject[] new_bullets = new GameObject[3];
        float diviationAngle = (10f/180f)*Mathf.PI;
        for(int i = 0; i < 3; i++)
        {
            new_bullets[i] = Instantiate(Bullet, FirePointTrans.position, FirePointTrans.rotation);
            float shootingAngle = Mathf.Atan2(aimDir.y, aimDir.x) + (i-1)*diviationAngle;
            new_bullets[i].GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos(shootingAngle), bulletSpeed * Mathf.Sin(shootingAngle));
            new_bullets[i].GetComponent<bulletBehaviour>()?.setGunBehaviour(this);
        }
    }
    
    public void createWeaponTracer(Vector3 toPos)
    {
       
        Vector3 dir = (toPos - fromPos).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(dir) - 90;
        float distance = Vector3.Distance(fromPos, toPos);
        Vector3 tracerSpawnPos = fromPos + dir * distance * 0.5f;
        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPos, eulerZ, 6f, distance, weaponTracerMaterial, null, 1000);
        worldMesh.SetSortingLayerName("Non_background_layer1");
        worldMesh.SetSortingOrder(3);
        float timer = 0.075f;
        FunctionUpdater.Create(() =>
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (gameObject.activeSelf) gunAnim.Play("gunHolding");
                worldMesh.DestroySelf();
                return true;
            }
            return false;
        });
    }

    private void manageGunAimingPosition(ref Vector3 aimDir)
    {
        //manage gun aiming position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        aimDir = (mousePos - aimTrans.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * 180 / Mathf.PI;
        aimTrans.eulerAngles = new Vector3(0, 0, angle);
        if (angle < 135 && angle > 45)
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void manageGunBehaviour()
    {
        Vector3 aimDir = new Vector3();
        autoWeaponCounter += Time.deltaTime;
        manageGunAimingPosition(ref aimDir);
        if (!canShootThisFrame)
        {
            canShootThisFrame = true;
            gunAnim.Play("gunHolding");
            return;
        }
        //control gun firing
        if(weaponType==3 && Input.GetMouseButton(0) && curBullet > 0 && autoWeaponCounter>0.1)
        {
            gunAnim.Play("gunFiring");
            handGunFiring(aimDir);
            autoWeaponCounter = 0f;
            curBullet--;
        }
        else if (weaponType!=3 && Input.GetMouseButtonDown(0) && curBullet >  0)
        {
            gunAnim.Play("gunFiring");
            if (weaponType == 2) shotGunFiring(aimDir);
            else handGunFiring(aimDir);
            curBullet--;
        }
        
        
        //reload
        if (Input.GetKeyDown(KeyCode.R) && megazinesNo>0)
        {
            curBullet = singleMegazineBullet;
            megazinesNo--;
        }
    }
    void Update()
    {
        manageGunBehaviour();
        fromPos = FirePointTrans.position;
    }
}