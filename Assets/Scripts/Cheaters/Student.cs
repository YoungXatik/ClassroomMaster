
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

    public void ShowCheatItem()
    {
        cheatItem1.SetActive(true);
    }

    public void HideCheatItem()
    {
        cheatItem1.SetActive(false);
    }

    public void YesItsMe()
    {
        Debug.Log("I`m Cheater");
    }
}
