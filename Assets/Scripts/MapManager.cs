using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using alturas = GameManager.alturas;
public class MapManager : MonoBehaviour
{
    //Public
    [SerializeField]
    int size = GameManager.tableroSize; 

    public GameManager gM; 
    public GameObject prefabW = null;
    public GameObject prefabB = null;
    [Header("RotateSpeed")]
    public float rSpeed = 5.0f;

    //Privates
    Quaternion wantedRotation = Quaternion.Euler(180,0,0);
    bool flipDone = true;
    bool topSide = true;

    void Start(){

        if(gM == null){
            Debug.Log("There is not GameManager");
        }

        generateMap();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("uwu");
            flipDone = false;
            topSide = !topSide;
            wantedRotation = (topSide)? Quaternion.Euler(0,0,0): Quaternion.Euler(180,0,0);
        }
        if(GameManager.getCell(size-1,size-1).transform.rotation.x == wantedRotation.x)
            flipDone = true;
            
        if(!flipDone) flipMap();
    }

    void generateMap(){

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

    void flipMap(){
        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                GameObject child = GameManager.getCell(i,j);
                Quaternion currentRotation = child.transform.rotation;
                child.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * rSpeed);
            }
        }        
    }
}
