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

    [Header("CameraScale")]
    [SerializeField] public float zoomOn;
    [SerializeField] public float zoomOff;
    [SerializeField] public bool isZooming;

    [Header("CullingMask")]
    [SerializeField] public LayerMask cheatingLayerMask;



    void Start()
    {
        zoomOff = Camera.main.fieldOfView;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            //isZooming = true;
            DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, zoomOn, 2f);
            Camera.main.cullingMask += cheatingLayerMask.value;

        }
        else if (Input.GetMouseButtonUp(1))
        {
            //isZooming = false;
            DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, zoomOff, 2f);
            Camera.main.cullingMask -= cheatingLayerMask.value;
        }
        
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

