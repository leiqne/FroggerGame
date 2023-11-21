using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeWASD : Personaje
{
    string received;

    protected override bool MoveUpInput()
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    protected override bool MoveDownInput()
    {
        return Input.GetKeyDown(KeyCode.S);
    }

    protected override bool MoveLeftInput()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

    protected override bool MoveRightInput()
    {
        return Input.GetKeyDown(KeyCode.D);
    }

    protected void MakeMove(string movimientos)
    {
        char[] movimientosArray = movimientos.ToCharArray();

        foreach (char movimiento in movimientosArray)
        {
            switch (movimiento)
            {
                case 'W':
                    MoveCharacter(Vector3.up);
                    break;
                case 'A':
                    MoveCharacter(Vector3.left);
                    break;
                case 'S':
                    MoveCharacter(Vector3.down);
                    break;
                case 'D':
                    MoveCharacter(Vector3.right);
                    break;
                // Puedes agregar más casos según sea necesario
                default:
                    UnityEngine.Debug.LogWarning("Movimiento no reconocido: " + movimiento);
                    break;
            }
        }
    }

    public void conect()
    {
        connectToServer();
        EnviarEntornoAlServidor(2);
    }

    public void Disconnect()
    {
        //TO-DO
    }

    public void EnviarEntornoAlServidor(int rango)
    {
        Entorno entorno = ObtenerEntornoDelPersonaje(rango);
        string dataString = entorno.convertToString();
        tcp.StartBot("bot2;bellman");
        tcp.ReceiveData();
        tcp.SendData("bot2;" + dataString);
        received = tcp.ReceiveData();
        MakeMove(received);
    }

    private void Start()
    {
        tcp = new TCP();
    }
}
