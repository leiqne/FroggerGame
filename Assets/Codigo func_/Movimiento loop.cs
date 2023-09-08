using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientoloop : MonoBehaviour
{
    public Vector2 direction=Vector2.left;//direccion
    public float speed=1;//velcoidad
    public int size=1;// para los bordes del juego
    private Vector3 izqBorde;
    private Vector3 derBorde;

    void Start()//solo se necesita al cargar el juego
    {
        izqBorde=Camera.main.ViewportToWorldPoint(Vector3.zero);//de 0
        derBorde=Camera.main.ViewportToWorldPoint(Vector3.right);//a 1
    }


    void Update()
    {                      //para que el objeto desaparezca cuando todo esta fuera de los bordes, se desplaza derecha
        if (direction.x>0&&(transform.position.x-size)>derBorde.x)
        {
            Vector3 pos= transform.position;
            pos.x=izqBorde.x-size;
            transform.position=pos;
        }
        else if (direction.x < 0 && (transform.position.x + size) < izqBorde.x)//objeto se desplaza izquierda
        {
            Vector3 pos = transform.position;
            pos.x = derBorde.x+size;
            transform.position = pos;
        }else
        {
            transform.Translate(direction * speed * Time.deltaTime);//deltatime, basado en los frames de tu juego
            //por si tienes una poderosa y una canaimita pa que no tengas dificultades en el juego con la velocidad de los obj
        }
    }
}
