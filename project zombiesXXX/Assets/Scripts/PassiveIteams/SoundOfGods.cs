using UnityEngine;

public class SoundOfGods : IteamPassive {
    
    int stackAmount;
    [SerializeField] float Timer;
    [SerializeField] GameObject effect;
    [SerializeField] Vector3 offset;
    GameObject effecref;
    Material[] materials;
    public override void OnStart(PlayerState playerState)
    {

        effecref = Instantiate(effect , Vector3.zero , Quaternion.identity);
        effecref.transform.SetParent(Player.Current.PlayerState.transform);
        effecref.transform.localPosition = Vector3.zero + offset;
        materials = new Material[2];
        materials[0] = effecref.GetComponent<Renderer>().material;
        materials[1] = effecref.transform.GetChild(0).GetComponent<Renderer>().material;
        materials[0].SetFloat("A2" , 0);
        materials[1].SetFloat("A2" , 0);
        

        base.OnStart(playerState);

    }
    private void OnEnable() {
        
        Player.Current.PlayerState.OnAbilityAttackDealDamage += AbilityUsed;

    }

    private void AbilityUsed(DamageData data, EnemyHp hp)
    {
        stackAmount++;
        t = Timer;


        if (stackAmount < 4)
            return;

        Player.Current.PlayerState.CalculatedValue.HpCurrent += Player.Current.PlayerState.CalculatedValue.HpMax * 0.01f;
        Player.Current.PlayerState.CalculatedValue.HpCurrent = Mathf.Clamp( Player.Current.PlayerState.CalculatedValue.HpCurrent , 0 , Player.Current.PlayerState.CalculatedValue.HpMax );
    }

    private void OnDisable() {
        
        Player.Current.PlayerState.OnAbilityAttackDealDamage -= AbilityUsed;

    }
    float t;
    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {
        stackAmount = Mathf.Clamp(stackAmount , 0 , 4);
        t -= Time.deltaTime;
        if(t < 0)
        {
            stackAmount = 0;
        }

       
        state.Crit += 0.25f * stackAmount;
        state.CritDamageMulty += 0.5f * stackAmount;
        
        
        materials[0].SetFloat("A2" , Mathf.Lerp(materials[0].GetFloat("A2") , Mathf.InverseLerp( 0 , 4 , stackAmount ), 5 * Time.deltaTime));
        materials[1].SetFloat("A2" , Mathf.Lerp(materials[1].GetFloat("A2") , Mathf.InverseLerp( 0 , 4 , stackAmount ) , 5 * Time.deltaTime));


        return base.OnUpdate(playerState, ref CalucatedValue, ref state);
    }


}