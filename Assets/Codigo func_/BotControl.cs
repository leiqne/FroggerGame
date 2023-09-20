using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public GameObject player; // Referencia al objeto del jugador
    private Vector3 desiredDirection; // La direcci�n deseada para mover al personaje
    private Personaje characterScript; // Referencia al script del personaje

    private void Start()
    {
        // Encuentra autom�ticamente el objeto del jugador si no se ha asignado en el Inspector
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Obt�n una referencia al script del personaje (aseg�rate de adjuntar el script correcto al jugador)
        characterScript = player.GetComponent<Personaje>();
    }

    private void Update()
    {
        // Verifica si el jugador y este objeto existen
        if (player != null && gameObject != null)
        {
            // Llama a la funci�n que simula el movimiento del bot
            SimulateBotMovement();
        }
    }

    // M�todo para simular el movimiento del bot
    private void SimulateBotMovement()
    {
        // Verifica la direcci�n deseada del bot y llama al m�todo del personaje para moverlo
        if (desiredDirection != Vector3.zero)
        {
            // Llama al m�todo del personaje para moverse y verificar colisiones
            characterScript.MoveCharacter(desiredDirection);
        }
    }

    // M�todo para establecer la direcci�n deseada desde afuera utilizando un car�cter
    public void SetDesiredDirection(char directionChar)
    {
        // Mapea caracteres a vectores de direcci�n
        switch (directionChar)
        {
            case 'U': // Arriba
                desiredDirection = Vector3.up;
                break;
            case 'D': // Abajo
                desiredDirection = Vector3.down;
                break;
            case 'L': // Izquierda
                desiredDirection = Vector3.left;
                break;
            case 'R': // Derecha
                desiredDirection = Vector3.right;
                break;
            default:
                desiredDirection = Vector3.zero; // Direcci�n nula por defecto
                break;
        }
    }
}