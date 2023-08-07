using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public abstract class PlayerEffect : PlayerComponetSystem
{
    private float t;
    [SerializeField] Material PlayerMatriale;
    [SerializeField] ParticleSystem[] jetpackparticale;
    public Transform feetpos;
    [SerializeField] VisualEffect visualEffect;
    [SerializeField] ParticleSystem[] thatparicale;
    [SerializeField] ParticleSystem[] wakeparticale;
    [SerializeField] ParticleSystem[] LazerParicale;
    [SerializeField] public LineRenderer lineRenderer;
    public VisualEffect Shooteffect;
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
    public void JumpSomeTimeThing()
    {
        var wow = Instantiate(visualEffect, Vector3.zero, Quaternion.identity);
        wow.transform.position = feetpos.transform.position;
        Destroy(wow.gameObject, 5);
        for (int i = 0; i < thatparicale.Length; i++)
        {
            thatparicale[i].Play();
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

    public async void StopLazer()
    {
        for (int i = 0; i < LazerParicale.Length; i++)
        {

            LazerParicale[i].Stop();

        }
        var t = 1f;
        //1 , 0.21875
        float x = 1;
        float y = 0.5f;
        float z = 0.21875f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            AnimationCurve curve = new AnimationCurve();
            x = Mathf.Lerp(x, 0, 8 * Time.deltaTime);
            y = Mathf.Lerp(y, 0, 8 * Time.deltaTime);
            z = Mathf.Lerp(z, 0, 8 * Time.deltaTime);
            curve.AddKey(0, x);
            curve.AddKey(.5f, y);
            curve.AddKey(1f, z);
            lineRenderer.widthCurve = curve;

            await Task.Yield();
        }

        lineRenderer.enabled = false;

    }
    public async void StartLazer()
    {
        for (int i = 0; i < LazerParicale.Length; i++)
        {

            LazerParicale[i].Play();

        }
        lineRenderer.enabled = true;
        var t = 1f;
        //1 , 0.21875
        float x = 0;
        float y = 0f;
        float z = 0f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            AnimationCurve curve = new AnimationCurve();
            x = Mathf.Lerp(x, 1, 8 * Time.deltaTime);
            y = Mathf.Lerp(y, 0.5f, 8 * Time.deltaTime);
            z = Mathf.Lerp(z, 0.21875f, 8 * Time.deltaTime);
            curve.AddKey(0, x);
            curve.AddKey(.5f, y);
            curve.AddKey(1f, z);
            lineRenderer.widthCurve = curve;

            await Task.Yield();
        }



    }

}
