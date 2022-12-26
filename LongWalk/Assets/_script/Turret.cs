using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform Head;
    [SerializeField] Transform Nick;
    [SerializeField] Transform enemy; 
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] float Range;
    [SerializeField] float AttackSpeed;

    public Vector3 HeadOffset;

    void Update()
    {
        enemy = null;
        var Colider = Physics.OverlapSphere(Head.transform.position , Range , EnemyLayer);
        
        if (Colider.Length == 0)
            return;
        enemy = Colider.OrderBy( k => (Head.transform.position - k.transform.position).magnitude ).FirstOrDefault().transform;
        if (enemy == null)
            return;
        
        float angle = Mathf.Atan2(enemy.transform.position.x-Head.transform.position.x,enemy.transform.position.z-Head.transform.position.z) * Mathf.Rad2Deg;
        Vector3 newVec = new Vector3(Head.eulerAngles.x,0f,Head.eulerAngles.z);
        newVec.y = angle;
        Head.eulerAngles = newVec;

        Nick.transform.LookAt(enemy , Vector3.up);
        Nick.transform.eulerAngles += HeadOffset;

        //Head.transform.localRotation = Quaternion.Lerp( transform.localRotation , Quaternion.LookRotation((enemy.transform.position - Head.transform.position).normalized) , 0.01f );

    }
    private void OnDrawGizmosSelected() {

        if (Head == null)
            return;

        Gizmos.DrawWireSphere(Head.transform.position , Range);

    }
}
