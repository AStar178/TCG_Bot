using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public abstract class PlayerEffectControler : PlayerComponetSystem
{
    private float t;
    [SerializeField] Material PlayerMatriale;
    public Transform feetpos;
    [SerializeField] ParticleSystem[] wakeparticale;
    public Animator animator;
    [SerializeField] public float LOR;
    public GameObject CameraRoot;
    public async void TurnOnInvisableEffectTime(float time)
    {
        t = time;
        while (t > 0)
        {
            t -= Time.deltaTime;
            PlayerMatriale.SetFloat("_Float_2", Mathf.Lerp(PlayerMatriale.GetFloat("_Float_2"), 0, 0.025f));

            await Task.Yield();
        }

        float newtime = 2;

        while (newtime > 0)
        {
            newtime -= Time.deltaTime;
            PlayerMatriale.SetFloat("_Float_2", Mathf.Lerp(PlayerMatriale.GetFloat("_Float_2"), 1, 0.025f));
            await Task.Yield();
        }
        PlayerMatriale.SetFloat("_Float_2", 1);
    }
    float x;
    private void Update() {
        
        x -= Time.deltaTime;

        if (x > 0)
            return;

        x = 0.25f;

        var collider = Physics.OverlapSphere( transform.position , LOR , SpawnerManager.Current.LoFLayerMask ).ToList();

        for (int i = 0; i < collider.Count; i++)
        {
            if (collider[i].gameObject.TryGetComponent<LOF>(out var lOF) && Vector3.Dot( Player.Current.CameraControler.transform.forward , (Player.Current.CameraControler.transform.position - collider[i].transform.position).normalized ) < 0)
                lOF.Enable();
        }
        for (int i = 0; i < SpawnerManager.Current.ListOfObject.Count; i++)
        {
            if (SpawnerManager.Current.ListOfObject[i].anibale == true)
                continue;
            SpawnerManager.Current.ListOfObject[i].DISABLE();
        }

    }
    public void WakeEffectleft()
    {
        wakeparticale[0].Play();
    }
    public void WakeEffectright()
    {

        wakeparticale[1].Play();

    }

    private void OnDrawGizmosSelected() {
        
        Gizmos.DrawWireSphere(transform.position , LOR);

    }

}