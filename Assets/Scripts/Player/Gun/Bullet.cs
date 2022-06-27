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
    [SerializeField] private GameObject bullet;
    [SerializeField] private Vector3 rotationAngle;

    private void Awake()
    {
        studentsManager = FindObjectOfType<StudentsManager>();
        player = FindObjectOfType<PlayerRaycast>();
        rb = GetComponent<Rigidbody>();
    }

    
    private void Start()
    {
        //Destroy(gameObject, 3f);
        //bullet.transform.DORotate(new Vector3(1800f, 0f, 0f), 3f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);
        bullet.transform.Rotate(rotationAngle);
        Destroy(gameObject,4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Student>().isCheating)
        {
            Student currentShootedStudent = other.gameObject.GetComponent<Student>();
            studentsManager.currentFoundedCheaters++;
            currentShootedStudent.StopAllCoroutines();
            currentShootedStudent.studentAnimator.SetBool("Shooted",true);
            currentShootedStudent.PlayShootClip();
            currentShootedStudent.studentAnimator.SetBool("Cheating_20",false);
            currentShootedStudent.studentAnimator.SetBool("Cheating_22",false);
            HitToHead(currentShootedStudent);
            currentShootedStudent.GetComponent<BoxCollider>().enabled = false;
            var hit = Instantiate(hitEffect, gameObject.transform.position,Quaternion.identity);
            Destroy(hit,1.5f);
            studentsManager.ShowHowCheatersNowYouBusted();
            studentsManager.currentNonFoundedCheaters[studentsManager.countOfNonFoundedCheaters - 1].GetComponent<CheaterUICounterNote>().ActivateNoteImage();
            studentsManager.countOfNonFoundedCheaters--;
            
            if (studentsManager.currentFoundedCheaters == studentsManager.countOfCheatingStudents)
            {
                player.cameraButtonBackGround.sprite = player.nonInteractableButtonSprite;
                player.fButtonBackGround.sprite = player.nonInteractableButtonSprite;
                player.shootButton.interactable = false;
                player.playerCamera.openButton.GetComponent<Button>().interactable = false;
                player.playerCamera.closeButton.GetComponent<Button>().interactable = false;
                //player.timeBetweenShootCounter = 0;
                studentsManager.Invoke("Win", 2.5f);
            }
        }
        else
        {
            Student currentShootedStudent = other.gameObject.GetComponent<Student>();
            currentShootedStudent.GetComponent<BoxCollider>().enabled = false;
            currentShootedStudent.PlayShootClip();
            other.gameObject.GetComponent<Student>().studentAnimator.SetBool("Shooted",true);
            var hit = Instantiate(hitEffect, gameObject.transform.position,Quaternion.identity);
            Destroy(hit,1.5f);
            player.countOfMistakes -= 1;
            if (player.countOfMistakes <= 0)
            {
                player.cameraButtonBackGround.sprite = player.nonInteractableButtonSprite;
                player.fButtonBackGround.sprite = player.nonInteractableButtonSprite;
                player.shootButton.interactable = false;
                player.playerCamera.openButton.GetComponent<Button>().interactable = false;
                player.playerCamera.closeButton.GetComponent<Button>().interactable = false;
                //player.timeBetweenShootCounter = 0;
                studentsManager.Invoke("Lose", 2.5f);
            }
        }
        Destroy(gameObject);
    }
[ContextMenu("TestHit")]
    public void HitToHead(Student currentStudent)
    {
        currentStudent.studentAnimator.SetTrigger("HitToHead");
    }
}