using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : TokenBehaviour
{
    GameManager gM;
    void Start(){
        gM = GameManager.instance;
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
