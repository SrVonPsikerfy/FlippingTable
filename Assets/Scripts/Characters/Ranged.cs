﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : TokenBehaviour
{
    GameManager gM;
    void Start(){
        gM = GameManager.instance;
    }

    // Update is called once per frame
    void Update() {
        
    }
    public override void Move(){
        Debug.Log("Move Ranged");
    }

    public override void Attack(){
        Debug.Log("Attack Ranged");
    }
}