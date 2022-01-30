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
        f = this.gameObject.GetComponent<FichaInfo>();
        f.setStats(1,1,1,2);
    }
    // Update is called once per frame
    void Update() {
        
        if(f.getDamage() > 0){

        if(attacks && Input.GetMouseButtonDown(0)){
            attacks=false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            FichaInfo other = hit.transform.gameObject.GetComponent<FichaInfo>();

            Debug.Log(f.getRange());
            Vector2 thisCoords = f.getCords();
            Vector2 otherCoords = other.getCords();

            if ( Mathf.Abs (thisCoords.x-otherCoords.x) <= f.getRange() ||
            Mathf.Abs (thisCoords.y-otherCoords.y)<=f.getRange()){
                other.receiveDamage();
                Debug.Log("boink");
            }
        }
        }
        else f.die();
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
}
