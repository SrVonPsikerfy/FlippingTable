﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaInfo : MonoBehaviour
{
    Vector2 cords;
    GameManager.fichas f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInfo(Vector2 c,GameManager.fichas fich){
        f = fich;

        cords = c;
    }

    public Vector2 getCords(){
        return cords;
    }
}