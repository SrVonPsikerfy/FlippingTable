using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Heights { Valley, Plane, Mountain }

public class PlayerController : MonoBehaviour
{
    public GameObject melee, ranged, tank, engi;

    //Variable de prefab 
    GameManager.fichas ficha = GameManager.fichas.none;
    public bool p1;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && myTurn())
        {   
            if(Input.GetKeyDown(KeyCode.Space)){
                GameManager.instance.tokenUnselected();
                GameManager.instance.changePlayer();
            }
            else if(GameManager.instance.getTurnAction() == GameManager.turnActions.none && GameManager.instance.getlockedToken() == null){  //Solo coloca si no ha hecho nada
                if (Input.GetMouseButtonDown(0)){
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    if (hit.transform == null) return;

                    CasillaInfo cell_info = hit.transform.gameObject.GetComponent<CasillaInfo>();
                
                    if(cell_info != null && ficha != GameManager.fichas.none){
                        createToken(ficha, hit.transform.position, cell_info.getCords());
                        ficha = GameManager.fichas.none;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha1)){
                    ficha = GameManager.fichas.melee;
                    Debug.Log(ficha);
                    }
                else if (Input.GetKeyDown(KeyCode.Alpha2)){
                    ficha = GameManager.fichas.ranged;
                    Debug.Log(ficha);
                    }
                else if (Input.GetKeyDown(KeyCode.Alpha3)){
                    ficha = GameManager.fichas.tank;
                    Debug.Log(ficha);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4)){
                    ficha = GameManager.fichas.engineer;
                    Debug.Log(ficha);
                }
            }
        }

    }

    void createToken(GameManager.fichas prefab, Vector3 pos, Vector2 cell_pos){
        
        GameObject g = null;
        switch(prefab){
            case GameManager.fichas.melee:
                g = Instantiate(melee, pos, Quaternion.identity);
                break;
            case GameManager.fichas.ranged:
                g = Instantiate(ranged, pos, Quaternion.identity);
                break;
            case GameManager.fichas.tank:
                g = Instantiate(tank, pos, Quaternion.identity);
                break;
            case GameManager.fichas.engineer:
                g = Instantiate(engi, pos, Quaternion.identity);
                break;

        }

        FichaInfo f = g.GetComponent<FichaInfo>();

        if (f != null)
            f.setInfo(cell_pos, GameManager.fichas.engineer);

        if (GameManager.instance.getTurn() == 1) GameManager.instance.addFicha(cell_pos, g);
        else GameManager.instance.addFichaPlayer2(cell_pos, g);

        f.setSide(p1);

        GameManager.instance.setTurnAction(GameManager.turnActions.placed);
    }
    private bool myTurn()
    {  
        return (p1 && GameManager.instance.getTurn() == 1) || (!p1 && GameManager.instance.getTurn() == 2);
    }
}
