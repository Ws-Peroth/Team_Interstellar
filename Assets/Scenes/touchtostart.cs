using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchtostart : MonoBehaviour
{
    public int count;
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;
    public float fadeSpeed = 1f;
    private SpriteRenderer Touchtostart;

    private void Start()
    {
        gameObject1.SetActive(true);
        count = 0;
        Touchtostart = gameObject1.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.name == "touch to start")
                {
                    count++;
                }
            }
        }
        if (count == 1)
        {
            Color newColor = Touchtostart.color;
            newColor.a -= fadeSpeed * Time.deltaTime;
            Touchtostart.color = newColor;
                if(gameObject1.color.a <= 0)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene(1);
            }
            gameObject.SetActive(false);
            gameObject1.SetActive(false);
            gameObject2.SetActive(true);
        }

    }
}