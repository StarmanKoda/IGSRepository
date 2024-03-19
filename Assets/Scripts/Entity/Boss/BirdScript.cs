using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : BossEntityScript
{
    public Rigidbody enemyRB;
    public List<Transform> waypoints;
    public Transform player;
    private bool facingRight = true;

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
    public float heightclamp = 10f;

    [Header("For Boss Phase Management")]
    public int phasenumber = 0;
    public double[] phasecaps = { 350, 150 };
    private bool[] phasereached = { false, false };
    public bool swoopdirectionforward = true;
    private int dirSwitch;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        nextWaypoint = waypoints[waypointIndex];
    }

    private void Awake()
    {
        if (player == null)
        {
            player = gameObject.transform.Find("Player");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flying();
        if (dmgTimer < InvincibilityTime)
        {
            dmgTimer += Time.deltaTime;
        }
        if (shotTimer < timeToShoot && ((facingRight && player.gameObject.transform.position.x > gameObject.transform.position.x) || (!facingRight && player.gameObject.transform.position.x < gameObject.transform.position.x)))
        {
            if ((player.gameObject.transform.position.y >= gameObject.transform.position.y - heightclamp) || (player.gameObject.transform.position.y <= gameObject.transform.position.y + heightclamp))
            {
                shotTimer += Time.deltaTime;
            }
        }
        else if (shotTimer >= timeToShoot)
        {
            shotTimer = 0;
            Shoot();
        }

        //checks what phase the boss is on
        if (health <= phasecaps[0] && !phasereached[0])
        {
            phasenumber = 1;
            phasereached[0] = true;
        }
        if (health <= phasecaps[1] && !phasereached[1] && phasereached[0])
        {
            phasenumber = 2;
            phasereached[1] = true;
            randomWaypoint = true;
        }
    }

    private void Flying()
    {
        Vector2 directiontoWaypoint = (nextWaypoint.position - transform.position).normalized;


        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        UpdateDirection();



        enemyRB.velocity = directiontoWaypoint * moveSpeed[phasenumber];

        if (distance <= waypointReachedDistance)
        {
            if (!randomWaypoint)
            {
                if((waypointIndex == 0 || waypointIndex == waypoints.Count - 1) && phasenumber > 0)
                {
                    dirSwitch = Random.Range(0, 2);
                }

                if(dirSwitch == 0)
                {
                    swoopdirectionforward = true;
                }
                else if (dirSwitch == 1)
                {
                    swoopdirectionforward = false;
                }

                if (swoopdirectionforward)
                waypointIndex++;
                else
                waypointIndex--;

                if (waypointIndex >= waypoints.Count)
                {
                    waypointIndex = 0;
                }
                if (waypointIndex < 0)
                {
                    waypointIndex = (waypoints.Count - 1);
                }

                
            }
            else
            {
                waypointIndex = Random.Range(0, waypoints.Count);
            }

            //Debug.Log(waypointIndex);
            nextWaypoint = waypoints[waypointIndex];

        }


    }
    void OnCollisionEnter(Collision coll)
    {
        if ((coll.gameObject.tag == "Player" || coll.gameObject.layer == 6) && (dmgTimer >= InvincibilityTime))
        {
            coll.gameObject.GetComponent<EntityScript>().takeDamage(atkDMG[phasenumber]);
            coll.gameObject.GetComponent<Movement>().knockBack(transform, (float)knockBackForce);
            dmgTimer = 0f;
        }
    }

    void Shoot()
    {

        GameObject bullet = Instantiate(smartBullet, smartShotPosition.position, Quaternion.identity);
        bullet.GetComponent<EnemyProjectile>().moveSpeed = 2 * moveSpeed[phasenumber];
        bullet.GetComponent<EnemyProjectile>().atkDMG = (float)atkDMG[phasenumber];

    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;


        if (transform.localScale.x > 0)
        {
            if (enemyRB.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (enemyRB.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }

        if (transform.localScale.x == -1)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }
}
