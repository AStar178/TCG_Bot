using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bullet : MonoBehaviour
{
    public GameObject onHit;
    public Damage Damage;
    public LayerMask target;
    public float Size;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);

        if (Contains(target , other.gameObject.layer))
            Destroy( gameObject );
        
    }

    private void OnDestroy()
    {
        GameObject onhit = Instantiate(onHit);
        onhit.transform.position = transform.position;
        List<Collider> a = Physics.OverlapSphere(transform.position, Size, target).ToList();
        Destroy(onhit, 1);
    }
    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
