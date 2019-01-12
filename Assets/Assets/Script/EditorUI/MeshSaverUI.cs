using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshSaver))]
public class MeshSaverUI : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        if (Application.isPlaying) {
            if (GUILayout.Button("Save to file")) {
                ((MeshSaver)target).Save();
            }
        }
    }
}
