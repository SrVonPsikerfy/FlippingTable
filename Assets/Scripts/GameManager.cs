using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // private UIManager uiManager;
    int currentLevel = 0;

    public enum alturas {valle, llano, colina};
    public enum fichas {ranged, melee, tank, engineer};

    struct Casilla{
        alturas altura;
        fichas fichas = null;
        bool ocupada = false;
        Vector2 position;
        GameObject cell;
    }

    Casilla[][] tablero = null;

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
        // uiManager.FinishGame(playerWins);
    }
    public int returncurrentLevel()
    {
        return currentLevel;
    }

    public void addCell(Vector2 pos, GameObject obj, alturas alt){
        Casilla c;

        c.position = pos;
        c.cell = obj;
        c.altura = alt;

        tablero[pos.y][pos.x] = c;
    }
}