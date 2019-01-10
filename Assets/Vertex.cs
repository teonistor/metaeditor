using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    internal Vertex G(Vertex b, Vertex c) {
        Vertex v = Instantiate(gameObject, transform.parent).GetComponent<Vertex>();
        v.transform.position = (c.transform.position + b.transform.position + transform.position) / 3f;
        return v;
    }
}
