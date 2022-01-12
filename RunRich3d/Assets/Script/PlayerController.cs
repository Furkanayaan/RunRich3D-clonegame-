using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float m_Speed = 5f;
    private Touch touch;
    
    public float xAxis;
    

    public GameObject starSplash;

    public GameObject wineSplash;

    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;
    
    public int currentHealth_UI;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth_UI = currentHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            xAxis += touch.deltaPosition.x;
        }
        transform.position = new Vector3(transform.position.x + xAxis, transform.position.y, transform.position.z);
        xAxis = Mathf.Clamp(xAxis, -5f, +5f);
        transform.position += transform.forward * Time.deltaTime * m_Speed;
        
        

        if (currentHealth_UI < currentHealth)
        {
            currentHealth_UI++;
        }
        if (currentHealth_UI > currentHealth)
        {
            currentHealth_UI--;
        }
        

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            GameObject copy = Instantiate(starSplash, transform.position, Quaternion.identity);
            Destroy(copy, 2f);
            Destroy(other.gameObject, 0f);
            GainMoney(20);

        }
        if (other.gameObject.CompareTag("Wine"))
        {
            GameObject copy = Instantiate(wineSplash, transform.position, Quaternion.identity);
            Destroy(copy, 2f);
            Destroy(other.gameObject, 0f);
            LostMoney(20);
        }
    }

    void LostMoney(int damage)
    {
        currentHealth -= damage;
    }

    void GainMoney(int healing)
    {
        currentHealth += healing;
    }
    
    
}
