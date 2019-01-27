using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DN_MenuMannager : MonoBehaviour
{
    public GameObject TimeText;
    private Text TextScripts;
    public GameObject FirstMenuPage;
    public GameObject CreditsPage;
    public GameObject PauseMenu;
    public GameObject TimePage;
    private bool MainMenu;
    private bool CreatePauseScene;
    public bool TimerStart;
    public float Timer;
    public float StarterTimer;
    public bool FirstPrompt;
    public GameObject Prompt;
    private Text VicotryText;
    public GameObject VictoryTextObj;
    public GameObject VictotryScreen;
    public bool StopShowScoring;
    public GameObject EndingCamera;
    public float AfterMathTimer;
    public float Score;
    // Start is called before the first frame update
    void Start()
    {
        TextScripts = TimeText.GetComponent<Text>();
        VicotryText = VictoryTextObj.GetComponent<Text>();
        MainMenu = true;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerStart)
        {
            Timer -= Time.deltaTime;
            TextScripts.text = Timer.ToString();
        }
        if(AfterMathTimer <=0)
        {
            
            if (!StopShowScoring)
            {
                VictotryScreen.SetActive(true);
                FindObjectOfType<ScoringIndicators>().ShowScoring();
                VicotryText.text = "Your score is " + Score.ToString();
                StopShowScoring = true;
            }
        }
        if(Timer <= 0)
        {
            AfterMathTimer -= Time.deltaTime;
            EndingCamera.SetActive(true);
            Timer = 0;
        }
     
        if(FirstPrompt)
        {
            Prompt.SetActive(true);
        }
        else
        {
            Prompt.SetActive(false);
        }
        
       
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
        FirstPrompt = true;
        TimerStart = true;
        TimePage.SetActive(true);
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
        EndingCamera.SetActive(false);
        if (sceneName == "DangTest")
        {
            CreatePauseScene = false;
            SceneManager.LoadScene("DangTest");
        }
        if (sceneName == "Main")
        {
            CreatePauseScene = false;
            SceneManager.LoadScene("Main");
        }
        Time.timeScale = 1;
        AfterMathTimer = 1;
        Timer = StarterTimer;
        VictotryScreen.SetActive(false);
        StopShowScoring = false;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        CreatePauseScene = false;
        
    }

}
