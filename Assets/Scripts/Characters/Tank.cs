using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : TokenBehaviour
{
    GameManager gM;

    FichaInfo f ;
    void Start(){
        gM = GameManager.instance;
        f = this.gameObject.GetComponent<FichaInfo>();
        f.setStats(1, 1, 1, 4);
    }

    // Update is called once per frame
    void Update() {
        
    }
    public override void Move(){
        Debug.Log("Move Tank");
    }

    public override void Attack(){
        Debug.Log("Attack Tank");
    }
}
