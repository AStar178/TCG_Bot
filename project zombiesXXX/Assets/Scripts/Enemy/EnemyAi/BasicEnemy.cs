using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;
using System.Threading.Tasks;

public class BasicEnemy : MonoBehaviour
{
    public float AttackRange;
    public float AggroRange;
    // how close the enemy will stay to the player
    public float Speed;
    public float DeadZone;
    public float AttackCooldownSet;
    private float AttackCooldwon;
    public float AttackDamageMin;
    public float AttackDamageMax;
    public float Jump;
    public float JumpCooldownSet;
    private float JumpCooldown;
    public float RangeTime;
    public GameObject AttackEffec;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;
    public float SpeedSlow;

    public Path path;

    [Space]
    public Rigidbody rb;
    public Transform target;
    public LayerMask TargetLayer;
    private Seeker Seeker;
    private void OnEnable() {
        GetComponent<EnemyHp>().TakeDamageEvent += IamVeryAngery;
    }
    private void OnDisable() {
        GetComponent<EnemyHp>().TakeDamageEvent -= IamVeryAngery;
    }
    private void Start()
    {
        Seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        TargetFunction();

        if (ZAWARDO.Current != null)
        {
            if (ZAWARDO.Current.ZawardoTimeded)
            {
                rb.isKinematic = true;
                return;
            }
            rb.isKinematic = false;
        }

        AttackPlayer();

        if (target != null)
            Seeker.StartPath(transform.position, target.position, OnPathComplete);

        if (path == null)
        {
            return;
        }

        reachedEndOfPath = false;

        float distanceToWaypoint;
        while (true)
        {
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        Vector3 velocity = dir * Speed * speedFactor * ( 1 - SpeedSlow);

        velocity.y = rb.velocity.y;

        rb.velocity = velocity;

        if (JumpCooldown > 0)
            JumpCooldown -= Time.deltaTime;

        if ( target != null && target.position.y > transform.position.y && JumpCooldown <= 0)
        {
            rb.AddForce(Vector3.up * Jump, ForceMode.Impulse);
            JumpCooldown = JumpCooldownSet;
        }
    }
    private void IamVeryAngery(DamageData t)
    {
        target = t.target.transform;
        RangeTime = 5;
    }
    private void TargetFunction()
    {
        if (RangeTime > 0)
        {
            RangeTime -= Time.deltaTime;
            return;
        }

        if (target != null && Vector3.Distance(target.position, transform.position) > AggroRange)
            target = null;

        if (target == null)
        {
            Collider[] objects = Physics.OverlapSphere(transform.position, AggroRange, TargetLayer);

            foreach (Collider obj in objects)
            {
                if (obj != null)
                {
                    if (target != null && Vector3.Distance(obj.transform.position, transform.position) < Vector3.Distance(transform.position, target.position))
                        target = obj.transform;

                    else if (target == null)
                        target = obj.transform;
                }
            }
        }
    }

    private async void AttackPlayer()
    {
        // everything in here is self explaned bruh

        if (target != null)
            if (AttackCooldwon <= 0 && Vector3.Distance(target.position, transform.position) <= AttackRange)
            {
                AttackCooldwon = AttackCooldownSet;
                
                var d = Instantiate(AttackEffec , transform.position + transform.forward * 1.5f , Quaternion.identity);
                float xc = 0.6f;
                while (xc > 0)
                {
                    xc -= Time.deltaTime;
                    transform.LookAt(target.position + Vector3.up );
                    d.transform.position = transform.position + transform.forward * 1.5f;
                    await Task.Yield();
                }
                DamageData dammen = new DamageData();
                var x = Physics.OverlapBox(transform.position + transform.forward * AttackRange , boxHitSize , default , TargetLayer );
                dammen.DamageAmount = Random.Range(AttackDamageMin, AttackDamageMax);
                target.GetComponentInParent<IDamageAble>().TakeDamage(dammen);
                Destroy(d , 2);
                return;
            }
            AttackCooldwon -= Time.deltaTime;
    }
    [SerializeField] Vector3 boxHitSize; 
    private void OnDrawGizmosSelected() {
        
        Gizmos.DrawWireCube( transform.position + transform.forward * AttackRange , boxHitSize );

    }
}
