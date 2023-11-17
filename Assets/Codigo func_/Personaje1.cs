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
    }
    public void Disconnectconect()
    {
        //TO-DO
    }
    public void EnviarEntornoAlServidor(int rango)
    {
        Entorno entorno = ObtenerEntornoDelPersonaje(rango);
        // Lógica para enviar el entorno al servidor usando tcp.SendData o cualquier método necesario
    }
}