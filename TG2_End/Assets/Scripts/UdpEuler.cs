using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UdpEuler : MonoBehaviour
{
    [HideInInspector] public bool isTxStarted = false;

    [SerializeField] string IP = "127.0.0.1"; // local host
    int rxPort = 8000; // port to receive data from Python on
    int txPort = 8001; // port to send data to Python on
        
    // Create necessary UdpClient objects
    UdpClient client;
    IPEndPoint remoteEndPoint;
    Thread receiveThread; // Receiving Thread

    //String Received
    //public static string[] textArray;
    public float  y_data;
    [SerializeField] private int    steps = 0;
    [SerializeField] private float  timer = 0.0f;
    [SerializeField] private bool   ascending = true;
    [SerializeField] private float  velocityConstant = 3.0f;
    [SerializeField] private float  media = 0;
    
    public  float   velocity = 0.0f;
    public  float   velocityMedia = 0.0f;
    private float   prevTime = 0.0f;
    private float   minY, maxY = 0.0f;
    List<float> velocityList = new List<float>();
    
    void Awake()
    {
        // Create remote endpoint  
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);

        // Create local client
        client = new UdpClient(rxPort);

        // local endpoint define (where messages are received)
        // Create a new thread for reception of incoming messages
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        // Initialize (seen in comments window)
        print("UDP Comms Initialised");

        
    }

    private void Update()
    {                       
        timer += Time.deltaTime;


        if(y_data < minY) // Calculating Min and Max values
        {
            minY = y_data;
        }
        if(y_data > maxY)
        {
            maxY = y_data;
        }
        media = (minY + maxY) / 2; // Calculating Media

        
        if (y_data > media && ascending == false)   // Calculating number of steps
        {
            ascending = true;
            steps++;
            velocity = SetVelocity(timer, prevTime, velocityConstant);
            velocityList.Add(velocity);
            prevTime = timer;
        }
        if (y_data < media && ascending == true)
        {
            ascending = false;
            steps++;            
            velocity = SetVelocity(timer, prevTime, velocityConstant);
            velocityList.Add(velocity);
            prevTime = timer;
            
        }

        if (velocityList.Count == 6)
        {
            float sum = 0f;
            foreach(float x in velocityList)
            {
                sum = sum + x;
            }
            velocityMedia = (sum / 6);
            velocityList.RemoveAt(0);
        }
            

        // Timer Starts on Enter. 
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            timer = 0f;
            Debug.Log("Timer Started");
            StartCoroutine(SendDataCoroutine()); // Added to show sending data from Unity to Python via UDP
        }
                 

    }

    IEnumerator SendDataCoroutine() //  Added to show sending data from Unity to Python via UDP
    {
        while (true)
        {
            SendData(timer.ToString("F2") + ":" + velocity.ToString("F3") + ":" + y_data.ToString("F1"));

            yield return new WaitForSeconds(0.05f);
        }
    }

    
    private float SetVelocity(float timer, float prevTime, float velocityConst)
    {
        // Calculating period between steps
        velocity = velocityConst * (1 / (timer - prevTime));

        if (velocity < 0.8f)
        {
            velocity = 0.0f;
        }
        if (velocity > 6.0f)
        {
            velocity = 6.0f;
        }

        return velocity;
    }


    // Receive data, update packets received
    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);                                          
                y_data = float.Parse(text); // Getting the y value in angles
                //Debug.Log(y_data);
                          
                ProcessInput(text);
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void ProcessInput(string input)
    {
        // PROCESS INPUT RECEIVED STRING HERE

        if (!isTxStarted) // First data arrived so tx started
        {
            isTxStarted = true;
        }
    }

    
    void OnDisable()  //Prevent crashes - close clients and threads properly!
    {
        if (receiveThread != null)
            receiveThread.Abort();

        client.Close();
    }
        

    public void SendData(string message) // Use to send data to Python
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }


}