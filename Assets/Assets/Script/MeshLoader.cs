using System.IO;
using System.Collections.Generic;
//using System.Text.RegularExpressions;
using UnityEngine;

public class MeshLoader : MonoBehaviour {
    
    [SerializeField] string pathToLoadFrom;
    [SerializeField] Vector3 instantiationPosition;
    [SerializeField] GameObject emptyMeshable;
    [SerializeField] Material singleObjectMaterial;
	
    internal void LoadSingle () {
        List<Vector3> points = new List<Vector3>();
        List<int> triangles = new List<int>();
        Read(points, triangles);

        Mesh mesh = new Mesh();
        mesh.name = "GeneratedMesh#" + UnityEngine.Random.Range(100000, 1000000);
        mesh.vertices = points.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        GameObject meshHolder = new GameObject("Mesh holder");
        meshHolder.AddComponent<MeshFilter>().mesh = mesh;
        meshHolder.AddComponent<MeshRenderer>().material = singleObjectMaterial;
        meshHolder.transform.position = instantiationPosition;
    }

    internal void LoadEditable () {
        List<Vector3> points = new List<Vector3>();
        List<int[]> triangles = new List<int[]>();
        Read(points, null, triangles);

        Meshable meshable = Instantiate(emptyMeshable, instantiationPosition, Quaternion.identity).GetComponent<Meshable>();
        List<Vertex> generatedVertices = new List<Vertex>();
        foreach (Vector3 point in points) {
            Vertex vertex = meshable.InstantiateVertex();
            vertex.transform.localPosition = point;
            generatedVertices.Add(vertex);
        }
        foreach(int[] triangle in triangles) {
            meshable.InstantiateFace().Init(
                generatedVertices[triangle[0]],
                generatedVertices[triangle[1]],
                generatedVertices[triangle[2]]
            );
        }
    }

    void Read(IList<Vector3> points, IList<int> flatTrianges=null, IList<int[]> nestedTriangles=null) {
        using(StreamReader reader = new StreamReader(pathToLoadFrom)) {
            string line = reader.ReadLine(); // Name... not used (yet)
            print("Loading polyhedron \"" + line + "\" from " + pathToLoadFrom);

            line = reader.ReadLine(); // The first "---"

            while (!"---".Equals(line = reader.ReadLine())) {
                string[] split = line.Split(' ');
                points.Add(new Vector3(
                    float.Parse(split[0]),
                    float.Parse(split[1]),
                    float.Parse(split[2])
                ));
            }
            print("Read data for " + points.Count + " vertices");

            while (!"---".Equals(line = reader.ReadLine())) {
                string[] split = line.Split(' ');
                if(flatTrianges!=null) {
                    flatTrianges.Add(int.Parse(split[0]));
                    flatTrianges.Add(int.Parse(split[1]));
                    flatTrianges.Add(int.Parse(split[2]));
                }
                if (nestedTriangles != null) {
                    nestedTriangles.Add(new int[] {
                        int.Parse(split[0]),
                        int.Parse(split[1]),
                        int.Parse(split[2])
                    });
                }
            }

            if (flatTrianges != null)
                print("Read " + flatTrianges.Count + " flat triangle coordinates");
            if (nestedTriangles != null)
                print("Read data for " + nestedTriangles.Count + " faces");
        }
    }
}
