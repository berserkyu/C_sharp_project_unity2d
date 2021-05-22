using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMove : MonoBehaviour
{
    private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startPosition;
    private Vector3 roamPosition;

    private void Awake()
    {
        pathfindingMovement = GetComponent<EnemyPathfindMovement>();
    }
    private void Start()
    {
        startPosition = transform.position;
        roamPosition = GetRoamingPosition;
    }

    private void Update()
    {
        pathfindingMovement.MoveTo(roamPosition);
        float reachedPositionDistance = 1f;
        if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
        {
            roamPosition = GetRandomPosition();
        }
    }
    private Vector3 GetRandomPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }
}