using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Rigidbody rb;

    private void Start()
    {
        //rb.AddForce(Vector3.forward * speed,ForceMode.Impulse);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Student>().isCheating)
        {
            Debug.Log("You shoot a cheater");
            other.gameObject.GetComponent<Student>().studentAnimator.SetBool("Shooted",true);
        }
        else
        {
            Debug.Log("You shoot a student");
            other.gameObject.GetComponent<Student>().studentAnimator.SetBool("Shooted",true);
        }
    }
}