using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    
    [SerializeField] public bool caninteracted = true;
    public virtual void OnInteracted()
    {

    }

}