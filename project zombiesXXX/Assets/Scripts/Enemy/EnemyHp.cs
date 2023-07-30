using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour, IDamageAble
{
    [SerializeField] public float CurrentHp;
    [SerializeField] public float MaxHp;
    //[SerializeField] public List<debuff> debuffss = new List<debuff>();
    public Action<DamageData> TakeDamageEvent;
    public Action KilledEvent;
    public MeshRenderer MainMatrial;
    public Mesh MainMesh;
    private void Awake() {
        CurrentHp = MaxHp;
    }
    /*private void Update() {
        

        for (int i = 0; i < debuffss.Count; i++)
        {
            var w = debuffss[i];
            w.Timer -= Time.deltaTime;
            if (w.Timer < 0)
            {
                debuffss.Remove(debuffss[i]);
                continue;
            }

            debuffss[i] = w;
        }

    }*/
    public void TakeDamage(DamageData Data)
    {
        TakeDamageEvent?.Invoke(Data);
        CurrentHp -= Data.DamageAmount;
        float a = -Data.DamageAmount;
        Minus.Instance.CreatCoustomTextPopup(a.ToString(), transform.position, Color.red);
        if (CurrentHp < 0)
        {
            Killed();
        }
    }

    private void Killed()
    {
        KilledEvent?.Invoke();
        Destroy(gameObject);
    }
}