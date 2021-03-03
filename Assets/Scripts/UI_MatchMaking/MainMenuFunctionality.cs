using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunctionality : MonoBehaviour
{
    [SerializeField] GameObject profileTab;
    [SerializeField] GameObject mainMenuTab;
    [SerializeField] GameObject matchmakingTab;
    [SerializeField] GameObject loginTab;
    [SerializeField] GameObject registrationTab;


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
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void JoinMatchmaking()
    {
        matchmakingTab.SetActive(true);
        mainMenuTab.SetActive(false);
    }

    public void ViewProfile()
    {
        profileTab.SetActive(true);
        mainMenuTab.SetActive(false);
    }

    public void BackFromProfile()
    {
        profileTab.SetActive(false);
        mainMenuTab.SetActive(true);
    }

    public void BackFromMatchmaking()
    {
        matchmakingTab.SetActive(false);
        mainMenuTab.SetActive(true);
    }
    public void BackFromRegistration()
    {
        registrationTab.SetActive(false);
        loginTab.SetActive(true);
    }

    public void CreateAccount()
    {
        loginTab.SetActive(false);
        registrationTab.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
