using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour
{
    public List<Button> buttonList = new List<Button>();
    public void boxLines(Button _button){
        buttonList.Add(_button);
    }
    public void ButtonSurrounded(bool _fpTurn){
        Debug.Log("Hey!!!");
        gameObject.tag = "checked";
        if(_fpTurn){

            gameObject.GetComponent<Image>().color = Color.blue;
        }
        else if(!_fpTurn){

            gameObject.GetComponent<Image>().color = Color.red;
        }
    }
}
