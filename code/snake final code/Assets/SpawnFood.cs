using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpawnFood : MonoBehaviour
{
    // Food Prefab
    public GameObject foodPrefab;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;
    public float food_speed = 1f;
    public Slider foodslider;
    public Text slider_text;
    public float spd;
    // Use this for initialization
    void Start()
    {
        foodslider.onValueChanged.AddListener(delegate {
          ChangeCheck();
            CancelInvoke();
            
            InvokeRepeating("Spawn", 0.3f, food_speed);
            slider_text.text = spd.ToString("0.00");
        });
        InvokeRepeating("Spawn", 0.3f, food_speed);

    }
    public void ChangeCheck()
    {

        spd = foodslider.value;
        Debug.Log("updated food speed : " + food_speed);
        food_speed = (11f - foodslider.value) * 0.1f;
    }
        // Spawn one piece of food
        void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
    }
}