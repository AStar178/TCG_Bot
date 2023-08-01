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
        RPGStatic.Instance.CreatCoustomTextPopup(a.ToString(), transform.position, Data.Crited == false ? Color.red : Color.yellow );

        if (Data.Crited)
        {
            Data.target.OnCritied?.Invoke(Data, this);
        }

        if (CurrentHp < 0)
        {
            Data.target.OnKilledEnemy?.Invoke(Data, this);
            Killed();
        }
    }

    private void Killed()
    {
        KilledEvent?.Invoke();
        Destroy(gameObject);
    }
}