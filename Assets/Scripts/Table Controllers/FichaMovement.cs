using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaMovement : MonoBehaviour
{
    public bool selected = false, moving = false;

    // float altDif = 0.5f;
    float defaultPos = 0.9f;
    float vallePos = 0.4f;
    float colinaPos = 1.4f;

    Vector3 newPos = new Vector3();

    FichaInfo info;

    #region Atributos
    TokenBehaviour behavior;
    Terraformer terraformer;
    #endregion

    // Start is called before the first frame update
    void Start(){
        info = this.gameObject.GetComponent<FichaInfo>();

        behavior = GetComponent<TokenBehaviour>();
        terraformer = GetComponent<Terraformer>();
        newPos = this.transform.position;
    }

    // Update is called once per frame
    void Update(){

        if (Input.GetKeyDown(KeyCode.M)){
            FichaInfo f = this.gameObject.GetComponent<FichaInfo>();

            if (f != null) f.die();
        }

        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.transform == null) return;

            CasillaInfo cell_info = hit.transform.gameObject.GetComponent<CasillaInfo>();

            FichaInfo token_info = hit.transform.gameObject.GetComponent<FichaInfo>();

            if (hit.transform.gameObject == this.gameObject && !selected){
                selected = true;
                GameManager.instance.tokenSelected(this.gameObject);
            }
            else if (cell_info != null && selected && behavior != null && behavior.getMove()) {
                Vector2 dist = info.getCords() - cell_info.getCords();

                if (info.getMovement() >= Mathf.Abs(dist.x) && info.getMovement() >= Mathf.Abs(dist.y)) {
                    newPos = hit.transform.position;
                    
                    if (cell_info.getAltura() == GameManager.alturas.colina) newPos.y = colinaPos;
                    else if (cell_info.getAltura() == GameManager.alturas.valle) newPos.y = vallePos;
                    else newPos.y = defaultPos;

                    GameManager.instance.SetFicha((int)cell_info.getCords().x, (int)cell_info.getCords().y, hit.transform.gameObject);
                    info.setCords(cell_info.getCords());

                    moving = true;
                }
                else{
                    selected = false;
                    GameManager.instance.hideRange();
                    if( behavior!= null)  behavior.setMove(false);
                }
            }
        }

        if (moving &&  behavior != null &&  behavior.getMove()) {
            this.transform.position = Vector3.Lerp(this.transform.position, newPos, 0.5f);
            if (this.transform.position == newPos) moving = false;

            GameManager.instance.hideRange();
            GameManager.instance.ShowRange(info.getCords(), info.getRange());
        }
        //Obtencion de la bandera
        GameObject cell = GameManager.getCell((int)this.gameObject.GetComponent<FichaInfo>().getCords().y, (int)this.gameObject.GetComponent<FichaInfo>().getCords().x);
        if(this.gameObject.GetComponent<Flag>() == null && cell.GetComponent<CasillaInfo>().getFlag() != GameManager.flagCell.None){
            if(cell.GetComponent<CasillaInfo>().getFlag() != GameManager.flagCell.player2 && this.gameObject.GetComponent<FichaInfo>().getSide()){
                GameManager.pickFlag(this.gameObject, cell);
            }
            else if(cell.GetComponent<CasillaInfo>().getFlag() != GameManager.flagCell.player1 && !this.gameObject.GetComponent<FichaInfo>().getSide()){
                GameManager.pickFlag(this.gameObject, cell);
            }
        }
    }
    void LateUpdate()
    {
        GameObject cell = GameManager.getCell((int)this.gameObject.GetComponent<FichaInfo>().getCords().y, (int)this.gameObject.GetComponent<FichaInfo>().getCords().x);
        newPos = new Vector3(newPos.x, 0.9f + 0.5f * (int)cell.GetComponent<CasillaInfo>().getAltura(),newPos.z);
        this.transform.position = Vector3.Lerp(this.transform.position, newPos, Time.deltaTime * 50);
    }

}


