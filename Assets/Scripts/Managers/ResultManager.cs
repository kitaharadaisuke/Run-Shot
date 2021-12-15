using System.Collections;
using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI conboText;
    [SerializeField] TextMeshProUGUI defeatText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] AudioClip submitSe;

    GameInput gameInput;
    AudioSource audioSource;

    int maxConbo;
    int maxDefeat;
    int conboScore = 0;
    int defeatScore = 0;
    int timeScore;
    int resultScore;
    int defeatRate;
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

    void ConboRank()
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
    }

    void DefeatRank()
    {
        StartCoroutine(ScoreAnimation(maxDefeat, 1, defeatText));
        defeatRate = maxDefeat / 54 * 100;

        if (defeatRate >= 90)
        {
            defeatScore = 4;
        }
        else if (defeatRate >= 70)
        {
            defeatScore = 3;
        }
        else if (maxConbo >= 50)
        {
            defeatScore = 2;
        }
        else
        {
            conboScore = 1;
        }
    }

    void TimeRank()
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
    }

    void ScoreRank()
    {
        resultScore = conboScore + defeatScore + timeScore;

        if (resultScore >= 4)
        {
            rankText.text = "C";
        }
        else if (resultScore >= 7)
        {
            rankText.text = "B";
        }
        else if (resultScore >= 10)
        {
            rankText.text = "A";
        }
        else
        {
            rankText.text = "S";
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
