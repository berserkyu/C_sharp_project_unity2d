using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class EnemyPathfindingMovement : MonoBehaviour
{
    
    private const float SPEED = 30f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col2dTile;
    private float movingSpeed = 2;
    [SerializeField] private Transform playerTrans;
    private List<Vector3> pathVectorList;
    private List<List<int>> directions;
    private int currentPathIndex;
    private float pathfindingTimer;
    private Vector3 moveDir;
    private Vector3 lastMoveDir;
    private System.Random rand;
    private DequeVector3 dq;
    void Awake()
    {
        //dq = new DequeVector3(60);
        int r = 5;
        rand = new System.Random();
        directions = new List<List<int>>();
              //{deltaX,deltaY}:��        ��         ��         ��         ����       ����      ����          ����
        int[,] t = new int[,]{ { 0, r }, { 0, -r }, { -r, 0 }, { r, 0 }, { r, r }, { -r, r }, { r, -r }, { -r, -r } };
        for(int i = 0; i < 8; i++)
        {
            List<int> temp = new List<int>();
            temp.Add(t[i, 0]);
            temp.Add(t[i, 1]);
            directions.Add(temp);
        }
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void hitNonPlayer(Collision2D collision)
    {

    }
    public void greedyPathFindingUpdate()
    {
        Vector3 dirVecToGo;
        RaycastHit2D hitPlayer = Physics2D.Linecast(rb.position, playerTrans.position);
        if (hitPlayer.collider.gameObject.name=="Player")
        {
            dirVecToGo = (hitPlayer.collider.transform.position-transform.position).normalized;
            rb.velocity = dirVecToGo * movingSpeed;
            return;
        }
            
        Vector3 curPos = rb.position;
        int t = rand.Next(7);
        List<int> dirToGo = directions[7];
        float minDist = 100000f;

        foreach(List<int> dir in directions)
        {
            Vector3 tempDir = new Vector3(curPos.x + dir[0], curPos.y + dir[1], curPos.z);
            RaycastHit2D hit = Physics2D.Linecast(rb.position, tempDir);
            if (hit.collider != null)
            {
                continue;
            }
            
            if (Vector3.Distance(tempDir,playerTrans.position) < minDist)
            {
                dirToGo = dir;
                minDist = Vector3.Distance(tempDir, playerTrans.position);
            } 
        }
        Vector3 toGo = new Vector3(curPos.x + dirToGo[0], curPos.y + dirToGo[1], curPos.z);
        dirVecToGo = (toGo - curPos).normalized;
        rb.velocity = dirVecToGo * movingSpeed;

    }
    /*
     private void HandleMovement()
    {
        PrintPathfindingPath();
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            float reachedTargetDistance = 5f;
            if (Vector3.Distance(GetPosition(), targetPosition) > reachedTargetDistance)
            {
                moveDir = (targetPosition - GetPosition()).normalized;
                lastMoveDir = moveDir;
                //enemyMain.CharacterAnims.PlayMoveAnim(moveDir);
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                   // enemyMain.CharacterAnims.PlayIdleAnim();
                }
            }
        }
        else
        {
           // enemyMain.CharacterAnims.PlayIdleAnim();
        }
    }

    public void StopMoving()
    {
        pathVectorList = null;
        moveDir = Vector3.zero;
    }

    public List<Vector3> GetPathVectorList()
    {
        return pathVectorList;
    }

    private void PrintPathfindingPath()
    {
        if (pathVectorList != null)
        {
            for (int i = 0; i < pathVectorList.Count - 1; i++)
            {
                Debug.DrawLine(pathVectorList[i], pathVectorList[i + 1]);
            }
        }
    }

    public void MoveToTimer(Vector3 targetPosition)
    {
        if (pathfindingTimer <= 0f)
        {
            SetTargetPosition(targetPosition);
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        //GridPathFinding??????
        pathVectorList = GridPathfinding.instance.GetPathRouteWithShortcuts(GetPosition(), targetPosition).pathVectorList;
        pathfindingTimer = .1f;

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetLastMoveDir()
    {
        return lastMoveDir;
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
        //enemy main ?????????
        //enemyMain.EnemyRigidbody2D.velocity = Vector3.zero;
    }
     
     */


}
