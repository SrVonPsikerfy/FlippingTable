using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using alturas = GameManager.alturas;
public class MapManager : MonoBehaviour
{
    //Public
    [SerializeField]
    int size = GameManager.tableroSize; 

    public GameObject prefabColina = null;
    public GameObject prefabLlano = null;
    public GameObject prefabValle = null;
    [Header("delay")]
    public float delay = 0.5f;
    public float rSpeed = 50;
    public Vector3 yVel = Vector3.zero;

    float movementY = 0.5f;
    //Privates
    Quaternion wantedRotation = Quaternion.Euler(0,0,0);
    bool flipDone = true;   //Ha terminado el flip
    bool topSide = true;    //Esta cara Arriba
    float countdown;
    int k = 0;  //diagonal inicial


    void Start(){
        countdown = delay;
        generateMap();
    }

    // Update is called once per frame
    void Update(){


        float difference = Mathf.Abs(GameManager.getCell(size-1,size-1).transform.rotation.x - wantedRotation.x);
        if (Input.GetKeyDown(KeyCode.F) && flipDone){
            flipDone = false;
            Debug.Log(topSide);

            wantedRotation = (topSide)? Quaternion.Euler(-90,0,0): Quaternion.Euler(90,0,0);
            topSide = !topSide;
        }
        else if(difference <= 0.02f){
            flipDone = true;
            k = 0;
            countdown = delay;
        }

        if(!flipDone) flipMap();
    }

    void generateMap(){

        float init = -size/2;

        float auxX = init, auxZ = init;
        GameObject cases = null;
        if(prefabLlano != null && prefabColina != null && prefabLlano != null ){
            for(int i = 0; i < size; i++){
                for(int j = 0; j < size; j++){

                    GameManager.alturas a;
                    int rng = Random.Range(0,100);

                    if(rng <= 80){
                        a = alturas.llano;
                        cases = Instantiate(prefabLlano, new Vector3(auxX, movementY * (int)a , auxZ), Quaternion.identity);
                    } 
                    else if(rng > 80 && rng <= 90) {
                        a = alturas.valle;
                        cases = Instantiate(prefabValle, new Vector3(auxX, movementY* (int)a , auxZ), Quaternion.identity);
                    }
                    else {
                        a = alturas.colina;
                        cases = Instantiate(prefabColina, new Vector3(auxX, movementY * (int)a , auxZ), Quaternion.identity);
                    }

                    cases.transform.Rotate(new Vector3(90, 0 , 0));

                    cases.transform.parent = this.transform;
                    Vector2 pos = new Vector2(j,i);
                    GameManager.addCell(pos, cases, a);
                    casillaInfo cas = cases.GetComponent<casillaInfo>();
                    if(cas != null){
                        cas.setInfo(pos, a);
                    }
                    auxZ++;
                }
                auxX++;
                auxZ = init;
            }
        }
    }

    void flipMap(){
        countdown -= Time.deltaTime;
        if(countdown <= 0){
            countdown = delay;
            k++;
        }    
        for(int i = k; i >= 0; i--)
            flipDiagonal(i);
    }

    
    void flipDiagonal(int k){
        for(int j = 0 ; j <= k ; j++ ) {
            int i = k - j;
            if( i < size && j < size ) {
                //rotacion
                GameObject child = GameManager.getCell(i,j);
                Quaternion currentRotation = child.transform.rotation;
                child.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * rSpeed);
                //traslacion
                casillaInfo cas = child.GetComponent<casillaInfo>();
                // if(cas.getAltura() == GameManager.alturas.valle){
                //     cas.setAltura(GameManager.alturas.colina);
                //     Vector3 currentPosition = child.transform.position;
                //     Vector3 wantedPosition = new Vector3(currentPosition.x ,currentPosition.y + child.transform.localScale.y, currentPosition.z); 
                //     child.transform.position = Vector3.SmoothDamp(currentPosition, wantedPosition, ref yVel, Time.deltaTime);
                // }
                // else if(cas.getAltura()==GameManager.alturas.colina){
                //     cas.setAltura( GameManager.alturas.valle);
                //     Vector3 currentPosition = child.transform.position;
                //     Vector3 wantedPosition = new Vector3(currentPosition.x ,currentPosition.y - child.transform.localScale.y, currentPosition.z); 
                //     child.transform.position = Vector3.SmoothDamp(currentPosition, wantedPosition, ref yVel, Time.deltaTime);
                // }
            }
        }
    }
}
