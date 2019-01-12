using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Meshable))]
public class MeshableUI : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        if (Application.isPlaying) {
            Meshable meshable = (Meshable)target;
            if (GUILayout.Button("Save to file")) {
                meshable.Save();
            }
        }
    }
}
