using UnityEngine;
using System.Collections;
using System;
using System.IO.Ports;
using System.Threading;

public class SerialHandler: MonoBehaviour {

	SerialPort port;
	Thread portReadingThread;
	public string comPort = "COM19";
	public const int baudRate = 9600;
	string strData;
	bool looping;

	[HideInInspector] public float x, y;
	[HideInInspector] public Vector2 myPamOrigin, myPamPosition, myPamInput;
	const int radius = 200;

	private void OnEnable()
	{
	 	looping = true;
		portReadingThread = new Thread(ReadPort);
		portReadingThread.Start();
		myPamOrigin.x = - 156;
		myPamOrigin.y = - 157;
	}

	private void OnDestroy()
	{
	 	looping = false;  // stop the thread when the game stops                        
		portReadingThread.Join();
		portReadingThread.Abort();
		port.Close();
	}

	public float Remap (float value, float from1, float to1, float from2, float to2) {
    	return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	void ReadPort()
	{        
		// for any COM number larger than 9, you should add prefix of \\\\.\\ to it. 
		// for example for COM15, you should write it as "\\\\.\\COM15" instead of "COM15".
		port = new SerialPort(comPort, baudRate);

		if (port == null)
		{
			Debug.LogError("port is null");
			return;
		}

		port.Handshake = Handshake.None;
		port.DtrEnable = true;
		//myPort.RtsEnable = true;
		port.ReadTimeout = 500; // NOTE: Don't Reduce it or the communication might break!
		port.WriteTimeout = 1000;
		port.Parity = Parity.None;
		port.StopBits = StopBits.One;
		port.DataBits = 8;
		port.NewLine = "\n";

		port.Open();
		if (!port.IsOpen)
			print("Port has not been open!");
		// send "START" command to the port
		port.WriteLine("Start");
		
		// start reading the data coming through the serial port
		while (looping)
		{
			strData = port.ReadLine(); // blocking call

        	string[] coordinates = strData.Split(','); // Separate values

        	for(int i = 0; i < 2; i++){
				if(coordinates[i] != "") //Check if all values are recieved
				{
					x = float.Parse(coordinates[i++]);
					y = float.Parse(coordinates[i++]);

					myPamPosition.x = x;
					myPamPosition.y = y;

					// Debug.Log(myPamPosition);

					myPamInput.x = Remap(	myPamPosition.x,
											(myPamOrigin.x - radius),
											(myPamOrigin.x + radius),
											-1,
											1
					);

					myPamInput.y = Remap(	myPamPosition.y,
											(myPamOrigin.y - radius),
											(myPamOrigin.y + radius),
											-1,
											1
					);

					// Debug.Log(myPamInput);
				}
        	}
			
			Thread.Sleep(0);
		}
	}
}

