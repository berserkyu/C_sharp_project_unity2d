using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoblinFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    private Rigidbody2D rb;
    private Transform player;
    public HealthBar healthBar;
    public HealthSystem healthSystem;
    private int damageVal;
    private EnemyPathfindingMovement pathfinding;
    private EnemyRandomMove randMove;
    private bool foundPlayer = false;

    void Start()
    {
        pathfinding = GetComponent<EnemyPathfindingMovement>();
        randMove = GetComponent<EnemyRandomMove>();
        damageVal = 10;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        HealthSystem healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite)
        {
            foundPlayer = true;
            pathfinding.greedyPathFindingUpdate();
        }
        else
        {
            if (foundPlayer) randMove.resetRandomMoveFrameCnt();
            foundPlayer = false;
            randMove.randomMoveUpdate();
        }


        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject gb = collision.gameObject.transform.GetChild(0).gameObject;
            if (gb == null) return;
            PlayerBehaviour pb = gb.GetComponent<PlayerBehaviour>();
            if (pb != null) pb.damage(damageVal);
        }
        else
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFromPlayer < 10 || foundPlayer)
            {
                pathfinding.hitNonPlayer(collision);
            }
            else
            {
                randMove.hitNonPlayer(collision);
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
