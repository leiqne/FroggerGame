using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;
using System.Net.Sockets;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;
    public string ip = "127.0.0.1";
    public int port = 1234;
    public int myId = 0;
    public TCP tcp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("La instancia ya existe");
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
    }

    public void connectToServer()
    {
        if (tcp.Connect(ip, port))
        {
            Debug.Log("Conexión exitosa al servidor.");
            tcp.SendData("HOLA");
        }
        else
        {
            Debug.LogError("No se pudo conectar al servidor.");
        }
    }

    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        private byte[] receiveBuffer;

        public bool Connect(string ip, int port)
        {
            socket = new TcpClient();
            socket.ReceiveBufferSize = dataBufferSize;
            socket.SendBufferSize = dataBufferSize;

            try
            {
                socket.Connect(ip, port);

                if (!socket.Connected)
                {
                    return false;
                }

                stream = socket.GetStream();
                receiveBuffer = new byte[dataBufferSize];
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("Error de conexión: " + e.Message);
                return false;
            }
        }

        public void SendData(string data)
        {
            if (socket == null || !socket.Connected)
            {
                Debug.LogError("No se puede enviar datos, el socket no está conectado.");
                return;
            }

            try
            {

                byte[] dataBuffer = Encoding.Default.GetBytes(data);
                stream.Write(dataBuffer, 0, dataBuffer.Length);
            }
            catch (Exception e)
            {
                Debug.LogError("Error al enviar datos: " + e.Message);
            }
        }
    }
}
