using System;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public GameObject test;

    private void Pause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        test.SetActive(false);
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Debug.Log("Resuming");
        Time.timeScale = 1f;
        test.SetActive(true);
    }

    private void Update()
    {
        Pause();
    }
}
