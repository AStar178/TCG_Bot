using UnityEngine;
using UnityEngine.UI;

public class AbilityPowerUps : MonoBehaviour 
{
    Player player1;
    [SerializeField] Image Icon;
    [SerializeField] string Name;
    [SerializeField] string Discripsen;
    public virtual void OnPowerUp(Player player)
    {
        player1 = player;
    }

    public virtual void OnDealDamage(Transform target , IHpValue targetHp) 
    {

    }
    public virtual void OnHpChange()
    {
        
    }

    public virtual void OnPowerUpUpdate()
    {

    }
    

}