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
    [Header("Traslacion")]
    public float ySpeed = 0.1f;

    float movementY = 0.45f;
    //Privates
    Quaternion wantedRotation = Quaternion.Euler(0,0,0);
    bool flipDone = true;   //Ha terminado el flip
    bool topSide = true;    //Esta cara Arriba
    float countdown;
    int k = 0;  //diagonal inicial
    Vector3 yVel = Vector3.zero;


    void Start(){
        countdown = delay;
        generateMap();
    }

    // Update is called once per frame
    void Update(){
        //Diferencia de rotacion maxima entre quaterniones origen y destino por fallo de redondeo 
        float difference = Mathf.Abs(GameManager.getCell(size-1,size-1).transform.rotation.x - wantedRotation.x);
        if (Input.GetKeyDown(KeyCode.F) && flipDone){
            flipDone = false;
            wantedRotation = (topSide)? Quaternion.Euler(-90,0,0): Quaternion.Euler(90,0,0);
            topSide = !topSide;
        }
        else if(difference <= 0.02f){
            flipDone = true;
            k = 0;
            countdown = delay;
            resetFlip();    //Resetea los tiles para que se hagan flip en el proximo flipeo
        }

        if(!flipDone) flipMap();
    }
    void createCell(ref GameObject cases,out alturas a, int rng, float auxX, float auxZ){
        GameObject obj; 
        if(rng <= 80){ //Llano
            a = alturas.llano;
            obj = prefabLlano;
        } 
        else if(rng > 80 && rng <= 90) {    //Valle
            a = alturas.valle;
            obj = prefabValle;
        }
        else {  //Colina
            a = alturas.colina;
            obj = prefabColina;
        }
        cases = Instantiate(obj, new Vector3(auxX, movementY * (int)a , auxZ), Quaternion.identity);
        cases.transform.Rotate(new Vector3(90, 0 , 0));
        cases.transform.parent = this.transform; //Asignar parent al objeto vacio Map
    }
    void generateMap(){

        float init = -size/2;

        float auxX = init, auxZ = init;
        GameObject cases = null;
        if(prefabLlano != null && prefabColina != null && prefabLlano != null ){
            for(int i = 0; i < size; i++){
                for(int j = 0; j < size; j++){
                    //Altura de cadad casilla
                    GameManager.alturas a;
                    int rng = Random.Range(0,100);

                    createCell(ref cases, out a, rng, auxX, auxZ);
                    //Asignacion de propiedades de cada casilla
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
        //delay entre cada flip diagonal del tablero
        countdown -= Time.deltaTime;
        if(countdown <= 0){
            countdown = delay;
            k++;
        }    
        //Seguir flip de las diagonales anteriores hasta terminar
        for(int i = k; i >= 0; i--)
            flipDiagonal(i);
    }

    
    void flipDiagonal(int k){
        //update de posicion y rotacion de la diagonal k
        for(int j = 0 ; j <= k ; j++ ) {
            int i = k - j;
            if( i < size && j < size ) {
                //rotacion
                GameObject child = GameManager.getCell(i,j);
                Quaternion currentRotation = child.transform.rotation;
                child.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * rSpeed);
                //traslacion
                casillaInfo cas = child.GetComponent<casillaInfo>();
                if(!cas.isFlip()){
                    if(cas.getAltura() == GameManager.alturas.valle){
                        cas.setAltura(GameManager.alturas.colina);
                    }
                    else if(cas.getAltura()==GameManager.alturas.colina){
                        cas.setAltura( GameManager.alturas.valle);
                    }
                    cas.setFlip(true);
                }
                else{
                    Vector3 currentPosition = child.transform.position;
                    Vector3 wantedPosition = new Vector3(currentPosition.x , movementY * (int)cas.getAltura(), currentPosition.z); 
                    child.transform.position = Vector3.Lerp(currentPosition, wantedPosition, Time.deltaTime * ySpeed);
                    }
            }
        }
    }

    void resetFlip(){ 
        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                GameObject child = GameManager.getCell(i,j);
                casillaInfo cas = child.GetComponent<casillaInfo>();
                cas.setFlip(false);
            }
        }
    }
}
