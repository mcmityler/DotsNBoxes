using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private GameObject manager;
    public bool clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetButtonTrue(){
        clicked = true;
        manager.GetComponent<ScoreManager>().CheckScore();
    }
}
