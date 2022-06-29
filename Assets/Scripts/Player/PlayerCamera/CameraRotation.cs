using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CameraRotation : MonoBehaviour
{
    [Header("CameraDragging")]
    [SerializeField] private float sensitivityHor = 9.0f;
    [SerializeField] private float sensitivityVert = 9.0f;
    [SerializeField] private float minimumVert;
    [SerializeField] private float maximumVert;
    [SerializeField] private float minimumHoriz;
    [SerializeField] private float maximumHoriz;
    private float _rotationX = 0;
    private float _rotationY = 0;
    [SerializeField] private bool dragging;
    
    

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }*/
#if UNITY_ANDROID 
        if (Input.touchCount == 1)
        {
            dragging = true;
        }
        else
        {
            dragging = false;
        }     
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            dragging = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
#endif
        

        if (dragging)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            _rotationY -= Input.GetAxis("Mouse X") * sensitivityHor;
            _rotationY = Mathf.Clamp(_rotationY, minimumHoriz, maximumHoriz);
            transform.localEulerAngles = new Vector3(_rotationX, -_rotationY, 0);
        }
        else
        {
            return;
        }

    }
    
}

