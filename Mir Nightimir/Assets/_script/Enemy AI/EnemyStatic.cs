using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class EnemyStatic : MonoBehaviour
{
    public static GameObject project;
    public GameObject Project;
    public static GameObject textPrefab;
    public GameObject TextPrefab;

    private void Awake()
    {
        project = Project;
        textPrefab = TextPrefab;
    }
    public static void CreatCoustomTextPopup(string v, Vector3 position, Color color)
    {
        var text = Instantiate(EnemyStatic.textPrefab, position, Quaternion.identity);
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.color = color;
        Tween tween = tMP_Text.transform.DOMoveY(position.y += 1.25f, 1f);
        KillTween(1f, tween, tMP_Text.transform.gameObject.transform.gameObject);
        Destroy(text, 2);
    }
    private static async void KillTween(float v, Tween tween, GameObject b)
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
}
