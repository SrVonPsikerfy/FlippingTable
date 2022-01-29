using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mCamera;
    void Start() 
    {
        mCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = mCamera.transform.forward;
        // transform.LookAt(Camera.main.transform.position, -Vector3.up);
        // transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
