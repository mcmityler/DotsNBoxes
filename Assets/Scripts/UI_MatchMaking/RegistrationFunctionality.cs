using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegistrationFunctionality : MonoBehaviour
{
    [SerializeField] GameObject usernameInput;
    [SerializeField] GameObject passwordInput;
    [SerializeField] GameObject fieldEmptyWarning;
    [SerializeField] GameObject loginTab;
    [SerializeField] GameObject registeredWarning;
    [SerializeField] GameObject registerButton;

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


        if (Input.GetKeyDown(KeyCode.Tab)) //Tabbing between fields
        {
            if (usernameInput.GetComponent<InputField>().isFocused)
                passwordInput.GetComponent<InputField>().Select();
            else if (passwordInput.GetComponent<InputField>().isFocused)
                usernameInput.GetComponent<InputField>().Select();
        }

        if (Input.GetKeyDown(KeyCode.Return))
            AttemptRegistration();
    }

    public void AttemptRegistration()
    {
        if (usernameInput.GetComponent<InputField>().textComponent.text == "" ||    // Display warning if field empty
            passwordInput.GetComponent<InputField>().textComponent.text == ""){
            fieldEmptyWarning.SetActive(true);
            GameObject.FindObjectOfType<AudioManager>().Play("errorClick");
        }
        else
        {
            registerButton.SetActive(false);
            registeredWarning.SetActive(true);
            GameObject.FindObjectOfType<AudioManager>().Play("click");
        }

    }
}
