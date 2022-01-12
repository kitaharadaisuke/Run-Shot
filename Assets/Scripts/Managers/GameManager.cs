using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Slider playerHpBar;
    [SerializeField] TextMeshProUGUI[] overSelect;
    [SerializeField] TextMeshProUGUI[] pauseSelect;
    [SerializeField] TextMeshProUGUI conboText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] AudioClip selectSe;
    [SerializeField] AudioClip submitSe;
    [SerializeField] GameObject overPanel;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject pausePanel;

    public static int maxConbo = 0;
    public static int maxDefeat = 0;
    public static float clearTime = 0;

    GameInput gameInput;
    AudioSource audioSource;

    public int conbo = 0;
    public int defeat = 0;

    int playerHp;
    int overSelectNum = 0;
    int pauseSelectNum = 0;

    float timer = 0;
    float moveTime;

    bool isEnabledGame = true;
    bool isFade = false;
    bool isClose = false;
    bool overCanMove = true;
    bool overCanSelect = true;
    bool pauseCanMove = true;
    bool pauseCanSelect = true;


    void Awake() => gameInput = new GameInput();
    void OnEnable() => gameInput.Enable();
    void OnDisable() => gameInput.Disable();
    void OnDestroy() => gameInput.Dispose();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        overPanel.SetActive(false);
        StartCoroutine("GameStart");
    }

    void Update()
    {
        Debug.Log(player.enabled);
        Debug.Log(moveTime);
        playerHp = player.hp;
        playerHpBar.value = playerHp;
        //ゲームオーバー時の処理
        if (playerHp < 0)
        {
            //playerHp = 0;
            player.enabled = false;
            overPanel.SetActive(true);
            GameOverMenu();
            player.anm.SetBool("Death", true);
            player.bc.center = new Vector3(0f, 0.15f, 0f);
        }
        //ポーズ関連処理
        if (gameInput.Menu.Open.triggered && isEnabledGame) { pausePanel.SetActive(true); isEnabledGame = false; }
        if (!isEnabledGame) { PauseMenu(); player.enabled = false; }
        if (isClose) { moveTime += Time.deltaTime; }
        if (moveTime >= 0.5f) { isEnabledGame = true; isClose = false; player.enabled = true;moveTime = 0; }
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

    void PauseMenu()
    {
        for (int i = 0; i < pauseSelect.Length; i++)
        {
            if (i == pauseSelectNum) { pauseSelect[i].alpha = 1f; }
            else { pauseSelect[i].alpha = 0.5f; }
        }
        if (gameInput.Menu.Down.triggered)
        {
            if (pauseCanMove)
            {
                if (pauseSelectNum < 2) { pauseSelectNum++; }
                else { pauseSelectNum = 0; }
                pauseCanMove = false;
            }
        }
        else if (gameInput.Menu.Up.triggered)
        {
            if (pauseCanMove)
            {
                if (pauseSelectNum > 0) { pauseSelectNum--; }
                else { pauseSelectNum = 2; }
                pauseCanMove = false;
            }
        }
        else { pauseCanMove = true; }

        if (gameInput.Menu.Submit.triggered && !isFade)
        {
            if (pauseCanSelect)
            {
                switch (pauseSelectNum)
                {
                    case 0: //Option(音量調整)
                        break;
                    case 1: //帰還(セレクトに戻る)
                        FadeManager.Instance.LoadScene("SelectScene", 1f);
                        isFade = true;
                        break;
                    case 2://閉じる
                        pausePanel.SetActive(false);
                        isClose = true;
                        break;
                }
            }
        }
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
                audioSource.PlayOneShot(selectSe);
            }
        }
        else if (gameInput.Menu.Down.triggered)
        {
            if (overCanMove)
            {
                if (overSelectNum > 0) { overSelectNum--; }
                else { overSelectNum = 1; }
                overCanMove = false;
                audioSource.PlayOneShot(selectSe);
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
                        audioSource.PlayOneShot(submitSe);
                        break;
                    case 1: //セレクトシーンに戻る
                        FadeManager.Instance.LoadScene("SelectScene", 1f);
                        isFade = true;
                        audioSource.PlayOneShot(submitSe);
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
        isEnabledGame = false;
        yield return new WaitForSeconds(1.0f);
        startPanel.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        player.enabled = true;
        isEnabledGame = true;
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
