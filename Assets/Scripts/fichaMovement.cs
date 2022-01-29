using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fichaMovement : MonoBehaviour
{
    bool selected = false;

    float altDif = 0.5f;
    float defaultPos = 1f;
    float vallePos = 0.3f;
    float colinaPos = 1.5f;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M)){


            FichaInfo f = this.gameObject.GetComponent<FichaInfo>();

            if(f != null){
                f.die();

                
            }    
        }  

        if (Input.GetMouseButtonDown(0))
        {
  	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   	        RaycastHit hit;
   	        // Casts the ray and get the first game object hit
   	        Physics.Raycast(ray, out hit);


            if(hit.transform.gameObject.GetComponent<casillaInfo>() != null && selected){
                
                Vector2 dist =   hit.transform.gameObject.GetComponent<casillaInfo>().getCords() -
                    this.transform.gameObject.GetComponent<FichaInfo>().getCords();

                    Debug.Log(dist);  
                    Debug.Log(this.gameObject.GetComponent<FichaInfo>().getMovement());

                    if(this.gameObject.GetComponent<FichaInfo>().getMovement() >= dist.x &&
                    this.gameObject.GetComponent<FichaInfo>().getMovement() >= dist.y){
                        Vector3 newPos = hit.transform.position;

                        if(hit.transform.gameObject.GetComponent<casillaInfo>().getAltura() ==
                            GameManager.alturas.colina) newPos.y = colinaPos;
                        else if(hit.transform.gameObject.GetComponent<casillaInfo>().getAltura() ==
                            GameManager.alturas.valle)newPos.y = vallePos;
                        else newPos.y = defaultPos;
                

                        this.transform.position = newPos;

                        this.transform.gameObject.GetComponent<FichaInfo>().setCords(hit.transform.gameObject.GetComponent<casillaInfo>().getCords());

                        selected = false;

                    }


            }
            else if(hit.transform.gameObject.GetComponent<FichaInfo>() != null){
                
                if(hit.transform.gameObject.GetComponent<FichaInfo>().getCords() != 
                this.gameObject.GetComponent<FichaInfo>().getCords()){
                    selected = false;

                    
                } 
                else{
                    selected = true;


                    // GameManager.instance.ShowRange(this.gameObject.GetComponent<FichaInfo>()
                    // .getCords(), this.gameObject.GetComponent<FichaInfo>().getRange());
                } 


            }  


        }
    }

    void move(Vector2 dir){
        
    }
}
