using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    int size; 
    public GameObject prefabW = null;

    public GameObject prefabB = null;
    // Start is called before the first frame update
    void Start(){
        float init = -size/2;

        float auxX = init, auxZ = init;
        bool alternate = true;
        if(prefabB != null && prefabW != null ){
            for(int i = 0; i < size; i++){
                for(int j = 0; j < size; j++){
                    if(alternate) Instantiate(prefabB, new Vector3(auxX, 0 , auxZ), Quaternion.identity);    
                    else Instantiate(prefabW, new Vector3(auxX, 0 , auxZ), Quaternion.identity);

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

        
    }
}
