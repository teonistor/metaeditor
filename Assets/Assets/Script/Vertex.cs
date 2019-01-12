using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour {

    [SerializeField] Material active, inctive;

 //   void Start () {
		
	//}
	
	//void Update () {
		
	//}

    internal Vertex G(Vertex b, Vertex c) {
        Vertex v = GetComponentInParent<Meshable>().InstantiateVertex();
        v.transform.position = (c.transform.position + b.transform.position + transform.position) / 3f;
        return v;
    }
}
