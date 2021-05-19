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
    public UnityEngine.UI.Text ammoCount; 
    private Animator gunAnim;
    private Vector3 fromPos;
   
    private List<int> megazines, megazinesBullet;
    private int curBullet, curMegazine;
    private float bulletSpeed = 600;
    // Update is called once per frame
    private void Start()
    {
        gunAnim = transform.GetChild(0).GetComponent<Animator>();
        curMegazine = 0;
        curBullet = 10;
        megazines = new List<int>();
        megazinesBullet = new List<int>();
        megazinesBullet.Add(10);
        megazines.Add(100);
    }


    
    public void createWeaponTracer(Vector3 toPos)
    {
        Debug.Log("created weapon tracer");
        gunAnim.Play("handGunFiring");
        Vector3 dir = (toPos - fromPos).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(dir) - 90;
        float distance = Vector3.Distance(fromPos, toPos);
        Vector3 tracerSpawnPos = fromPos + dir * distance * 0.5f;
        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPos, eulerZ, 6f, distance, weaponTracerMaterial, null, 1000);
        //worldMesh.SetSortingLayerName("Non_background_layer1");
        worldMesh.SetSortingOrder(5);
        float timer = 0.1f;
        FunctionUpdater.Create(() =>
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gunAnim.Play("handGunHolding");
                worldMesh.DestroySelf();
                return true;
            }
            return false;
        });
    }
    private void manageGunBehaviour()
    {
        //manage gun aiming position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 aimDir = (mousePos - aimTrans.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * 180 / Mathf.PI;
        aimTrans.eulerAngles = new Vector3(0, 0, angle);
        if(angle <135 && angle > 45)
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
        //control gun firing
        if (Input.GetMouseButtonDown(0) && curBullet >  0)
        {
                     
            //GameObject new_bullet = Instantiate(Bullet, FirePointTrans.position, FirePointTrans.rotation);
          //  float shootingAngle = Mathf.Atan2(aimDir.y,aimDir.x);
           // new_bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos(shootingAngle), bulletSpeed * Mathf.Sin(shootingAngle));
           // new_bullet.GetComponent<bulletBehaviour>()?.setShootFrom(this);
            
            //createWeaponTracer(mousePos);
            curBullet--;
        }
        //reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (megazines[curMegazine] < megazinesBullet[curMegazine])
            {
                curBullet = megazines[curMegazine];
                megazines[curMegazine] = 0;
            }
            else
            {
                curBullet = 10;
                megazines[curMegazine] -= megazinesBullet[curMegazine];
            }
        }
    }
    void Update()
    {
        fromPos = FirePointTrans.position;
        manageGunBehaviour();
        //ammoCount.text = ("currentAmmo : " + curBullet + "(" + megazines[curMegazine] + ")");
    }
}
