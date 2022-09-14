using UnityEngine;
using System;
using TMPro;
using DG.Tweening;
using System.Threading.Tasks;

public class PlayerWeaponManger : MonoBehaviour 
{    
    public GameObject OnMeeleHit;
    public GameObject OnMagicHit;
    public float AttackSpeed;
    public float DamageAd;
    public float DamageAp;
    public float AmoroReduse;
    public float MagicReduse;
    public Action<int , Vector2 , bool> OnDealDamage;
    [SerializeField] public AbilityWeapons CurrentWeapons;
    [SerializeField] public GameObject TextFonstDamage;
    [HideInInspector] public float attackSpeed;
    [SerializeField] Color[] TextColors; 
    private void OnEnable() {
        OnDealDamage += OnDealDamageFuncens;
    }
    private void OnDisable() {
        OnDealDamage -= OnDealDamageFuncens;
    }

    private void OnDealDamageFuncens(int obj , Vector2 pos , bool ap)
    {   
        var Texts = Instantiate(TextFonstDamage , pos , Quaternion.identity);
        var fonts = Texts.GetComponentInChildren<TMP_Text>();
        fonts.text = obj.ToString();
        fonts.color = TextColors[0];
        if (ap == true)
            fonts.color = TextColors[1];
        
        Tween tween = Texts.transform.DOMove(pos += new Vector2(0 , 1.25f) , 1f);
        KillTween( 1f , tween , Texts.transform.gameObject.transform.gameObject );
    }

    private void Start() {
        if (CurrentWeapons == null) { return; }

        CurrentWeapons.StartAbilityWp(Player.Singleton);
    }
    private void Update() 
    {
        if(attackSpeed > 0)
        {
            attackSpeed -= Time.deltaTime;
        }

        CurrentWeapons.UpdateAbilityWp();
    }
    public void DealDamage(IHpValue enemyHp , Transform pos)
    {

        CurrentWeapons.DealDamage(enemyHp , pos); 
    }
    public void SweichWeapon(GameObject newWeapons)
    {
        CurrentWeapons.StopAbilityWp();
        var newweapos = Instantiate(newWeapons , transform.position , Quaternion.identity);
        CurrentWeapons = newWeapons.GetComponent<AbilityWeapons>();
    }
    
    private async void KillTween(float v, Tween tween , GameObject b)
    {
        float zz = v;
        while (zz > 0)
        {
            zz -= Time.deltaTime;
            await Task.Yield();
        }
        tween.Kill();
        Destroy(b);
    }

}