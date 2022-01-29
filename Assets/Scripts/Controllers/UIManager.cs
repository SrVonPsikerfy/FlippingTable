using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public static UIManager instance;

    public GameObject actions;

	[Header("Id")]
    public Text idText;
	
    void Start(){
        GameManager.instance.SetUIManager(this);
        GameManager.instance.changePlayer();
    }

    void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) {
            GameManager.instance.changePlayer();
            Camera.main.GetComponent<RotationCamera>().flip();
        }
    }

    public void changeId(string id){
        idText.text = id;
    }

    public void displayActions(GameObject token){
        Terraformer isEngineer = token.GetComponent<Terraformer>();

        TokenBehaviour behavior = token.GetComponent<TokenBehaviour>();
        
        actions.SetActive(true);

        Transform tr = actions.transform;
        Button btn; 
        for(int i = 0; i < tr.childCount; i++){
            GameObject button = tr.GetChild(i).gameObject;
            btn = button.GetComponent<Button>();

            if(button.name == "Move"){
                btn.onClick.AddListener(behavior.Move);    
                button.SetActive(true);
            }
            else if(button.name == "Attack"){
                btn.onClick.AddListener(behavior.Attack);    
                button.SetActive(true);
            }
            else if(button.name == "Edit")
                if(isEngineer != null) {
                    btn.onClick.AddListener(isEngineer.Edit);    
                    button.SetActive(true);
                }
                else continue;

            // button.SetActive(true);
        }
    }
}