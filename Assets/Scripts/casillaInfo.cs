﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casillaInfo : MonoBehaviour
{
    Vector2 cords;
    GameManager.alturas a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInfo(Vector2 c, GameManager.alturas alt){
        cords = c;
        a = alt;
    }

    public Vector2 getCords(){
        return cords;
    }
}
