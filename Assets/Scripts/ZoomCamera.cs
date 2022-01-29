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


    public GameObject map;
    private Camera mainCamera;
    private float zoom = 0;

    void Awake()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        zoom = mainCamera.orthographicSize = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
    }

    void LateUpdate() 
    {

        Quaternion camAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up); 
        mainCamera.orthographicSize = Mathf.Lerp (mainCamera.orthographicSize, zoom, Time.deltaTime * zoomSpeed);

        // transform.LookAt(map.transform);
    }
}
