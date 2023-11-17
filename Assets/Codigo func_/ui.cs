using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ui : MonoBehaviour
{
    public static ui instance;
    public GameObject startMenu;
    public GameObject startMenu2;
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
        Personaje.instance.connectToServer();
    }
    public void DisconnecttoServ()
    {
        startMenu2.SetActive(false);
        Personaje.instance.Disconnect();
    }
}
