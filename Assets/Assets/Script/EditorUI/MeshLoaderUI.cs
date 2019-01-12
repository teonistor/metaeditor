using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Face))]
public class FaceUI : Editor {

    public override void OnInspectorGUI () {
        DrawDefaultInspector();

        if (Application.isPlaying) {
            Face face = (Face)target;
            if (GUILayout.Button("Split face")) {
                face.Split();
            }
        }
    }
}
