using System.Threading.Tasks;
using UnityEngine;
using RPG.Dialogue;

public class PlayerActionScript : MonoBehaviour
{
    private FirstPersonMove Movement;
    private PlayerConversant Conversation;
    private PlayerStat stat;
    private Option opt;
    #region Grab able Obj
    [Header("PickUp / Drop")]
    public Transform Cam;
    public Transform ObjectTransformPoint;
    private bool drap;
    [Header("Weapon")]
    public Transform Hand;
    public GameObject Father;
    public float ExhustTime = 2;
    private bool Equipted;
    private Vector3 BulletOffset, rotation;
    [Space]
    private float throwCooldown;
    private float throwForce;
    private float throwUpwardForce;
    private bool readyToThrow = true;
    private bool throwAble;
    private float ShotMulti;

    private ObjectGrabbable objectGrabbable;
    #endregion

    private void Start()
    {
        Movement = GetComponent<FirstPersonMove>();
        Conversation = GetComponent<PlayerConversant>();
        readyToThrow = true;
        stat = Static.PlayerStat;
        opt = Static.option;
    }

    // Update is called once per frame
    void Update()
    {
        IntractRayShow();

        PickupItem();
        InputHandler();

        if (Input.GetKeyDown(opt.Intract))
        {
            float talkDistance = 3f;

            if (Physics.Raycast(Cam.position, Cam.forward, out RaycastHit rayHit, talkDistance))
            {
                if (rayHit.transform.TryGetComponent(out AIConversant intractable))
                {
                    intractable.OnIntract(Conversation);
                }
                if (rayHit.transform.TryGetComponent(out Intract intract))
                {
                    intract.OnIntract();
                }
            }
        }

    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(opt.Throw) && readyToThrow == true && throwAble && !objectGrabbable.EquiptAble)
        {
            drap = false;
            Throw();
        }

        if (Input.GetKeyDown(opt.Throw) && readyToThrow == true && throwAble && Equipted)
        {
            unequipt();
        }

        if (Input.GetKeyDown(opt.Use) && Equipted && objectGrabbable.WeaponType == WeaponType.Gun)
        {
            StartShot(1);
        }

        if (Input.GetKey(opt.Use) && Equipted && objectGrabbable.WeaponType == WeaponType.Bow)
        {
            ShotBow();
        }

