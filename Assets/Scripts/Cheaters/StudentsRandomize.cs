using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StudentsRandomize : MonoBehaviour
{
    [SerializeField] public List<Student> students = new List<Student>();
    [SerializeField] public int countOfCheatingStudents;
    [SerializeField] public int countOfDefaultStudents;
    [SerializeField] public GameObject[] foundedStudents;
    
    private void Awake()
    {
        foundedStudents = GameObject.FindGameObjectsWithTag("Student");

        for (int i = 0; i < foundedStudents.Length; i++)
        {
            students.Add(foundedStudents[i].GetComponent<Student>());
        }
        
        Random random = new Random();
        for (int i = 0; i < students.Count; i++)
        {
            students[i].stateNumber = random.Next(1, 3);
            students[i].cheatNumber = random.Next(1, students[i].countOfCheatingAnims);
            students[i].studyNumber = random.Next(1, students[i].countOfStudyAnims);
            if (students[i].stateNumber == 1)
            {
                countOfCheatingStudents++;
            }
            else
            {
                countOfDefaultStudents++;
            }
        }
    }

    private void Start()
    {
        
    }
}