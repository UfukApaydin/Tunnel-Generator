using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof(MeshFilter),typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour {
    public InputField InputFieldLayer;
    public InputField InputFieldCorner;
    public GameObject cube;
    public GameObject sphere;
    public Vector3[] centerPoints;
   

    //For mesh
    public Mesh mesh;
    Vector3[] vertices; 
    int[] triangles;
    public int cornerCount = 12;
    Vector3[] corner;
    public int layerNumber = 2;
    

	void Awake () {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.subMeshCount = 2;
    }
    void Start()
    {
       
        Debug.Log("Submeshes: " + mesh.subMeshCount);
    }
    //Get layer number 
    public void UI()
    {
        layerNumber = int.Parse(InputFieldLayer.text);
        cornerCount = int.Parse(InputFieldCorner.text);
        if (layerNumber >=2)
        {
            DrawDodecagon(CalculateDodecagon(10, new Vector3(0, 0, 0), cornerCount), layerNumber);
            UpdateMesh();
        }    
    }
    //Dodecagon corner calculation
    Vector3[] CalculateDodecagon (float r ,Vector3 centerPoint,int corners)
    {
        corner = new Vector3[corners];
        //float degree = 335;
        float degreeOffset = 360 / corners;
        float degree = 360 - (degreeOffset / ((corners + 1) % 2 + 1));
        for (int i = 0; i < cornerCount; i++)
        {
            corner[i] = new Vector3(r * Mathf.Sin(degree * Mathf.Deg2Rad), r * Mathf.Cos(degree * Mathf.Deg2Rad),0) + centerPoint;
            degree -= degreeOffset;
        }
        return corner;

    }
    //Dodecagon create mesh
    void DrawDodecagon(Vector3[] corner, int layer)
    {
        GameObject[] cubes;
        cubes = GameObject.FindGameObjectsWithTag("Cubes");
        foreach (GameObject Cube in cubes)
        {
            Destroy(Cube);
        }
        Vector3 nextCenterPoint = new Vector3(0,0,10);
        Vector3 oldCenterPoint = new Vector3(0, 0, 0);

        centerPoints = new Vector3[layer-1];
        //vertices = new Vector3[cornerCount * layer+1];
        //triangles = new int[(layer-1)* 6* cornerCount];

        vertices = new Vector3[cornerCount * layer ];
        triangles = new int[2*6* cornerCount*(layer-1)];
        int v = 0,k=0,t=0;


       
        for(int j = 0; j < layer -1; j++)
        {
            centerPoints[j] = oldCenterPoint;
            Instantiate(sphere, oldCenterPoint, Quaternion.identity);
            foreach (Vector3 var in corner)
            {
                Instantiate(cube, var + oldCenterPoint, Quaternion.identity);
            }

            for (int i = 0; i < cornerCount; i++)
            {
                
                vertices[v] = corner[k] + oldCenterPoint;
                vertices[v + cornerCount] = corner[k] + nextCenterPoint;


                triangles[t + 0] = v;
                triangles[t + 1] = v + cornerCount;
                triangles[t + 3] = v;

                if ((v + 1) % cornerCount == 0)
                {

                    triangles[t + 2] = v + 1;
                    triangles[t + 4] = v + 1;
                    triangles[t + 5] = (v + 1) - cornerCount;
                }
                else
                {

                    triangles[t + 2] = v + cornerCount + 1;
                    triangles[t + 4] = v + cornerCount + 1;
                    triangles[t + 5] = (v + 1);
                }

                v++;
                k++;
                k = k % cornerCount;
                t += 6;
            }
            oldCenterPoint = nextCenterPoint;
            nextCenterPoint = NextCenterPoint(nextCenterPoint);
        }


        foreach (Vector3 var in corner)
        {
            Instantiate(cube, var, Quaternion.identity);
        }

    }
    Vector3 NextCenterPoint(Vector3 currentCenterPoint)
    {

       return currentCenterPoint + new Vector3(Random.Range(-5,5), Random.Range(-5, 5), Random.Range(10, 20));
       //return currentCenterPoint + new Vector3(0,10,15);
    }
   
   
    void UpdateMesh () {
       
        mesh.Clear();
        mesh.vertices = vertices;
        
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
