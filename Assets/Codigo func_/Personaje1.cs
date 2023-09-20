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
}