using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateScriptAbleObject))]
public class StatEditor : Editor {
    public override void OnInspectorGUI() {
        StateScriptAbleObject stateScriptAbleObject = (StateScriptAbleObject)target;
        base.OnInspectorGUI();
        if (GUILayout.Button ("Create Prefab"))
        {
            stateScriptAbleObject.CreatNewUnityPrefabs();
        }
        if (GUILayout.Button ("Apply Prefab Changes"))
        {
            stateScriptAbleObject.Applay();
        }
    }
}