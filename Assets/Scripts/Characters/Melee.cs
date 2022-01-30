using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee  : TokenBehaviour
{
    GameManager gM;
    bool attacks=false;

    FichaInfo f ;

    void Start(){
        gM = GameManager.instance;
        f= this.gameObject.GetComponent<FichaInfo>();
        f.setStats(1,1,1,2);
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.M))
        {
            receiveDamage();
        }
    }
    public override void Move(){
        gonMove = !gonMove;
        if(gonMove) GameManager.instance.ShowRange(f.getCords(), f.getRange());
        else GameManager.instance.hideRange();
    }

    public override void Attack(){
        if(GameManager.getTableroSide()){
            Debug.Log("Attack Melee");
            attacks=true;
        } 
        else Debug.Log("Heal attack");
    }
    
    public void receiveDamage(){
        f.receiveDamage();
        if(f.getDamage() > 0) Debug.Log(f.getDamage());
        else f.die();
    }
}
