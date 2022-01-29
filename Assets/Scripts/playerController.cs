using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject ficha1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){


            GameObject casillaAux = GameManager.getCell(0,1);
            this.gameObject.transform.position = new Vector3(casillaAux.transform.position.x,
            this.transform.position.y,casillaAux.transform.position.z);

            Vector3 pos = casillaAux.transform.position;

            //Hay que hacer una variable con las alturas
            pos.y = 1.53f;

            GameObject g = Instantiate(ficha1, pos, Quaternion.identity);

            FichaInfo f = g.GetComponent<FichaInfo>();

            if(f != null){
                f.setInfo(new Vector2(0,1), GameManager.fichas.none);
            }    
        }
    }
}
