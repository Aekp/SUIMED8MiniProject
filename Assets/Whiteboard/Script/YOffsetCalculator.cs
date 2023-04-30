using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using System;

public class YOffsetCalculator : MonoBehaviour
{
    public GameObject hand, whiteboard;
    public float yOffset, clampedyOffset, firstyOffset = 0.0f, maxOffset = -1.0f, pensize;

    public float calculatedyOffset;
    
    public float minPenSize = 0.05f, maxPenSize = 40.0f;
    public int pensizeint;

    public WhiteboardMarker whiteboardMarker;

    public float scaleFactor = 2.0f, scaledyOffset;

    // Start is called before the first frame update
    void Start()
    {
        yOffset = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        yOffset = hand.transform.position.z - whiteboard.transform.position.z;
        Debug.Log("y offset :" + yOffset);

    }




    public void OnCollisionStay(Collision collision)
    {
        yOffset = hand.transform.position.y - whiteboard.transform.position.y;
        //Debug.Log("Y offset :" + yOffset);

        
       
    }

    public void OnCollisionExit(Collision collision)
    {
        yOffset = 0;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //firstyOffset = hand.transform.position.y - whiteboard.transform.position.y;
        //Debug.Log("First yOffset :" + firstyOffset);

    }

    
    public void CalculatePensize()
    {

        scaledyOffset = yOffset * scaleFactor;

        //calculatedyOffset = Mathf.Pow(yOffset / maxOffset, 2);
        //calculatedyOffset = Math.Clamp(calculatedyOffset, 0.0f, 1.0f);


        //These valules needs to be explored more. It is on the brink of working



        //pensize = Mathf.Lerp(minPenSize, maxPenSize, calculatedyOffset);
        pensize = Mathf.Lerp(minPenSize, maxPenSize, Mathf.InverseLerp(firstyOffset, maxOffset, yOffset));
        pensizeint = Mathf.RoundToInt(pensize);
        whiteboardMarker._penSize = pensizeint;
        //Debug.Log("Pen Size :" + whiteboardMarker._penSize);
    }

}
