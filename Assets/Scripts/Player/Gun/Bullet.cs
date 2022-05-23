using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Rigidbody rb;
    [SerializeField] public StudentsManager studentsManager;
    [SerializeField] public GameObject hitEffect;

    private void Awake()
    {
        studentsManager = FindObjectOfType<StudentsManager>();
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
        }
        else
        {
            Debug.Log("You shoot a student");
            other.gameObject.GetComponent<Student>().studentAnimator.SetBool("Shooted",true);
            var hit = Instantiate(hitEffect, gameObject.transform.position,Quaternion.identity);
            Destroy(hit,1.5f);
        }
    }
}