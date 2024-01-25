using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EntityScript
{
    public Rigidbody enemyRB;
    public List<Transform> waypoints;

    Transform nextWaypoint;
    public int waypointIndex = 0;
    public float waypointReachedDistance;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        nextWaypoint = waypoints[waypointIndex];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flying();
    }

    private void Flying()
    {
        Vector2 directiontoWaypoint = (nextWaypoint.position - transform.position).normalized;
        Debug.Log("Distance:" + directiontoWaypoint);

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        enemyRB.velocity = directiontoWaypoint * moveSpeed;

        if(distance <= waypointReachedDistance)
        {
            waypointIndex++;

            if(waypointIndex >= waypoints.Count) 
            {
                waypointIndex = 0;
            }

            nextWaypoint = waypoints[waypointIndex];
        }

        
    }
}
