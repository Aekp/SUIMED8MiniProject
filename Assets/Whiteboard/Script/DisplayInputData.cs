using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DisplayInputData : MonoBehaviour
{
    private InputData _inputData;

    private Vector2 _joystickY, _joystickX;
    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<InputData>();
        
    }

    // Update is called once per frame
    void Update()
    {
       if(_inputData._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightJoystick))
        {
            //Debug.Log("Joystick :" + rightJoystick.ToString());
        }
    }
}
