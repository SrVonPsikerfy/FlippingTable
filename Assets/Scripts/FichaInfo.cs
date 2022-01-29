using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using fichas = GameManager.fichas;
public class FichaInfo : MonoBehaviour
{
    Vector2 cords;
    fichas f;
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

    public void setCords(Vector2 newC){
        cords = newC;
    }

    public fichas getFich(){
        return f;
    }
}
