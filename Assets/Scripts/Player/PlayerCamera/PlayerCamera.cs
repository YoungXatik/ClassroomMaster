using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] public List<Renderer> itemsToChange = new List<Renderer>();
    [SerializeField] private Material changedMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] public bool isOpened;
    [SerializeField] private GameObject openButton;
    [SerializeField] private GameObject closeButton;
    
    [Header("CameraScale")]
    [SerializeField] public float zoomOn;
    [SerializeField] public float zoomOff;
    [SerializeField] public bool isZooming;
    [SerializeField] public LayerMask cheatingLayerMask;
    [SerializeField] public Camera mainCamera;

    private void Start()
    {
        zoomOff = Camera.main.fieldOfView;
        GameObject[] values = GameObject.FindGameObjectsWithTag("itemToChange");

        for (int i = 0; i < values.Length; i++)
        {
            itemsToChange.Add(values[i].GetComponent<Renderer>());
        }
    }

    public void ShowChangedMaterials()
    {
        for (int i = 0; i < itemsToChange.Count; i++)
        {
            itemsToChange[i].material = changedMaterial;
        }
    }

    public void OpenCamera()
    {
        if (isOpened == false)
        {
            cameraAnimator.SetBool("IdleState", false);
            cameraAnimator.SetBool("OpenCamera", true);
            isOpened = true;
            openButton.SetActive(!false);
            closeButton.SetActive(true);
        }
    }

    public void CloseCamera()
    {
        if (isOpened)
        {
            cameraAnimator.SetBool("OpenCamera", false);
            isOpened = false;
            openButton.SetActive(true);
            closeButton.SetActive(false);
        }
    }

    public void ToIdleState()
    {
        cameraAnimator.SetBool("IdleState",true);
    }

    public void GetMaterialBack()
    {
        for (int i = 0; i < itemsToChange.Count; i++)
        {
            itemsToChange[i].material = defaultMaterial;
        }
    }

    public void EnableZoom()
    {
        DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, zoomOn, 2f);
        mainCamera.cullingMask += cheatingLayerMask.value;
    }

    public void DisableZoom()
    {
        DOTween.To(x => Camera.main.fieldOfView = x, Camera.main.fieldOfView, zoomOff, 2f);
        mainCamera.cullingMask -= cheatingLayerMask.value;
    }
}
