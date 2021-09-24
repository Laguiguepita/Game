using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mouvement : MonoBehaviour
{
    // Composant pour appliquer la gravité à l'objet
    private Rigidbody _rigidbody;
    
    // On serialize la variable speed pour pouvoir préciser sa valeur dans l'inspecteur
    [SerializeField] private float speed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Récupère le rigidbody dans les composants de l'objet
        _rigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        speed = 5;
        Move();
    }


    private void Move()
    {
        // Récupère les inputs
        float moveHorizontal = Input.GetAxis("Horizontal"); // Q/A = -1   |   D   = 1
        float moveVertical = Input.GetAxis("Vertical");     // S   = -1   |   Z/W = 1

        Vector3 direction = (transform.right * moveHorizontal + transform.forward * moveVertical) * speed;
        direction.y = _rigidbody.velocity.y;
        _rigidbody.velocity = direction;
    }

}
