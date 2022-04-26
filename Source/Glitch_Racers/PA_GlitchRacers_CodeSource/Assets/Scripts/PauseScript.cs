using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    //Objects to show when pausing the game
    private GameObject[] objects;

    private void Pause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        Cursor.lockState = CursorLockMode.None;
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameObject.FindWithTag("PauseObject").SetActive(false);
        Time.timeScale = 1f;
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    private void Start()
    {
        objects = GameObject.FindGameObjectsWithTag("PauseObject");
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        Pause();
    }
}
