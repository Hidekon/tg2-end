
using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UdpSocket : MonoBehaviour
{
    [HideInInspector] public bool isTxStarted = false;

    [SerializeField] string IP = "127.0.0.1"; // local host
    int rxPort = 8000; // port to receive data from Python on
    int txPort = 8001; // port to send data to Python on

    int i = 0; // DELETE THIS: Added to show sending data from Unity to Python via UDP

    // Create necessary UdpClient objects
    UdpClient client;
    IPEndPoint remoteEndPoint;
    Thread receiveThread; // Receiving Thread

    //String Received
    //public static string[] textArray;
    public string[] str_text;
    public int idIMU;
    public Quaternion receivedQuaternion;
    public Quaternion quat1;
    public Quaternion quat2;
    public Quaternion quat3;
    public Quaternion quat4;


    IEnumerator SendDataCoroutine() //  Added to show sending data from Unity to Python via UDP
    {
        while (true)
        {
            SendData("Sent from Unity: " + i.ToString());
            i++;
            yield return new WaitForSeconds(1f);
        }
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

        //StartCoroutine(SendDataCoroutine()); // Added to show sending data from Unity to Python via UDP
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

                str_text = text.Split(':');
                idIMU = int.Parse(str_text[0]);
                receivedQuaternion = StringToQuaternion(str_text[1]);
                
                // Already assign when received.
                switch (idIMU)
                {
                    case 1:
                        Quaternion quat1 = receivedQuaternion;
                        break;
                    case 2:
                        Quaternion quat2 = receivedQuaternion;
                        break;
                    case 3:
                        Quaternion quat3 = receivedQuaternion;
                        break;
                    case 4:
                        Quaternion quat4 = receivedQuaternion;
                        break;

                }


                //print(text);
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

    //Prevent crashes - close clients and threads properly!
    void OnDisable()
    {
        if (receiveThread != null)
            receiveThread.Abort();

        client.Close();
    }

    public static Quaternion StringToQuaternion(string sQuaternion)
    {
        // Split the items
        string[] sArray = sQuaternion.Split(',');
        // Store as a Quaternion
        Quaternion result = new Quaternion(float.Parse(sArray[0]), float.Parse(sArray[1]),
                                            float.Parse(sArray[2]), float.Parse(sArray[3]));

        return result;
    }

    
}