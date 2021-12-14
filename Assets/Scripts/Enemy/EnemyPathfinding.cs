using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathfinding : MonoBehaviour
{
    Transform target;
    [SerializeField] float enemySpeed, destinationReachedDistance, pathUpdateInterval;
    Path path;
    int currentWayPoint;
    bool reachedDestination;
    float distanceToDestination;
    Vector2 lookDirection;
    Seeker seeker;
    Rigidbody2D enemyRB;
    Vector2 directionMoving;
    void OnEnable()
    {
        target = GameObject.Find("Player").transform;
        seeker = GetComponent<Seeker>();
        enemyRB = GetComponent<Rigidbody2D>();
        reachedDestination = false;
        currentWayPoint = 0;

        InvokeRepeating("UpdatePath", 0f, pathUpdateInterval);
    }
    void PathConstructed(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(enemyRB.position, target.position, PathConstructed);
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            Debug.Log("Path Error, Update Function Stalled");
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedDestination = true;
            Debug.Log("Reached Destination");
        }
        else
            reachedDestination = false;

        try
        {
            directionMoving = ((Vector2)path.vectorPath[currentWayPoint] - enemyRB.position).normalized;

            if (Vector2.Distance((Vector2)path.vectorPath[currentWayPoint], enemyRB.position) > 0.2)
                lookDirection = (Vector2)path.vectorPath[currentWayPoint] - enemyRB.position;
            else
                lookDirection = (Vector2)path.vectorPath[currentWayPoint + 1] - enemyRB.position;

            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg));
            enemyRB.MovePosition(enemyRB.position + (directionMoving * enemySpeed * Time.fixedDeltaTime));
            distanceToDestination = Vector2.Distance(enemyRB.position, path.vectorPath[currentWayPoint]);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        if (distanceToDestination < destinationReachedDistance && !reachedDestination)
            currentWayPoint++;
    }
}
