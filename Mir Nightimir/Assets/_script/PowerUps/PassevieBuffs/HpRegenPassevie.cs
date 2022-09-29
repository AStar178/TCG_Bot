using UnityEngine;

public class HpRegenPassevie : AbilityPowerUps 
{
    [SerializeField] float healtimeScale = 2;
    [SerializeField] GameObject Healer;
    [SerializeField] Vector2 pos;

    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        HpRegen.Singeleton.TimeToHeal = HpRegen.Singeleton.TimeToHeal / healtimeScale;
    }
    public override void OnFirstTime(Player player)
    {
        base.OnFirstTime(player);

        var Bird = Instantiate(Healer, Vector3.zero, Quaternion.identity);
        Bird.transform.SetParent(GetPlayer().Body);
        Bird.transform.localPosition = pos;
        Bird.GetComponent<HpRegen>().pos = pos;
        Bird.GetComponent<HpRegen>().StartAI(player);

    }



}