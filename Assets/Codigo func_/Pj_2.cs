using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PersonajeWASD : Personaje
{


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



    public void conect()
    {
        connectToServer();
        EnviarEntornoAlServidor();
    }

    public void Disconnect()
    {
        //TO-DO
    }
    public void EnviarEntornoAlServidor()
    {
        string id = "bot2;";
        string comando = "bot2;bellman";
        setComando(comando);
        Run(comando);
        setId(id);
        StartCoroutine(iteracion(id));
    }

    private void Start()
    {
        tcp = new TCP();
    }
    private void Update() {
 
    }
}
