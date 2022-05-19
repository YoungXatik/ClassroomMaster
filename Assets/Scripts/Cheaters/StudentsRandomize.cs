using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StudentsRandomize : MonoBehaviour
{
    [SerializeField] public List<Student> students = new List<Student>();
    [SerializeField] public int countOfCheatingStudents;
    [SerializeField] public int countOfDefaultStudents;

    private void Awake()
    {
        Random random = new Random();
        for (int i = 0; i < students.Count; i++)
        {
            students[i].stateNumber = random.Next(1, 2);
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