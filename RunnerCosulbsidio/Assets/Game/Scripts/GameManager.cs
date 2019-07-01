using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameState currentState = GameState.MainScreen;
    private bool bCanAdvanceScreen = true;
    public static GameManager Instance;

    public EasyTween mainScreen;
    public EasyTween instruccions;
    public EasyTween mapGame;
    public EasyTween thanksForPlaying;
    int count = 0;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }


    void Start()
    {
        mainScreen.OpenCloseObjectAnimation();
        currentState = GameState.MainScreen;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.S) && bCanAdvanceScreen)
        {

            switch (currentState)
            {
                case GameState.MainScreen:
                    Instruccions();
                    currentState = GameState.Instruccions;

                    break;
                case GameState.Instruccions:
                    MapGame();
                    currentState = GameState.MapGame;

                    break;
                case GameState.MapGame:
                    ThanksForPlaying();
                    
                   
                    break;

                case GameState.ThanksForPlaying:
                    MainScreen();
                    currentState = GameState.MainScreen;
                    break;
            }

        }
    }


    public void MainScreen()
    {
        if (bCanAdvanceScreen == false)
            return;
        bCanAdvanceScreen = false;
        Invoke("AllowAdvanceScreen", 3.0f);
        thanksForPlaying.OpenCloseObjectAnimation();
        mainScreen.OpenCloseObjectAnimation();
    }

    public void Instruccions()
    {
        if (bCanAdvanceScreen == false)
            return;

        RaceGameManager.Instance.ResetAvatarProgress();
        bCanAdvanceScreen = false;
        Invoke("AllowAdvanceScreen", 3.0f);
        mainScreen.OpenCloseObjectAnimation();
        instruccions.OpenCloseObjectAnimation();
    }
    public void MapGame()
    {
        if (bCanAdvanceScreen == false)
            return;

        bCanAdvanceScreen = false;
        Invoke("AllowAdvanceScreen", 3.0f);
        instruccions.OpenCloseObjectAnimation();
        mapGame.OpenCloseObjectAnimation();
        RaceGameManager.Instance.OnGameStarted();
    }
    public void ThanksForPlaying()
    {
        if (bCanAdvanceScreen == false)
            return;

        bCanAdvanceScreen = false;
        Invoke("AllowAdvanceScreen", 3.0f);
        mapGame.OpenCloseObjectAnimation();
        thanksForPlaying.OpenCloseObjectAnimation();
        currentState = GameState.ThanksForPlaying;
    }

    public  void RaceFinished()
    {

        ThanksForPlaying();


    }



    private void AllowAdvanceScreen()
    {
        bCanAdvanceScreen = true;
    }

}











public enum GameState
{
    MainScreen,
    Instruccions,
    MapGame,
    ThanksForPlaying,

}
