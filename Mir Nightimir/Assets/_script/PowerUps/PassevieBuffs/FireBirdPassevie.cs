using UnityEngine;

public class FireBirdPassevie : AbilityPowerUps 
{
    [SerializeField] float damageScale;
    [SerializeField] GameObject theBird;
    [SerializeField] Vector2 pos;

    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        FireBirdTheBird.Singeleton.ScalDamage += damageScale;
    }
    public override void OnFirstTime(Player player)
    {
        base.OnFirstTime(player);

        var Bird = Instantiate ( theBird , Vector3.zero , Quaternion.identity );
        Bird.transform.SetParent ( player.Body );
        Bird.transform.localPosition = pos;
        Bird.GetComponent<FireBirdTheBird>().StartAI( player );

    }



}