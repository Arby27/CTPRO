using UnityEngine;
using System.Collections;

public class FlipNormals : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        MeshFilter meshFilter = GetComponent(typeof(MeshFilter)) as MeshFilter;

        if (meshFilter != null)
        {
            Mesh thisMesh = meshFilter.mesh;
            Vector3[] normals = thisMesh.normals;
            for(int i = 0; i < normals.Length; i++)
            {
                normals[i] = -normals[i];
            }

            thisMesh.normals = normals;

            for (int j = 0; j < thisMesh.subMeshCount; j++)
            {
                int[] tris = thisMesh.GetTriangles(j);
                for (int i = 0; i < tris.Length; i += 3)
                {
                    int temp = tris[i + 0];
                    tris[i + 0] = tris[i + 1];
                    tris[i + 1] = temp;
                }
                thisMesh.SetTriangles(tris, j);
            }
        }
	}
	
	
}
