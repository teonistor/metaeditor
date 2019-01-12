using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class MeshSaver : MonoBehaviour {

    [SerializeField] string pathToSaveTo = "Assets/Resources/polyhedron.bin";

    internal void Save () {
        Vertex[] vertices = GetComponentsInChildren<Vertex>();
        Face[] faces = GetComponentsInChildren<Face>();
        IDictionary<string, int> vertexIndices = new Dictionary<string, int>();
        int vertexIndexReached = 0;

        using(StreamWriter writer = new StreamWriter(pathToSaveTo)) {
            writer.WriteLine(Meshable.objName); // TODO Assign and use names
            writer.WriteLine("---");

            foreach(Vertex v in vertices) {
                vertexIndices.Add(v.gameObject.name, vertexIndexReached);
                vertexIndexReached++;
                writer.WriteLine(string.Format("{0} {1} {2}",
                   v.transform.localPosition.x,
                   v.transform.localPosition.y,
                   v.transform.localPosition.z
                ));
            }

            writer.WriteLine("---");

            foreach (Face f in faces) {
                writer.WriteLine(string.Format("{0} {1} {2}",
                    vertexIndices[f.a.gameObject.name],
                    vertexIndices[f.b.gameObject.name],
                    vertexIndices[f.c.gameObject.name]
                ));
            }

            writer.WriteLine("---");
        }
	}
}
