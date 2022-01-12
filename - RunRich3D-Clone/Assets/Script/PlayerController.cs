using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float m_Speed = 5f;
    public Joystick joystick;

    public GameObject starSplash;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 m_Input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        rb.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);

        if (m_Input != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(m_Input);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Instantiate(starSplash, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
