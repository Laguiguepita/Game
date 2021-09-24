using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform camAnchor;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private bool invertYRotation;
    
    // Pour verouiller la vue et éviter de faire des tours complets du personnage
    private const float MinxLook = -15;
    private const float MaxxLook = 60;
    private float _curXRot;
    
    // Start is called before the first frame update
    void Start()
    {
        // Enleve le curseur et le fixe au milieu de l'écran
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        lookSensitivity = 5;
        LookAndMove();
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
