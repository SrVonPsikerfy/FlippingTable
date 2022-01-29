using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using alturas = GameManager.alturas;
using flag = GameManager.flagCell;
public class MapManager : MonoBehaviour
{
    //Public
    [SerializeField]
    int size = GameManager.tableroSize; 

    public GameObject prefabColina = null;
    public GameObject prefabLlano = null;
    public GameObject prefabValle = null;
    public GameObject prefabFlag = null;
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
    GameObject player1Flag = null;
    GameObject player2Flag = null;

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

        float init = -((float)size)/2.0f;
        if(size%2 == 0) init += 0.5f;
        float auxX = init, auxZ = init;
        GameObject cases = null;
        if(prefabLlano != null && prefabColina != null && prefabLlano != null ){
            for(int i = 0; i < size; i++){
                for(int j = 0; j < size; j++){
                    //Altura de cadad casilla
                    GameManager.alturas a;
                    GameManager.flagCell f;
                    int rng = Random.Range(0,100);

                    createCell(ref cases, out a, rng, auxX, auxZ);
                    //Asignacion de propiedades de cada casilla
                    //Vector posicion
                    Vector2 pos2D = new Vector2(j,i);
                    GameManager.addCell(pos2D, cases, a);
                    //Si es esquina crea flag correspondiente
                    if(i == 0 && j == 0){
                        Vector3 pos = cases.transform.position;
                        switch(a){
                            case GameManager.alturas.valle: pos.y = 0.5f; break;
                            case GameManager.alturas.llano: pos.y = 1f; break;
                            case GameManager.alturas.colina: pos.y = 1.5f; break;
                        }
                        player1Flag = Instantiate(prefabFlag, pos, Quaternion.identity);
                        f = GameManager.flagCell.player1;
                    }
                    else if(i == size-1 && j == size - 1){
                        Vector3 pos = cases.transform.position;
                        switch(a){
                            case GameManager.alturas.valle: pos.y = 0.5f; break;
                            case GameManager.alturas.llano: pos.y = 1f; break;
                            case GameManager.alturas.colina: pos.y = 1.5f; break;
                        }
                        player2Flag = Instantiate(prefabFlag, pos, Quaternion.identity);
                        f = GameManager.flagCell.player2;
                    }
                    else
                        f = GameManager.flagCell.None;
                    CasillaInfo cas = cases.GetComponent<CasillaInfo>();
                    if(cas != null){
                        cas.setInfo(pos2D, a, f);
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
                CasillaInfo cas = child.GetComponent<CasillaInfo>();
                //ficha de encima de la casilla
                GameObject fichaCas = GameManager.GetFicha(i,j);
                Debug.Log(fichaCas);
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
                //Traslacion flags
                if(fichaCas!=null){
                    Debug.Log("aaaaaa");
                    Vector3 currentPosition = fichaCas.transform.position;
                    Vector3 wantedPosition = new Vector3(currentPosition.x , 1.0f + 0.5f * (int)cas.getAltura(), currentPosition.z); 
                    fichaCas.transform.position = Vector3.Lerp(currentPosition, wantedPosition, Time.deltaTime * ySpeed);
                }  
                if(cas.getFlag() == GameManager.flagCell.player1){
                    Vector3 currentPosition = player1Flag.transform.position;
                    Vector3 wantedPosition = new Vector3(currentPosition.x , 1.0f + 0.5f * (int)cas.getAltura(), currentPosition.z); 
                    player1Flag.transform.position = Vector3.Lerp(currentPosition, wantedPosition, Time.deltaTime * ySpeed);
                }
                else if(cas.getFlag() == GameManager.flagCell.player2){
                    Vector3 currentPosition = player2Flag.transform.position;
                    Vector3 wantedPosition = new Vector3(currentPosition.x , 1.0f + 0.5f * (int)cas.getAltura(), currentPosition.z); 
                    player2Flag.transform.position = Vector3.Lerp(currentPosition, wantedPosition, Time.deltaTime * ySpeed);
                }
            }
        }
    }

    void resetFlip(){ 
        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                GameObject child = GameManager.getCell(i,j);
                CasillaInfo cas = child.GetComponent<CasillaInfo>();
                cas.setFlip(false);
            }
        }
    }
}
