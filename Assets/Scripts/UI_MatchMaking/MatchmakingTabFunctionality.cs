using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchmakingTabFunctionality : MonoBehaviour
{
    [SerializeField] GameObject roomNumInput;
    [SerializeField] GameObject fieldEmptyWarning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            AttemptJoinRoom();
    }

    public void AttemptJoinRoom()
    {
        if (roomNumInput.GetComponent<InputField>().textComponent.text == ""){
            fieldEmptyWarning.SetActive(true);
            GameObject.FindObjectOfType<AudioManager>().Play("errorClick");
        }
        else
        {
            GameObject.FindObjectOfType<AudioManager>().Play("click");
            SceneManager.LoadScene("GameScene");
        }
        

    }
}
