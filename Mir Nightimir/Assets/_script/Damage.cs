using UnityEngine;

public struct Damage
{
    public float AdDamage;
    public float ApDamage;
    public float Ad_DefenceReduser;
    public float Mp_DefenceReduser;
    public Vector2 knockback;
    public DamageType type;
    public Player PlayerRefernce;
    public IHpValue GameObjectRefernce;
}
public enum DamageType 
{
    AD ,
    AP , 
    Critial ,
}