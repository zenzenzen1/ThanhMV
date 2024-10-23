using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    public TextMeshProUGUI scoreText;
    int score = 0;
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector2 movementDirection = new Vector2(horizontal, vertical);
        
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        transform.Translate(inputMagnitude * speed * Time.deltaTime * movementDirection, Space.World);
        
        
        var v = GameObject.FindGameObjectsWithTag("cherry").Length;
        if(v == 0){
            PlayerPrefs.SetInt("score", score);
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("cherry"))
        {
            score++;
            scoreText.text = "Score: " + score;
            Destroy(other.gameObject);
        }
    }
}
