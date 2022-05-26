using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using DG.Tweening;

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
    [SerializeField] private ParticleSystem winEmotion;
    [SerializeField] private ParticleSystem loseEmotion;

    private void Awake()
    {
        playerCamera = FindObjectOfType<PlayerCamera>();
        foundedStudents = GameObject.FindGameObjectsWithTag("Student");

        for (int i = 0; i < foundedStudents.Length; i++)
        {
            if (foundedStudents[i].GetComponent<Student>().isCheating)
            {
                cheaters.Add(foundedStudents[i].GetComponent<Student>());
            }
            else
            {
                students.Add(foundedStudents[i].GetComponent<Student>());
            }
        }

        Random random = new Random();
        for (int i = 0; i < students.Count; i++)
        {
            //students[i].stateNumber = random.Next(1, 3);
            cheaters[i].cheatNumber = random.Next(1, cheaters[i].countOfCheatingAnims + 1);
            students[i].studyNumber = random.Next(1, students[i].countOfStudyAnims + 1);
            cheaters[i].timeToStartAnimations = random.Next(1, 3);
            students[i].timeToStartAnimations = random.Next(1, 3);
        }
    }

    private void Start()
    {
        countOfCheatingStudents = cheaters.Count;
        countOfDefaultStudents = students.Count;
    }

    private void Update()
    {
        cheatersCount.text = "Found cheaters" + ":" + currentFoundedCheaters.ToString() + "/" + countOfCheatingStudents;
    }

    public void Lose()
    {
        player.gunObject.SetActive(false);
        endGameScreen.GetComponentInChildren<Text>().text = "YOU`VE LOSE";
        var endScreen = Instantiate(endGameScreen);
        Destroy(endScreen,4f);
        Invoke("LoseAnimation",3f);
    }

    public void Win()
    {
        player.gunObject.SetActive(false);
        endGameScreen.GetComponentInChildren<Text>().text = "YOU`VE WIN";
        var endScreen = Instantiate(endGameScreen);
        Destroy(endScreen,4f);
        Invoke("WinAnimation",3f);
    }

    public void LoseAnimation()
    {
        endGameCamera.SetActive(true);
        for (int i = 0; i < itemsToDeactivate.Length; i++)
        {
            itemsToDeactivate[i].SetActive(false);
        }
        teacherAnimator.SetBool("Lose",true);
        loseEmotion.Play();
        endGameCamera.transform.DOMove(endCameraPosition.position, 3f);
    }

    public void WinAnimation()
    {
        endGameCamera.SetActive(true);
        for (int i = 0; i < itemsToDeactivate.Length; i++)
        {
            itemsToDeactivate[i].SetActive(false);
        }
        teacherAnimator.SetBool("Win",true);
        winEmotion.Play();
        endGameCamera.transform.DOMove(endCameraPosition.position, 3f);
    }
}