using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasillaInfo : MonoBehaviour
{
    Vector2 cords;
    GameManager.alturas a;
    GameManager.flagCell flag;
    
    bool flip = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInfo(Vector2 c, GameManager.alturas alt, GameManager.flagCell f){
        cords = c;
        a = alt;
        flag = f;
    }
    public GameManager.alturas getAltura(){
        return a;
    }
    public void setAltura(GameManager.alturas alt){
        a = alt;
    }

    public Vector2 getCords(){
        return cords;
    }
    public bool isFlip(){
        return flip;
    }
    public void setFlip(bool state){
        flip = state;
    }
    public GameManager.flagCell getFlag(){
        return flag;
    }
    public void setFlip(GameManager.flagCell f){
        flag = f;
    }

}
