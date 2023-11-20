using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeFlechas : Personaje
{
    protected override bool MoveUpInput()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }

    protected override bool MoveDownInput()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }

    protected override bool MoveLeftInput()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }

    protected override bool MoveRightInput()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
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
    private void Start()
    {
        tcp = new TCP();
    }
    public void EnviarEntornoAlServidor(int rango)
{

        Entorno entorno = ObtenerEntornoDelPersonaje(rango);
        string dataString = entorno.convertToString();
        tcp.SendData(dataString);
        tcp.ReceiveData();
    }
}