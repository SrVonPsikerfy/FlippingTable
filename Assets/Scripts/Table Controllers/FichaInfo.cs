using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using fichas = GameManager.fichas;
public class FichaInfo : MonoBehaviour
{
    public GameObject Flag = null;
    public Sprite[] spriteArray;
    Vector2 cords;
    fichas f;
    GameObject currentFlag;
    int rango, dmg, mov, vida;
    
    bool dead = false;

    bool player1Side;
    bool lightSide;
    // Start is called before the first frame update
    void Start()
    {
        currentFlag = Instantiate(Flag, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f , this.gameObject.transform.position.z), Quaternion.identity);
        currentFlag.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        lightSide = GameManager.getTableroSide();
        if(player1Side){
            if(lightSide)
                currentFlag.GetComponent<SpriteRenderer>().sprite = spriteArray[0];
            else
                currentFlag.GetComponent<SpriteRenderer>().sprite = spriteArray[2];
        }
        else{
            if(lightSide)
                currentFlag.GetComponent<SpriteRenderer>().sprite = spriteArray[1];
            else
                currentFlag.GetComponent<SpriteRenderer>().sprite = spriteArray[3];
        }
        if(this.gameObject.GetComponent<Flag>() == null)
            currentFlag.SetActive(false);
        else
            currentFlag.SetActive(true);
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

    public void setStats(int r, int d, int m, int v){
        rango = r;

        dmg = d;

        mov = m;

        vida = v;

        dead = false;
    }

    public void die(){
        dead = true;
        GameObject cell = GameManager.getCell((int)this.GetComponent<FichaInfo>().getCords().y, (int)this.GetComponent<FichaInfo>().getCords().x);
        GameManager.dropFlag(this.gameObject, cell);
        Destroy(this.gameObject);
        GameManager.instance.removeDead();
    }

    public bool getDead(){
        return dead;
    }

    public int getRange(){
        return rango;
    }

    public int getMovement(){
        return mov;
    }

    public int getDamage(){
        return dmg;
    }

    public void receiveDamage(){

        vida--;
        if(vida<=0) die();
    }

    public void setSide(bool p1){
        player1Side = p1;
    }
    public bool getSide(){
        return player1Side;
    }
}
