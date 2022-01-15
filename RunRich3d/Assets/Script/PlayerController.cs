using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

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
    [SerializeField] private Material myMaterial;

    public GameObject uiObject_Rich;
    public GameObject uiObject_Good;
    public GameObject uiObject_Poor;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth_UI = currentHealth;
        uiObject_Rich.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
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

        if (currentHealth >= 60 & currentHealth <= 100)
        {
            myMaterial.color = Color.green;
            uiObject_Rich.SetActive(true);
            uiObject_Good.SetActive(false);
            uiObject_Poor.SetActive(false);
        }
        
        if (currentHealth >= 30 & currentHealth < 60)
        {
            myMaterial.color = Color.yellow;
            uiObject_Good.SetActive(true);
            uiObject_Rich.SetActive(false);
            uiObject_Poor.SetActive(false);
        }
        
        if (currentHealth > 0 & currentHealth < 30)
        {
            myMaterial.color = Color.red;
            uiObject_Poor.SetActive(true);
            uiObject_Rich.SetActive(false);
            uiObject_Good.SetActive(false);
        }
        

        
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

        if (other.gameObject.CompareTag("Door"))
        {
            GainMoney(10);
            Destroy(other.GetComponent<BoxCollider>());
        }

        if (other.gameObject.CompareTag("Door1"))
        {
            LostMoney(10);
            Destroy(other.GetComponent<BoxCollider>());
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
