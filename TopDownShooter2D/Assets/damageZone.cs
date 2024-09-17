using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageZone : MonoBehaviour
{
    [SerializeField] private int dmg = 5;
    public void rotate(int i)
    {
        switch (i)
        {
            case 1:
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case 2:
                transform.eulerAngles = new Vector3(0, 0, 179);
                break;
            case 3:
                transform.eulerAngles = new Vector3(0, 0, -90);
                break;
            default:
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject gb = collision.gameObject.transform.GetChild(0).gameObject;
            if (gb == null) return;
            PlayerBehaviour pb = gb.GetComponent<PlayerBehaviour>();
            if (pb != null) pb.damage(dmg);
        }
    }
}
