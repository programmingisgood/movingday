using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DN_MenuMannager : MonoBehaviour
{
    public GameObject FirstMenuPage;
    public GameObject CreditsPage;
    public GameObject PauseMenu;
    private bool MainMenu;
    private bool CreatePauseScene;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu = true;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && MainMenu == false)
            {
            CreatePauseScene = !CreatePauseScene;
        }
        if(MainMenu == false && CreatePauseScene)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
        if(MainMenu == false && CreatePauseScene == false)
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }
    }
    public void StartGame()
    {
        CreatePauseScene = false;
        PauseMenu.SetActive(false);
        SceneManager.LoadScene(1);
        MainMenu = false;
    }
    public void Credits()
    {
        CreditsPage.SetActive(true);
        FirstMenuPage.SetActive(false);
    }
    public void back()
    {
        CreditsPage.SetActive(false);
        FirstMenuPage.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void QuitToDesktop()
    {
        SceneManager.LoadScene(0);
        CreatePauseScene = false;
        PauseMenu.SetActive(false);
        MainMenu = true;
    }
    public void ResetScene()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();
        string sceneName = CurrentScene.name;
        if (sceneName == "Main")
        {
            CreatePauseScene = false;
            SceneManager.LoadScene("Main");
        }
        Time.timeScale = 1;
        
    }
    public void Resume()
    {
        Time.timeScale = 1;
        CreatePauseScene = false;
        
    }

}
