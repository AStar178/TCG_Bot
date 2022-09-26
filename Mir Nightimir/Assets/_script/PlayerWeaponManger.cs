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
    public float CurrentMana;
    public float MaxMana;
    public float ManaRejyAmount;
    public float TimeToGetMana;
    public Action<int , Vector2 , DamageType , DamageResult> OnDealDamage;
    [SerializeField] public AbilityWeapons CurrentWeapons;
    [SerializeField] public GameObject TextFonstDamage;
    [HideInInspector] public float attackSpeed;
    [SerializeField] Color[] TextColors; 
    float TimezzzzManas;
    private void OnEnable() {
        OnDealDamage += OnDealDamageFuncens;
    }


    private void OnDisable() {
        OnDealDamage -= OnDealDamageFuncens;
    }

    private void OnDealDamageFuncens(int obj , Vector2 pos , DamageType type , DamageResult result)
    {   

        var Texts = Instantiate(TextFonstDamage , pos , Quaternion.identity);
        var fonts = Texts.GetComponentInChildren<TMP_Text>();
        fonts.text = result != DamageResult.DealadDamaged ? result.ToString() : obj.ToString();
        SetDamageColor( type , fonts );
        
        Tween tween = Texts.transform.DOMove(pos += new Vector2(0 , 1.25f) , 1f);
        KillTween( 1f , tween , Texts.transform.gameObject.transform.gameObject );
    }

    private void SetDamageColor(DamageType type, TMP_Text fonts)
    {
        if (type == DamageType.Critial)
        {
            fonts.color = TextColors[2];
            fonts.transform.localScale *= 1.5f;
            return;
        }

        if (type == DamageType.AD)
            fonts.color = TextColors[0];

        if (type == DamageType.AP)
            fonts.color = TextColors[1];

    }

    private void Start() {
        if (CurrentWeapons == null) { return; }

        CurrentWeapons.StartAbilityWp(Player.Singleton);
    }
    private void Update() 
    {
        TimezzzzManas -= Time.deltaTime;
        if (TimezzzzManas < 0)
        {
            CurrentMana += ManaRejyAmount;
            CurrentMana = Mathf.Clamp(CurrentMana , 0 , MaxMana);
            TimezzzzManas = TimeToGetMana;
            Player.Singleton.UpdateUI();
        }
        if(attackSpeed > 0)
        {
            attackSpeed -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q))
            CurrentWeapons.AbilityWeaponsUseAbility();

        CurrentWeapons.UpdateAbilityWp();
    }

    public void CreatCoustomTextPopup( string v , Vector3 position , Color color )
    {
        var text = Instantiate( TextFonstDamage , position , Quaternion.identity );
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.color = color;
        Tween tween = tMP_Text.transform.DOMoveY(position.y += 1.25f , 1f);
        KillTween( 1f , tween , tMP_Text.transform.gameObject.transform.gameObject );
        Destroy( text , 2 );
    }
    public void CreatCoustomTextPopup( string v , Vector3 position)
    {
        var text = Instantiate( TextFonstDamage , position , Quaternion.identity );
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.color = Color.yellow;
        Tween tween = tMP_Text.transform.DOMoveY(position.y += 1.25f , 1f);
        KillTween( 1f , tween , tMP_Text.transform.gameObject.transform.gameObject );
        Destroy( text , 2 );
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