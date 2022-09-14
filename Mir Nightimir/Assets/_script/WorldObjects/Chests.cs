using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Chests : MonoBehaviour
{
    [SerializeField] RandomChestSpawnerManger randomChestSpawnerManger;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject upgrateObject;
    private RareyValue Value;
    private async void Start()
    {
        while (randomChestSpawnerManger == null)
        {
            randomChestSpawnerManger = FindObjectOfType<RandomChestSpawnerManger>();
            await Task.Yield();
        }


        var color = randomChestSpawnerManger.ChooseRandomIteam( out var rareyValue , out var upgrateiteam );
        upgrateObject = upgrateiteam;
        Value = rareyValue;    
        spriteRenderer.material.SetColor("_Color" , color);
    }



}
