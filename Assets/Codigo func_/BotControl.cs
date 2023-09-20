using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public GameObject player; // Referencia al objeto del jugador
    private Vector3 desiredDirection; // La dirección deseada para mover al personaje
    private Personaje characterScript; // Referencia al script del personaje

    private void Start()
    {
        // Encuentra automáticamente el objeto del jugador si no se ha asignado en el Inspector
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Obtén una referencia al script del personaje (asegúrate de adjuntar el script correcto al jugador)
        characterScript = player.GetComponent<Personaje>();
    }

    private void Update()
    {
        // Verifica si el jugador y este objeto existen
        if (player != null && gameObject != null)
        {
            // Llama a la función que simula el movimiento del bot
            SimulateBotMovement();
        }
    }

    // Método para simular el movimiento del bot
    private void SimulateBotMovement()
    {
        // Verifica la dirección deseada del bot y llama al método del personaje para moverlo
        if (desiredDirection != Vector3.zero)
        {
            // Llama al método del personaje para moverse y verificar colisiones
            characterScript.MoveCharacter(desiredDirection);
        }
    }

    // Método para establecer la dirección deseada desde afuera utilizando un carácter
    public void SetDesiredDirection(char directionChar)
    {
        // Mapea caracteres a vectores de dirección
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
                desiredDirection = Vector3.zero; // Dirección nula por defecto
                break;
        }
    }
}