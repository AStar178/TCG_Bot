using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomChestSpawnerManger : MonoBehaviour 
{
    public List<AbilityPowerUps> commanIteams = new List<AbilityPowerUps>();
    public int Moneycomman;
    [SerializeField] Color commanColor;
    public List<AbilityPowerUps> GreenIteams = new List<AbilityPowerUps>();
    public int MoneyGreen;
    [SerializeField] Color GreenColor;
    [Range(0 , 100)] [SerializeField] int RangeGreen;
    public List<AbilityPowerUps> RareIteams = new List<AbilityPowerUps>();
    public int MoneyRare;
    [SerializeField] Color RareColor;
    [Range(0 , 100)] [SerializeField] int RangeRare;
    public List<AbilityPowerUps> EpicIteams = new List<AbilityPowerUps>();
    public int MoneyEpic;
    [SerializeField] Color EpicColor;
    [Range(0 , 100)] [SerializeField] int RangeEpic;
    public List<AbilityPowerUps> LegenderyIteams = new List<AbilityPowerUps>();
    public int MoneyLegendery;
    [SerializeField] Color LegenderyColor;
    [Range(0 , 100)] [SerializeField] int RangeLegendery;
    [SerializeField] GameObject Cheast;
    [SerializeField] float x;
    [SerializeField] float y;
    private void Start() {
        
        var chestAmount = Random.Range( 23 , 30 );

        Vector2 posin = new Vector2( transform.position.x , transform.position.y );
        for (int i = 0; i < chestAmount; i++)
        {   
            posin.x = Random.value > 0.5f ?
            -Random.Range(0 - transform.position.x , x/2 - transform.position.x):
            Random.Range(0 + transform.position.x , x/2 + transform.position.x);
            posin.y = Random.value > 0.5f ?
            -Random.Range(0 - transform.position.y , y/2 - transform.position.y):
            Random.Range(0 + transform.position.y , y/2 + transform.position.y);

            var gameObject = Instantiate( Cheast , new Vector2 ( posin.x , posin.y ) , Quaternion.identity );
            gameObject.transform.SetParent( transform );
        }

    }
    public Color ChooseRandomIteam( out RareyValue RareyValue2 , out AbilityPowerUps UpgradeObject , out int money )
    {
        if ( Random.value >= ( RangeGreen * .01f ) )
        {
            RareyValue2 = RareyValue.comman;
            UpgradeObject = PickRandomCommanObject();
            money = Moneycomman;
            return commanColor * (2 * 3);;
        }
        if ( Random.value >= ( RangeRare * .01f ) )
        {
            RareyValue2 = RareyValue.Green;
            UpgradeObject = PickRandomGreenObject();
            money = MoneyGreen;
            return GreenColor * (3 * 3);;
        }
        if ( Random.value >= ( RangeEpic * .01f ) )
        {
            RareyValue2 = RareyValue.Rare;
            UpgradeObject = PickRandomRareObject();
            money = MoneyRare;
            return RareColor * (4 * 3);;
        }
        if ( Random.value >= ( RangeLegendery * .01f ) )
        {
            RareyValue2 = RareyValue.Epic;
            UpgradeObject = PickRandomEpicObject();
            money = MoneyEpic;
            return EpicColor * (5 * 3);
        }

        RareyValue2 = RareyValue.Legendery;
        UpgradeObject = PickRandomLegenderyObject();
        money = MoneyLegendery;
        return LegenderyColor * (6 * 3);  
    }

    private AbilityPowerUps PickRandomLegenderyObject()
    {
        var id = Random.Range(0 , LegenderyIteams.Count);

        return LegenderyIteams[id];
    }
    private AbilityPowerUps PickRandomEpicObject()
    {
        var id = Random.Range(0 , EpicIteams.Count);

        return EpicIteams[id];
    }

    private AbilityPowerUps PickRandomRareObject()
    {
        var id = Random.Range(0 , RareIteams.Count);

        return RareIteams[id];
    }

    private AbilityPowerUps PickRandomGreenObject()
    {
        var id = Random.Range(0 , GreenIteams.Count);

        return GreenIteams[id];
    }

    private AbilityPowerUps PickRandomCommanObject()
    {
        var id = Random.Range(0 , commanIteams.Count);

        return commanIteams[id];
    }


    #if UNITY_EDITOR
    void OnApplicationQuit()
       {

            var constructor = SynchronizationContext.Current.GetType().GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {typeof(int)}, null);
            var newContext = constructor.Invoke(new object[] {Thread.CurrentThread.ManagedThreadId });
            SynchronizationContext.SetSynchronizationContext(newContext as SynchronizationContext);  
           
       }
    private void OnDrawGizmosSelected() {
        
        Handles.DrawWireCube( transform.position , new Vector2( x , y ) );

    }
    #endif
}
public enum RareyValue
    {
        comman ,
        Green ,
        Rare ,
        Epic ,
        Legendery
    }