using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terraformer : TokenBehaviour
{
    // Start is called before the first frame update

    GameManager gM;
    FichaInfo info;
    bool gonMove = false;
    bool gonEdit = false;
    bool gonAttack = false;
    void Start(){
        gM = GameManager.instance;
        info = GetComponent<FichaInfo>();
    }

    // Update is called once per frame
    void Update() {
        if(gonAttack){

        }
            
    }

    public override void Move(){
        gonMove = !gonMove;
        if(gonMove) GameManager.instance.ShowRange(info.getCords(), info.getRange());
        else GameManager.instance.hideRange();
    }

    public void setMove(bool move) => gonMove = move;
    public void setAttack(bool att) => gonAttack = att;
    public void setEdit(bool edit) => gonEdit = edit;

    public bool getMove() => gonMove;
    public bool getAttack() => gonAttack;
    public bool getEdit() => gonEdit;

    public override void Attack(){
        Debug.Log("Attack Terra");
        gonAttack = !gonAttack;
        if(gonAttack) GameManager.instance.ShowRange(info.getCords(), info.getRange());
        else GameManager.instance.hideRange();
    }

    public void Edit(){
        gonEdit = !gonEdit;
    }
}
