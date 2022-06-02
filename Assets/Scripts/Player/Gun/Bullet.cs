using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public StudentsManager studentsManager;
    [SerializeField] public PlayerRaycast player;
    [SerializeField] public GameObject hitEffect;
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        studentsManager = FindObjectOfType<StudentsManager>();
        player = FindObjectOfType<PlayerRaycast>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Student>().isCheating)
        {
            studentsManager.currentFoundedCheaters++;
            other.gameObject.GetComponent<Student>().studentAnimator.SetBool("Shooted",true);
            var hit = Instantiate(hitEffect, gameObject.transform.position,Quaternion.identity);
            Destroy(hit,1.5f);
            studentsManager.ShowHowCheatersNowYouBusted();
            if (studentsManager.currentFoundedCheaters == studentsManager.countOfCheatingStudents)
            {
                player.shootButton.interactable = false;
                player.playerCamera.openButton.GetComponent<Button>().interactable = false;
                player.playerCamera.closeButton.GetComponent<Button>().interactable = false;
                player.timeBetweenShootCounter = 0;
                studentsManager.Invoke("Win", 2.5f);
            }
        }
        else
        {
            other.gameObject.GetComponent<Student>().studentAnimator.SetBool("Shooted",true);
            var hit = Instantiate(hitEffect, gameObject.transform.position,Quaternion.identity);
            Destroy(hit,1.5f);
            player.countOfMistakes -= 1;
            if (player.countOfMistakes <= 0)
            {
                player.shootButton.interactable = false;
                player.playerCamera.openButton.GetComponent<Button>().interactable = false;
                player.playerCamera.closeButton.GetComponent<Button>().interactable = false;
                player.timeBetweenShootCounter = 0;
                studentsManager.Invoke("Lose", 2.5f);
            }
        }
        Destroy(gameObject);
    }
}