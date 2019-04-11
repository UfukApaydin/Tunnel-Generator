using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour {
    Camera mainCamera;
    public GameObject proceduralGrid;
    Vector3[] centerPointsTunnel;
    bool startMove = false;
    public float speed=100;
    public float rotationSpeed = 10;
    Vector3 speedVectors;
    int curretPoint = 0;
   
    public void StartCamera()
    {
        centerPointsTunnel = new Vector3[proceduralGrid.GetComponent<ProceduralGrid>().centerPoints.Length];
        for (int i = 0; i < proceduralGrid.GetComponent<ProceduralGrid>().centerPoints.Length; i++)
        {
            centerPointsTunnel[i] = proceduralGrid.GetComponent<ProceduralGrid>().centerPoints[i];
        }
        NextPoint();

    }
    public void ResetCamera()
    {

        transform.position = new Vector3(-200,90,-300);
        startMove = false;
        curretPoint = 0;
    }
    
    void NextPoint()
    {
        speedVectors = centerPointsTunnel[curretPoint] - transform.position;
        speedVectors /= speed;

        startMove = true;
    }


    float rotateDrag = 0;
    void Update () {
        if(startMove)
        {
             rotateDrag = Mathf.MoveTowards(rotateDrag, rotationSpeed, 0.02f);
            Debug.Log(rotateDrag);

            //rotate camera
            Quaternion lookRotation = Quaternion.LookRotation(speedVectors.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateDrag);
          
            //move camera
            transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, centerPointsTunnel[curretPoint].x, Mathf.Abs(speedVectors.x)),
                                             Mathf.MoveTowards(transform.position.y, centerPointsTunnel[curretPoint].y, Mathf.Abs(speedVectors.y)),
                                             Mathf.MoveTowards(transform.position.z, centerPointsTunnel[curretPoint].z, Mathf.Abs(speedVectors.z)));
            if(transform.position==centerPointsTunnel[curretPoint])
            {
                startMove = false;
                 rotateDrag = 0;
                if (curretPoint<centerPointsTunnel.Length-1)
                {
                    curretPoint++;
                    NextPoint();
                }
              
            }
        }
    }
      
}
