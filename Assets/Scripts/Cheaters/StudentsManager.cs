using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

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

    private void Awake()
    {
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
        cheatersCount.text = "Fount cheaters" + ":" + currentFoundedCheaters.ToString() + "/" + countOfCheatingStudents;

        if (currentFoundedCheaters == countOfCheatingStudents)
        {
            Debug.Log("Win!");
        }
    }
}