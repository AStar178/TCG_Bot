using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomChestSpawnerManger : MonoBehaviour 
{
    public List<GameObject> commanIteams = new List<GameObject>();
    public int Moneycomman;
    [SerializeField] Color commanColor;
    public List<GameObject> GreenIteams = new List<GameObject>();
    public int MoneyGreen;
    [SerializeField] Color GreenColor;
    [Range(0 , 100)] [SerializeField] int RangeGreen;
    public List<GameObject> RareIteams = new List<GameObject>();
    public int MoneyRare;
    [SerializeField] Color RareColor;
    [Range(0 , 100)] [SerializeField] int RangeRare;
    public List<GameObject> LegenderyIteams = new List<GameObject>();
    public int MoneyLegendery;
    [SerializeField] Color LegenderyColor;
    [Range(0 , 100)] [SerializeField] int RangeLegendery;
    [SerializeField] GameObject Cheast;
    [SerializeField] float x;
    [SerializeField] float y;
    private void Start() {
        
        var chestAmount = Random.Range( 67 , 101 );

        Vector2 posin = new Vector2( transform.position.x , transform.position.y );
        for (int i = 0; i < chestAmount; i++)
        {   
            TryToGetSpawnChest( posin );
        }

    }

    private void TryToGetSpawnChest(Vector2 posin)
    {
        posin.x = Random.value > 0.5f ?
            -Random.Range(0 - transform.position.x , x/2 - transform.position.x):
            Random.Range(0 + transform.position.x , x/2 + transform.position.x);
            posin.y = Random.value > 0.5f ?
            -Random.Range(0 - transform.position.y , y/2 - transform.position.y):
            Random.Range(0 + transform.position.y , y/2 + transform.position.y);

            if (Physics2D.OverlapBox( posin , Vector2.one , 0 ) != null) { TryToGetSpawnChest( new Vector2( transform.position.x , transform.position.y ) ); return; }

            var gameObject = Instantiate( Cheast , posin , Quaternion.identity );
            gameObject.transform.SetParent( transform );
    }

    public Color ChooseRandomIteam( out RareyValue RareyValue2 , out GameObject UpgradeObject , out int money )
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
        if ( Random.value >= ( RangeLegendery * .01f ) )
        {
            RareyValue2 = RareyValue.Rare;
            UpgradeObject = PickRandomRareObject();
            money = MoneyRare;
            return RareColor * (4 * 3);;
        }
        
        RareyValue2 = RareyValue.Legendery;
        UpgradeObject = PickRandomLegenderyObject();
        money = MoneyLegendery;
        return LegenderyColor * (6 * 3);  
    }

    private GameObject PickRandomLegenderyObject()
    {
        var id = Random.Range(0 , LegenderyIteams.Count);

        return LegenderyIteams[id];
    }
    private GameObject PickRandomRareObject()
    {
        var id = Random.Range(0 , RareIteams.Count);

        return RareIteams[id];
    }

    private GameObject PickRandomGreenObject()
    {
        var id = Random.Range(0 , GreenIteams.Count);

        return GreenIteams[id];
    }

    private GameObject PickRandomCommanObject()
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