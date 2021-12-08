using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;

public class mouvement : MonoBehaviour
{
    // Composant pour appliquer la gravité à l'objet
    private Rigidbody _rigidbody;
    
    // On serialize la variable speed pour pouvoir préciser sa valeur dans l'inspecteur
    [SerializeField] private float speed;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Transform camAnchor;
    
    private float _curXRot;
    

    private Animator animator;
    [SerializeField] public float jumpForce = 40;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        // Récupère le rigidbody dans les composants de l'objet
        _rigidbody = GetComponent<Rigidbody>();
        
        // Enleve le curseur et le fixe au milieu de l'écran
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        speed = 15;
    }


    // Update is called once per frame
    void Update()
    {
        bool iswalking = animator.GetBool("IsWalking");
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        Move();
        if (forwardPressed)
        {
            transform.rotation = Quaternion.Euler(0, camAnchor.rotation.eulerAngles.y , 0);
            camAnchor.localEulerAngles = new Vector3(camAnchor.rotation.eulerAngles.x, 0, 0);
            animator.SetBool("IsWalking",true);
        }
        if(!forwardPressed)
        {
            animator.SetBool("IsWalking",false);
        }
        if (backwardPressed)
        {
            animator.SetBool("IsWalkingBack",true);
        }
        if(!backwardPressed)
        {
            animator.SetBool("IsWalkingBack",false);
        }
    }


    private void Move()
    {
        // Récupère les inputs
        float moveHorizontal = Input.GetAxis("Horizontal"); // Q/A = -1   |   D   = 1
        float moveVertical = Input.GetAxis("Vertical");     // S   = -1   |   Z/W = 1

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));
        Vector3 direction = transform.TransformDirection(movement) * speed;
        _rigidbody.velocity = new Vector3(direction.x, _rigidbody.velocity.y, direction.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("Jumped", true);
            
        }
        else 
        {
            animator.SetBool("Jumped", false);

        }
        
        
    }

}
