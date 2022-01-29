using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField]
    private float zoomSensitivity = 10.0f;

    [SerializeField]
    private float zoomSpeed = 2.5f;

    [SerializeField]
    private float zoomMin = 3.5f;

    [SerializeField]
    private float zoomMax = 5f;

    [SerializeField]
    private float rotateSpeed = 5f;

    [Range(0.0f, 1.0f)]
    private float smoothFactor = 0.5f;


    public GameObject map;
    private Camera mainCamera;
    private float zoom = 0;


    private Vector3 cameraOffset;

    void Awake()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        zoom = mainCamera.orthographicSize = 4f;


        cameraOffset = transform.position - map.transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);

    }

    void LateUpdate() {
        if (Input.GetMouseButton(2)){
            Quaternion camAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up); 
            cameraOffset =  camAngleX * cameraOffset;
        }

        transform.position = Vector3.Slerp(transform.position, cameraOffset, smoothFactor);
        mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize, zoom, Time.deltaTime * zoomSpeed);

        transform.LookAt(map.transform);
    }
}
