using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class gunBehaviour : MonoBehaviour
{
    [SerializeField] private Material weaponTracerMaterial;
    [SerializeField] private Transform aimTrans, FirePointTrans;
    [SerializeField] private int weaponType , megazinesNo, singleMegazineBullet;
    [SerializeField] private AudioSource gunFireSound,reloadSound;
    [SerializeField] private AudioClip gunFire;
    [SerializeField] private CameraShake cameraShake;
    
    public UnityEngine.UI.Text ammoCount; 
    private bool canShootThisFrame = true;
    private Animator gunAnim;
    private Vector3 fromPos;
    private int curBullet;
    private float bulletSpeed = 600;
    private float autoWeaponCounter = 0f, reloadingCounter = 1f;
    private float angle;

    public void die()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        aimTrans.eulerAngles = new Vector3(0, 0, -90);
    }
    public void addAmmo()
    {
        megazinesNo++;
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
        float shootingAngle = Mathf.Atan2(aimDir.y, aimDir.x);
        Vector2 v = new Vector2(bulletSpeed * Mathf.Cos(shootingAngle), bulletSpeed * Mathf.Sin(shootingAngle));
        bulletInstanceManager.fireABullet(FirePointTrans.position, v, this);

    }
    private void shotGunFiring(Vector3 aimDir)
    {
        float diviationAngle = (10f/180f)*Mathf.PI;
        for(int i = 0; i < 3; i++)
        {
            float shootingAngle = Mathf.Atan2(aimDir.y, aimDir.x) + (i-1)*diviationAngle;
            Vector2 v = new Vector2(bulletSpeed * Mathf.Cos(shootingAngle), bulletSpeed * Mathf.Sin(shootingAngle));
            bulletInstanceManager.fireABullet(FirePointTrans.position, v, this);
        }
    }
    public float getAngle()
    {
        return angle;
    }
    public void createWeaponTracer(Vector3 toPos)
    {
       
        Vector3 dir = (toPos - fromPos).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(dir) - 90;
        float distance = Vector3.Distance(fromPos, toPos);
        Vector3 tracerSpawnPos = fromPos + dir * distance * 0.5f;
        World_Mesh worldMesh = World_Mesh.Create(tracerSpawnPos, eulerZ, 6f, distance, weaponTracerMaterial, null, 1000);
        worldMesh.SetSortingLayerName("Non_background_layer1");
        worldMesh.setMaterialTransparency(0.5f);
        worldMesh.SetSortingOrder(6);
        float timer = 0.075f;
        FunctionUpdater.Create(() =>
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (gameObject.activeSelf) gunAnim.Play("gunHolding");
                worldMesh.setMaterialTransparency(timer);
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
        angle = Mathf.Atan2(aimDir.y, aimDir.x) * 180 / Mathf.PI;
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
        if (reloadSound.isPlaying) return;
        /*
         * if (!canShootThisFrame || reloadingCounter<0.5)
        {
            reloadingCounter += Time.deltaTime;
            canShootThisFrame = true;
            gunAnim.Play("gunHolding");
            return;
        }
         */

        //reload
        if (Input.GetKeyDown(KeyCode.R) && megazinesNo > 0)
        {
            reloadSound.Play();
            gunFireSound.Stop();
            curBullet = singleMegazineBullet;
            megazinesNo--;
            canShootThisFrame = false;
            return;
        }
        //control gun firing
        //rifle firing
        //if out of bullets or stop shooting
        if (weaponType == 3 && Input.GetMouseButton(0) && curBullet > 0 && autoWeaponCounter > 0.1)
        {
            cameraShake.shakeCamera(0.1f, 0.1f);
            gunAnim.Play("gunFiring");
            gunFireSound.PlayOneShot(gunFire);
            handGunFiring(aimDir);
            autoWeaponCounter = 0f;
            curBullet--;
        }
        else if (weaponType != 3 && Input.GetMouseButtonDown(0) && curBullet > 0)
        {
            gunAnim.Play("gunFiring");
            gunFireSound.PlayOneShot(gunFire);

            //shot gun firing
            if (weaponType == 2) {
                shotGunFiring(aimDir);
                cameraShake.shakeCamera(0.1f, 0.3f);
            }
            //hand gun firing 
            else
            {
                handGunFiring(aimDir); 
                cameraShake.shakeCamera(0.1f, 0.1f);
            }
            curBullet--;
        }
        
        
        
    }
    void Update()
    {
        manageGunBehaviour();
        fromPos = FirePointTrans.position;
    }
}
