using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaMovement : MonoBehaviour
{
    bool selected = false, moving = false;

    float altDif = 0.5f;
    float defaultPos = 0.9f;
    float vallePos = 0.4f;
    float colinaPos = 1.4f;

    Vector3 newPos = new Vector3();

    FichaInfo info = new FichaInfo();

    // Start is called before the first frame update
    void Start()
    {
        info = this.gameObject.GetComponent<FichaInfo>();
        newPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            FichaInfo f = this.gameObject.GetComponent<FichaInfo>();

            if (f != null)
                f.die();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            if (hit.transform == null) return;
            CasillaInfo infCas = hit.transform.gameObject.GetComponent<CasillaInfo>();

            FichaInfo infHit = hit.transform.gameObject.GetComponent<FichaInfo>();

            if (hit.transform.gameObject == this.gameObject && !selected)
            {

                //Vector2 dist = info.getCords() - infHit.getCords();

                selected = true;
                GameManager.instance.ShowRange(this.gameObject.GetComponent<FichaInfo>().getCords(),
                this.gameObject.GetComponent<FichaInfo>().getRange());

                //asies tengo retraso mental severo by Laura

                //para codigo limpito

                // if (infCas != null && selected)
                // {

                //     // Vector2 dist = infCas.getCords() - infGeneral.getCords();

                //     // if (infGeneral.getMovement() >= Mathf.Abs(dist.x) && infGeneral.getMovement() >= Mathf.Abs(dist.y))
                //     // {
                //     //     newPos = hit.transform.position;

                //     //     if (infCas.getAltura() == GameManager.alturas.colina) newPos.y = colinaPos;
                //     //     else if (infCas.getAltura() == GameManager.alturas.valle) newPos.y = vallePos;
                //     //     else newPos.y = defaultPos;

                //     //     this.transform.position = Vector3.Lerp(this.transform.position, newPos, 0.5f);
                //     //     //GameManager.instance.SetFicha((int)infCas.getCords().x,(int) infCas.getCords().y, hit.transform.gameObject);
                //     //     //Creo que esta aqui
                //     //     infGeneral.setCords(infCas.getCords());

                //     //     selected = false; moving = true;
                //     // }
                // }
                // else if (infHit != null)
                // {
                //     // selected = (infHit.getCords() == infGeneral.getCords());
                //     // Debug.Log(infHit.getCords());
                //     // Debug.Log(infGeneral.getCords());
                //     // Debug.Log(selected);
                //     // if (selected)
                //     // {
                //     //     GameManager.instance.ShowRange(this.gameObject.GetComponent<FichaInfo>().getCords(),
                //     //      this.gameObject.GetComponent<FichaInfo>().getRange());
                //     // }
                // }
                // else
                // {
                //     selected = false;
                // }
            }
            else if (infCas != null && selected)
            {
                Vector2 dist = info.getCords() - infCas.getCords();

                if (info.getMovement() >= Mathf.Abs(dist.x) && info.getMovement() >= Mathf.Abs(dist.y))
                {
                    newPos = hit.transform.position;
                    
                    Debug.Log(infCas.getAltura());
                    if (infCas.getAltura() == GameManager.alturas.colina) newPos.y = colinaPos;
                    else if (infCas.getAltura() == GameManager.alturas.valle) newPos.y = vallePos;
                    else newPos.y = defaultPos;

                    
                    GameManager.instance.SetFicha((int)infCas.getCords().x, (int)infCas.getCords().y, hit.transform.gameObject);
                    info.setCords(infCas.getCords());

                    selected = false; moving = true;
                }
            }
            else if (hit.transform.gameObject != this.gameObject && selected) 
            {
                selected = false;
                GameManager.instance.hideRange();
            }
            // else
            // {
            //     selected = false;
            // }

            if (selected){

                GameManager.instance.tokenSelected(this.gameObject);
                // GameManager.instance.ShowRange(this.gameObject.GetComponent<FichaInfo>().getCords(),
                //  this.gameObject.GetComponent<FichaInfo>().getRange());
            }
        }

        if (moving)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, newPos, 0.5f);
            if (this.transform.position == newPos) moving = false;

            GameManager.instance.hideRange();
        }
        //Obtencion de la bandera
        GameObject cell = GameManager.getCell((int)this.gameObject.GetComponent<FichaInfo>().getCords().y, (int)this.gameObject.GetComponent<FichaInfo>().getCords().x);
        if(this.gameObject.GetComponent<Flag>() == null && cell.GetComponent<CasillaInfo>().getFlag() != GameManager.flagCell.None){
            GameManager.pickFlag(this.gameObject, cell);
            Debug.Log(cell.GetComponent<CasillaInfo>().getFlag());
        }
    }
    void LateUpdate()
    {
        GameObject cell = GameManager.getCell((int)this.gameObject.GetComponent<FichaInfo>().getCords().y, (int)this.gameObject.GetComponent<FichaInfo>().getCords().x);
        newPos = new Vector3(newPos.x, 0.9f + 0.5f * (int)cell.GetComponent<CasillaInfo>().getAltura(),newPos.z);
        this.transform.position = Vector3.Lerp(this.transform.position, newPos, Time.deltaTime * 50);
    }

}


