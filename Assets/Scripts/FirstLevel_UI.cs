using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstLevel_UI : MonoBehaviour
{
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private PlayerRaycast playerRaycast;
    [SerializeField] private CameraRotation cameraRotation;

    private void Start()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
        playerRaycast = FindObjectOfType<PlayerRaycast>();
        cameraRotation = FindObjectOfType<CameraRotation>();
        
        playerCamera.enabled = false;
        playerRaycast.shootButton.gameObject.SetActive(false);
        playerCamera.openButton.SetActive(false);
        playerRaycast.countOfMistakesText.gameObject.SetActive(false);
        cameraRotation.enabled = false;
    }

    public void ClickOnPanel()
    {
        playerRaycast.countOfMistakesText.gameObject.SetActive(true);
        playerCamera.enabled = true;
        playerRaycast.shootButton.gameObject.SetActive(true);
        playerCamera.openButton.SetActive(true);
        cameraRotation.enabled = true;
        Destroy(gameObject);
    }
}
