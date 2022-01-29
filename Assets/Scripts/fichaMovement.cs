using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fichaMovement : MonoBehaviour
{
    bool selected = false;

    float altDif = 0.5f;
    float defaultPos = 1.5f;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

          if (Input.GetMouseButtonDown(0))
        {
  	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   	        RaycastHit hit;
   	        // Casts the ray and get the first game object hit
   	        Physics.Raycast(ray, out hit);
            Debug.Log(hit.transform);


            if(hit.transform.gameObject.GetComponent<casillaInfo>() != null && selected){

                Vector3 newPos = hit.transform.position;

                if(hit.transform.gameObject.GetComponent<casillaInfo>().getAltura() ==
                 GameManager.alturas.colina) newPos.y = this.transform.position.y + altDif;
                else if(hit.transform.gameObject.GetComponent<casillaInfo>().getAltura() ==
                 GameManager.alturas.valle)newPos.y = this.transform.position.y - altDif;
                else newPos.y = defaultPos;
                

                this.transform.position = newPos;

                this.transform.gameObject.GetComponent<FichaInfo>().setCords(hit.transform.gameObject.GetComponent<casillaInfo>().getCords());

                selected = false;

                Debug.Log("Awa");
            }
            else if(hit.transform.gameObject.GetComponent<FichaInfo>() != null){
                
                if(hit.transform.gameObject.GetComponent<FichaInfo>().getCords() != 
                this.gameObject.GetComponent<FichaInfo>().getCords()){
                    selected = false;

                    Debug.Log("Iwi");
                } 
                else{
                    selected = true;

                    Debug.Log("Ewe");
                } 


            }    
        }
    }

    void move(Vector2 dir){
        
    }
}
