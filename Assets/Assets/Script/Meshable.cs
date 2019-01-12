using System.Collections;
using System;
using UnityEngine;

public class Meshable : MonoBehaviour {
    internal const string objName = "Default";

    internal static event Action<string, Vector3, float> OnCentreChange;

    // Initially a tetrahedron TODO generate these from prefab
    //[SerializeField] Vertex a, b, c, d;
    [SerializeField] GameObject vertex, face;
    
    Vector3 centre;
    int vertexCount, faceCount;

    IEnumerator Start () {
        //Face[] faces = GetComponentsInChildren<Face>();
        //if (faces.Length != 4) {
        //    Debug.LogErrorFormat("Cannot construct tetrahedron with {0} faces", faces.Length);
        //    yield break;
        //}

        //faces[0].Init(a, b, c);
        //faces[1].Init(a, c, d);
        //faces[2].Init(a, d, b);
        //faces[3].Init(b, d, c);

        // More seldom update the centre point
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        while (true) {
            Vector3 newCentre = Vector3.zero;
            Vertex[] vertices = GetComponentsInChildren<Vertex>();
            foreach (Vertex v in vertices) {
                newCentre += v.transform.position;
            }
            newCentre /= vertices.Length;

            if (!newCentre.Equals(centre)) {
                centre = newCentre;
                if (OnCentreChange != null) {
                    float dist = 0f;
                    foreach (Vertex v in vertices) {
                        dist = Mathf.Max(dist, Vector3.Distance(centre, v.transform.position));
                    }

                    OnCentreChange(objName, centre, dist);
                }
            }

            yield return wait;
        }
    }

	internal void Update () {

    }

    internal Face InstantiateFace () {
        return InstantiateWithIncrementalName(face, ref faceCount, "Face").GetComponent<Face>();
    }

    internal Vertex InstantiateVertex () {
        return InstantiateWithIncrementalName(vertex, ref vertexCount, "Vertex").GetComponent<Vertex>();
    }

    GameObject InstantiateWithIncrementalName (GameObject prefab, ref int count, string name) {
        GameObject go = Instantiate(prefab, transform);
        go.name = string.Format("{0} #{1:D3}", name, ++count);
        print("Instantiated " + go.name);
        return go;
    }
}
