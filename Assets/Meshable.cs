using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshable : MonoBehaviour {

    // Initially a tetrahedron
    [SerializeField] Vertex a, b, c, d;
    
	void Start () {
        Face[] faces = GetComponentsInChildren<Face>();
        if (faces.Length != 4) {
            Debug.LogErrorFormat("Cannot construct tetrahedron with {0} faces", faces.Length);
            return;
        }

        faces[0].Init(a, b, c);
        faces[1].Init(a, c, d);
        faces[2].Init(a, d, b);
        faces[3].Init(b, d, c);
    }
	
	void Update () {
		
	}
}
