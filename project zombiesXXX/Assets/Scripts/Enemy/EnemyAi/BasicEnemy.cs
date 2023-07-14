using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float Speed;
    public float AttackRange;
    public float AggroRange;
    // how close the enemy will stay to the player
    public float DeadZone;
    public float AttackCooldownSet;
    private float AttackCooldwon;
    public float AttackDamageMin;
    public float AttackDamageMax;

    [Space]
    public Transform target;
    public LayerMask TargetLayer;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        TargetFunction();

        AttackPlayer();

        if (target != null && Vector3.Distance(transform.position, target.position) > DeadZone)
        {
            rb.velocity = (target.position - transform.position).normalized * Speed;
        }
        else
            rb.velocity = new Vector3(0,rb.velocity.y,0);
    }

    private void TargetFunction()
    {
        // check if the target is not null if it wan't then it well check if it's out of aggro range if both were true then set the target to null
        if (target != null && Vector3.Distance(target.position, transform.position) > AggroRange)
            target = null;

        // check if the target is null
        if (target == null)
        {
            // get all the possible targets of the selected layer
            Collider[] objects = Physics.OverlapSphere(transform.position, AggroRange, TargetLayer);

            // do a foreach loop to set the target
            foreach (Collider obj in objects)
            {
                // if the selected obj in the loop wasn't null continue
                if (obj != null)
                {
                    // if the target wasn't null and check if the obj is closer or the target; if obj is closer then set it as the target.
                    // we check if the target is not null becuase down we set it if it was null and because this is loop we end up here again so we dont want to set
                    // the target to the obj if it isn't closer than the target.
                    if (target != null && Vector3.Distance(obj.transform.position, transform.position) < Vector3.Distance(transform.position, target.position))
                        target = obj.transform;
                    // if the target IS null then set the obj as the target
                    else if (target == null)
                        target = obj.transform;
                }
            }
        }
    }

    private void AttackPlayer()
    {
        // everything in here is self explaned bruh

        if (target != null)
            if (AttackCooldwon <= 0 && Vector3.Distance(target.position, transform.position) <= AttackRange)
            {
                DamageData dammen = new DamageData();
                dammen.DamageAmount = Random.Range(AttackDamageMin, AttackDamageMax);
                target.GetComponentInParent<IDamageAble>().TakeDamage(dammen);
                AttackCooldwon = AttackCooldownSet;
            }
            else AttackCooldwon -= Time.deltaTime;
    }
}
