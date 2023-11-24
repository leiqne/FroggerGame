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
        EnviarEntornoAlServidor();
    }

    public void Disconnect()
    {
        //TO-DO
    }
    public void EnviarEntornoAlServidor()
    {
        string id = "bot1;";
        string comando = "bot1;a*";
        setComando(comando);
        Run(comando);
        setId(id);
        StartCoroutine(iteracion(id));
    }

    private void Start()
    {
        tcp = new TCP();
    }
    private void Update()
    {

    }

}