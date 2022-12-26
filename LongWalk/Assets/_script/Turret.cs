using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform Head;
    [SerializeField] Transform enemy; 
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] float Range;
    [SerializeField] float AttackSpeed;
    
    void Update()
    {
        var Colider = Physics.OverlapSphere(Head.transform.position , Range , EnemyLayer);

        var target = Colider.OrderBy( k => (k.transform.position - Head.transform.position).magnitude ).FirstOrDefault();

        if (target == null)
            return;
        
        Head.transform.localRotation = Quaternion.Lerp( transform.localRotation , Quaternion.LookRotation((target.transform.position - Head.transform.position).normalized) , 0.1f );

    }
    private void OnDrawGizmosSelected() {

        Gizmos.DrawWireSphere(Head.transform.position , Range);
        
    }
}
