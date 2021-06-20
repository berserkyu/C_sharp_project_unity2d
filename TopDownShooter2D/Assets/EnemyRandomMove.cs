using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startPosition;
    private Vector3 roamPosition;
    private float randomMovementFrameCounter = 4f,movingSpeed = 3f;
    private float distance = 10f;
    
    public void randomMoveUpdate()
    {
        randomMovementFrameCounter += Time.deltaTime;
        if (randomMovementFrameCounter >= 1.5)
        {
            Vector3 curPosition = rb.position;
            float turningAngle = Random.Range(0, 359);
            turningAngle = (turningAngle/180)*Mathf.PI;
            Vector3 targetPosition = new Vector3(curPosition.x + distance *Mathf.Cos(turningAngle), curPosition.y +distance* Mathf.Sin(turningAngle), curPosition.z);
            Vector3 dir = (targetPosition - curPosition).normalized;
            rb.velocity = dir * movingSpeed;
            resetRandomMoveFrameCnt();
        }
    }
    public void resetRandomMoveFrameCnt()
    {
        randomMovementFrameCounter = 0;
    }

    public void hitNonPlayer(Collision2D collision)
    {
        Vector2 contactPt = collision.GetContact(0).point;
        Vector3 t = new Vector3(contactPt.x,contactPt.y, 0);
        Vector3 dir = (contactPt - rb.position).normalized;
        rb.velocity = dir * (-movingSpeed);
        resetRandomMoveFrameCnt();
    }



    /*
     private void Awake()
    {
        pathfindingMovement = GetComponent<EnemyPathfindingMovement>();
    }
    private void Start()
    {
        startPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }
     private void Update()
    {
        //moveToTimer?
        pathfindingMovement.MoveTo(roamPosition);
        float reachedPositionDistance = 1f;
        if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0);
    }
    private Vector3 GetRoamingPosition()
    {
        //random dir ?
        return startPosition + GetRandomDir() * Random.Range(10f, 70f);
    }
    */
}
     
     

    