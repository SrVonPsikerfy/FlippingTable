using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    int size; 
    
    public GameObject prefabW = null;

    public GameObject prefabB = null;
    [Header("RotateSpeed")]
    public float rSpeed = 5.0f;
    // Start is called before the first frame update
    void Start(){
        float init = -size/2;

        float auxX = init, auxZ = init;
        bool alternate = true;
        GameObject cases = null;
        if(prefabB != null && prefabW != null ){
            for(int i = 0; i < size; i++){
                for(int j = 0; j < size; j++){
                    if(alternate) 
                        cases = Instantiate(prefabB, new Vector3(auxX, 0 , auxZ), Quaternion.identity);    
                    else 
                        cases = Instantiate(prefabW, new Vector3(auxX, 0 , auxZ), Quaternion.identity);

                    auxZ++;
                    alternate = !alternate;
                }
                alternate = !alternate;
                auxX++;
                auxZ = init;
            }
        }
    }

    // Update is called once per frame
    void Update(){
            if (Input.GetKeyDown(KeyCode.F)){
                Debug.Log("uwu");
                rotateMap();
            }
        
    }

    void rotateMap(){
        Quaternion currentRotation = transform.rotation;
        Quaternion wantedRotation = Quaternion.Euler(180,0,0);
        transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * rSpeed);
    }
}
