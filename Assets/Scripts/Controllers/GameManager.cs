using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Controla en que lado del escenario está para controlar que ataque tiene
    public static bool sideUp = true;

    public MapManager map = null;
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
    public bool turnPlayer1;
    public enum fichas {ranged, melee, tank, engineer, none};
    public static int tableroSize = 8;
    public struct Casilla{
        public alturas altura;
        public fichas ficha;
        public bool ocupada;
        public Vector2 position;
        public GameObject cell;
        public GameObject fichaObj;
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
    static Casilla[,] tablero = new Casilla[tableroSize,tableroSize];

    List<GameObject> currentFichas = new List<GameObject>();
    List<GameObject> currentFichasPlayer2 = new List<GameObject>();
    List<KeyValuePair<MeshRenderer,alturas>>changedCasillas = new List<KeyValuePair<MeshRenderer,alturas>>();
    int nFichas = 0;
    int nFichasPlayer2 = 0;

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
    }

    public List<GameObject> getTabFichas(){
        return currentFichas;
    }

    public bool getTurn(){
        return turnPlayer1;
    }
    public void ChangeScene(string name)
    {
		if (name != "Transition" && name!= "Main Menu" && name!= "Cinematic_Video") currentLevel++;
        if (name == "Main Menu") currentLevel = 0;
        SceneManager.LoadScene(name);
        Time.timeScale = 1f; //Restaurar la ejecucion en caso de que esté pausado el juego.
    }

    public void changePlayer(){
        uiM.flip();

        turnPlayer1 = !turnPlayer1;
        int id = 1;
        if(!turnPlayer1)id = 2;

        if(uiM != null) uiM.changeId(id.ToString());

        
    }

    public void deadFicha(GameObject ficha){
        if(ficha.GetComponent<FichaInfo>() != null)
        ficha.GetComponent<FichaInfo>().die();
    }

    public void LevelFinished(bool playerWins)
    {
        
    }

    public void tokenSelected(GameObject token){
        uiM.displayActions(token);
    }
    public void tokenUnselected(){
        uiM.hideActions();
    }
    
    public int returncurrentLevel()
    {
        return currentLevel;
    }

    void Update(){
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
        c.fichaObj = null;

        tablero[(int)pos.y,(int)pos.x] = c;
    }

    //get theGameObject from a Cell
    public static GameObject getCell(int i, int j){
        return tablero[i,j].cell;
    }

    public void addFicha(Vector2 coord, GameObject obj){
        currentFichas.Add(obj);
        SetFicha((int)coord.x,(int)coord.y,obj);
        nFichas++;
    }

    public void addFichaPlayer2(Vector2 coord, GameObject obj){
        currentFichasPlayer2.Add(obj);
        SetFicha((int)coord.x,(int)coord.y,obj);
        nFichasPlayer2++;
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
        Vector2 esqIzq = new Vector2(cords.y - range, cords.x - range);
        Vector2 esqDer = new Vector2(cords.y + range + 1, cords.x + range + 1);

        for(int i = (int)esqIzq.y; i < esqDer.y; i++){
            for(int j = (int)esqIzq.x; j < esqDer.x; j++){
                Vector2 newCord = new Vector2(i,j);

                if(!validCords(newCord)) continue;

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
        // Debug.Log(changedCasillas.Count);

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

    public void SetFicha(int x, int y, GameObject obj){
        tablero[y,x].fichaObj = obj;
    }

    public void debug(){
        for(int i=0; i < currentFichas.Count;i++){
            Debug.Log(currentFichas[i].GetComponent<FichaInfo>().getCords());
        }
    }

    public static GameObject GetFicha(int x, int y){
        return tablero[y,x].fichaObj;
    }

    public static void setTableroSide() { sideUp = !sideUp;}
    public static bool getTableroSide() { return sideUp;}

    public static void pickFlag(GameObject obj, GameObject cell){
        if(cell.GetComponent<CasillaInfo>().getFlag() != GameManager.flagCell.None){
            if(cell.GetComponent<CasillaInfo>().getFlag() != GameManager.flagCell.player1){
                obj.AddComponent<Flag>().setType(GameManager.flagCell.player2);
                cell.transform.parent.GetComponent<MapManager>().deleteFlag(GameManager.flagCell.player2);
            }
            else{
                obj.AddComponent<Flag>().setType(GameManager.flagCell.player1);
                cell.transform.parent.GetComponent<MapManager>().deleteFlag(GameManager.flagCell.player1);
            }
                
            cell.GetComponent<CasillaInfo>().setFlag(GameManager.flagCell.None);
        }
    }

    public static void dropFlag(GameObject obj, GameObject cell){
        if(obj.GetComponent<Flag>() != null){
            GameManager.flagCell flag = obj.GetComponent<Flag>().getType();
            Destroy(obj.GetComponent<Flag>());
            cell.transform.parent.GetComponent<MapManager>().createFlag(cell.transform.position, cell.GetComponent<CasillaInfo>().getAltura(), flag);
            cell.GetComponent<CasillaInfo>().setFlag(flag);
        }
    }
    
}