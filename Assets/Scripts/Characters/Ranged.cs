using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : TokenBehaviour
{
    GameManager gM;
    bool attacks=false;

    FichaInfo f ;
    float timer = 0;
    void Start(){
        gM = GameManager.instance;
        f = this.gameObject.GetComponent<FichaInfo>();
    }

    // Update is called once per frame
    void Update() {
            FichaMovement mov = this.gameObject.GetComponent<FichaMovement>();
            Debug.Log(mov.selected);
        if(attacks && timer <= 0 && Input.GetMouseButtonDown(1)&&mov.selected){
            timer = 5.0f;
            attacks=false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
           // if(hit == null) return;

            FichaInfo other = hit.transform.gameObject.GetComponent<FichaInfo>();
           // if(other==null)return;

            Debug.Log("rango: "+f.getRange());
            Debug.Log(other.getDamage());
            Vector2 thisCoords = f.getCords();
            Vector2 otherCoords = other.getCords();

            Vector2 diff = thisCoords - otherCoords;
            Debug.Log("diferencia: " + diff.magnitude);
            if (mov.selected && Mathf.Abs(diff.x) <= f.getRange()&&Mathf.Abs(diff.y) <= f.getRange()){
                other.receiveDamage();
                Debug.Log("boink");

            }
        }

        if(timer > 0.0f) timer -= Time.deltaTime*0.5f;
    }   

    public override void Move(){
         gonMove = !gonMove;
        if(gonMove) GameManager.instance.ShowRange(f.getCords(), f.getRange());
        else GameManager.instance.hideRange();
        Debug.Log("Move Ranged");
    }

    public override void Attack(){
        if(GameManager.getTableroSide()){
            Debug.Log("Attack Melee");
            attacks=true;
        } 
        else Debug.Log("Other attack");
    }
}
