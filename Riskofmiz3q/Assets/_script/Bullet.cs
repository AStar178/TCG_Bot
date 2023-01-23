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
        GameObject onhit = Instantiate(onHit);
        onhit.transform.position = transform.position;
        List<Collider> a = Physics.OverlapSphere(transform.position, Size, target).ToList();
        Destroy(gameObject);
        Destroy(onhit, 6);
    }
}
