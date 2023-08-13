using UnityEngine;
using Cinemachine;
using TMPro;

public class ChampionSelector : MonoBehaviour
{
    public GameObject Camvase;
    public GameObject IteamSpawn;
    public GameObject Indicator;
    public GameObject Targetxxxxx;

    [SerializeField]
    private Heroes[] Heroes;
    private int SelectedHero;

    private GameObject View;
    [SerializeField]
    private Vector3 ViewPoint;
    [SerializeField]
    private TMP_Text Counter;
    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text Description;

    public void Start()
    {
        SetInfo();
    }

    public void Update()
    {
        if (View != null)
            View.transform.position = ViewPoint + Heroes[SelectedHero].Offset;
    }

    public void SetInfo()
    {
        if (View != null)
        {
            Destroy(View);
            View = null;
        }

        var b = Instantiate(Heroes[SelectedHero].Hero, transform);
        Counter.text = $"{SelectedHero + 1} / {Heroes.Length}";
        Name.text = Heroes[SelectedHero].State.GetName();
        Description.text = Heroes[SelectedHero].State.GetDescription();
        View = b;
    }

    public void Select()
    {
        Destroy(View);
        var b = Instantiate(Heroes[SelectedHero].Hero, Player.Current.transform);
        var c = b.GetComponent<PlayerState>();
        c.SetHero(IteamSpawn, Indicator);
        Player.Current.PlayerState = c;
        Player.Current.PlayerInputSystem = b.GetComponent<PlayerInputSystem>();
        Player.Current.PlayerTargetSystem = b.GetComponent<PlayerTargetSystem>();
        b.GetComponent<PlayerTargetSystem>().SetTargexxxxx(Targetxxxxx);
        Player.Current.PlayerEffect = b.GetComponent<PlayerEffectControler>();
        Player.Current.PlayerThirdPersonController = b.GetComponent<ThirdPersonCam>();
        Player.Current.CameraControler.StarterAssetsInputs = b.GetComponent<PlayerInputSystem>();
        var cam = Player.Current.CameraControler;
        cam.GetComponent<CinemachineVirtualCamera>().Follow = b.GetComponent<PlayerEffectControler>().CameraRoot.transform;
        cam.GetComponent<CinemachineVirtualCamera>().LookAt = b.transform;
        Player.Current.UIManager.SetUIActive(true);

        Camvase.SetActive(false);
    }

    public void NextHero()
    {
        SelectedHero++;
        if (SelectedHero >= Heroes.Length)
            SelectedHero = 0;

        SetInfo();
    }
    public void PrevHero()
    {
        SelectedHero--;
        if (SelectedHero < 0)
            SelectedHero = Heroes.Length - 1;

        SetInfo();
    }
}

[System.Serializable]
public struct Heroes
{
    public GameObject Hero;
    public Vector3 Offset;
    public PlayerState State;
}