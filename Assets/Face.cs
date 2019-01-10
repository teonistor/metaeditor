﻿using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Face : MonoBehaviour {

    Vertex a, b, c;
    Mesh mesh;
    bool dirty;

    IEnumerator Start () {
        mesh = new Mesh {
            name = "GeneratedFace#" + UnityEngine.Random.Range(100000, 1000000)
        };
        mesh.MarkDynamic();
        GetComponent<MeshFilter>().mesh = mesh;

        WaitForSeconds wait = new WaitForSeconds(1f);
        while(true) {
            yield return wait;
            if (dirty) {
                //mesh.RecalculateNormals();
                mesh.RecalculateTangents();
                mesh.RecalculateBounds();
            }
        }
    }

    internal void Init(Vertex a, Vertex b, Vertex c) {
        this.a = a;
        this.b = b;
        this.c = c;

        mesh.vertices = new Vector3[3];
        mesh.triangles = new int[] { 0, 1, 2 };
        mesh.uv = new Vector2[3];
        dirty = true;
    }

    void Update () {
        // TODO Only update when changes occur
        if (dirty) {
            //mesh = new Mesh();
            //mesh.MarkDynamic();

            //mesh.vertices = new Vector3[3];
            //mesh.triangles = new int[] { 0, 1, 2 };
            //mesh.uv = new Vector2[] {
            //    new Vector2(0, 0),
            //    new Vector2(0, 1),
            //    new Vector2(1, 0)
            //};

            mesh.vertices = new Vector3[]{ a.transform.position,
             b.transform.localPosition,
              c.transform.localPosition
            };
            mesh.RecalculateNormals();


            //GetComponent<MeshFilter>().mesh = mesh;

            //print(a.transform.position + " WTF " + mesh.vertices[0] + mesh.vertices[2] + mesh.vertices[1] + mesh.normals[0] + mesh.normals[2] + mesh.normals[1]);
        }
    }

    internal void Split (out Face f1, out Face f2) {
        throw new NotImplementedException();
    }
    
}