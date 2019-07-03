using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    public static PlayerInputManager Instance;

    [SerializeField]
    private PlayerInputData[] Players;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        int inputInt = SerialPortManager.Instance.inputInt;

        string reverseText = ReverseText(Binario(inputInt));
        string sortString = reverseText;


        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] =  ReadPlayerInput(Players[i], sortString);
        }
        
    }

    private PlayerInputData ReadPlayerInput(PlayerInputData player, string sortString)
    {
        Char[] serialInput = sortString.ToCharArray();
        
        int LeftIndex = (player.PlayerNumber - 1) * 2;
        int RightIndex = LeftIndex + 1;

        if (serialInput.Length >= (2 * player.PlayerNumber) && (serialInput[LeftIndex] == '1' && serialInput[RightIndex] == '1'))
        {
            //If the player press both platforms, do nothing!!
        }
        else if (player.bLeftStep && serialInput.Length > LeftIndex && serialInput[LeftIndex] == '1')
        {
            player.playerInput = 1;
            player.bLeftStep = false;
            Debug.Log("Left Step");
        }
        else if (!player.bLeftStep && serialInput.Length > RightIndex && serialInput[RightIndex] == '1')
        {
            player.playerInput = 1;
            player.bLeftStep = true;
            Debug.Log("Rigth Step");
        }
        else
        {
            player.playerInput = 0;
        }

        return player;
    }

    public string Binario(int n)
    {
        string s = "";
        while (n > 0)
        {
            s = (n % 2 == 1) ? s.Insert(0, "1") : s.Insert(0, "0");
            n /= 2;
        }
        return s;
    }

    public static string ReverseText(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public int GetPlayerInput (int playerNumber)
    {
        if(Players.Length >= playerNumber)
        {
            return Players[playerNumber - 1].playerInput;
        }

        return 0;
    }
}

[System.Serializable]
public struct PlayerInputData
{
    public int PlayerNumber;
    public bool bLeftStep;
    public int playerInput;
}
