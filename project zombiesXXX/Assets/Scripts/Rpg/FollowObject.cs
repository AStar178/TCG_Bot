using UnityEngine;

public class FollowObject : MonoBehaviour {
    public GameObject s;
    [SerializeField] bool rotate;
    private void Update() {
        
        transform.position = s.transform.position;
        if (rotate)
            transform.localEulerAngles = s.transform.localEulerAngles;
    }

}