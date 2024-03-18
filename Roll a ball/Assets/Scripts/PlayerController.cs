using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private int count;
    public TextMeshProUGUI countText;
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private Timer timer;
    private float loseTime = 0;
    private bool loseTimeIsRunning = false;
    private float winTime = 0;
    private bool winTimeIsRunning = false;
    public AudioSource collectiblesSound;
    public AudioSource winSound;
    public AudioSource loseSound;
    public AudioSource backgroundMusic;
    
    void Awake()
    {
        // Obtém a referência ao script Timer
        timer = FindObjectOfType<Timer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        count = 0; 
        rb = GetComponent<Rigidbody>();
        SetCountText();
        loseTextObject.SetActive(false);
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
        if (count >= 20)
        {
            winSound.Play();
            winTextObject.SetActive(true);
            timer.timerIsRunning = false;
            winTimeIsRunning = true;
            backgroundMusic.Stop();
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other) 
    {
    if (!loseTextObject.activeSelf && !winTextObject.activeSelf)
    {
        if (other.gameObject.CompareTag("PickUp")) 
       {
        collectiblesSound.Play();
        other.gameObject.SetActive(false);
        count = count + 1;
        SetCountText();
       }
    }
}   

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!loseTextObject.activeSelf && !winTextObject.activeSelf)
            {
            loseSound.Play();
            loseTextObject.SetActive(true);
            timer.timerIsRunning = false;
            loseTimeIsRunning = true;
            backgroundMusic.Stop();
            }
        }
    }

    void Update()
    {
        // Verifica se o tempo acabou no script Timer e ativa o texto "Lose" se necessário
        if (timer.timeRemaining <= 0 && !loseTimeIsRunning)
        {
            loseSound.Play();
            loseTextObject.SetActive(true);
            loseTimeIsRunning = true;
            backgroundMusic.Stop();

        }
        if (loseTimeIsRunning){
            loseTime += Time.deltaTime;
            if (loseTime >= 3){
                SceneManager.LoadScene("MainMenu");
            }
        }
        
        if (winTimeIsRunning){
            winTime += Time.deltaTime;
            if (winTime >= 3){
                SceneManager.LoadScene("MainMenu");
            }
        }

    }

    

}
