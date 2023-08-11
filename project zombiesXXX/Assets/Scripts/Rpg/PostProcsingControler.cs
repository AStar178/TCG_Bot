using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcsingControler : MonoBehaviour {
    
    public static PostProcsingControler Current;
    public Volume volume;
    public ColorAdjustments colorAdjustments;

    private void Awake() {
        
        Current = this;


        volume.profile.TryGet(out colorAdjustments);
    }





}