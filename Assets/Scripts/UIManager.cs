using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public static UIManager instance;

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
        
    }

    public void changeId(string id){
        idText.text = id;
    }
}