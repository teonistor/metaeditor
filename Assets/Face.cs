using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Face : MonoBehaviour {

    Vertex a, b, c;
    Mesh mesh;
    bool dirty;

    [SerializeField] bool split = false;

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

        // Temp
        if (split) {
            split = false;
            Split();
        }
    }

    internal void Split () {
        Vertex d = a.G(b, c);
        Instantiate(gameObject, transform.parent).GetComponent<Face>().Init(b, c, d);
        Instantiate(gameObject, transform.parent).GetComponent<Face>().Init(c, a, d);
        Init(a, b, d); // Must be last
    }
}
