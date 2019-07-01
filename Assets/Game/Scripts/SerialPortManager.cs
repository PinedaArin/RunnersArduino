using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
public class SerialPortManager : MonoBehaviour {

    public static SerialPortManager Instance;

    SerialPort serialPort = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.One);

    public int stepPlayer_1;
    public int stepPlayer_2;
    public int stepPlayer_3;

    private bool bCanReadP1 = true;
    private bool bCanReadP2 = true;
    private bool bCanReadP3 = true;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }


    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 100;

    }

    void Update()
    {
        try
        {
            if (serialPort.IsOpen)
            {                        
               serialPort.DiscardInBuffer();

                if (serialPort.ReadLine().Equals("1") || serialPort.ReadLine().Equals("2") && bCanReadP1)
                {
                    stepPlayer_1 = 1;
                    bCanReadP1 = false;
                    Invoke("EnableP1Input", 0.5f);
                }
                else
                {
                    stepPlayer_1 = 0;
                }
                if (serialPort.ReadLine().Equals("4") || serialPort.ReadLine().Equals("8") && bCanReadP2)
                {
                    stepPlayer_2 = 1;
                    Invoke("EnableP2Input",0.5f);

                }
                else
                {
                    stepPlayer_2 = 0;
                }
                if (serialPort.ReadLine().Equals("16") || serialPort.ReadLine().Equals("32") && bCanReadP3)
                {
                    stepPlayer_3 = 1;
                    Invoke("EnableP3Input",0.5f);

                }
                else
                {
                    stepPlayer_3 = 0;
                }        
            }
        }     
        catch (System.Exception ex)
        {
            ex = new System.Exception();
        }
    }

    public void EnableP1Input()
    {
        bCanReadP1 = true;
    }
    public void EnableP2Input()
    {
        bCanReadP2 = true;
    }
    public void EnableP3Input()
    {
        bCanReadP3 = true;
    }
}
