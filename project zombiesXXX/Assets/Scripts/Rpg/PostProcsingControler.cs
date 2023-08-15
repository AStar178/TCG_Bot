using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcsingControler : MonoBehaviour {
    
    public static PostProcsingControler Current;
    public Volume volume;
    public ColorAdjustments colorAdjustments;
    public ChromaticAberration chromaticAberration;
    public LensDistortion lensDistortion;

    private void Awake() {
        
        Current = this;


        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out lensDistortion);
    }





}