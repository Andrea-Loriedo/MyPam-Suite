using UnityEngine;
using System.Collections;
using System;
using System.IO.Ports;
using System.Threading;

public class SerialHandler: MonoBehaviour {

	private bool looping;
	private SerialPort port;
	public string comPort = "COM19";
	private const int baudRate = 9600;
	private Thread portReadingThread;
	private string strData;

	[HideInInspector] public float x, y;
	[HideInInspector] public Vector2 MyPAMOrigin, MyPAMPosition, BallInputPosition;
	public Vector2 myPamInput;

	private void OnEnable()
	{
	 	looping = true;
		portReadingThread = new Thread(ReadPort);
		portReadingThread.Start();
	}

	private void OnDestroy()
	{
	 	looping = false;  // this is a necessary command to stop the thread.
						// if you comment this line, Unity gets frozen when you stop the game in the editor.                           
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
			// origin at -180,-160
			strData = port.ReadLine(); // blocking call

			MyPAMOrigin.x = - 156;
			MyPAMOrigin.y = - 157;
			int radius = 200;

        	string[] armCoordinates = strData.Split(','); // Separate values

        	for(int i = 0; i < 2; i++){
				if(armCoordinates[i] != "") //Check if all values are recieved
				{
					x = float.Parse(armCoordinates[i++]);
					y = float.Parse(armCoordinates[i++]);

					MyPAMPosition.x = x;
					MyPAMPosition.y = y;

					// Debug.Log(MyPAMPosition);

					BallInputPosition = (MyPAMPosition - MyPAMOrigin);

					myPamInput.x = Remap(	MyPAMPosition.x,
											(MyPAMOrigin.x - radius),
											(MyPAMOrigin.x + radius),
											-1,
											1
					);

					myPamInput.y = Remap(	MyPAMPosition.y,
											(MyPAMOrigin.y - radius),
											(MyPAMOrigin.y + radius),
											-1,
											1
					);

					// Debug.Log(myPamInput);

					// Debug.Log(BallInputPosition.ToString());
				}
        	}
			
			Thread.Sleep(0);
		}
	}
}

