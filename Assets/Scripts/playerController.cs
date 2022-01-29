using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject melee;
    public GameObject ranged;
    public GameObject tank;
    public GameObject engi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            GameObject casillaAux = GameManager.getCell(0,1);
            this.gameObject.transform.position = new Vector3(casillaAux.transform.position.x,
            this.transform.position.y,casillaAux.transform.position.z);

            Vector3 pos = casillaAux.transform.position;

            //Hay que hacer una variable con las alturas
            pos.y = 1.53f;

            if (Input.GetKeyDown(KeyCode.A)){

                GameObject g = Instantiate(melee, pos, Quaternion.identity);

                FichaInfo f = g.GetComponent<FichaInfo>();

                if(f != null){
                    f.setInfo(new Vector2(0,1), GameManager.fichas.melee);
                }    
            }
            else if(Input.GetKeyDown(KeyCode.S)){
                GameObject g = Instantiate(ranged, pos, Quaternion.identity);

                FichaInfo f = g.GetComponent<FichaInfo>();

                if(f != null){
                    f.setInfo(new Vector2(0,1), GameManager.fichas.ranged);
                }  
            }
            else if(Input.GetKeyDown(KeyCode.D)){
                GameObject g = Instantiate(tank, pos, Quaternion.identity);

                FichaInfo f = g.GetComponent<FichaInfo>();

                if(f != null){
                    f.setInfo(new Vector2(0,1), GameManager.fichas.tank);
                }  
            }
            else if(Input.GetKeyDown(KeyCode.R)){
                GameObject g = Instantiate(engi, pos, Quaternion.identity);

                FichaInfo f = g.GetComponent<FichaInfo>();

                if(f != null){
                    f.setInfo(new Vector2(0,1), GameManager.fichas.engineer);
                }  
            }
        }

    }
}
