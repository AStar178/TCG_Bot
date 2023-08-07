using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public class MetroidEffect : PlayerEffectControler
{
    public static MetroidEffect Current;
    [SerializeField] ParticleSystem[] LazerParicale;
    [SerializeField] ParticleSystem[] jetpackparticale;
    [SerializeField] VisualEffect visualEffect;
    [SerializeField] ParticleSystem[] thatparicale;
    [SerializeField] public LineRenderer lineRenderer;
    public VisualEffect Shooteffect;
    private void Awake() {
        Current = this;
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
}