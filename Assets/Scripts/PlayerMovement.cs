using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Transform Pivot = null;
    private Quaternion destRot = Quaternion.identity;

    public float pivotDistance = 5F;
    public float pivotDistanceOffSet = 5;
    public float rotSpeed = 10f;
    
    private float rotY = 0f;
    

	void Update () {
        float Horz = Input.GetAxis("Horizontal");
        
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            pivotDistanceOffSet = 3;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pivotDistanceOffSet = 5;
        }

        if(pivotDistance != pivotDistanceOffSet)
        {
            pivotDistance = Mathf.MoveTowards(pivotDistance, pivotDistanceOffSet, 0.05f);
        }
        


        rotY += Horz * Time.deltaTime * rotSpeed;

        Quaternion Yrot = Quaternion.Euler(0f, 0f, rotY);
        destRot = Yrot;

        transform.rotation = destRot;
        transform.position = Pivot.position + transform.rotation * Vector3.up * -pivotDistance;
    }
}
