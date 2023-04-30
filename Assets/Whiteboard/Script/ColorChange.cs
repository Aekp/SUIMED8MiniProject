using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public MeshRenderer _tip;
    public MeshRenderer _sphere;

    // Start is called before the first frame update
    void Start()
    {
       _tip = GetComponent<MeshRenderer>();
        _sphere = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I reached here");
       _tip.material = _sphere.material;
    }
}