        if (Input.GetKeyUp(opt.Use) && Equipted && objectGrabbable.WeaponType == WeaponType.Bow)
        {
            StartShot(ShotMulti);
            ShotMulti = 0;
        }
    }

    private void PickupItem()
    {
        if (Input.GetKeyDown(opt.Pick) && !Equipted)
        {
            // Not having an object in our hand trying to grab it
            if (objectGrabbable == null)
            {
                float pickupDistance = 3f;

                if (Physics.Raycast(Cam.position, Cam.forward, out RaycastHit rayHit, pickupDistance))
                    if (rayHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        if (!objectGrabbable.Bussy)
                        {
                            objectGrabbable.Grab(ObjectTransformPoint);
                            throwAble = objectGrabbable.ThrowAble;
                            if (throwAble)
                            {
                                throwForce = objectGrabbable.ThrowForce;
                                throwUpwardForce = objectGrabbable.ThrowUpwardForce;
                                throwCooldown = objectGrabbable.throwCooldown;
                            }
                            if (objectGrabbable.EquiptAble)
                            {
                                beforeEquipte();
                                equipt();
                            }
                        }

                    }
            }
        }
        if (Input.GetKeyDown(opt.Drop) && !Equipted && objectGrabbable)
        {
            throwForce = 5;
            throwUpwardForce = 0;
            drap = true;
            Throw();
        }
        if (Input.GetKeyDown(opt.Drop) && Equipted && objectGrabbable)
        {
            throwForce = 5;
            throwUpwardForce = 0;
            drap = true;
            unequipt();
        }
    }

    public void IntractRayShow()
    {
        if (Static.InMenu())
        {
            Static.UiManager.HideIntract();
            return;
        }

        if (Physics.Raycast(Cam.position, Cam.forward, out RaycastHit rayHit, 3))
        {
            if (rayHit.transform.GetComponent<ObjectGrabbable>() != null && !rayHit.transform.GetComponent<ObjectGrabbable>().Bussy)
            {
                Static.UiManager.ShowIntract("Item");
                return;
            }

            if (rayHit.transform.GetComponent<AIConversant>() != null)
            {
                Static.UiManager.ShowIntract("Talk");
                return;
            }

            if (rayHit.transform.GetComponent<Intract>() != null)
            {
                Static.UiManager.ShowIntract("Intract");
                return;
            }
        }


        Static.UiManager.HideIntract();

    }

    #region Grabable Obj

    void StartShot(float M)
    {
        if (objectGrabbable.CostType == CostType.mana)
            if (stat.Drained)
                return;

        if (objectGrabbable.CostType == CostType.stamina)
            if (stat.Exhusted)
                return;

        if (objectGrabbable.CostType == CostType.mana)
        {
            Static.PlayerStat.DamageMana(objectGrabbable.Cost);
        }
        if (objectGrabbable.CostType == CostType.stamina)
        {
            Static.PlayerStat.DamageStamina(objectGrabbable.Cost);
        }

        Shot(M);
    }

    void beforeEquipte()
    {
        rotation = objectGrabbable.rotation;
        BulletOffset = objectGrabbable.BulletOffset;
    }

    void Shot(float M)
    {
        // Weapon Transform
        Transform g = objectGrabbable.transform;
        // create bullet
        if (objectGrabbable.Bullet == null)
            return;
        #region
        GameObject b = Instantiate(objectGrabbable.Bullet, new Vector3(BulletOffset.x + g.position.x, BulletOffset.y + g.position.y, BulletOffset.z + g.position.z), Quaternion.identity);
        #endregion
        // get damage
        b.GetComponent<BulletN>().damage = objectGrabbable.damage;
        // tell who is the gun so the bullet dont destroy on Gun
        b.GetComponent<BulletN>().Gun = objectGrabbable.gameObject;
        // tell who is the father so the bullet dont destroy on player
        b.GetComponent<BulletN>().Father = Father;
        // Set the rotation
        b.transform.eulerAngles = b.transform.eulerAngles + objectGrabbable.GetOBj().transform.eulerAngles;
        // Get throw position
        Vector3 forceToAdd = (Cam.forward * objectGrabbable.BulletForce * M + Cam.up * objectGrabbable.BulletUpwardForce * M);
        // shot the bullet
        b.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);
    }

    void ShotBow()
    {
        if (ShotMulti < objectGrabbable.MultiCap)
            ShotMulti += 0.01f;
    }

    void equipt()
    {
        objectGrabbable.Equipt(Hand);
        objectGrabbable.gameObject.AddComponent<dontDestroy>();
        objectGrabbable.GetComponent<Collider>().isTrigger = true;
        objectGrabbable.transform.eulerAngles = rotation;
        Equipted = true;
    }

    void unequipt()
    {
        objectGrabbable.Unquipt();
        objectGrabbable.GetComponent<Collider>().isTrigger = false;
        Equipted = false;
        Throw();
    }

    void Drop()
    {
        objectGrabbable.Drop();
        objectGrabbable = null;
        throwAble = false;
    }

    void Throw()
    {
        readyToThrow = false;

        // instancient object if more than 1 totalthrow
        GameObject go;

        
        go = objectGrabbable.gameObject;
        objectGrabbable.Transform = Cam;

        // get rigidbody
        Rigidbody goRb = go.GetComponent<Rigidbody>();

        goRb.drag = 0;
        goRb.useGravity = true;
        if (!drap)
            objectGrabbable.thow();

        // implement Throw Cooldown
        ResetThrow();

        // add force
        Vector3 forceToAdd = Cam.forward * throwForce + Cam.up * throwUpwardForce;

        if (objectGrabbable.throwEffect)
        {
            throwEffect();
        }

        Drop();

        goRb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    private void throwEffect()
    {
        if (objectGrabbable.throwEffect.pushOnCollid)
        {
            // add force
            objectGrabbable.ForceAdd = Cam.forward * objectGrabbable.throwEffect.pushForce;
        }
    }

    public static async Task Wait(float Timez)
    {
        while (Timez > 0)
        {
            Timez -= Time.deltaTime;
            await Task.Yield();
        }
    }

    async void ResetThrow()
    {
        await Wait(throwCooldown);
        readyToThrow = true;
    }
    #endregion

}
