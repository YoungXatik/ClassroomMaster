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

    public bool isThisStudentSeatAtLastTable;

    public bool isThisStudentNotAloneAtTable;

    public bool thisStudentDoNonRandomAnimation;
    public int numberOfAnimation;

    [SerializeField] public Animator studentAnimator;
    [SerializeField] public int countOfCheatingAnims;
    [SerializeField] public int countOfStudyAnims;

    [Header("StateCounters")] [SerializeField]
    public int stateNumber;

    [SerializeField] public int cheatNumber;
    [SerializeField] public int studyNumber;
    [SerializeField] public int timeToStartAnimations;

    [Header("CheatingItemsForDifferentAnims")]
    [SerializeField] private GameObject cheatItem1;
    [SerializeField] private GameObject cheatItem2;
    [SerializeField] private GameObject cheatItem3;
    [SerializeField] private GameObject cheatItem4;
    [SerializeField] private GameObject cheatItem5;
    [SerializeField] private GameObject cheatItem6;
    [SerializeField] private GameObject cheatItem7Pen;
    [SerializeField] private GameObject cheatItem8Calculator;
    [SerializeField] private GameObject cheatShelf;
    [SerializeField] private GameObject windowCheat;
    [SerializeField] private GameObject catBackpack;
    [SerializeField] private AudioSource catSource;
    [SerializeField] private AudioClip catClip;

    [SerializeField] private GameObject BackwardLookLine;
    [SerializeField] private GameObject SideLookLine;

    [SerializeField] public string cheatType;
    [SerializeField] public GameObject textPrefab;
    
    [SerializeField] private GameObject penObject;

    [Header("Particles")] [SerializeField] private ParticleSystem[] particleSystemsEmojisGood;
    [SerializeField] private ParticleSystem[] particleSystemsEmojisBad;

    [Header("PointToGoOut")] [SerializeField]
    private Transform leftPoint;

    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform backwardPoint;

    [SerializeField] private bool isLeftSideStudent;
    [SerializeField] private float timeToWalking;

    [SerializeField] public GameObject door;

    [Header("StudentSounds")] 
    [SerializeField] private AudioSource studentAudioSource;
    [SerializeField] private AudioClip openDoorClip;
    [SerializeField] private AudioClip shootedClip;
    [SerializeField] private AudioClip notificationClip;
    [SerializeField] private AudioClip emojiClip;


    private void Awake()
    {

        cheatItem1.SetActive(false);
        cheatItem2.SetActive(false);
        cheatItem3.SetActive(false);
        cheatItem4.SetActive(false);
        cheatItem5.SetActive(false);
        cheatItem6.SetActive(false);
        cheatItem7Pen.SetActive(false);
        cheatItem8Calculator.SetActive(false);
        catBackpack.SetActive(false);
    }

    private void Start()
    {
        studentAnimator = GetComponent<Animator>();
        Debug.Log(timeToStartAnimations + "sec to start");
        Invoke("StartAnimations", timeToStartAnimations);
        penObject.SetActive(false);
        SideLookLine.SetActive(false);
    }

    public void StartAnimations()
    {
        if (thisStudentDoNonRandomAnimation)
        {
            studentAnimator.SetBool("Cheating_" + numberOfAnimation, true);
            Debug.Log("Cheating variant is -  " + numberOfAnimation + "This non random");
        }
        else
        {
            if (isCheating)
            {
                Random random = new Random();
                if (cheatNumber == 0)
                {
                
                    cheatNumber = random.Next(1, 5);
                    studentAnimator.SetBool("Cheating_" + cheatNumber, true);
                    Debug.Log("Cheating variant is -  " + cheatNumber);
                }
                else if (cheatNumber == 9 || cheatNumber == 13)
                {
                    //StartCoroutine("PlayBackWardCheating");
                    cheatNumber = random.Next(1, 5);
                    studentAnimator.SetBool("Cheating_" + cheatNumber, true);
                    Debug.Log("Cheating variant is -  " + cheatNumber);
                }
                else if(cheatNumber == 10 || cheatNumber == 12)
                {
                    StartCoroutine("PlaySideCheating");
                }
                else
                {
                    studentAnimator.SetBool("Cheating_" + cheatNumber, true);
                    Debug.Log("Cheating variant is -  " + cheatNumber);
                }
            }
            else
            {
                studentAnimator.SetBool("Study_" + studyNumber, true);
                //Debug.Log("Study variant is -  " + studyNumber);
            }
        }
        
    }

    IEnumerator PlayBackWardCheating()
    {
        if (isThisStudentSeatAtLastTable)
        {
            studentAnimator.SetBool("Cheating_" + (cheatNumber - 5), true);
            Debug.Log("Cheating variant is -  " + (cheatNumber - 1));
        }
        else
        {
            studentAnimator.SetTrigger("BackWard");
            Debug.Log("IsBackWardCheatingStudent");
            cheatType = " ";
            yield return new WaitForSeconds(10f);
            StartCoroutine("PlayBackWardCheating");
        }
    }

    IEnumerator PlaySideCheating()
    {
        if (!isThisStudentNotAloneAtTable)
        {
            studentAnimator.SetBool("Cheating_" + (cheatNumber - 6), true);
            Debug.Log("Cheating variant is -  " + (cheatNumber - 2));
        }
        else
        {
            studentAnimator.SetTrigger("SideCheating");
            Debug.Log("IsSideCheatingStudent");
            cheatType = " ";
            yield return new WaitForSeconds(10f);
            StartCoroutine("PlaySideCheating");
        }
    }
    
    public void ShowPaperWhileStudentWriting()
    {
        penObject.SetActive(true);
    }
    
    public void ShowCheatItem1()
    {
        cheatType = "with a Phone";
        cheatItem1.SetActive(true);
    }
    
    public void ShowCheatItem2()
    {
        cheatType = "with a Cheat sheet";
        cheatItem2.SetActive(true);
    }

    public void ShowCheatItem3()
    {
        cheatType = "with a Book";
        cheatItem3.SetActive(true);
    }

    public void ShowCheatItem4()
    {
        cheatType = "with a Watches";
        cheatItem4.SetActive(true);
    }
    
    public void ShowCheatItem5()
    {
        cheatType = "with a cheat sheet under the leg";
        cheatItem5.SetActive(true);
    }
    
    public void ShowCheatItem6()
    {
        cheatType = "with a Cheat sheet in the books";
        cheatItem6.SetActive(true);
    }
    
    public void ShowCheatItem7Pen()
    {
        cheatType = "with a cheat pen";
        cheatItem7Pen.SetActive(true);
    }

    public void ShowCheatItem8Calculator()
    {
        cheatType = "with a calculator";
        cheatItem8Calculator.SetActive(true);
    }

    public void ShowLookLineBackWard()
    {
        BackwardLookLine.SetActive(true);
    }

    public void ShowLookLineSide()
    {
        SideLookLine.SetActive(true);
    }

    public void HideLookLineBackWard()
    {
        BackwardLookLine.SetActive(false);
    }

    public void HideLookLineSide()
    {
        SideLookLine.SetActive(false);
    }

    public void ShowCatInBackpack()
    {
        catSource.PlayOneShot(catClip);
        catBackpack.SetActive(true);
    }


    public void PlayGoodEmojiEffect()
    {
        Random randomState = new Random();
        var state = randomState.Next(1, 3);
        if (state == 1)
        {
            return;
        }
        else
        {
            Random random = new Random();
            particleSystemsEmojisGood[random.Next(0, particleSystemsEmojisGood.Length)].Play();
            //studentAudioSource.PlayOneShot(emojiClip);  
        }
       
    }

    public void PlayBadEmojiEffect()
    {
        Random randomState = new Random();
        var state = randomState.Next(1, 3);
        if (state == 1)
        {
            return;
        }
        else
        {
            Random random = new Random();
            particleSystemsEmojisBad[random.Next(0, particleSystemsEmojisBad.Length)].Play();
            //studentAudioSource.PlayOneShot(emojiClip);
        }
    }

    public void BackwardStandUp()
    {
        gameObject.transform.DOMove(backwardPoint.position, 2f);
    }

    public void GoOutFromClass()
    {
        HideAllItems();
        door.GetComponent<Animator>().SetBool("Open",true);
        studentAudioSource.PlayOneShot(openDoorClip);
        Invoke("CloseDoor",5f);


        if (isLeftSideStudent)
        {
            gameObject.transform.DOMove(leftPoint.position, timeToWalking).SetEase(Ease.Linear);
            gameObject.transform.DORotate(new Vector3(0, 270, 0), 0.5f).SetEase(Ease.Linear);
            studentAnimator.SetBool("Walking", true);
            studentAnimator.SetBool("Cheating_20",false);
            Destroy(gameObject, timeToWalking);
        }
        else
        {
            gameObject.transform.DOMove(rightPoint.position, timeToWalking).SetEase(Ease.Linear);
            gameObject.transform.DORotate(new Vector3(0, 90, 0), 0.5f).SetEase(Ease.Linear);
            studentAnimator.SetBool("Walking", true);
            studentAnimator.SetBool("Cheating_20",false);
            Destroy(gameObject, timeToWalking);
        }

        if (isCheating)
        {
            textPrefab.GetComponentInChildren<Text>().text = "You founded cheater " + " " + cheatType + "!";
            studentAudioSource.PlayOneShot(notificationClip);
            var createdText = Instantiate(textPrefab);
            Destroy(createdText, 5f);
        }
    }

    public void SeatDown()
    {
        Random random = new Random();
        studentAnimator.SetBool("Study_" + random.Next(1, countOfStudyAnims + 1), false);
        studentAnimator.SetBool("Shooted",false);
        
        studentAudioSource.PlayOneShot(notificationClip);
        textPrefab.GetComponentInChildren<Text>().text = "You pick a wrong student!";
        var createdText = Instantiate(textPrefab);
        Destroy(createdText, 5f);
    }

    public void CloseDoor()
    {
        door.GetComponent<Animator>().SetBool("Open",false);
    }
    
    [ContextMenu("DebugHitToHead")]
    private void DebugHitToHead()
    {
        studentAnimator.SetTrigger("HitToHead");
    }
    [ContextMenu("DebugBackWard")]
    private void DebugBackWard()
    {
        studentAnimator.SetTrigger("BackWard");
    }
    
    [ContextMenu("DebugSide")]
    private void DebugSide()
    {
        studentAnimator.SetTrigger("SideCheating");
    }
    
    public void PlayShootClip()
    {
        studentAudioSource.PlayOneShot(shootedClip);
    }

    public void StopAudioSource()
    {
        //studentAudioSource.Stop();
    }

    public void HideAllItems()
    {
        cheatItem1.SetActive(false);
        cheatItem2.SetActive(false);
        cheatItem3.SetActive(false);
        cheatItem4.SetActive(false);
        cheatItem5.SetActive(false);
        cheatItem6.SetActive(false);
        cheatItem7Pen.SetActive(false);
        cheatItem8Calculator.SetActive(false);
        HideLookLineSide();
        HideLookLineBackWard();
        cheatShelf.transform.parent = null;
        cheatShelf.GetComponent<Animator>().SetBool("Open",true);
        windowCheat.SetActive(false);
        catBackpack.SetActive(false);
    }
}