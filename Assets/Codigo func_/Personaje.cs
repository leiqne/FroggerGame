using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    //public readonly string moveUpButton = "W";
    //public readonly string moveDownButton = "S";
    //public readonly string moveLeftButton = "A";
    //public readonly string moveRightButton = "D";
    private SpriteRenderer sprite1;
    public Sprite sSalto;//public para q se vea en unity como propiedad
    public Sprite sDfault;
    public Sprite sMuerto;
    private void Awake()
    {
        sprite1 = GetComponent<SpriteRenderer>();
    }
    void Update()
    {


        // Mover hacia arriba
        if (/*Input.GetButton(moveUpButton)*/ Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            MoveCharacter(Vector3.up);
        }
        // Mover hacia abajo
        else if (/*Input.GetButton(moveUpButton)*/Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            MoveCharacter(Vector3.down);
        }
        // Mover hacia la izquierda
        else if (/*Input.GetButton(moveUpButton)*/Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            MoveCharacter(Vector3.left);
        }
        // Mover hacia la derecha
        else if (/*Input.GetButton(moveUpButton)*/Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            MoveCharacter(Vector3.right);
        }
    }

 

    private void MoveCharacter(Vector3 direc)
    {
        //transform.position += direc;
        Vector3 dest = transform.position + direc;
        Collider2D barrera=Physics2D.OverlapBox(dest, Vector2.zero, 0f, LayerMask.GetMask("barrera"));
        Collider2D plataforma=Physics2D.OverlapBox(dest, Vector2.zero, 0f, LayerMask.GetMask("plataforma"));
        Collider2D obs=Physics2D.OverlapBox(dest, Vector2.zero, 0f, LayerMask.GetMask("obstaculo"));
        if (barrera != null) { return; }//para que cuando haya contacto con la barrera este no se mueva por lo que no se movera el pj
        if (plataforma!=null){
            transform.SetParent(plataforma.transform);//para que se mueva con la plataforma
        }else{
            transform.SetParent(null);//en caso ya no este encima de la plataforma
        }
        if (obs != null && plataforma == null)
        {
            transform.position = dest;
            muerto();
        }
        else
        {
            StartCoroutine(salto(dest));
        }
    }
    private IEnumerator salto(Vector3 desti)//cambiar posicion
    {
        Vector3 stPosition = transform.position;
        float trans = 0f;
        float duracion = 0.1f;
        sprite1.sprite = sSalto;//cambio de sprite cuando se mueve
        while (trans < duracion)
        {
            float t = trans / duracion;
            transform.position = Vector3.Lerp(stPosition, desti, t);
            trans += Time.deltaTime;//tiempo que ha pasado 
            yield return null;
        }

        transform.position = desti;
        sprite1.sprite = sDfault;//retornar al sprite default
    }
    /*no funciona,cosas por arreglar, no se mueve al ponerlo modo player (no probado con el bot aun)*/
    //Función para mover el personaje desde una fuente externa(por ejemplo, un bot)
    public void MoveCharacterExternal(Vector3 externalDirection)// para mover el pj desde afuera (IA)
    {
        // Mueve el personaje en la dirección proporcionada desde la fuente externa
        transform.position += externalDirection * Time.deltaTime;

        // Rotar el personaje en la dirección del movimiento (opcional)
        if (externalDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(externalDirection.y, externalDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
    void muerto()
    {
        transform.rotation = Quaternion.identity;
        sprite1.sprite = sMuerto;
        enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)   
    {
        if (enabled&&other.gameObject.layer== LayerMask.NameToLayer("obstaculo")&&transform.parent==null)
        {
            muerto();
        }   
    }
}