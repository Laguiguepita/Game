using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using UnityEngine.UI;

public class mouvement : MonoBehaviour
{
    // Composant pour appliquer la gravité à l'objet
    private Rigidbody _rigidbody;
    
    // On serialize la variable speed pour pouvoir préciser sa valeur dans l'inspecteur
    [SerializeField] private float speed;

    
    [SerializeField] private Transform camAnchor;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private bool invertYRotation;
    
    // Pour verouiller la vue et éviter de faire des tours complets du personnage
    private const float MinxLook = -15;
    private const float MaxxLook = 60;
    private float _curXRot;
    
    
    PhotonView view;

    private Animator animator;
    public float jumpForce = 7;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        // Récupère le rigidbody dans les composants de l'objet
        _rigidbody = GetComponent<Rigidbody>();
        
        // Enleve le curseur et le fixe au milieu de l'écran
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        speed = 15;
        lookSensitivity = 5;
        
    }


    // Update is called once per frame
    void Update()
    {
        bool iswalking = animator.GetBool("IsWalking");
        bool forwardPressed = Input.GetKey("w");
        view = GetComponent<PhotonView>();
        Move();
        LookAndMove();
        if (forwardPressed)
        {
            animator.SetBool("IsWalking",true);
        }
        if(!forwardPressed)
        {
            animator.SetBool("IsWalking",false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        }
    }


    private void Move()
    {
        // Récupère les inputs
        float moveHorizontal = Input.GetAxis("Horizontal"); // Q/A = -1   |   D   = 1
        float moveVertical = Input.GetAxis("Vertical");     // S   = -1   |   Z/W = 1

        Vector3 direction = (transform.right * moveHorizontal + transform.forward * moveVertical);
        direction.Normalize();
        direction = direction * speed;
        _rigidbody.velocity = direction;
    }
    
    private void LookAndMove()
    {
        transform.eulerAngles += Vector3.up * (Input.GetAxis("Mouse X") * lookSensitivity * 0.1f);
        _curXRot += Input.GetAxis("Mouse Y") * lookSensitivity * 0.1f * (invertYRotation ? 1 : -1);

        Vector3 clampedAngle = camAnchor.eulerAngles;
        clampedAngle.x = Mathf.Clamp(_curXRot, MinxLook, MaxxLook);
        camAnchor.eulerAngles = clampedAngle;
    }

}
