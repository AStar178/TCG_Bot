using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGenratorEditor : Editor {
    public override void OnInspectorGUI() {

        MapGenerator mapGenerator = (MapGenerator)target;
        if (DrawDefaultInspector())
        {
            if (mapGenerator.update)
            {
                mapGenerator.GenerateMap();
            }
        }
        if (GUILayout.Button ("Generate"))
        {
            mapGenerator.GenerateMap();
        }


    }
}
