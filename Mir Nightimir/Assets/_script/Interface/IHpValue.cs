
public interface IHpValue
{
    
    void HpValueChange( Damage damage , out DamageResult result );


}
public enum DamageResult 
{
    Block ,
    Failed ,
    DealadDamaged ,
    Miss ,
    Killed ,
    DEID
}
