using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StudentsManager : MonoBehaviour
{
    
    [Header("Counter")]
    [SerializeField] public List<Student> students = new List<Student>();
    [SerializeField] public List<Student> cheaters = new List<Student>();
    
    [SerializeField] public int countOfCheatingStudents;
    [SerializeField] public int countOfDefaultStudents;
    [SerializeField] public GameObject[] foundedStudents;

    [Header("UI")] 
    [SerializeField] private Text cheatersCount;

    [SerializeField] public int currentFoundedCheaters;

    [Header("WinLoseCondition")]
    [SerializeField] public PlayerRaycast player;
    [SerializeField] public PlayerCamera playerCamera;
    
    [SerializeField] private GameObject endGameCamera;
    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private Transform startCameraPos, endCameraPosition;
    [SerializeField] private Animator teacherAnimator;
    [SerializeField] private GameObject[] itemsToDeactivate;
    [SerializeField] private GameObject[] itemsToActivate;
    [SerializeField] private ParticleSystem winEmotion;
    [SerializeField] private ParticleSystem winConfetti1, winConfetti2;
    [SerializeField] private ParticleSystem loseEmotion;

    [SerializeField] public List<GameObject> currentNonFoundedCheaters;
    [SerializeField] public int countOfNonFoundedCheaters;
    [SerializeField] private GameObject uiNotePrefab;
    [SerializeField] private Transform notesLayoutGroup;

    [SerializeField] private GameObject playerObject;
    [SerializeField] private Transform startPlayerPos;

    [Header("StartAnimationsTimings")] 
    [SerializeField] private int minTimeToStart;
    [SerializeField] private int maxTimeToStart;

    [Header("Audio")]
    [SerializeField] private AudioSource playerSource;

    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;

    [Header("ProgressBar")] 
    [SerializeField] private LevelProgress levelProgressBar;


    private void Awake()
    {
       // playerObject = GameObject.FindGameObjectWithTag("Player");
       // startPlayerPos = playerObject.GetComponent<Transform>();
        playerCamera = FindObjectOfType<PlayerCamera>();
        foundedStudents = GameObject.FindGameObjectsWithTag("Student");
        player = FindObjectOfType<PlayerRaycast>();

        for (int i = 0; i < foundedStudents.Length; i++)
        {
            if (foundedStudents[i].GetComponent<Student>().isCheating)
            {
                cheaters.Add(foundedStudents[i].GetComponent<Student>());
                countOfCheatingStudents++;
                var note = Instantiate(uiNotePrefab);
                note.transform.parent = notesLayoutGroup;
                currentNonFoundedCheaters.Add(note);
                countOfNonFoundedCheaters++;
            }
            else
            {
                students.Add(foundedStudents[i].GetComponent<Student>());
                countOfDefaultStudents++;
            }
        }
    }

    private void Start()
    {
        Random random = new Random();
        for (int i = 0; i < students.Count; i++)
        {
            //students[i].stateNumber = random.Next(1, 3);
            //cheaters[i].cheatNumber = random.Next(11,11);

            int randomCheat = random.Next(1, cheaters[i].countOfCheatingAnims + 1);
            cheaters[i].cheatNumber = randomCheat;

            int randomStudy = random.Next(1, students[i].countOfStudyAnims + 1);
            students[i].studyNumber = randomStudy;
            
           // cheaters[i].cheatNumber = random.Next(1, cheaters[i].countOfCheatingAnims + 1);
           // students[i].studyNumber = random.Next(1, students[i].countOfStudyAnims + 1);

            cheaters[i].timeToStartAnimations = random.Next(minTimeToStart, maxTimeToStart);
            students[i].timeToStartAnimations = random.Next(minTimeToStart, maxTimeToStart);
        }
    }

    public void ShowHowCheatersNowYouBusted()
    {
        cheatersCount.text = "Found cheaters" + " " +":" + " " + currentFoundedCheaters + "/" + countOfCheatingStudents;
    }

    public void Lose()
    {
        player.gunObject.SetActive(false);
        Invoke("LoseAnimation",4.5f);
    }

    public void Win()
    {
        player.gunObject.SetActive(false);
        Invoke("WinAnimation",4.5f);
    }

    public void LoseAnimation()
    {
        playerObject.transform.position = startPlayerPos.position;
        playerObject.transform.rotation = startPlayerPos.rotation;
        
        endGameCamera.SetActive(true);
        for (int i = 0; i < itemsToDeactivate.Length; i++)
        {
            itemsToDeactivate[i].SetActive(false);
        }
        for (int i = 0; i < itemsToActivate.Length; i++)
        {
            itemsToActivate[i].SetActive(true);
        }
        teacherAnimator.SetBool("Lose",true);
        loseEmotion.Play();
        endGameCamera.transform.DOMove(endCameraPosition.position, 3f);
        playerSource.PlayOneShot(loseClip);
        Invoke("RestartCurrentLevel",3f);
    }

    public void WinAnimation()
    {
        levelProgressBar.OpenBar();
        playerObject.transform.position = startPlayerPos.position;
        playerObject.transform.rotation = startPlayerPos.rotation;
        
        endGameCamera.SetActive(true);
        for (int i = 0; i < itemsToDeactivate.Length; i++)
        {
            itemsToDeactivate[i].SetActive(false);
        }

        for (int i = 0; i < itemsToActivate.Length; i++)
        {
            itemsToActivate[i].SetActive(true);
        }
        teacherAnimator.SetBool("Win",true);
        winEmotion.Play();
        winConfetti1.Play();
        winConfetti2.Play();
        endGameCamera.transform.DOMove(endCameraPosition.position, 3f);
        playerSource.PlayOneShot(winClip);
        Invoke("LoadNextLevel",3f);
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);    
        }
        
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [ContextMenu("TestWin")]
    public void TestCamera()
    {
        WinAnimation();
    }
}