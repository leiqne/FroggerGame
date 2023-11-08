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
    public static int dataBufferSize = 1234;
    public string ip = "127.0.0.1";
    public int port = 15841;
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
    public void Disconnect()
    {
        tcp.Close();
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
        public void Close()
        {
            if (socket != null && socket.Connected)
            {
                socket.Close();
                Debug.Log("Conexión cerrada.");
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
                byte[] datasend = Encoding.Default.GetBytes(data);
                int length = data.Length;
                byte[] lengthBytes = BitConverter.GetBytes(length);

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(lengthBytes);
                }

                byte[] headerBytes = new byte[18];

                // Copia la longitud (en formato little-endian) en el encabezado
                Array.Copy(lengthBytes, 0, headerBytes, 0, 4);

                // Agrega el texto "|bot" al encabezado
                byte[] botBytes = Encoding.Default.GetBytes("|bot");
                Array.Copy(botBytes, 0, headerBytes, 4, botBytes.Length);

                // Llena el resto del encabezado con '\x00' si es necesario
                for (int i = 4 + botBytes.Length; i < headerBytes.Length; i++)
                {
                    headerBytes[i] = (byte)'\x00';
                }

                stream.Write(headerBytes, 0, headerBytes.Length);

                int bytesSent = 0;
                int chunkSize = 254; // Tamaño del fragmento

                // Enviar los datos en fragmentos
                while (bytesSent < datasend.Length)
                {
                    int remainingBytes = datasend.Length - bytesSent;
                    int currentChunkSize = Math.Min(chunkSize, remainingBytes);
                    stream.Write(datasend, 0, datasend.Length);
                    bytesSent += currentChunkSize;
                }

            }
            catch (Exception e)
            {
                Debug.LogError("Error al enviar datos: " + e.Message);
            }
        }
    }
}
