using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeshLoader))]
public class MeshLoaderUI : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        if (Application.isPlaying) {
            MeshLoader meshLoader = (MeshLoader)target;
            if (GUILayout.Button("Load from file")) {
                meshLoader.LoadEditable();
            }
            if (GUILayout.Button("Load as single object")) {
                meshLoader.LoadSingle();
            }
        }
    }
}
