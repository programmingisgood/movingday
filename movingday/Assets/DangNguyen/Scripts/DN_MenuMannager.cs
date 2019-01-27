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
    public bool TestScene;
    public static DN_MenuMannager MenuInstance;
    public static bool Restart;
    public GameObject CreditCamera;
    public GameObject MainMenuCamera;
    // Start is called before the first frame update
    private void Awake()
    {
        if (MenuInstance == null)
        {
            MenuInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        if (TestScene)
        {
                FirstPrompt = true;
            TimerStart = true;
            TimePage.SetActive(true);
            CreatePauseScene = false;
            PauseMenu.SetActive(false);
            MainMenu = false;
        }
    }
    void Start()
    {
        TextScripts = TimeText.GetComponent<Text>();
        VicotryText = VictoryTextObj.GetComponent<Text>();
        MainMenu = true;
        DontDestroyOnLoad(this.gameObject);
        Timer = StarterTimer;
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if(TimerStart)
        {
            Timer -= Time.deltaTime;

            TextScripts.text = Timer.ToString() ;
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
        Score = 0;
    }
    public void Credits()
    {
        CreditsPage.SetActive(true);
        FirstMenuPage.SetActive(false);
        MainMenuCamera.SetActive(false);
        CreditCamera.SetActive(true);
    }
    public void back()
    {
        CreditsPage.SetActive(false);
        FirstMenuPage.SetActive(true);
        MainMenuCamera.SetActive(true);
        CreditCamera.SetActive(false);
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
        Restart = false;
        Score = 0;
    }
    public void ResetScene()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();
        string sceneName = CurrentScene.name;
        EndingCamera.SetActive(false);
        Score = 0;
        Time.timeScale = 1;
        AfterMathTimer = 1;
        Timer = StarterTimer;
        VictotryScreen.SetActive(false);
        StopShowScoring = false;
        FirstPrompt = false;
        Restart = true;
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
        if (sceneName == "ThomasScene_02")
        {
            CreatePauseScene = false;
            SceneManager.LoadScene("ThomasScene_02");
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        CreatePauseScene = false;
        
    }
    public void NextScene()
    {
        Score = 0;
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
        AfterMathTimer = 1;
        Timer = StarterTimer;
        VictotryScreen.SetActive(false);
        StopShowScoring = false;
        EndingCamera.SetActive(false);
    }

}
