using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginTabFunctionality : MonoBehaviour
{
    [SerializeField] GameObject usernameInput;
    [SerializeField] GameObject passwordInput;
    [SerializeField] GameObject fieldEmptyWarning;
    [SerializeField] GameObject mainMenuTab;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
     

        if (Input.GetKeyDown(KeyCode.Tab))    //Tabbing between fields
        {
            if (usernameInput.GetComponent<InputField>().isFocused)
                passwordInput.GetComponent<InputField>().Select();
            else if (passwordInput.GetComponent<InputField>().isFocused)
                usernameInput.GetComponent<InputField>().Select();
        }

        if (Input.GetKeyDown(KeyCode.Return))
            AttemptLogin();
    }

    public void AttemptLogin()
    {
        if (usernameInput.GetComponent<InputField>().textComponent.text == "" ||           // Display warning if field empty
            passwordInput.GetComponent<InputField>().textComponent.text == "")
            fieldEmptyWarning.SetActive(true);
        else
        {
            mainMenuTab.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }


}
