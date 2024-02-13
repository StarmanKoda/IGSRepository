using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingShooterEnemy : EntityScript
{
    public Rigidbody enemyRB;
    public List<Transform> waypoints;

    Transform nextWaypoint;
    public bool randomWaypoint;
    public int waypointIndex = 0;
    public float waypointReachedDistance;
    
    [Header("For Damage")]
    private float dmgTimer;
    private float InvincibilityTime = 0.5f;

    [Header("For Shooting")]
    public GameObject smartBullet;
    public Transform smartShotPosition;
    public float shotTimer;
    public float timeToShoot = 3f;
    public bool isSmartShot = true;

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
        if (dmgTimer < InvincibilityTime)
        {
            dmgTimer += Time.deltaTime;
        }
        if (shotTimer < timeToShoot)
        {
            shotTimer += Time.deltaTime;
        }
        else
        {
            shotTimer = 0;
            Shoot();
        }
    }

    private void Flying()
    {
        Vector2 directiontoWaypoint = (nextWaypoint.position - transform.position).normalized;


        float distance = Vector2.Distance(nextWaypoint.position, transform.position);



        enemyRB.velocity = directiontoWaypoint * moveSpeed;

        if (distance <= waypointReachedDistance)
        {
            if (!randomWaypoint)
            {
                waypointIndex++;

                if (waypointIndex >= waypoints.Count)
                {
                    waypointIndex = 0;
                }
            }
            else
            {
                waypointIndex = Random.Range(0, waypoints.Count);
            }

            Debug.Log(waypointIndex);
            nextWaypoint = waypoints[waypointIndex];

        }


    }
    void OnCollisionEnter(Collision coll)
    {
        if ((coll.gameObject.tag == "Player" || coll.gameObject.layer == 6) && (dmgTimer >= InvincibilityTime))
        {
            coll.gameObject.GetComponent<EntityScript>().takeDamage(atkDMG);
            coll.gameObject.GetComponent<Movement>().knockBack(transform, (float)knockBackForce);
            dmgTimer = 0f;
        }
    }
    
    void Shoot()
    {
        if(isSmartShot)
        {
            Instantiate(smartBullet, smartShotPosition.position , Quaternion.identity);
        }
    }
}
