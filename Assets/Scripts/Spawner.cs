using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using alturas = GameManager.alturas;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    int size = GameManager.tableroSize; 

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

                    GameManager.alturas a;
                    int rng = Random.Range(0,100);

                    if(rng <= 80) a = alturas.llano;
                    else if(rng > 80 && rng <= 90) a = alturas.valle;
                    else a = alturas.colina;

                    Vector3 ss = prefabB.transform.localScale / 2;

                    if(alternate){ 
                        
                        cases = Instantiate(prefabB, new Vector3(auxX, ss.y * (int)a , auxZ), Quaternion.identity);
                    }    
                    else cases = Instantiate(prefabW, new Vector3(auxX, ss.y * (int)a , auxZ), Quaternion.identity);

                    Vector2 pos = new Vector2(j,i);
                    GameManager.addCell(pos, cases, a);
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
