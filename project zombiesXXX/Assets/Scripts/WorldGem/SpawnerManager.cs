using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public class SpawnerManager : MonoBehaviour {
    public List<StateScriptAbleObject> stateScriptAbleObjects = new List<StateScriptAbleObject>();
    [SerializeField] private Transform World;
    [SerializeField] private AstarPath astarPath;
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

    }

    private void SpawnInWorldObjectLikeTreeOrGrassSoOn()
    {

    }


    private void PathFindingGenerate()
    {
        astarPath.Scan();
    }
}
struct SpawnObejct
{
    
    public Vector3 offset;
    public GameObject spawnObject;
    public int AmountToSpawn;
    public float Raduios;

}