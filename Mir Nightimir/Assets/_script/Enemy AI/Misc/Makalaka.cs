using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class Makalaka : MonoBehaviour
{
    public GameObject target;
    public float Speed;
    public float Healing;
    public EnemyHp BozzHP;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
        if (Vector2.Distance(target.transform.position, transform.position) <= .5f)
        {
            BozzHP.Currenthp = BozzHP.Currenthp + Healing;
            CreatCoustomTextPopup("+" + Healing, target.transform.position);
            Destroy(gameObject);
        }
        if (target == null)
        {
            Destroy(gameObject);
            CreatCoustomTextPopup("Dissapeared", gameObject.transform.position);
        }
    }
    public void CreatCoustomTextPopup(string v, Vector3 position)
    {
        var text = Instantiate(EnemyStatic.textPrefab, position, Quaternion.identity);
        TMPro.TMP_Text tMP_Text = text.GetComponentInChildren<TMPro.TMP_Text>();
        tMP_Text.text = v;
        tMP_Text.color = Color.yellow;
        Tween tween = tMP_Text.transform.DOMoveY(position.y += 1.25f, 1f);
        KillTween(1f, tween, tMP_Text.transform.gameObject.transform.gameObject);
        Destroy(text, 2);
    }
    private async void KillTween(float v, Tween tween, GameObject b)
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
