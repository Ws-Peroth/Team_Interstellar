using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    enum SceneList
    {
        FeedbackTitleScene = 2,
        FeedbackMainGameScene
    }
    [SerializeField] private Sprite[] slimeSprites = new Sprite[2];
    [SerializeField] private SpriteRenderer slimeRenderer;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject startButtonLabel;

    // fadeSpeed : 1초 동안에 변화할 alpha 값의 변화량
    // 0 ~ 1 범위 내의 수
    [SerializeField] private float _fadeSpeed = 0.5f;
    private Sprite startButtonLabelRenderer; 


    // Start is called before the first frame update
    void Start()
    {
        startButtonLabelRenderer = startButtonLabel.GetComponent<Sprite>();

        // 버튼을 클릭하였을 경우에 isStarted를 true로 변경
        startButton.onClick.AddListener(() => 
            {
                // isStarted가 눌러지면 Scene 교체 효과 시작
                StartCoroutine(SceneChangeEffect());
            });

        // 라벨이 페이드 효과를 반복하도록 하는 코루틴 실행
        // Todo: Study about "Coroutine"
        StartCoroutine(LoopingFadeEffect(startButtonLabelRenderer));
    }

    IEnumerator SceneChangeEffect(){
        // Scene 전환 효과 속도를 0.75로 설정
        var sceneChangeEffectSpeed = 0.75f;

        // 화면 전체를 덮고있는 startButton의 이미지 컴포넌트를 가져옴
        var startButtonRenderer = startButton.gameObject.GetComponent<Sprite>();

        // StartButton의 색을 검정(0,0,0)에 alpha가 0(투명한)인 색으로 지정
        startButtonRenderer.color = new Color(0, 0, 0, 0);

        // FadeIn 효과를 시작
        StartCoroutine(FadeIn(startButtonRenderer, sceneChangeEffectSpeed));
        
        // FadeIn 효과가 끝날때 까지 대기
        yield return WaitForSeconds(1 / sceneChangeEffectSpeed);

        // FadeIn효과가 끝나면 scene을 전환
        SceneManager.LoadScene((int)SceneList.FeedbackTitleScene);
    }

    // Sprite나 Image은 Texture2D를 상속받고 있다.
    IEnumerator FadeOut(Sprite texture, float fadeSpeed = 0){

        // 매개변수로 주어진 fadeSpeed이 0이면 멤버변수인 _fadeSpeed를 fadeSpeed로 사용하고,
        // fadeSpeed가 0이 아니라면 매개변수로 주어진 fadeSpeed를 그대로 이용한다.
        fadeSpeed = fadeSpeed == 0 ? _fadeSpeed : fadeSpeed;

        // 텍스쳐 Fade out
        // 텍스쳐의 alpha값이 0 보다 크면 반복
        while (texture.color.a > 0)
        {
            // 텍스쳐의 색을 가져옴
            var tempColor = texture.color;

            // 텍스쳐의 alpha값을 서서히 0으로 만듦
            // fadeSpeed와 시간 변화량인 Time.deltaTime 을 곱하여 변화량을 구한다
            // distance = speed * time 과 동일한 원리
            // distance 가 시간에 따른 alpha 값의 변화량이 되었을 뿐

            // 현재 색 - fadeSpeed * deltaTime와 0을 비교하여 더 큰 값을 현재의 색으로 정함
            // 최소값이 0임으로 -n의 값을 가지지 않도록 처리
            tempColor.a = Mathf.Max(0, tempColor.a - (fadeSpeed * Time.deltaTime));

            // 변경된 색을 현재 텍스쳐에 대입
            texture.color = tempColor;

            // 1프레임 대기
            yield return null;
        }
    }

    IEnumerator FadeIn(Sprite texture, float fadeSpeed = 0){
        // 매개변수로 주어진 fadeSpeed이 0이면 멤버변수인 _fadeSpeed를 fadeSpeed로 사용하고,
        // fadeSpeed가 0이 아니라면 매개변수로 주어진 fadeSpeed를 그대로 이용한다.
        fadeSpeed = fadeSpeed == 0 ? _fadeSpeed : fadeSpeed;

        // 텍스쳐 Fade In
        // 텍스쳐의 alpha값이 1 보다 작으면 반복
        while (texture.color.a < 1)
        {
            // 텍스쳐의 색을 가져옴
            var tempColor = texture.color;

            // 텍스쳐의 alpha값을 서서히 1로 만듦
            // fadeSpeed와 시간 변화량인 Time.deltaTime 을 곱하여 변화량을 구한다
            // distance = speed * time 과 동일한 원리
            // distance 가 시간에 따른 alpha 값의 변화량이 되었을 뿐

            // 현재 색 + fadeSpeed * deltaTime와 1을 비교하여 더 작은 값을 현재의 색으로 정함
            // 최대값이 1임으로 1.n 이상의 값을 가지지 않도록 처리
            tempColor.a = Mathf.Min(1, tempColor.a + (fadeSpeed * Time.deltaTime));

            // 변경된 색을 현재 텍스쳐에 대입
            texture.color = tempColor;

            // 1프레임 대기
            yield return null;
        }
    }

    IEnumerator LoopingFadeEffect(Sprite texture, float fadeSpeed = 0, bool isFadeInFirst = true){
        // 매개변수로 주어진 fadeSpeed이 0이면 멤버변수인 _fadeSpeed를 fadeSpeed로 사용하고,
        // fadeSpeed가 0이 아니라면 매개변수로 주어진 fadeSpeed를 그대로 이용한다.
        fadeSpeed = fadeSpeed == 0 ? _fadeSpeed : fadeSpeed;

        // 텍스쳐의 색을 설정하기 위해, 현재 텍스쳐의 색을 가져옴
        var settingColor = texture.color;
        // settingColor의 alpha색을 isFadeInFirst에 따라,
        // FadeIn을 먼저 할거면 alpha를 0으로,
        // fadeOut을 먼저 할거면 alpha를 1로 바꾼다
        settingColor.a = isFadeInFirst ? 0 : 1;

        // settingColor의 값을 텍스쳐의 색에 대입한다
        texture.color = settingColor;

        while(true){
            // 값 변경 전(완료 후) 대기 시간
            yield return new WaitForSeconds(0.5f);

            // alpha값이 1이면, FadeOut을 호출
            if(texture.color.a == 1){
                StartCoroutine(FadeIn(texture));
            }
            // 아니면 FadeOut을 호출
            else{
                StartCoroutine(FadeOut(texture));
            }
            // 코루틴이 종료될 때 까지 대기
            // 1초에 alpha값을 fadeSpeed 만큼 변경
            // alpha값을 전부 변경하는데 걸리는 시간 t는 1 / fadeSpeed
            // ex:  (1 / 1) / ( 1 / 2) = 2 / 1 = 2
            //      fadeSpeed가 0.5일때, t = 2
            yield return new WaitForSeconds(1 / fadeSpeed);

        }

        // isStarted가 true가 되면 이 코루틴은 종료됨
        // 코루틴 종료 구문
        yield break;
    }
}
