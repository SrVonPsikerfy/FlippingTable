﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terraformer : TokenBehaviour
{
    // Start is called before the first frame update

    GameManager gM;
    void Start(){
        gM = GameManager.instance;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public override void Move(){
        Debug.Log("Move Terra");
    }

    public override void Attack(){
        Debug.Log("Attack Terra");
    }

    public void Edit(){
        // if()

        Debug.Log("Funka");
    }
}
