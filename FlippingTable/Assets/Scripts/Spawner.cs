using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    int size; 
    public GameObject prefab = null;
    // Start is called before the first frame update
    void Start()
    {
        float init = size/2;

        float auxX = init, auxZ = init;
        if(prefab != null){
            Debug.Log("Cosas");
            for(int i = 0; i < size; i++){
                for(int j = 0; j < size; j++){
                    auxZ++;
                    Instantiate(prefab, new Vector3(auxX, 0 , auxZ), Quaternion.identity);
                }
                auxX++;
                auxZ = init;
            }
        }
    }

    // Update is called once per frame
    void Update(){

        
    }
}
