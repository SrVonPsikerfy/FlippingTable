using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using flagType = GameManager.flagCell;
public class Flag : MonoBehaviour
{ 
    flagType flag = flagType.None;

    public void setType(flagType f){
        flag = f;
    }
    public flagType getType(){
        return flag;
    }
}
