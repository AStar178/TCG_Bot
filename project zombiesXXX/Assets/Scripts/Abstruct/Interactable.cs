using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    
    [SerializeField] public bool caninteracted = true;
    [SerializeField] public string OneText;
    [SerializeField] public string TwoText;
    public virtual void OnInteracted()
    {

    }

    public virtual string GetText()
    {
        
        return OneText + TwoText;
    }

}