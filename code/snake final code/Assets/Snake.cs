using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Snake : MonoBehaviour
{
    public Text slider_text;
    public Slider slider;
    // Current Movement Direction
    // (by default it moves to the right)
    Vector2 dir;
    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();
    // Did the snake eat something?
    public AudioSource snakeeatsound;
    public AudioSource gameoversound;
    bool ate = false;
    public GameObject snake;
    // Tail Prefab
    public GameObject tailPrefab;
    public GameObject gamePanel;
    Vector2 a;
    public float spd;
    public float snake_speed = 0.3f;
    // Use this for initialization
   public void Start()
    {
        // Move the Snake every 300ms
        dir = Vector2.right;
        slider.onValueChanged.AddListener(delegate {
            ValueChangeCheck();
            CancelInvoke();

            InvokeRepeating("Move", 0.6f, snake_speed);
            slider_text.text = spd.ToString("0.00");
        });
        InvokeRepeating("Move", 0.6f, snake_speed);
    }
    public void ValueChangeCheck()
    {
       spd = slider.value;
        snake_speed = (10.1f - slider.value) * 0.01f;
        //Debug.Log("changed speed from slider  " + a);

    }

    // Update is called once per frame
    // Update is called once per Frame
    void Update()
    {
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //SoundManagerScript.PlaySound("controlclick");
            if (dir != Vector2.left)
                dir = Vector2.right;

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
           // SoundManagerScript.PlaySound("controlclick");
            if (dir != Vector2.right)
                dir = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //SoundManagerScript.PlaySound("controlclick");
            if (dir != Vector2.down)
                dir = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //SoundManagerScript.PlaySound("controlclick");
            if (dir != Vector2.up)
                dir = Vector2.down;
        }
    }

    void Move()
    {

        this.transform.Translate(dir);
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        
   
        // Ate something? Then insert new Element into gap
        if (ate)
        {
            snakeeatsound.Play();
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                  a,
                                                  Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = a;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
        a= v;

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith("FoodPrefab"))
        {
          
            // Get longer in next Move call
            ate = true;
           
            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else if(coll.tag=="obstacle")
        {
            gameoversound.Play();
            //SoundManagerScript.PlaySound("gameover");
            // ToDo 'You lose' screen
            gamePanel.SetActive(true);
            Destroy(snake);
            

        }
      
    }

}    
