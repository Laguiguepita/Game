using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

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
    
    // Start is called before the first frame update
    void Start()
    {
        // Récupère le rigidbody dans les composants de l'objet
        _rigidbody = GetComponent<Rigidbody>();
        
        // Enleve le curseur et le fixe au milieu de l'écran
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void Update()
    {
        view = GetComponent<PhotonView>();
        speed = 5;
        lookSensitivity = 5;
        Move();
        LookAndMove();
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
    
    private void LookAndMove()
    {
        transform.eulerAngles += Vector3.up * (Input.GetAxis("Mouse X") * lookSensitivity * 0.1f);
        _curXRot += Input.GetAxis("Mouse Y") * lookSensitivity * 0.1f * (invertYRotation ? 1 : -1);

        Vector3 clampedAngle = camAnchor.eulerAngles;
        clampedAngle.x = Mathf.Clamp(_curXRot, MinxLook, MaxxLook);
        camAnchor.eulerAngles = clampedAngle;
    }

}
