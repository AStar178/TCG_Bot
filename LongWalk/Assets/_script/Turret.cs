using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.VFX;
using UnityEngine.Pool;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform Head;
    [SerializeField] Transform Nick;
    [SerializeField] Transform enemy; 
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] float Range;
    [SerializeField] float AttackSpeed;
    [SerializeField] Transform BulitSpawnPos;
    [SerializeField] VisualEffect visualEffect;
    [SerializeField] float BulitSpeed;
    float TimeSpeed;
    public Vector3 HeadOffset;

    private void Start() {
        TimeSpeed = AttackSpeed;
    }


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
        
        TimeSpeed -= Time.deltaTime;

        if (TimeSpeed < 0)
            Shoot();

        //Head.transform.localRotation = Quaternion.Lerp( transform.localRotation , Quaternion.LookRotation((enemy.transform.position - Head.transform.position).normalized) , 0.01f );

    }

    private void Shoot()
    {
        
        var buuuuu = EnemySpawner.Bulits.Get();

        buuuuu.transform.position = BulitSpawnPos.position; buuuuu.transform.rotation = Quaternion.LookRotation((enemy.transform.position - BulitSpawnPos.transform.position).normalized);
        buuuuu.GetComponent<Rigidbody>().velocity = (enemy.transform.position - buuuuu.transform.position).normalized * BulitSpeed;
        TimeSpeed = AttackSpeed;
        visualEffect.Play();
    }

    private void OnDrawGizmosSelected() {

        if (Head == null)
            return;

        Gizmos.DrawWireSphere(Head.transform.position , Range);

    }
}
