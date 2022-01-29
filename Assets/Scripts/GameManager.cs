using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // private UIManager uiManager;
    int currentLevel = 0;

    public enum alturas {valle =  -1, llano = 0, colina = 1};
    public enum fichas {ranged, melee, tank, engineer, none};
    public static int tableroSize = 9;
    public struct Casilla{
        public alturas altura;
        public fichas ficha;
        public bool ocupada;
        public Vector2 position;
        public GameObject cell;
    }

    static Casilla[,] tablero = new Casilla[tableroSize,tableroSize];

    private void Awake()
    {
        //Cosa que viene en los apuntes para que el gamemanager no se destruya entre escenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeScene(string name)
    {
		if (name != "Transition" && name!= "Main Menu" && name!= "Cinematic_Video") currentLevel++;
        if (name == "Main Menu") currentLevel = 0;
        SceneManager.LoadScene(name);
        Time.timeScale = 1f; //Restaurar la ejecucion en caso de que esté pausado el juego.
    }

    public void LevelFinished(bool playerWins)
    {
        //Debug.LogError("levelfinished");
        // uiManager.FinishGame(playerWins);s
    }
    public int returncurrentLevel()
    {
        return currentLevel;
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
    GameObject getCell(int i, int j){
        return tablero[i,j].cell;
    }
}