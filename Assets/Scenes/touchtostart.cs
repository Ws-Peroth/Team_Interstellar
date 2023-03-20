using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SceneManager을 사용하기 위해서는 아래의 namespace를 추가 해 주어야 합니다
using UnityEngine.SceneManagement;

public class touchtostart : MonoBehaviour
{
    public int count;

    // 이왕이면 멤버 변수 이름은 이름만 보고 어떤 오브젝트인지 알 수 있도록 지어줍시다
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
        // Touch to Start 화면에서 클릭하는 부분은...
        // ui에 GameObject보다는 UI의 Button을 이용해서 다시 해 봅시다
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

        // 화면을 여러번 클릭하였을 경우에는 count가 2, 3... 등등이 되어 작동이 안 될 수 있어보입니다
        // count를 int 대신 bool isClicked 등으로 선언하여 확인하는 편이 나아보입니다
        if (count == 1)
        {
            Color newColor = Touchtostart.color;
            newColor.a -= fadeSpeed * Time.deltaTime;
            Touchtostart.color = newColor;
            
            // 위에서는 SpriteRenderer인 TouchtoStart 에서 색을 잘 가져오셨는데
            // 어쩌다 아래에서는 갑자기 gameObject에서 가져오신겁니까 선생님...
            // Color 멤버는 GameObject가 아닌 SpriteRenderer 컴포넌트에 있습니다
            /*
            if(gameObject1.color.a <= 0)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene(1);
            }
            */
            if(Touchtostart.color.a <= 0)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene(1);
            }

            gameObject.SetActive(false);

            // 무엇보다, gameObject1이 표시되는 글자인듯 한데...
            // 클릭하자 말자 setActive를 false로 하시면 클릭하자망자 곧바로 안보여집니다...
            gameObject1.SetActive(false);
            gameObject2.SetActive(true);
        }

    }
}