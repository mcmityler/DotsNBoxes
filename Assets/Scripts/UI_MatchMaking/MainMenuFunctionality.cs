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

    private AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        audioManager =  GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        audioManager.Play("click");
    }

    public void JoinMatchmaking()
    {
        audioManager.Play("click");
        matchmakingTab.SetActive(true);
        mainMenuTab.SetActive(false);
    }

    public void ViewProfile()
    {
        audioManager.Play("click");
        profileTab.SetActive(true);
        mainMenuTab.SetActive(false);
    }

    public void BackFromProfile()
    {
        audioManager.Play("click");
        profileTab.SetActive(false);
        mainMenuTab.SetActive(true);
    }

    public void BackFromMatchmaking()
    {
        audioManager.Play("click");
        matchmakingTab.SetActive(false);
        mainMenuTab.SetActive(true);
    }
    public void BackFromRegistration()
    {
        audioManager.Play("click");
        registrationTab.SetActive(false);
        loginTab.SetActive(true);
    }

    public void CreateAccount()
    {
        audioManager.Play("click");
        loginTab.SetActive(false);
        registrationTab.SetActive(true);
    }

    public void QuitGame()
    {
        audioManager.Play("click");
        Application.Quit();
    }
}
