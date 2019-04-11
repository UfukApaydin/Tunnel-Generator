using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

    public Material mat;

    float width = 2;
    float height = 4;
    float zet = 2;
    float upper=2;
    float lower=2;
    bool a = true;

	void Start () {
        // CreateMesh();
       // Motion();
        //InvokeRepeating("Motion", 1, 2);
    }
    private void Update()
    {
        Motion();
        CreateMesh();
    }
    public void CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.Clear();
        Vector3[] vertices = new Vector3[8];

        vertices[0] = new Vector3(lower *- width, -height, lower* - zet );
        vertices[1] = new Vector3(upper *- width, upper*height, upper *- zet );
        vertices[2] = new Vector3(upper*width, upper*height, upper* - zet);
        vertices[3] = new Vector3(lower*width, -height, lower* - zet);

        vertices[4] = new Vector3(lower *- width, -height, lower*zet);
        vertices[5] = new Vector3(upper *- width, upper*height, upper* zet);
        vertices[6] = new Vector3(upper*width, upper*height, upper*zet);
        vertices[7] = new Vector3(lower*width, -height, lower*zet);

        mesh.vertices = vertices;
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 4, 5, 1, 4, 1, 0, 7, 6, 5, 7, 5, 4, 3, 2, 6, 3, 6, 7, 1, 5, 6, 1, 6, 2, 4, 0, 3, 4, 3, 7 };
        GetComponent<MeshRenderer>().material = mat;
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.RecalculateNormals();

    }
    public void Motion()

    {
        transform.position= new Vector3(0, Mathf.MoveTowards(0, 10, 0.1f), 0);
        if (lower == 3.5f || lower == 2)
        {
            a = !a;
        }
        

        if(a)
        {
            lower = Mathf.MoveTowards(lower, 3.5f, 0.05f);
            upper = Mathf.MoveTowards(upper, 0.5f, 0.05f);
        }
        else
        {
            lower = Mathf.MoveTowards(lower, 2, 0.05f);
            upper = Mathf.MoveTowards(upper, 2, 0.05f);
        }
     

    }
	
}
