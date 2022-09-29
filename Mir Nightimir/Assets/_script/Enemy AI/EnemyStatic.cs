using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class AIStatic : MonoBehaviour
{
    public static GameObject project;
    public GameObject Project;
    public static GameObject soulHunterMinios;
    public GameObject SoulHunterMinions;
    public static GameObject fireBurstPartical;
    public GameObject FireBurstPartical;
    public static GameObject stand;
    public GameObject Stand;
    public static GameObject textPrefab;
    public GameObject TextPrefab;
    public static Sprite GraveStoneSprit;
    public Sprite graveStoneSprit;
    public static GameObject playerSimpBullet;
    public GameObject PlayerSimpBullet;
    [SerializeField] Vector3 OffSet;
    public static AIStatic uI_Upgratededd;
    [SerializeField] public UI_Upgrateded tUpgrateded;
    private void Awake()
    {
        uI_Upgratededd = this;
        project = Project;
        soulHunterMinios = SoulHunterMinions;
        fireBurstPartical = FireBurstPartical;
        stand = Stand;
        textPrefab = TextPrefab;
        GraveStoneSprit = graveStoneSprit;
        playerSimpBullet = PlayerSimpBullet;
    }
    public static void CreatCoustomTextPopup(string v, Vector3 position, Color color)
    {
        var text = Instantiate(AIStatic.textPrefab, position, Quaternion.identity);
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.color = color;
        Tween tween = tMP_Text.transform.DOMoveY(position.y += 1.25f, 1f);
        KillTween(1f, tween, tMP_Text.transform.gameObject.transform.gameObject);
        Destroy(text, 2);
    }
    public static async void KillTween(float v, Tween tween, GameObject b)
    {
        float zz = v;
        while (zz > 0)
        {
            zz -= Time.deltaTime;
            await Task.Yield();
        }
        tween.Kill();
        Destroy(b);
    }
    public static Vector2 randomVector2(Vector2 rand,Vector2 v2, float minX, float maxX, float minY, float maxY)
    {
        rand = v2 + new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        return rand;
    }
    public static async Task Wait(float Timez)
    {
        while (Timez > 0)
        {
            Timez -= Time.deltaTime;
            await Task.Yield();
        }
    }
    private void OnDrawGizmosSelected() {
        
        Gizmos.DrawLine(transform.position , (transform.position + Vector3.up - OffSet).normalized);

    }
}
