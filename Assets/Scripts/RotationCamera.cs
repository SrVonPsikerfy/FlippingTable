using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MouseButton { Left, Right, Wheel }

public class RotationCamera : MonoBehaviour
{
    public GameObject map;
 
    [SerializeField]
    private float rotateSpeed = 5f;

    private Camera mainCamera = null;
    private Vector3 cameraOffset = new Vector3();
    private float smoothFactor = 0.5f;

    void Awake()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - map.transform.position;    
    }

    // Update is called once per frame
    void LateUpdate() {
        if(Input.GetMouseButton((int)MouseButton.Wheel)) 
        {
            Quaternion camAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up); 
            cameraOffset = camAngleX * cameraOffset;
        }

        transform.position = Vector3.Slerp(transform.position, cameraOffset, smoothFactor);

        transform.LookAt(map.transform);
    }
}
