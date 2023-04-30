using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class OffsetCalculator : MonoBehaviour
{
    public bool C1, C2;

    public GameObject whiteboard;
    public GameObject whiteboardmarker; 
    public GameObject hand;

    public WhiteboardMarker whiteboardMarker;
    private InputData inputData;

    [HideInInspector]
    public float handPositionx, whiteboardPositionx, offset;
    [HideInInspector]
    //for offset based penSize modulation
    public float penSize, minPenSize = 0.5f, maxPenSize=60.0f;
    [HideInInspector]
    public float inverseLerpValue;
    [HideInInspector]
    public int intPenSize, intpenSizeJoystick;
    [HideInInspector]
    public float minOffset = 0.0f, maxOffset = 0.5f;

    [HideInInspector]
    //for joystick based penSize modulation
    public float joystickPensize, minJoysticky = 0.0f, maxJoysticky = 1.0f;
  

    
    // Start is called before the first frame update
    void Start()
    {
        inputData = GetComponent<InputData>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCondition();
    }

    public void CheckCondition()
    {
        if (C1 == true)
        {
            offsetCalculator();
        }
        else if (C2 == true)
        {
            joystickCalculator();
        }
    }

    public void offsetCalculator()
    {
        handPositionx = hand.transform.position.x;
        whiteboardPositionx = whiteboard.transform.position.x;


        //Calculates the offset in the x-axis
        
        offset = Mathf.Abs(handPositionx - whiteboardPositionx);
        Debug.Log("Y - offset aboslute :" + offset);

        inverseLerpValue = Mathf.InverseLerp(minOffset, maxOffset, offset);

        penSize = Mathf.Pow(Mathf.Lerp(minPenSize, maxPenSize, inverseLerpValue), 2);
        intPenSize = Mathf.RoundToInt(penSize);
        whiteboardMarker._penSize = intPenSize;

        Debug.Log("Pensize :" + whiteboardMarker._penSize);
        //Debug.Log("Inverse Offset :" + inverseLerpValue);
        
    }

    public void joystickCalculator()
    {
        if (inputData._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightJoystick))
        {
            Debug.Log("Joystick :" + rightJoystick.y.ToString());
            
            joystickPensize = Mathf.Lerp(minPenSize, maxPenSize, Mathf.InverseLerp(minJoysticky, maxJoysticky, rightJoystick.y));
            intpenSizeJoystick = Mathf.RoundToInt(joystickPensize);
            whiteboardMarker._penSize = intpenSizeJoystick;

            Debug.Log("Pensize with joystick :" + whiteboardMarker._penSize);
           
        }
    }
   
}
