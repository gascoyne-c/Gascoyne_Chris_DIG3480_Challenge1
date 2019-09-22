using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //[SerializeField] private string Level2;

    public float speed;
    public Text countText;
    public Text count2Text;
    public Text winText;
    public Text loseText;
    public Text livesText;
    public Text retryText;

    private Rigidbody2D rb2d;
    private int count;
    private int count2;
    private int lives;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        count2 = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        retryText.text = "";
        SetCountText();
        //Scene currentScene = SceneManager.GetActiveScene();
        //string sceneName = currentScene.name;
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Pickup2"))
        {
            other.gameObject.SetActive(false);
            count2 = count2 + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy2"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetCountText();
        }

    }

    void SetCountText()
    {
        countText.text = "Gold: " + count.ToString();
        if (count >= 12)
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        if (count < 0)
        {
            retryText.text = "Press R to restart!";
        }

        count2Text.text = "Gems: " + count2.ToString();
        if (count2 >= 12)
        {
            winText.text = "Good job! Made by Chris Gascoyne";
        }

        livesText.text = "Lives: " + lives.ToString();
        if (lives < 1)
        {
            loseText.text = "...Yikes";
            Destroy(gameObject);
        }
    }

}
