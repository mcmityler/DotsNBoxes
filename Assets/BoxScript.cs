using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour
{

    private bool alreadyCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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






    //public void CheckIfSurrounded(){
    ///    if(!alreadyCollected){
    //        if(upButton.GetComponent<ButtonScript>().clicked && downButton.GetComponent<ButtonScript>().clicked && leftButton.GetComponent<ButtonScript>().clicked && rightButton.GetComponent<ButtonScript>().clicked){
    //            Debug.Log("Score");
     //           alreadyCollected = true;
     //       }
    //    }
   // }
}
