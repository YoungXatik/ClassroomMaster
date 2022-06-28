
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    private Transform _selection;
    [SerializeField] public Button shootButton;
    [SerializeField] public Image cameraButtonBackGround;
    [SerializeField] public Image fButtonBackGround;
    [SerializeField] public Sprite nonInteractableButtonSprite;
    [SerializeField] public Sprite interactableButtonSprite;

    [Header("AimTimings")]
    [SerializeField] public GameObject gunObject;

    [SerializeField] public int countOfMistakes;
    [SerializeField] public Text countOfMistakesText;

    [SerializeField] private float timeToDetectStudent;
    [SerializeField] private float timeToDetectStudentCounter;

    [Header("PistolTransform")] [SerializeField]
    private Transform startPos;

    [SerializeField] private Transform endPos;
    [SerializeField] private float timeToChangePos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletStartPos;

    [Header("StudentsInfo")] [SerializeField]
    public PlayerCamera playerCamera;

    [Header("CameraSounds")] 
    [SerializeField] private AudioSource cameraAudioSource;
    [SerializeField] private AudioClip detectedClip;
    [SerializeField] private AudioClip shootClip;
    private void Awake()
    {
        shootButton = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<Button>();
        playerCamera = FindObjectOfType<PlayerCamera>();
        //shootButton.interactable = false;
    }

    private void Start()
    {
        countOfMistakes = FindObjectOfType<StudentsManager>().countOfCheatingStudents - 1;
    }

    private void Update()
    {
        countOfMistakesText.text = "x" + countOfMistakes;

        if (playerCamera.isOpened)
        {
            shootButton.interactable = false;
            fButtonBackGround.sprite = nonInteractableButtonSprite;
        }
        else
        {
            shootButton.interactable = true;
            fButtonBackGround.sprite = interactableButtonSprite;
        }
        
        Ray ray = new Ray(transform.position, transform.forward);

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);
#endif
        
        
        
        RaycastHit hit;

        if (_selection != null)
        {
            var selectionOutline = _selection.GetComponent<Outline>();
            selectionOutline.enabled = false;
            _selection = null;
        }
        else
        {
            gunObject.transform.DOMove(startPos.transform.position, timeToChangePos);
        }
        
        if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                var selectionOption = selection.GetComponent<Outline>();
                var selectedStudent = hit.collider.gameObject.GetComponent<Student>();
                
                    if (selectionOption.enabled == false)
                    {
                        selectionOption.enabled = true;
                    }

                    /*if (playerCamera.isOpened)
                    {
                        timeToDetectStudentCounter += Time.deltaTime;
                        if (timeToDetectStudentCounter >= timeToDetectStudent)
                        {
                            if (selectedStudent.isCheating)
                            {
                                cameraAudioSource.PlayOneShot(detectedClip);
                            }
                        }
                    }
                    else
                    {
                        timeToDetectStudentCounter = 0;
                    }*/

                    _selection = selection;
            }
        else
        {
            timeToDetectStudentCounter = 0;
                //cameraAudioSource.Stop();

        }
        
        
    }

    public void Shoot()
    {
        var Seq = DOTween.Sequence();
        Seq.Append(fButtonBackGround.transform.DOScale(new Vector3(0.7f, 0.7f, 0), 0.1f));
        Seq.Append(fButtonBackGround.transform.DOScale(new Vector3(1f, 1f, 0), 0.1f));
        var bullet = Instantiate(bulletPrefab, bulletStartPos.position, transform.rotation);
        cameraAudioSource.PlayOneShot(shootClip);
    }
}