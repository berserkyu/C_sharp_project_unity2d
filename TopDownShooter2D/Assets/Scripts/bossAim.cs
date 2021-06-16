using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAim : MonoBehaviour
{
    [SerializeField] private Transform playerTrans, firePointTrans;
    [SerializeField] private GameObject bossBullet, magicBall;
    [SerializeField] private soundManager sm;
    [SerializeField] private AudioClip fire, skill;
    private Vector3 aimDir;
    private float shootCnt = 0, skillCnt = 0;
    private float bulletSpeed = 40, angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float getAngle()
    {
        return angle;
    }
    private void castBallSkill()
    {
        sm.playSound(skill);
        Vector3 pos = playerTrans.position;
        pos.y -= 2;
        GameObject new_ball = Instantiate(magicBall, pos, new Quaternion(0,0,0,0));

    }
    private void fireBullet()
    {
        sm.playSound(fire);
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
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
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
        shootCnt += Time.deltaTime;
        skillCnt += Time.deltaTime;
        if (skillCnt >= 4)
        {
            castBallSkill();
            shootCnt = 1;
            skillCnt = 0;
        }
        else if (shootCnt >= 2)
        {
            fireBullet();
            shootCnt = 0;
        }
    }
}
