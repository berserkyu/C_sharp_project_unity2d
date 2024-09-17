using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletInstanceManager : MonoBehaviour
{
    public static List<GameObject> bullets;
    public const int bulletInstanceCnt = 20;
    [SerializeField] private GameObject Bullet;
    private static int bulletIndex = 0;
    private static bulletInstanceManager instance;
    // Start is called before the first frame update
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
        //
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < bulletInstanceCnt; i++)
        {
            GameObject new_bullet = Instantiate(Bullet, transform.position, transform.rotation);
            new_bullet.transform.parent = transform;
            bullets.Add(new_bullet);
        }
    }
    public static void fireABullet(Vector3 initPos,Vector2 v,gunBehaviour fireFrom)
    {
        //activate the bullet
        bullets[bulletIndex].SetActive(true);
        //put it at gun fire point
        bullets[bulletIndex].transform.position = initPos;
        //set gun behaviour to be used to create weapon tracer
        bullets[bulletIndex].GetComponent<bulletBehaviour>()?.setGunBehaviour(fireFrom);
        //fire
        bullets[bulletIndex].GetComponent<Rigidbody2D>().velocity = v;
        //move the index/pointer to next bullet
        bulletIndex = (bulletIndex + 1) % bulletInstanceCnt;
    }
}
