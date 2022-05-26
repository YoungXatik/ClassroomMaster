using System;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Rigidbody rb;
    [SerializeField] public StudentsManager studentsManager;
    [SerializeField] public PlayerRaycast player;
    [SerializeField] public GameObject hitEffect;

    private void Awake()
    {
        studentsManager = FindObjectOfType<StudentsManager>();
        player = FindObjectOfType<PlayerRaycast>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Student>().isCheating)
        {
            studentsManager.currentFoundedCheaters++;
            other.gameObject.GetComponent<Student>().studentAnimator.SetBool("Shooted",true);
            var hit = Instantiate(hitEffect, gameObject.transform.position,Quaternion.identity);
            Destroy(hit,1.5f);
            if (studentsManager.currentFoundedCheaters == studentsManager.countOfCheatingStudents)
            {
                player.shootButton.interactable = false;
                player.playerCamera.openButton.GetComponent<Button>().interactable = false;
                player.playerCamera.closeButton.GetComponent<Button>().interactable = false;
                player.timeBetweenShootCounter = 0;
                studentsManager.Invoke("Win", 2f);
            }
        }
        else
        {
            other.gameObject.GetComponent<Student>().studentAnimator.SetBool("Shooted",true);
            var hit = Instantiate(hitEffect, gameObject.transform.position,Quaternion.identity);
            Destroy(hit,1.5f);
            FindObjectOfType<PlayerRaycast>().countOfMistakes -= 1;
            if (player.countOfMistakes <= 0)
            {
                player.shootButton.interactable = false;
                player.playerCamera.openButton.GetComponent<Button>().interactable = false;
                player.playerCamera.closeButton.GetComponent<Button>().interactable = false;
                player.timeBetweenShootCounter = 0;
                studentsManager.Invoke("Lose", 2f);
            }
        }
        Destroy(gameObject);
    }
}