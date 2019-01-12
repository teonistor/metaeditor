using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Face : MonoBehaviour {

    [SerializeField] Material active, inctive;

    internal Vertex a { get; private set; }
    internal Vertex b { get; private set; }
    internal Vertex c { get; private set; }
    Mesh mesh;
    bool dirty;

    IEnumerator Start () {
        mesh = new Mesh {
            name = "GeneratedFace#" + UnityEngine.Random.Range(100000, 1000000)
        };
        mesh.MarkDynamic();
        mesh.vertices = new Vector3[3];
        mesh.triangles = new int[] { 0, 1, 2 };
        mesh.uv = new Vector2[3];
        GetComponent<MeshFilter>().mesh = mesh;

        // Perform mesh recalculations more seldom, to hopefully increase performance
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        for (int i = 0; ; i = (i + 1) % 10) {
            yield return wait;
            if (dirty) {
                mesh.RecalculateNormals();
                if (i == 0) {
                    mesh.RecalculateTangents();
                    mesh.RecalculateBounds();
                }
            }
        }
    }

    internal void Init(Vertex a, Vertex b, Vertex c) {
        this.a = a;
        this.b = b;
        this.c = c;
        
        dirty = true;
    }

    void Update () {
        // TODO Only update when changes occur
        if (dirty) {
            mesh.vertices = new Vector3[]{ a.transform.localPosition,
             b.transform.localPosition,
              c.transform.localPosition
            };
        }
    }

    internal void Split () {
        Vertex d = a.G(b, c);
        GetComponentInParent<Meshable>().InstantiateFace().Init(b, c, d);
        GetComponentInParent<Meshable>().InstantiateFace().Init(c, a, d);
        Init(a, b, d); // Must be last
    }
}
