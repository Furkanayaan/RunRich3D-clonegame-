using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float m_Speed = 8f;
    private Touch touch;
    
    public float xAxis;
    

    public GameObject starSplash;

    public GameObject wineSplash;

    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;
    
    public int currentHealth_UI;

    private bool isColliding;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth_UI = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log(123456);
            touch = Input.GetTouch(0);
            xAxis += touch.deltaPosition.x;
        }
        xAxis += Input.GetAxis("Horizontal") * Time.deltaTime * 10f;
        transform.position = new Vector3(xAxis, transform.position.y, transform.position.z) + (Vector3.forward * Time.deltaTime * m_Speed); 
        xAxis = Mathf.Clamp(xAxis, -4f, 4f);
        
        
        
        
        

        if (currentHealth_UI < currentHealth)
        {
            currentHealth_UI++;
        }
        if (currentHealth_UI > currentHealth)
        {
            currentHealth_UI--;
        }
        
        healthBar.SetHealth(currentHealth_UI);
        

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isColliding) return;
        isColliding = true;
        
        StartCoroutine(Reset());
        
        if (other.gameObject.CompareTag("Coins"))
        {
            GameObject copy = Instantiate(starSplash, transform.position, Quaternion.identity);
            
            Destroy(other.gameObject, 0f);
            GainMoney(20);
            Destroy(copy, 2f);
            Debug.Log(123);
            

        }
        if (other.gameObject.CompareTag("Wine"))
        {
            GameObject copy = Instantiate(wineSplash, transform.position, Quaternion.identity);
            
            Destroy(other.gameObject, 0f);
            LostMoney(20);
            Destroy(copy, 2f);
            Debug.Log(456);
            
        }
        
    }
    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
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
