using System;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private GameObject[] objects;
    public GameObject test;

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
        Time.timeScale = 0f;
        test.SetActive(false);
    }
    
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameObject.FindWithTag("PauseObject").SetActive(false);
        Debug.Log("Resuming");
        Time.timeScale = 1f;
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
        test.SetActive(true);
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
