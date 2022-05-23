using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    private Transform _selection;
    [SerializeField] private Button shootButton;

    [Header("AimTimings")] 
    [SerializeField] private float timeBetweenShoot;
    [SerializeField] private float timeBetweenShootCounter;
    [SerializeField] private GameObject gunObject;

    [Header("PistolTransform")] 
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float timeToChangePos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletStartPos;

    [Header("StudentsInfo")] 
    [SerializeField] public PlayerCamera playerCamera;

    private void Awake()
    {
        shootButton = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<Button>();
        playerCamera = FindObjectOfType<PlayerCamera>();
        shootButton.interactable = false;
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        Debug.DrawRay(transform.position,transform.forward * 100,Color.blue);
        RaycastHit hit;

        if (_selection != null)
        {
            var selectionOutline = _selection.GetComponent<Outline>();
            selectionOutline.enabled = false;
            _selection = null;
        }
        else
        {
            timeBetweenShootCounter = 0;
            gunObject.transform.DOMove(startPos.transform.position, timeToChangePos);
            shootButton.interactable = false;
        }
        

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            var selectionOption = selection.GetComponent<Outline>();

            if (selectionOption.enabled == false)
            {
                selectionOption.enabled = true;
                if (playerCamera.isOpened)
                {
                    timeBetweenShootCounter += Time.deltaTime;
                    if (timeBetweenShootCounter >= timeBetweenShoot)
                    {
                        shootButton.interactable = true;
                        gunObject.transform.DOMove(endPos.transform.position, timeToChangePos);
                    }
                }
            }
            _selection = selection;
        }

    }

    public void Shoot()
    {
     var bullet =  Instantiate(bulletPrefab, bulletStartPos.position, bulletStartPos.rotation);
     
    }
}
