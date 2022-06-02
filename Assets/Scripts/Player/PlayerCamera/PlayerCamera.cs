using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public List<Renderer> itemsToChange = new List<Renderer>();
    [SerializeField] public bool isOpened;
    [SerializeField] public GameObject openButton;
    [SerializeField] public GameObject closeButton;
    [SerializeField] private Transform startCameraPosition;
    [SerializeField] private Transform finalCameraPosition;
    [SerializeField] private float timeToChangeCameraState;
    
    [Header("CameraScale")]
    [SerializeField] public float zoomOn;
    [SerializeField] public float zoomOff;
    [SerializeField] public LayerMask cheatingLayerMask;
    [SerializeField] public Camera mainCamera;
    
    [Header("TestCamera")]
    [SerializeField] private GameObject testCamera;

    [SerializeField] public PlayerRaycast playerRaycast;

    private void Start()
    {
        playerRaycast = FindObjectOfType<PlayerRaycast>();
        testCamera.SetActive(false);
        zoomOff = Camera.main.fieldOfView;
        GameObject[] values = GameObject.FindGameObjectsWithTag("itemToChange");

        for (int i = 0; i < values.Length; i++)
        {
            itemsToChange.Add(values[i].GetComponent<Renderer>());
        }
    }

    public void OpenCamera()
    {
        if (isOpened == false)
        {
            testCamera.SetActive(true);
            testCamera.transform.DOMove(finalCameraPosition.position, timeToChangeCameraState);
            isOpened = true;
            openButton.SetActive(!false);
            closeButton.SetActive(true);
            DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, zoomOn, 2f);
            Camera.main.cullingMask += cheatingLayerMask;
            EnableZoom();
            playerRaycast.timeBetweenShootCounter = 0;
        }
    }

    public void CloseCamera()
    {
        if (isOpened)
        {
            testCamera.transform.DOMove(startCameraPosition.position, timeToChangeCameraState);
            testCamera.SetActive(false);
            isOpened = false;
            openButton.SetActive(true);
            closeButton.SetActive(false);
            DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, zoomOff, 2f);
            Camera.main.cullingMask -= cheatingLayerMask;
            DisableZoom();
        }
    }

    public void EnableZoom()
    {
        DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, zoomOn, 2f);
    }

    public void DisableZoom()
    {
        DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, zoomOff, 2f);
    }
}
