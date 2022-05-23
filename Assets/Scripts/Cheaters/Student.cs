
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Student : MonoBehaviour
{
    [Header("functional")]
    [SerializeField] public bool isCheating;
    [SerializeField] public Animator studentAnimator;
    [SerializeField] public int countOfCheatingAnims;
    [SerializeField] public int countOfStudyAnims;
    [Header("StateCounters")]
    [SerializeField] public int stateNumber;
    [SerializeField] public int cheatNumber;
    [SerializeField] public int studyNumber;

    [Header("CheatingItemsForDifferentAnims")]
    [SerializeField] public GameObject cheatItem1;
    [SerializeField] public GameObject cheatItem2;
    [SerializeField] public GameObject cheatItem3;
    [SerializeField] public GameObject cheatItem4;

    [Header("Particles")]
    [SerializeField] private ParticleSystem[] particleSystemsEmojisGood;
    [SerializeField] private ParticleSystem[] particleSystemsEmojisBad;


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
        if (stateNumber == 1)
        {
            isCheating = true;
            Debug.Log("State number = " + stateNumber);
        }
        else if(stateNumber == 2)
        {
            isCheating = false;
            Debug.Log("State number = " + stateNumber);
        }
        
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
    }

    public void HideCheatItem1()
    {
        cheatItem1.SetActive(false);
    }
    
    public void ShowCheatItem2()
    {
        cheatItem2.SetActive(true);
    }

    public void HideCheatItem2()
    {
        cheatItem2.SetActive(false);
    }
    
    public void ShowCheatItem3()
    {
        cheatItem3.SetActive(true);
    }

    public void HideCheatItem3()
    {
        cheatItem3.SetActive(false);
    }
    
    public void ShowCheatItem4()
    {
        cheatItem4.SetActive(true);
    }

    public void HideCheatItem4()
    {
        cheatItem4.SetActive(false);
    }

    public void PlayGoodEmojiEffect()
    {
        Random random = new Random();
        particleSystemsEmojisGood[random.Next(0,particleSystemsEmojisGood.Length)].Play();
    }
    public void PlayBadEmojiEffect()
    {
        Random random = new Random();
        particleSystemsEmojisBad[random.Next(0,particleSystemsEmojisBad.Length)].Play();
    }

    public void YesItsMe()
    {
        Debug.Log("I`m Cheater");
    }
}
