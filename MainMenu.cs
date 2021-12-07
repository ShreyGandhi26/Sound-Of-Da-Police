using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*private void Start()
    {
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    */

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void Level5()
    {
        SceneManager.LoadScene("Level5");
    }

    public void QuitGame()

    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
