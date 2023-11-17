using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui : MonoBehaviour
{
    public static ui instance;
    public GameObject startMenu;
    public GameObject startMenu2;

    // Mantén referencias a las instancias de PersonajeFlechas y PersonajeWASD
    public PersonajeFlechas personajeFlechas;
    public PersonajeWASD personajeWASD;

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

    public void ConnecttoServ()
    {
        startMenu.SetActive(false);

        // Verifica si las instancias no son nulas antes de llamar a los métodos
        if (personajeFlechas != null)
        {
            personajeFlechas.conect();
        }
        else
        {
            Debug.LogError("Instancia de PersonajeFlechas no asignada en el Inspector.");
        }

        if (personajeWASD != null)
        {
            personajeWASD.conect();
        }
        else
        {
            Debug.LogError("Instancia de PersonajeWASD no asignada en el Inspector.");
        }
    }

    public void DisconnecttoServ()
    {
        startMenu2.SetActive(false);

        // Llama al método Disconnect() de las instancias correspondientes
        if (personajeFlechas != null)
        {
            personajeFlechas.Disconnect();
        }
        else
        {
            Debug.LogError("Instancia de PersonajeFlechas no asignada en el Inspector.");
        }

        if (personajeWASD != null)
        {
            personajeWASD.Disconnect();
        }
        else
        {
            Debug.LogError("Instancia de PersonajeWASD no asignada en el Inspector.");
        }
    }
}
