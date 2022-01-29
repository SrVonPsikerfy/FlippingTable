using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fichaMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
                Debug.Log("uwu");

                GameObject casillaAux = GameManager.getCell(0,1);
                Debug.Log(casillaAux);
                this.gameObject.transform.position = new Vector3(casillaAux.transform.position.x,
                this.transform.position.y,casillaAux.transform.position.z);
                
        }

          if (Input.GetMouseButtonDown(0))
        {
  	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   	        RaycastHit hit;
   	        // Casts the ray and get the first game object hit
   	        Physics.Raycast(ray, out hit);
            Debug.Log(hit.transform);

   	        Debug.Log("This hit at " + hit.transform.gameObject.name);

            if(hit.transform.gameObject.GetComponent<casillaInfo>() != null){

                Vector3 newPos = hit.transform.position;

                newPos.y = 1.53f;

                this.transform.position = newPos;
            }   
        }
    }

    void move(Vector2 dir){
        
    }
}
