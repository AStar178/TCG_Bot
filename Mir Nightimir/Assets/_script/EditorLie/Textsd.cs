using UnityEngine;

public class Textsd : MonoBehaviour {
    
    [SerializeField] Transform target;
    private void OnDrawGizmos() {
        
        if ( target is null )
            return;
        
        var dir = ( target.transform.position - transform.transform.position ).normalized;
        Gizmos.DrawLine( transform.position , transform.position + dir);
        transform.LookAt( target );

        // var raduis = Mathf.Atan2( dir.y , dir.x );
        // var angel = raduis * Mathf.Rad2Deg; 
        // transform.localRotation = Quaternion.Euler( 0 , 0 , angel );
    }

}