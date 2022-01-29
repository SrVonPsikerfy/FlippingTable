using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // private UIManager uiManager;
    int currentLevel = 0;

    public Material[] valleSelectedMaterial = new Material[2];
    public Material[] llanoSelectedMaterial = new Material[2];
    public Material[] colinaSelectedMaterial = new Material[2];

    public Material[] valleInitMaterial = new Material[2];
    public Material[] llanoInitMaterial = new Material[2];
    public Material[] colinaInitMaterial = new Material[2];

    public enum alturas {valle =  -1, llano = 0, colina = 1};

    public enum flagCell { None, player1, player2}
    public enum playerName { player1, player2};
    public enum fichas {ranged, melee, tank, engineer, none};
    public static int tableroSize = 10;
    public struct Casilla{
        public alturas altura;
        public fichas ficha;
        public bool ocupada;
        public Vector2 position;
        public GameObject cell;
    }

    private UIManager uiM;
    //Array para las direcciones
    public static Vector2[] direcciones = {
        new Vector2(1,-1),
        new Vector2(1,0),
        new Vector2(1,1),
        new Vector2(0,-1),
        new Vector2(0,0),
        new Vector2(0,1),
        new Vector2(-1,-1),
        new Vector2(-1,0),
        new Vector2(-1,1),
    };
    private playerName turn;
    static Casilla[,] tablero = new Casilla[tableroSize,tableroSize];

    List<GameObject> currentFichas = new List<GameObject>();
    List<KeyValuePair<MeshRenderer,alturas>>changedCasillas = new List<KeyValuePair<MeshRenderer,alturas>>();
    int nFichas = 0;

    int nChanged = 0;
    private void Awake()
    {
        //Cosa que viene en los apuntes para que el gamemanager no se destruya entre escenas
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }

        turn = playerName.player2;
    }

    public bool getTurn(){
        return turn != playerName.player1;
    }
    public void ChangeScene(string name)
    {
		if (name != "Transition" && name!= "Main Menu" && name!= "Cinematic_Video") currentLevel++;
        if (name == "Main Menu") currentLevel = 0;
        SceneManager.LoadScene(name);
        Time.timeScale = 1f; //Restaurar la ejecucion en caso de que esté pausado el juego.
    }

    public void changePlayer(){
        Debug.Log(uiM);
        turn = (playerName)(((int)turn+1) % 2);
        int id = (int)turn;
        id++;
        if(uiM != null) uiM.changeId(id.ToString());
    }

    public void LevelFinished(bool playerWins)
    {
        
    }
    
    public int returncurrentLevel()
    {
        return currentLevel;
    }

    void Update(){
        Debug.Log(uiM);
    }

    public void SetUIManager(UIManager ui){
        uiM = ui;
    }
    //We store all the information about the table
    public static void addCell(Vector2 pos, GameObject obj, alturas alt){
        Casilla c;

        c.position = pos;
        c.cell = obj;
        c.altura = alt;
        c.ocupada = false;
        c.ficha = fichas.none;    

        tablero[(int)pos.y,(int)pos.x] = c;
    }

    //get theGameObject from a Cell
    public static GameObject getCell(int i, int j){
        return tablero[i,j].cell;
    }

    public void addFicha(GameObject obj){
        currentFichas.Add(obj);

        nFichas++;
    }

    public GameObject getFichas(int indice){
        return currentFichas[indice];
    }

    public void removeDead(){

        for(int i = 0; i < nFichas;i++){
            if(currentFichas[i].GetComponent<FichaInfo>().getDead()){

                Destroy(currentFichas[i]);

                currentFichas.RemoveAt(i);

                nFichas--;
            }
        }
    }

    public void ShowRange(Vector2 cords, int range){
        for(int i=0; i < direcciones.Length; i++){
            Vector2 newCord = cords + (direcciones[i] * range);

            if(validCords(newCord)){

                GameObject g = tablero[(int)newCord.y,(int) newCord.x].cell;

                alturas a = tablero[(int)newCord.y,(int) newCord.x].altura;

                KeyValuePair<MeshRenderer,alturas> p;

                if(a == alturas.valle){
                    MeshRenderer mes = g.gameObject.GetComponent<MeshRenderer>();

                    for(int index = 0; index < mes.materials.Length; index++){
                        mes.materials = valleSelectedMaterial;
                    }

                    p = new KeyValuePair<MeshRenderer,alturas>(mes,a);
                    changedCasillas.Add(p);
                }
                else if(a == alturas.llano){
                    MeshRenderer mes = g.gameObject.GetComponent<MeshRenderer>();

                    for(int index = 0; index < mes.materials.Length; index++){
                        mes.materials = llanoSelectedMaterial;
                    }

                    p = new KeyValuePair<MeshRenderer,alturas>(mes,a);
                    changedCasillas.Add(p);
                }
                else{
                    MeshRenderer mes = g.gameObject.GetComponent<MeshRenderer>();

                    for(int index = 0; index < mes.materials.Length; index++){
                        mes.materials = colinaSelectedMaterial;
                    }

                    p = new KeyValuePair<MeshRenderer,alturas>(mes,a);
                    changedCasillas.Add(p);
                }

                nChanged++;
            }
        }
    }

    public void hideRange(){
        Debug.Log(changedCasillas.Count);

        while(nChanged > 0){

            if(changedCasillas[nChanged - 1].Value == alturas.valle){

                changedCasillas[nChanged - 1].Key.materials = valleInitMaterial;

            }
            else if(changedCasillas[nChanged - 1].Value== alturas.llano){

                changedCasillas[nChanged - 1].Key.materials = llanoInitMaterial;
            }
            else{
                changedCasillas[nChanged - 1].Key.materials = colinaInitMaterial;
            }

            changedCasillas.RemoveAt(nChanged - 1);
            
            nChanged--;
        }

    }

    public bool validCords(Vector2 cord){

        bool valid = cord.x >= 0 && cord.y >= 0 && 
        cord.x < tableroSize && cord.y < tableroSize;

        return valid;
    }
}