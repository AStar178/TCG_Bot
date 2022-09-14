using System.Collections.Generic;
using UnityEngine;

public class RandomChestSpawnerManger : MonoBehaviour 
{
    public List<GameObject> commanIteams = new List<GameObject>();
    [SerializeField] Color commanColor;
    public List<GameObject> GreenIteams = new List<GameObject>();
    [SerializeField] Color GreenColor;
    [Range(0 , 100)] [SerializeField] int RangeGreen;
    public List<GameObject> RareIteams = new List<GameObject>();
    [SerializeField] Color RareColor;
    [Range(0 , 100)] [SerializeField] int RangeRare;
    public List<GameObject> EpicIteams = new List<GameObject>();
    [SerializeField] Color EpicColor;
    [Range(0 , 100)] [SerializeField] int RangeEpic;
    public List<GameObject> LegenderyIteams = new List<GameObject>();
    [SerializeField] Color LegenderyColor;
    [Range(0 , 100)] [SerializeField] int RangeLegendery;


    public Color ChooseRandomIteam( out RareyValue RareyValue2 , out GameObject UpgradeObject )
    {
        if ( Random.value >= ( RangeGreen * .01f ) )
        {
            RareyValue2 = RareyValue.comman;
            UpgradeObject = PickRandomCommanObject();
            return commanColor * (2 * 3);;
        }
        if ( Random.value >= ( RangeRare * .01f ) )
        {
            RareyValue2 = RareyValue.Green;
            UpgradeObject = PickRandomGreenObject();
            return GreenColor * (3 * 3);;
        }
        if ( Random.value >= ( RangeEpic * .01f ) )
        {
            RareyValue2 = RareyValue.Rare;
            UpgradeObject = PickRandomRareObject();
            return RareColor * (4 * 3);;
        }
        if ( Random.value >= ( RangeLegendery * .01f ) )
        {
            RareyValue2 = RareyValue.Epic;
            UpgradeObject = PickRandomEpicObject();
            return EpicColor * (5 * 3);
        }

        RareyValue2 = RareyValue.Legendery;
        UpgradeObject = PickRandomLegenderyObject();
        return LegenderyColor * (6 * 3);  
    }

    private GameObject PickRandomLegenderyObject()
    {
        var id = Random.Range(0 , LegenderyIteams.Count);

        return LegenderyIteams[id];
    }
    private GameObject PickRandomEpicObject()
    {
        var id = Random.Range(0 , EpicIteams.Count);

        return EpicIteams[id];
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
}
public enum RareyValue
    {
        comman ,
        Green ,
        Rare ,
        Epic ,
        Legendery
    }