using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MouseButton { Left, Right, Wheel }

public class RotationCamera : MonoBehaviour
{
    public GameManager gM;
    public GameObject map;
 
    [SerializeField]
    private float rotateSpeed = 5f;

    [SerializeField]
    private float smoothFactor = 0.5f;
    private Camera mainCamera = null;
    private Vector3 cameraOffset = new Vector3();
    
    private Vector3 mainPosition;
    private Quaternion mainRotation;

    Vector3 rotateAngle = new Vector3(0, 180, 0);

    void Awake()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
    }

    private float alt = 1;

    // Start is called before the first frame update
    void Start()
    {   
        mainRotation = transform.rotation;
        mainPosition = transform.position;
        cameraOffset = transform.position - map.transform.position;    
    }
    
    public void flip(){
        transform.position = mainPosition;
        transform.rotation = mainRotation;

        if(gM.getTurn())
            alt = -1;
        else alt = 1;
        
        Quaternion camAngleY = Quaternion.AngleAxis(rotateAngle.y * alt * rotateSpeed, Vector3.up);
        cameraOffset = camAngleY * cameraOffset;
    }

    void Update(){        
    }


    // Update is called once per frame
    void LateUpdate() {
        if(Input.GetMouseButton((int)MouseButton.Wheel)) {
            Quaternion camAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.up); 
            cameraOffset = camAngleX * cameraOffset;
        }

        transform.position = Vector3.Slerp(transform.position, cameraOffset, smoothFactor);

        transform.LookAt(map.transform);
    }
}
