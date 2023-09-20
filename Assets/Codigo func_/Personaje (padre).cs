using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Personaje : MonoBehaviour
{
    private SpriteRenderer sprite1;
    public Sprite sSalto;
    public Sprite sDfault;
    public Sprite sMuerto;
    private Vector3 spawnpos;
    protected abstract bool MoveUpInput();
    protected abstract bool MoveDownInput();
    protected abstract bool MoveLeftInput();
    protected abstract bool MoveRightInput();

    private void Awake()
    {
        sprite1 = GetComponent<SpriteRenderer>();
        spawnpos = transform.position;
    }

    private void Update()
    {
        // Mover hacia arriba
        if (MoveUpInput())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            MoveCharacter(Vector3.up);
        }
        // Mover hacia abajo
        else if (MoveDownInput())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            MoveCharacter(Vector3.down);
        }
        // Mover hacia la izquierda
        else if (MoveLeftInput())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            MoveCharacter(Vector3.left);
        }
        // Mover hacia la derecha
        else if (MoveRightInput())
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            MoveCharacter(Vector3.right);
        }
    }

    private void MoveCharacter(Vector3 direc)
    {
        Vector3 dest = transform.position + direc;
        Collider2D barrera = Physics2D.OverlapBox(dest, Vector2.zero, 0f, LayerMask.GetMask("barrera"));
        Collider2D plataforma = Physics2D.OverlapBox(dest, Vector2.zero, 0f, LayerMask.GetMask("plataforma"));
        Collider2D obs = Physics2D.OverlapBox(dest, Vector2.zero, 0f, LayerMask.GetMask("obstaculo"));

        if (barrera != null) { return; }

        if (plataforma != null)
        {
            transform.SetParent(plataforma.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        if (obs != null && plataforma == null)
        {
            transform.position = dest;
            Muerto();
        }
        else
        {
            StartCoroutine(Salto(dest));
        }
    }

    private IEnumerator Salto(Vector3 desti)
    {
        Vector3 stPosition = transform.position;
        float trans = 0f;
        float duracion = 0.1f;
        sprite1.sprite = sSalto;

        while (trans < duracion)
        {
            float t = trans / duracion;
            transform.position = Vector3.Lerp(stPosition, desti, t);
            trans += Time.deltaTime;
            yield return null;
        }

        transform.position = desti;
        sprite1.sprite = sDfault;
    }
    public void respawn()
    {
        StopAllCoroutines();
        transform.rotation= Quaternion.identity;
        transform.position = spawnpos;
        sprite1.sprite = sDfault;
        gameObject.SetActive(true);
        enabled = true;
    }
    protected void Muerto()
    {
        StopAllCoroutines();
        transform.rotation = Quaternion.identity;
        sprite1.sprite = sMuerto;
        enabled = false;

        Invoke(nameof(respawn), 1f);//para que le de un time out cuando muera
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("obstaculo") && transform.parent == null)
        {
            Muerto(); /*sprite1.sprite = sMuerto;*/
        }
    }
}