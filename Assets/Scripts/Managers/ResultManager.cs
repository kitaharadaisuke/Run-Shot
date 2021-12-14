using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI conboText;
    [SerializeField] TextMeshProUGUI defeatText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI rankText;

    GameInput gameInput;

    int maxConbo;
    int maxDefeat;
    int conboScore = 0;
    int defeatScore = 0;
    int timeScore;
    int resultScore;
    int defeatRate;
    float clearTime;

    bool isFade = false;

    void Awake() => gameInput = new GameInput();
    void OnEnable() => gameInput.Enable();
    void OnDisable() => gameInput.Disable();
    void OnDestroy() => gameInput.Dispose();

    private void Start()
    {
        maxConbo = GameManager.GetMaxConbo();
        maxDefeat = GameManager.GetMaxDefeat();
        clearTime = GameManager.GetClearTime();
    }

    private void Update()
    {
        StartCoroutine("DisplayResult");
        if (gameInput.Menu.Submit.triggered && isFade)
        {
            SceneManager.LoadScene("SelectScene");
        }
    }

    void ConboRank()
    {
        conboText.text = maxConbo.ToString();
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
        defeatText.text = maxDefeat.ToString();
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
        timeText.text = clearTime.ToString();
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
        yield return new WaitForSeconds(2.0f);
        ConboRank();
        yield return new WaitForSeconds(2.0f);
        DefeatRank();
        yield return new WaitForSeconds(2.0f);
        TimeRank();
        yield return new WaitForSeconds(2.0f);
        ScoreRank();
        yield return new WaitForSeconds(2.0f);
        isFade = true;
    }
}
