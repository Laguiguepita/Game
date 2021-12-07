using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer;

        [HideInInspector] public bool colliding = false;
        [HideInInspector] public Vector3[] adjustedCameraClipPoints;
        [HideInInspector] public Vector3[] desiredCameraClipPoints;

        Camera camera;

        public void Initialize(Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }

        public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            //clear the contents of intoArray
            intoArray = new Vector3[5];
            
            float z = camera.nearClipPlane;
            float x = Mathf.Tan(camera.fieldOfView / 3.41f) * z;
            float y = x / camera.aspect;
            
            //top left
            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; //added and rotated the point relative to camera
            //top right
            intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition; //added and rotated the point relative to camera
            //bottom left
            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition; //added and rotated the point relative to camera
            //bottom right
            intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition; //added and rotated the point relative to camera
            //camera's position
            intoArray[4] = cameraPosition - camera.transform.forward;
        }

        bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for (int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
                float distance = Vector3.Distance(clipPoints[i], fromPosition);
                if (Physics.Raycast(ray,distance))
                {
                    return true;
                }
            }

            return false;
        }

        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            float distance = -1;
            for (int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                RaycastHit hit;
                if (Physics.Raycast(ray,out hit))
                {
                    if (distance == -1)
                        distance = hit.distance;
                    else
                    {
                        if (hit.distance < distance)
                            distance = hit.distance;
                    }
                }
            }

            if (distance == -1)
                return 0;
            else
            {
                return distance;
            }
        }

        public void CheckColliding(Vector3 targetPosition)
        {
            if (CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition))
            {
                colliding = true;
            }
            else
            {
                colliding = false;
            }
        }
        
    }
    
    [SerializeField] private Transform camAnchor;
    [SerializeField] private float lookSensitivity;
    
    private const float minXRotation = -70;
    private const float maxXRotation = 40;
    private const float zoomSmooth = 1000;
    private const float maxZoom = -2;
    private const float minZoom = -10;
    private float distanceFromTarget = -8;
    private float xRotation = -20;
    private float yRotation = -180;
    private float vOrbitInput;
    private float hOrbitInput;
    private float zoomInput;
    private float adjustementDistance = -8;
    private bool smoothFollow = false;
    public float smooth = 0.05f;
    
    public Vector3 camVel = Vector3.zero;
    
    private Vector3 destination = Vector3.zero;
    private Vector3 adjustedDestination = Vector3.zero;
    

    public CollisionHandler collision;

    void Start()
    {
        collision = new CollisionHandler();
        lookSensitivity = 100;
        collision.Initialize(Camera.main);
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);
    }
    
    void Update()
    {
        GetInput();
        OrbitTarget();
        ZoomInOnTarget();
    }
    /*private void LateUpdate()
    {
        MoveToTarget();
        LookAtTarget();
    }*/

    private void LateUpdate()
    {
        //moving
        MoveToTarget();
        //rotating
        LookAtTarget();
        //player input orbit
        OrbitTarget();
        
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

        for (int i = 0; i < 5; i++)
        {
            Debug.DrawLine(camAnchor.position, collision.desiredCameraClipPoints[i], Color.green);
            Debug.DrawLine(camAnchor.position, collision.adjustedCameraClipPoints[i], Color.red);
        }
        collision.CheckColliding(camAnchor.position);
        adjustementDistance = collision.GetAdjustedDistanceWithRayFrom(camAnchor.position);
    }

    private void GetInput()
    {
        vOrbitInput = Input.GetAxis("Mouse X");
        hOrbitInput = Input.GetAxis("Mouse Y");
        zoomInput = Input.GetAxis("Mouse ScrollWheel");
    }
    
    private void MoveToTarget()
    {
        destination = Quaternion.Euler(xRotation, yRotation, 0) * -Vector3.forward * distanceFromTarget + camAnchor.position;

        if (collision.colliding)
        {
            adjustedDestination = Quaternion.Euler(xRotation, yRotation, 0) * Vector3.forward * adjustementDistance + camAnchor.position;
            if (smoothFollow)
            {
                transform.position =
                    Vector3.SmoothDamp(transform.position, adjustedDestination, ref camVel, smooth);
            }
            else
            {
                transform.position = adjustedDestination;
            }
        }
        else
        {
            if (smoothFollow)
            {
                transform.position =
                    Vector3.SmoothDamp(transform.position, destination, ref camVel, smooth);
            }
            else
            {
                transform.position = destination;
            }
        }
    }

    private void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(camAnchor.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lookSensitivity * Time.deltaTime);
    }

    private void OrbitTarget()
    {
        xRotation += hOrbitInput * lookSensitivity * Time.deltaTime;
        yRotation += vOrbitInput * lookSensitivity * Time.deltaTime;

        if (xRotation > maxXRotation)
        {
            xRotation = maxXRotation;
        }

        if (xRotation < minXRotation)
        {
            xRotation = minXRotation;
        }
    }

    private void ZoomInOnTarget()
    {
        distanceFromTarget += zoomInput * zoomSmooth * Time.deltaTime;
        if (distanceFromTarget > maxZoom)
        {
            distanceFromTarget = maxZoom;
        }

        if (distanceFromTarget < minZoom)
        {
            distanceFromTarget = minZoom;
        }
    }
    
    
}
