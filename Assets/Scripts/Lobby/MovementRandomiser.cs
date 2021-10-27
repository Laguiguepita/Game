using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Random = System.Random;


public class MovementRandomiser : MonoBehaviour
{
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;
    [SerializeField] private Vector2 yRotationRange;
    [SerializeField] [Range(0.01f, 0.1f)] private float lerpSpeed = 0.05f;

    public Quaternion _newRotation;

    public Vector3 _newPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _newPosition = transform.position;
        _newRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * lerpSpeed);
        if (Vector3.Distance(transform.position,_newPosition)<1f)
        {
            GetNewPosition();
        }
    }

    void GetNewPosition()
    {
        var xPos = UnityEngine.Random.Range(min.x, max.x);
        var zPos = UnityEngine.Random.Range(min.y, max.y);
        _newRotation=Quaternion.Euler(0,UnityEngine.Random.Range(yRotationRange.x,yRotationRange.y),0);
        _newPosition = new Vector3(xPos, 0, zPos);
    }
}
