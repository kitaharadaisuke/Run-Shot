using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI conboText;
    [SerializeField] TextMeshProUGUI defeatText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Image rankS;
    [SerializeField] Image rankA;
    [SerializeField] Image rankB;
    [SerializeField] Image rankC;
    [SerializeField] AudioClip submitSe;

    GameInput gameInput;
    AudioSource audioSource;

    int maxConbo;
    int maxDefeat;
    int conboScore = 0;
    int defeatScore = 0;
    int timeScore;
    int resultScore;
    float clearTime;

    bool isFade = false;
    bool isEnabled = false;

    void Awake() => gameInput = new GameInput();
    void OnEnable() => gameInput.Enable();
    void OnDisable() => gameInput.Disable();
    void OnDestroy() => gameInput.Dispose();

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxConbo = GameManager.GetMaxConbo();
        maxDefeat = GameManager.GetMaxDefeat();
        clearTime = GameManager.GetClearTime();
        StartCoroutine("DisplayResult");
        rankS.enabled = false;
        rankA.enabled = false;
        rankB.enabled = false;
        rankC.enabled = false;
    }

    private void Update()
    {
        if (gameInput.Menu.Submit.triggered && isEnabled && !isFade)
        {
            FadeManager.Instance.LoadScene("SelectScene", 1f);
            isFade = true;
            audioSource.PlayOneShot(submitSe);
        }
    }

    int ConboRank()
    {
        StartCoroutine(ScoreAnimation(maxConbo, 1, conboText));

        if (maxConbo >= 100)
        {
            conboScore = 4;
        }
        else if (maxConbo >= 75)
        {
            conboScore = 3;
        }
        else if (maxConbo >= 50)
        {
            conboScore = 2;
        }
        else
        {
            conboScore = 1;
        }

        return conboScore;
    }

    int DefeatRank()
    {
        StartCoroutine(ScoreAnimation(maxDefeat, 1, defeatText));
        
        if (maxDefeat >= 30)
        {
            defeatScore = 4;
        }
        else if (maxDefeat >= 20)
        {
            defeatScore = 3;
        }
        else if (maxDefeat >= 10)
        {
            defeatScore = 2;
        }
        else
        {
            defeatScore = 1;
        }

        return defeatScore;
    }

    int TimeRank()
    {
        StartCoroutine(ScoreAnimation(clearTime, 1, timeText));
        if (clearTime >= 300)
        {
            timeScore = 1;
        }
        else if (clearTime >= 200)
        {
            timeScore = 2;
        }
        else if (clearTime >= 150)
        {
            timeScore = 3;
        }
        else
        {
            timeScore = 4;
        }

        return timeScore;
    }

    void ScoreRank()
    {
        conboScore = ConboRank();
        defeatScore = DefeatRank();
        timeScore = TimeRank();
        resultScore = conboScore + defeatScore + timeScore;

        if (resultScore <= 4)
        {
            rankC.enabled = true;
        }
        else if (resultScore <= 7)
        {
            rankB.enabled = true;
        }
        else if (resultScore <= 10)
        {
            rankA.enabled = true;
        }
        else
        {
            rankS.enabled = true;
        }
    }

    IEnumerator DisplayResult()
    {
        yield return new WaitForSeconds(1.0f);
        ConboRank();
        yield return new WaitForSeconds(1.0f);
        DefeatRank();
        yield return new WaitForSeconds(1.0f);
        TimeRank();
        yield return new WaitForSeconds(1.0f);
        ScoreRank();
        yield return new WaitForSeconds(1.0f);
        isEnabled = true;

        Debug.Log(resultScore);
        Debug.Log(conboScore);
        Debug.Log(defeatScore);
        Debug.Log(timeScore);
    }

    //数字のカウントアップアニメーション
    IEnumerator ScoreAnimation(float after, float time, TextMeshProUGUI text)
    {
        float befor = 0;

        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            float rate = elapsedTime / time;

            text.text = (befor + (after - befor) * rate).ToString("f0");

            elapsedTime += Time.deltaTime;

            yield return new WaitForSeconds(0.01f);
        }

        text.text = after.ToString("0");
    }
}
