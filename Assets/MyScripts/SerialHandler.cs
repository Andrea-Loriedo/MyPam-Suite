using UnityEngine;
using System.Collections;
using System;
using System.IO.Ports;
using System.Threading;

public class SerialHandler: MonoBehaviour {

	private bool _looping;
	private SerialPort _port;
	public string _comPort = "COM19";
	private const int _baudRate = 9600;
	private Thread portReadingThread;
	private string strData;
	[HideInInspector] public float x, y;

	private void OnEnable()
	{
		_looping = true;
		portReadingThread = new Thread(ReadArduino);
		portReadingThread.Start();
	}

	private void OnDestroy()
	{
		_looping = false;  // This is a necessary command to stop the thread.
						// if you comment this line, Unity gets frozen when you stop the game in the editor.                           
		portReadingThread.Join();
		portReadingThread.Abort();
		_port.Close();
	}

	void ReadArduino()
	{        
		// For any COM number larger than 9, you should add prefix of \\\\.\\ to it. 
		// For example for COM15, you should write it as "\\\\.\\COM15" instead of "COM15".
		_port = new SerialPort(_comPort, _baudRate);
		if (_port == null)
		{
			Debug.LogError("_port is null");
			return;
		}

		_port.Handshake = Handshake.None;
		_port.DtrEnable = true;
		//myPort.RtsEnable = true;
		_port.ReadTimeout = 500; // NOTE: Don't Reduce it or the communication might break!
		_port.WriteTimeout = 1000;
		_port.Parity = Parity.None;
		_port.StopBits = StopBits.One;
		_port.DataBits = 8;
		_port.NewLine = "\n";

		_port.Open();
		if (!_port.IsOpen)
			print("PORT HAS NOT BEEN OPEN!");
		// Send "START" command to the arduino.
		_port.WriteLine("START");
		// Start reading the data coming through the serial port.

		while (_looping)
		{
			strData = _port.ReadLine(); // blocking call.
			print(strData);

        	string[] armCoordinates = strData.Split(','); // Separate values

        	for(int i = 0; i < 2; i++){
				if(armCoordinates[i] != "") //Check if all values are recieved
				{
					x = float.Parse(armCoordinates[i++]);
					y = float.Parse(armCoordinates[i++]);
				}
        	}

			Thread.Sleep(0);
		}
	}
}

