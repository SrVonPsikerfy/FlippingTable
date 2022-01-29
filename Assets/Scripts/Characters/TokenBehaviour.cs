using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenBehaviour : MonoBehaviour {
    // Start is called before the first frame update
    public virtual void Move(){
        Debug.Log("Move Base");
    } 

    public virtual void Attack(){
        Debug.Log("Attack Base");
    }
}
