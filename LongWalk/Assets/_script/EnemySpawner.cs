using UnityEngine;
using System.Collections;
using UnityEditor;
     
public class EnemySpawner : MonoBehaviour {
     
    public int numObjects = 10;
    public GameObject Enemy;
    public float radius;
    [SerializeField] public static Transform falafa;
    [SerializeField] public Transform sasa;
    private void Awake() {
        falafa = sasa;
    }
     
    void Start() {
        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++){
            Vector3 pos = RandomCircle(center);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
            Instantiate(Enemy, pos, rot);
        }
    }
     
    Vector3 RandomCircle ( Vector3 center  ){
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
    private void OnDrawGizmosSelected() {
        
        Handles.DrawWireDisc(transform.position , Vector3.up , radius);

    }
}