using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenBehaviour : MonoBehaviour {
    // Start is called before the first frame update

    protected bool gonMove = false;
    protected bool gonAttack = false;


    public virtual void Move(){
        Debug.Log("Move Base");
    } 

    public virtual void Attack(){
        Debug.Log("Attack Base");
    }

    public void setMove(bool move) => gonMove = move;
    public void setAttack(bool att) => gonAttack = att;

    public bool getMove() => gonMove;
    public bool getAttack() => gonAttack;
}
