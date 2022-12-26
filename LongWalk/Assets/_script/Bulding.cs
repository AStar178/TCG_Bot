using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulding : MonoBehaviour
{

    [SerializeField] List<GameObject> _BulidingBlockPrefabs;
    [SerializeField] private float MaxDistance;
    [SerializeField] private LayerMask GroundLayer;
    void Update()
    {
        Vector3 Pos = new Vector3();

        Physics.Raycast(transform.position , transform.forward , out RaycastHit hit , MaxDistance , GroundLayer );

        if (hit.collider == null)
            return;
        
        Pos = hit.point;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Instantiate(Build(1 - 1)  , Pos , Quaternion.LookRotation(hit.normal));
        
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Instantiate(Build(2 - 1)  , Pos , transform.rotation);

    }

    private GameObject Build(int index)
    {
        for (int i = 0; i < _BulidingBlockPrefabs.Count; i++)
        {
            if (i == index)
                return _BulidingBlockPrefabs[i];
        }

        return null;
    } 

    private void OnDrawGizmos() {
        
        Gizmos.DrawLine(transform.position , transform.position + transform.forward * MaxDistance);

    }
}
