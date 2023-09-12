using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Data/New Item", order = 1)]
public class Item : ScriptableObject
{
    public string ID = Guid.NewGuid().ToString().ToUpper();
    public string FriendlyName;
    public string Description;
    public Categories Category;
    public bool Stackable;
    public int BuyPrice;
    [Range(0,2)]
    public float SellPercentage;
    public Sprite Icon;
    public float Damage;
    public List<ActionStruct> iteamActions;
    public Item Reference;
    private void OnEnable() {
        Reference = this;
        if (iteamActions == null || iteamActions.Count == 0)
        {

            
            iteamActions = new List<ActionStruct>();
            iteamActions.Add(new ActionStruct());

        }

    }
    public enum Categories
    {
        Armor,
        Food,
        Potion,
        Weapon,
        Staff,
        Ring,
        Resousers,
        Junk
    }

}
