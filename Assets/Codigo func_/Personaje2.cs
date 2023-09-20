using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}