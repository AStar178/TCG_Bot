using System.Threading.Tasks;
using UnityEngine;

public class PlayerEffect : PlayerComponetSystem {
    
    private float t;
    [SerializeField] Material PlayerMatriale;
    [SerializeField] ParticleSystem[] jetpackparticale;
    public async void TurnOnInvisableEffectTime(float time)
    {
        t = time;
        while (t > 0)
        {
            t -= Time.deltaTime;
            PlayerMatriale.SetFloat( "_Float_2" , Mathf.Lerp( PlayerMatriale.GetFloat("_Float_2") , 0 , 0.025f ) );

            await Task.Yield();
        }

        float newtime = 2;

        while (newtime > 0)
        {
            newtime -= Time.deltaTime;
            PlayerMatriale.SetFloat( "_Float_2" , Mathf.Lerp( PlayerMatriale.GetFloat("_Float_2") , 1 ,  0.025f ) );
            await Task.Yield();     
        }
        PlayerMatriale.SetFloat( "_Float_2" , 1 );
    }
    public void TurnOnJectPackEffect()
    {
        for (int i = 0; i < jetpackparticale.Length; i++)
        {
            jetpackparticale[i].Play();
        }
    }
    public void TurnOffJectPackEffect()
    {
        for (int i = 0; i < jetpackparticale.Length; i++)
        {
            jetpackparticale[i].Stop();
        }
    }
}