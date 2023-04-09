using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectGrabbable : MonoBehaviour
{
    [HideInInspector]
    public bool Bussy;
    [HideInInspector]
    public Transform Transform;
    [Header("PickUp / Drop")]
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    [Header("Throw")]
    private bool Throwing;
    public bool ThrowAble;
    public float throwCooldown;
    public float ThrowForce;
    public float ThrowUpwardForce;
    public float ThrowDamage;
    public ThrowEffect throwEffect;
    [HideInInspector]
    public Vector3 ForceAdd;
    [Space]
    bool targetHit;
    [Header("Weapon Stat")]
    public bool EquiptAble;
    public WeaponType WeaponType;
    public GameObject Bullet;
    public CostType CostType;
    public int damage;
    public float Cost, BulletForce, BulletUpwardForce, MultiCap;
    public Vector3 BulletOffset, rotation, WeaponOffset;
    private bool equipted;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Transform GetOBj() => objectGrabPointTransform;

    public void Grab(Transform objectGrabPointTransform)
    {
        if (!Bussy)
        {
            this.objectGrabPointTransform = objectGrabPointTransform;
            Bussy = true;
            rb.drag = 5;
            rb.useGravity = false;
        }
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        Bussy = false;
        rb.drag = 1;
        rb.useGravity = true;
    }

    public void thow()
    {
        objectGrabPointTransform = null;
        Throwing = true;
    }

    public void Equipt(Transform Parent)
    {
        equipted = true;
        objectGrabPointTransform = Parent;
        rb.freezeRotation = true;
        Bussy = true;
        rb.drag = 5;
        rb.useGravity = false;
    }

    public void Unquipt()
    {
        equipted = false;
        objectGrabPointTransform = null;
        rb.freezeRotation = false;
        Bussy = false;
        rb.drag = 1;
        rb.useGravity = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!Throwing)
            return;

        // make sure only to stick to the first target hit
        if (targetHit)
            return;
        else
            targetHit = true;

        // get the collision rigidbody if had one
        collision.gameObject.TryGetComponent(out Rigidbody rb);

        // Check if hit an enemy and get enemy script to deal damage
        if (ThrowDamage != 0)
        {
            collision.gameObject.TryGetComponent(out HpScript hp);
            if (hp != null)
            {
                hp.Damage(ThrowDamage);
            }
        }

        // Stick it to the target Disabled for now for not working corricle
        /*
        if (StickAble)
        {
            // make sure projectile stick to the surface
            rb.isKinematic = true;

            // make sure this projectile move with the target
            transform.SetParent(collision.transform);
        }
        */


        Throwing = false;
        ForceAdd = new Vector3(0,0,0);

        // if throw effect was empty then return
        if (throwEffect == null)
            return;

        // if in the throw effect push on collid was true then push the target
        if (throwEffect.pushOnCollid && rb != null)
        {
            collision.gameObject.TryGetComponent(out NavMeshAgent nav);
            if (nav != null)
                rb.AddForce(ForceAdd * 4, ForceMode.Impulse);
            else
                rb.AddForce(ForceAdd, ForceMode.Impulse);
        }

        if (throwEffect.destroyOnCollid)
        {
            Destroy(this); Destroy(gameObject);

        }
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            if (!equipted)
            {
                float learpSpeed = 20f;
                Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * learpSpeed);
                rb.MovePosition(newPosition);
            }
            else if (equipted)
            {
                rb.MovePosition(objectGrabPointTransform.position + WeaponOffset);
                transform.eulerAngles = objectGrabPointTransform.eulerAngles + rotation;
            }
        }
    }
}

public enum WeaponType
{
    Gun,
    Bow
}

public enum CostType
{
    nothing,
    mana,
    stamina,
    health
}