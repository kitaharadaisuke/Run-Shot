using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Slider playerHpBar;
    [SerializeField] TextMeshProUGUI[] overSelect;
    [SerializeField] TextMeshProUGUI conboText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject overPanel;
    [SerializeField] GameObject startPanel;

    public static int maxConbo = 0;
    public static int maxDefeat = 0;
    public static float clearTime = 0;

    GameInput gameInput;

    public int conbo = 0;
    public int defeat = 0;

    int playerHp;
    int overSelectNum = 0;

    float timer = 0;

    bool isFade = false;
    bool overCanMove = true;
    bool overCanSelect = true;

    void Awake() => gameInput = new GameInput();
    void OnEnable() => gameInput.Enable();
    void OnDisable() => gameInput.Disable();
    void OnDestroy() => gameInput.Dispose();

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
        overPanel.SetActive(false);
        StartCoroutine("GameStart");
    }

    void Update()
    {
        //デバッグ用シーン遷移
        if (gameInput.Menu.Open.triggered && !isFade)
        {
            FadeManager.Instance.LoadScene("ResultScene", 1f);
            isFade = true;
        }
        playerHp = player.hp;
        playerHpBar.value = playerHp;
        //ゲームオーバー時の処理
        if (playerHp < 0)
        {
            //playerHp = 0;
            player.enabled = false;
            overPanel.SetActive(true);
            GameOverMenu();
        }
        numCount();

        //タイマー
        if (player.enabled == true)
        {
            timer += Time.deltaTime;
        }

        //コンボ数表示
        conboText.text = conbo.ToString("0");
        //タイマー表示
        timeText.text = timer.ToString("00.00");
    }

    void GameOverMenu()
    {
        for (int i = 0; i < overSelect.Length; i++)
        {
            if (i == overSelectNum) { overSelect[i].alpha = 1f; }
            else { overSelect[i].alpha = 0.5f; }
        }
        if (gameInput.Menu.Up.triggered)
        {
            if (overCanMove)
            {
                if (overSelectNum < 1) { overSelectNum++; }
                else { overSelectNum = 0; }
                overCanMove = false;
            }
        }
        else if (gameInput.Menu.Down.triggered)
        {
            if (overCanMove)
            {
                if (overSelectNum > 0) { overSelectNum--; }
                else { overSelectNum = 1; }
                overCanMove = false;
            }
        }
        else { overCanMove = true; }

        if (gameInput.Menu.Submit.triggered && !isFade)
        {
            if (overCanSelect)
            {
                switch (overSelectNum)
                {
                    case 0: //リトライ
                        FadeManager.Instance.LoadScene("MainScene", 1f);
                        isFade = true;
                        break;
                    case 1: //セレクトシーンに戻る
                        FadeManager.Instance.LoadScene("SelectScene", 1f);
                        isFade = true;
                        break;
                }
            }
        }
    }

    //リザルト用の管理
    void numCount()
    {
        if (maxConbo <= conbo)
        {
            maxConbo = conbo;
        }

        if (maxDefeat <= defeat)
        {
            maxDefeat = defeat;
        }
        clearTime = timer;
    }

    IEnumerator GameStart()
    {
        player.enabled = false;
        yield return new WaitForSeconds(1.0f);
        startPanel.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        player.enabled = true;
        startPanel.SetActive(false);
    }

    public static int GetMaxConbo()
    {
        return maxConbo;
    }
    public static int GetMaxDefeat()
    {
        return maxDefeat;
    }
    public static float GetClearTime()
    {
        return clearTime;
    }
}
