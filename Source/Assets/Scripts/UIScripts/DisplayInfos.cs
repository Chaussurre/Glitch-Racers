using System;
using TMPro;
using UnityEngine;

public class DisplayInfos : MonoBehaviour
{
    [SerializeField] private GameObject SingleLevel;
    [SerializeField] private GameObject GPLevel;
    public TextMeshPro a1;
    public TextMeshPro a2;
    public TextMeshPro a3;
    
    // Start is called before the first frame update
    private void Start()
    {
        switch (GameModeManager.CurrentLevelGamemode)
        {
            case GameModeManager.GameMode.SingleLevel:
                SingleLevel.SetActive(true);
                a1.text = $"Level :{LevelManager.GetLevelName()}";
                a2.text = $"Place : {LevelManager.GetPlace()}";
                a3.text = $"Time :{LevelManager.GetTime()}";
                break;
            case GameModeManager.GameMode.GrandPrix:
                GPLevel.SetActive(true);
                break;
        }

        /*GPManager.Level newLevel = new GPManager.Level
        {
            Place = 1,
            LevelName = "level1",
            Time = 12f
        };*/

        /*GPManager.grandPrix[0] = newLevel;

        a1.text = $"Level :{GPManager.grandPrix[0].LevelName}";
        a2.text = $"Place : {GPManager.grandPrix[0].Place}";
        a3.text = $"Time :{GPManager.grandPrix[0].Time}";*/
    }
}
