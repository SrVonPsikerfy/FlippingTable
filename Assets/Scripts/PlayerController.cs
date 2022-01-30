using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Heights { Valley, Plane, Mountain }

public class PlayerController : MonoBehaviour
{
    public GameObject melee, ranged, tank, engi;

    public bool p1;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && myTurn())
        {   
            GameObject casillaAux = GameManager.getCell(0, 1);
            this.gameObject.transform.position = new Vector3(casillaAux.transform.position.x,
            this.transform.position.y, casillaAux.transform.position.z);

            Vector3 pos = casillaAux.transform.position;

            //Hay que hacer una variable con las alturas
            GameManager.alturas alt = casillaAux.GetComponent<CasillaInfo>().getAltura();
            switch (alt)
            {
                case GameManager.alturas.valle: pos.y = 0.4f; break;
                case GameManager.alturas.llano: pos.y = 0.9f; break;
                case GameManager.alturas.colina: pos.y = 1.4f; break;
            }

            Debug.Log("Es el player 1:" + p1);
            if (Input.GetKeyDown(KeyCode.A))
            {
                GameObject g = Instantiate(melee, pos, Quaternion.identity);
                FichaInfo f = g.GetComponent<FichaInfo>();

                if (f != null) f.setInfo(new Vector2(0, 1), GameManager.fichas.melee);

                if(GameManager.instance.getTurn() == 1)GameManager.instance.addFicha(new Vector2(0, 1), g);
                else GameManager.instance.addFichaPlayer2(new Vector2(0, 1), g);

                f.setStats(1, 1, 1, 2, p1);

                GameManager.instance.changePlayer();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                GameObject g = Instantiate(ranged, pos, Quaternion.identity);

                FichaInfo f = g.GetComponent<FichaInfo>();

                if (f != null)
                    f.setInfo(new Vector2(0, 1), GameManager.fichas.ranged);

                if (GameManager.instance.getTurn() == 1) GameManager.instance.addFicha(new Vector2(0, 1), g);
                else GameManager.instance.addFichaPlayer2(new Vector2(0, 1), g);

                f.setStats(2, 1, 2, 1, p1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                GameObject g = Instantiate(tank, pos, Quaternion.identity);

                FichaInfo f = g.GetComponent<FichaInfo>();

                if (f != null)
                    f.setInfo(new Vector2(0, 1), GameManager.fichas.tank);

                if (GameManager.instance.getTurn() == 1) GameManager.instance.addFicha(new Vector2(0, 1), g);
                else GameManager.instance.addFichaPlayer2(new Vector2(0, 1), g);

                f.setStats(1, 1, 1, 3, p1);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject g = Instantiate(engi, pos, Quaternion.identity);

                FichaInfo f = g.GetComponent<FichaInfo>();

                if (f != null)
                    f.setInfo(new Vector2(0, 1), GameManager.fichas.engineer);

                if (GameManager.instance.getTurn() == 1) GameManager.instance.addFicha(new Vector2(0, 1), g);
                else GameManager.instance.addFichaPlayer2(new Vector2(0, 1), g);

                f.setStats(1, 1, 1, 1, p1);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                GameManager.instance.debug();
            }
        }

    }

    private bool myTurn()
    {  
        return (p1 && GameManager.instance.getTurn() == 1) || (!p1 && GameManager.instance.getTurn() == 2);
    }
}
