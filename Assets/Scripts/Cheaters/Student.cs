using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Student : MonoBehaviour
{
    [Header("functional")] [SerializeField]
    public bool isCheating;

    [SerializeField] public Animator studentAnimator;
    [SerializeField] public int countOfCheatingAnims;
    [SerializeField] public int countOfStudyAnims;

    [Header("StateCounters")] [SerializeField]
    public int stateNumber;

    [SerializeField] public int cheatNumber;
    [SerializeField] public int studyNumber;
    [SerializeField] public int timeToStartAnimations;

    [Header("CheatingItemsForDifferentAnims")] [SerializeField]
    public GameObject cheatItem1;

    [SerializeField] public GameObject cheatItem2;
    [SerializeField] public GameObject cheatItem3;
    [SerializeField] public GameObject cheatItem4;
    [SerializeField] public string cheatType;
    [SerializeField] public GameObject textPrefab;

    [Header("Particles")] [SerializeField] private ParticleSystem[] particleSystemsEmojisGood;
    [SerializeField] private ParticleSystem[] particleSystemsEmojisBad;

    [Header("PointToGoOut")] [SerializeField]
    private Transform leftPoint;

    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform backwardPoint;

    [SerializeField] private bool isLeftSideStudent;
    [SerializeField] private float timeToWalking;


    private void Awake()
    {
        cheatItem1.SetActive(false);
        cheatItem2.SetActive(false);
        cheatItem3.SetActive(false);
        cheatItem4.SetActive(false);
    }

    private void Start()
    {
        studentAnimator = GetComponent<Animator>();
        Debug.Log(timeToStartAnimations + "sec to start");
        Invoke("StartAnimations", timeToStartAnimations);
    }

    public void StartAnimations()
    {
        if (isCheating)
        {
            studentAnimator.SetBool("Cheating_" + cheatNumber, true);
            Debug.Log("Cheating variant is -  " + cheatNumber);
        }
        else
        {
            studentAnimator.SetBool("Study_" + studyNumber, true);
            Debug.Log("Study variant is -  " + studyNumber);
        }
    }

    public void ShowCheatItem1()
    {
        cheatItem1.SetActive(true);
        cheatType = "Phone";
    }

    public void HideCheatItem1()
    {
        cheatItem1.SetActive(false);
    }

    public void ShowCheatItem2()
    {
        cheatItem2.SetActive(true);
        cheatType = "Cheat sheet";
    }

    public void HideCheatItem2()
    {
        cheatItem2.SetActive(false);
    }

    public void ShowCheatItem3()
    {
        cheatItem3.SetActive(true);
        cheatType = "Book";
    }

    public void HideCheatItem3()
    {
        cheatItem3.SetActive(false);
    }

    public void ShowCheatItem4()
    {
        cheatItem4.SetActive(true);
        cheatType = "Watches";
    }

    public void HideCheatItem4()
    {
        cheatItem4.SetActive(false);
    }

    public void PlayGoodEmojiEffect()
    {
        Random random = new Random();
        particleSystemsEmojisGood[random.Next(0, particleSystemsEmojisGood.Length)].Play();
    }

    public void PlayBadEmojiEffect()
    {
        Random random = new Random();
        particleSystemsEmojisBad[random.Next(0, particleSystemsEmojisBad.Length)].Play();
    }

    public void BackwardStandUp()
    {
        gameObject.transform.DOMove(backwardPoint.position, 2f);
    }

    public void GoOutFromClass()
    {
        HideCheatItem1();
        HideCheatItem2();
        HideCheatItem3();
        HideCheatItem4();


        if (isLeftSideStudent)
        {
            gameObject.transform.DOMove(leftPoint.position, timeToWalking).SetEase(Ease.Linear);
            gameObject.transform.DORotate(new Vector3(0, 270, 0), 0.5f).SetEase(Ease.Linear);
            studentAnimator.SetBool("Walking", true);
            Destroy(gameObject, timeToWalking);
        }
        else
        {
            gameObject.transform.DOMove(rightPoint.position, timeToWalking).SetEase(Ease.Linear);
            gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.5f).SetEase(Ease.Linear);
            studentAnimator.SetBool("Walking", true);
            Destroy(gameObject, timeToWalking);
        }

        if (isCheating)
        {
            textPrefab.GetComponentInChildren<Text>().text = "You founded cheater with a" + " " + cheatType + "!";
            var createdText = Instantiate(textPrefab);
            Destroy(createdText, 2f);
        }
    }

    public void SeatDown()
    {
        Random random = new Random();
        studentAnimator.SetBool("Study_" + random.Next(1, countOfStudyAnims + 1), false);
        studentAnimator.SetBool("Shooted",false);
        
        textPrefab.GetComponentInChildren<Text>().text = "You pick a wrong student!";
        var createdText = Instantiate(textPrefab);
        Destroy(createdText, 2f);
    }
}