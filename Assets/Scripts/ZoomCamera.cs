using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField]
    private float zoomSensitivity = 10.0f, zoomSpeed = 2.5f, zoomMin = 3.5f, zoomMax = 5f;

    private Camera mainCamera = null;
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
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoom, Time.deltaTime * zoomSpeed);
    }
}
