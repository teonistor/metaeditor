using System.IO;
using System.Collections.Generic;
//using System.Text.RegularExpressions;
using UnityEngine;

public class MeshLoader : MonoBehaviour {
    
    [SerializeField] string pathToLoadFrom;
    [SerializeField] Vector3 instantiationPosition;
    [SerializeField] GameObject emptyMeshable;
	
    internal void LoadSingle () {
        Debug.LogWarning("Coming soon. " + pathToLoadFrom);
    }

    internal void LoadEditable () {
        Debug.LogWarning("Coming soon. " + pathToLoadFrom);

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

            line = reader.ReadLine(); // The first "---"

            while (!"---".Equals(line = reader.ReadLine())) {
                string[] split = line.Split(' ');
                points.Add(new Vector3(
                    float.Parse(split[0]),
                    float.Parse(split[1]),
                    float.Parse(split[2])
                ));
            }

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
        }
    }
}
