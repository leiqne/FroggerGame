using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casa : MonoBehaviour
{
    public GameObject frog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        frog.SetActive(true);
    }
    private void OnDisable()
    {
        frog.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player") { 
            enabled=true; 
            Personaje frogger=collision.GetComponent<Personaje>();
            frogger.gameObject.SetActive(false);
            frogger.Invoke(nameof(frogger.respawn),1f);

        }
    }
}
