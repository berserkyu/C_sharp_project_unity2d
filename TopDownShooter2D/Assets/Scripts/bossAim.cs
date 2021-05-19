using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAim : MonoBehaviour
{
    [SerializeField] private Transform playerTrans, firePointTrans;
    [SerializeField] private GameObject bossBullet;
    private Vector3 aimDir;
    private float shootCnt = 0;
    private float bulletSpeed = 40, angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float getAngle()
    {
        return angle;
    }
    private void fireBullet()
    {
        GameObject new_bullet = Instantiate(bossBullet, firePointTrans.position, firePointTrans.rotation);
        float shootingAngle = Mathf.Atan2(aimDir.y, aimDir.x);
        new_bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos(shootingAngle), bulletSpeed * Mathf.Sin(shootingAngle));
        shootCnt = 0;
    }
    // Update is called once per frame
    void Update()
    {
        aimDir = (playerTrans.position - transform.position).normalized;
        angle = Mathf.Atan2(aimDir.y, aimDir.x) * 180 / Mathf.PI;
        transform.eulerAngles = new Vector3(0, 0, angle);
        if (angle < 135 && angle > 45)
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        shootCnt += Time.deltaTime;
        if (shootCnt >= 1) fireBullet();
    }
}
