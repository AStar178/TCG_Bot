using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System;

public class SpawnerManager : MonoBehaviour {
    public List<StateScriptAbleObject> stateScriptAbleObjects = new List<StateScriptAbleObject>();
    [SerializeField] private GameObject Chest;
    [SerializeField] private Transform World;
    [SerializeField] private AstarPath astarPath;
    [SerializeField] private Vector3 SpawnBox;
    [SerializeField] private int CheastAmount;
    private void Awake() {
        


        stateScriptAbleObjects.AddRange( Resources.LoadAll<StateScriptAbleObject>("StateIteam") );

        SetupTheWorld();

    }
    private void SetupTheWorld()
    {
        AddColisaneForToWorld();
        SpawnChest();
        SpawnInWorldObjectLikeTreeOrGrassSoOn();
        PathFindingGenerate();
    }

    private void AddColisaneForToWorld()
    {
        World.gameObject.AddComponent<MeshCollider>();
    }
    
    private void SpawnChest()
    {
        var s = SpawnObjectOfType( CheastAmount , Chest );

        for (int i = 0; i < s.Count; i++)
        {
            var x = s[i].GetComponent<Chest>();
            x.Iteam = stateScriptAbleObjects.OrderBy( c => UnityEngine.Random.value ).FirstOrDefault().GiveIteam();
            x.GetReady();
        }
    }

    private void SpawnInWorldObjectLikeTreeOrGrassSoOn()
    {

    }


    private void PathFindingGenerate()
    {
        astarPath.Scan();
    }
    private void OnDrawGizmosSelected() {
        
        Gizmos.DrawWireCube( transform.position , SpawnBox );

    }
    private List<GameObject> SpawnObjectOfType(int amount , GameObject spawnedObject)
    {
        var ob = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            Physics.Raycast( new Vector3( UnityEngine.Random.Range(-SpawnBox.x , SpawnBox.x) , SpawnBox.y , UnityEngine.Random.Range(-SpawnBox.z , SpawnBox.z ))  , Vector3.down , out var hit , SpawnBox.y  , Player.Current.PlayerThirdPersonController.GroundLayers );

            if (hit.collider == null)
                hit = TryAgain();
            var s = Instantiate(spawnedObject , hit.point , Quaternion.identity);
            ob.Add(s);
            s.transform.localEulerAngles = new Vector3( 0 , UnityEngine.Random.Range(0 , 360) , 0 );
        }
        return ob;
    }

    private RaycastHit TryAgain()
    {
        Physics.Raycast( new Vector3( UnityEngine.Random.Range(-SpawnBox.x , SpawnBox.x) , SpawnBox.y , UnityEngine.Random.Range(-SpawnBox.z , SpawnBox.z ))  , Vector3.down , out var hit , SpawnBox.y  , Player.Current.PlayerThirdPersonController.GroundLayers );
        if (hit.collider == null)
            return TryAgain();
        return hit;
    }
}
struct SpawnObejct
{
    
    public Vector3 offset;
    public GameObject spawnObject;
    public int AmountToSpawn;
    public float Raduios;

}