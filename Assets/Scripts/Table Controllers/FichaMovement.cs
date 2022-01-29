using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaMovement : MonoBehaviour
{
    bool selected = false, moving = false;

    float altDif = 0.5f;
    float defaultPos = 1f;
    float vallePos = 0.5f;
    float colinaPos = 1.5f;

    Vector3 newPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)){
            FichaInfo f = this.gameObject.GetComponent<FichaInfo>();

            if(f != null)
                f.die();   
        }  

        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.hideRange();
  	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   	        RaycastHit hit;
   	        // Casts the ray and get the first game object hit
   	        Physics.Raycast(ray, out hit);

            //asies tengo retraso mental severo by Laura
            if(hit.transform == null) return;

            //para codigo limpito
            CasillaInfo infCas = hit.transform.gameObject.GetComponent<CasillaInfo>();
            FichaInfo infGeneral = this.gameObject.GetComponent<FichaInfo>();
            FichaInfo infTransf = this.transform.gameObject.GetComponent<FichaInfo>();
            FichaInfo infHit = hit.transform.gameObject.GetComponent<FichaInfo>();

            if(infCas != null && selected && !moving){
                
                Vector2 dist = infCas.getCords() - infTransf.getCords();

                if(infGeneral.getMovement() >= Mathf.Abs(dist.x) && infGeneral.getMovement() >= Mathf.Abs(dist.y)){
                    newPos = hit.transform.position;

                    if(infCas.getAltura() == GameManager.alturas.colina) newPos.y = colinaPos;
                    else if(infCas.getAltura() == GameManager.alturas.valle) newPos.y = vallePos;
                    else newPos.y = defaultPos;
            
                    this.transform.position = Vector3.Lerp(this.transform.position, newPos, 0.5f);
                    GameManager.instance.SetFicha((int)infCas.getCords().x,(int) infCas.getCords().y, hit.transform.gameObject);
                    infTransf.setCords(infCas.getCords());

                    selected = false; moving = true;
                }
            }
            else if(infHit != null){         
                selected = (infHit.getCords() == infGeneral.getCords());

                if(selected){
                    GameManager.instance.ShowRange(this.gameObject.GetComponent<FichaInfo>().getCords(),
                     this.gameObject.GetComponent<FichaInfo>().getRange());
                }
            }  
        }

        if(moving) {
            this.transform.position = Vector3.Lerp(this.transform.position, newPos, 0.5f);
            if(this.transform.position == newPos) moving = false;
        }
    }
}
