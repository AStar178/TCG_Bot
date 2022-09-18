using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Chests : MonoBehaviour
{
    [SerializeField] RandomChestSpawnerManger randomChestSpawnerManger;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject upgrateObject;
    [SerializeField] TMPro.TMP_Text TMP_Text;
    private RareyValue Value;
    public int MoneyNeeded;
    private async void Start()
    {
        while (randomChestSpawnerManger == null)
        {
            randomChestSpawnerManger = FindObjectOfType<RandomChestSpawnerManger>();
            await Task.Yield();
        }


        var color = randomChestSpawnerManger.ChooseRandomIteam( out var rareyValue , out var upgrateiteam  , out var money);
        upgrateObject = upgrateiteam;
        Value = rareyValue;
        MoneyNeeded = money;
        TMP_Text.text = money.ToString();
        spriteRenderer.material.SetColor("_Color" , color);
    }

    public void Purchist(Player player)
    {
        if (player.CurrentCoins < MoneyNeeded)
            return;

        player.CurrentCoins -= MoneyNeeded;
        player.AddPowerUp ( upgrateObject );
        Destroy(this.gameObject);
    }   


}
