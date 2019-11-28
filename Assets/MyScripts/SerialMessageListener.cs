using UnityEngine;
using System.Collections;

public class SerialMessageListener : MonoBehaviour
{
	[HideInInspector] public float x,y;
    [HideInInspector] public Vector2 position;
    public SerialController serialController;

    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        StartCoroutine(Poll(0.5f, "s"));
	}

	// Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
       else
            Debug.Log("Connection attempt failed or disconnection detected");
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        //Debug.Log("Values received: " + msg);
        // parse the message to obtain x, y
        Debug.Log("recieved");

        string values = msg; // Read the serial message
        string[] armCoordinates = values.Split(','); // Separate values

        for(int i = 0; i < 2; i++){
            if(armCoordinates[i] != "") //Check if all values are recieved
            {
                x = float.Parse(armCoordinates[i++]) * 0.01f;
                y = float.Parse(armCoordinates[i++]) * 0.01f;
            }
        }
        
        position = new Vector2(x,y); 
    }

    IEnumerator Poll(float delayTime, string message)
    {
        while(true)
        {
            Debug.Log("Polling");
            serialController.SendSerialMessage(message);
            yield return new WaitForSeconds(delayTime);
        }
    }
}