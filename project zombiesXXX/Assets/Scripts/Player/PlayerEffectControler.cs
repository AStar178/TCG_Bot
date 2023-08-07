using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public abstract class PlayerEffectControler : PlayerComponetSystem
{
    private float t;
    [SerializeField] Material PlayerMatriale;
    public Transform feetpos;
    [SerializeField] ParticleSystem[] wakeparticale;
    public Animator animator;
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

    public void WakeEffectleft()
    {
        wakeparticale[0].Play();
    }
    public void WakeEffectright()
    {

        wakeparticale[1].Play();

    }



}