using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System;

public class SerialPortManager : MonoBehaviour
{

    public static SerialPortManager Instance;

    SerialPort serialPort = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.One);

    public int inputInt;

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
                inputInt = int.Parse(serialPort.ReadLine());
                serialPort.DiscardInBuffer();
            }
        }
        catch (System.Exception ex)
        {
           Debug.LogError(ex.ToString());
            inputInt = 0;

        }
    }

}
