using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomEnemySpwaner : MonoBehaviour 
{
    public List<GameObject> Enemy = new List<GameObject>();
    public List<GameObject> EnemyLegendery = new List<GameObject>();
    [Range(0 , 100)] [SerializeField] int LegenderyEnemyRange;
    public List<GameObject> Boss = new List<GameObject>();
    [Range(0 , 100)] [SerializeField] int BossRange;
    [SerializeField] float x;
    [SerializeField] float y;
    private async void Start() {

        while (Player.Singleton.Body.gameObject.activeSelf == false)
        {
            await Task.Yield();
        }    

        var EnemyAmount = Random.Range( 67 , 101 );

        Vector2 posin = new Vector2( transform.position.x , transform.position.y );
        for (int i = 0; i < EnemyAmount; i++)
        {   
            TryToGetSpawnEnemy( posin );
        }

    }

    private void TryToGetSpawnEnemy(Vector2 posin)
    {
        posin.x = Random.value > 0.5f ?
            -Random.Range(0 - transform.position.x , x/2 - transform.position.x):
            Random.Range(0 + transform.position.x , x/2 + transform.position.x);
            posin.y = Random.value > 0.5f ?
            -Random.Range(0 - transform.position.y , y/2 - transform.position.y):
            Random.Range(0 + transform.position.y , y/2 + transform.position.y);

            if ( Vector2.Distance( posin , Player.Singleton.Body.transform.position ) < 6 )
            {
                TryToGetSpawnEnemy( new Vector2( transform.position.x , transform.position.y ) ); return;
            }

            if (Physics2D.OverlapBox( posin , Vector2.one , 0 ) != null) { TryToGetSpawnEnemy( new Vector2( transform.position.x , transform.position.y ) ); return; }

            var enemy = ChooseRandomEnemy();

            var gameObject = Instantiate( enemy , posin , Quaternion.identity );
            enemy.GetComponent<TESTei>().target = Player.Singleton.BodyColider;

            gameObject.transform.SetParent( transform );
    }

    public GameObject ChooseRandomEnemy()
    {
        if ( Random.value >= ( LegenderyEnemyRange * .01f ) )
        {
            return PickRandomCommanObject();
            
        }
        if ( Random.value >= ( BossRange * .01f ) )
        {
            return PickRandomGreenObject();
        }
        
        return PickRandomRareObject();
    }


    private GameObject PickRandomRareObject()
    {
        var id = Random.Range(0 , Boss.Count);

        return Boss[id];
    }

    private GameObject PickRandomGreenObject()
    {
        var id = Random.Range(0 , EnemyLegendery
.Count);

        return EnemyLegendery
[id];
    }

    private GameObject PickRandomCommanObject()
    {
        var id = Random.Range(0 , Enemy.Count);

        return Enemy[id];
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
