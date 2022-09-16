using UnityEngine;

public class AbilityPowerUps : MonoBehaviour 
{
    Player player1;
    public virtual void OnPowerUp(Player player)
    {
        player1 = player;
    }
    public virtual void OnPowerUpUpdate()
    {

    }
    

}