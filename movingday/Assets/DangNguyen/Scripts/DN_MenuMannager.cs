using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DN_MenuMannager : MonoBehaviour
{
    public GameObject FirstMenuPage;
    public GameObject CreditsPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
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

}
