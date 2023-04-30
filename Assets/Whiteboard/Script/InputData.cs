using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class InputData : MonoBehaviour
{
    public InputDevice _rightController;
    public InputDevice _leftController;

    private void Start()
    {
       
    }

  

    private void Update()
    {
        if (!_rightController.isValid || !_leftController.isValid);
            InitializeInputDevices();
    }

    private void InitializeInputDevices()
    {
        if (!_rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);
        if (!_leftController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref _leftController);
            
    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputDeviceCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        if(devices.Count > 0)
        {
            inputDevice = devices[0]; 
        }
    }
}
