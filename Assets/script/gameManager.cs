using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // 👈 NHỚ THÊM DÒNG NÀY Ở ĐẦU FILE

public class GameManager : MonoBehaviour
{
    // ... (các biến cũ giữ nguyên) ...
    public float thoiGianChoPhepVeDich = 30f;
    public float thoiGianHoiKhiQuaCheckPoint = 5f;
    private float thoiGianCoTheQuaLai = 0f;

    public bool ketThucGame = false;
    public bool winGame = false;
    private bool isPaused = false;

    public GameObject timeGameObject;
    public GameObject gameOverObject;
    public GameObject winGameObject;
    public GameObject tutorialPanelObject;
    public GameObject pausePanelObject;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake() { if (instance == null) instance = this; }

    private void Start()
    {
        if (timeGameObject != null) timeGameObject.SetActive(true);
        if (gameOverObject != null) gameOverObject.SetActive(false);
        if (winGameObject != null) winGameObject.SetActive(false);
        if (pausePanelObject != null) pausePanelObject.SetActive(false);

        if (tutorialPanelObject != null)
        {
            tutorialPanelObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void BatDauGame()
    {
        if (tutorialPanelObject != null)
        {
            tutorialPanelObject.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ketThucGame && !winGame && (tutorialPanelObject == null || !tutorialPanelObject.activeSelf))
            {
                if (isPaused) ResumeGame();
                else PauseGame();
            }
        }

        if (!ketThucGame && !winGame && !isPaused)
        {
            thoiGianChoPhepVeDich -= Time.deltaTime;
            if (thoiGianChoPhepVeDich <= 0)
            {
                thoiGianChoPhepVeDich = 0;
                KetThucGame();
            }
            HienThiThoiGianGame();
        }

        if (winGame)
        {
            if (timeGameObject != null) timeGameObject.SetActive(false);
            if (winGameObject != null) winGameObject.SetActive(true);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        if (pausePanelObject != null) pausePanelObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanelObject != null) pausePanelObject.SetActive(false);
        Time.timeScale = 1f;
    }

    // 👇 SỬA HÀM QUIT GAME ĐỂ QUAY VỀ SCENE MENU
    public void QuitGame()
    {
        Time.timeScale = 1f; // Phải trả lại thời gian bình thường trước khi chuyển scene
        SceneManager.LoadScene("menu"); // Tên scene menu của Nhi trong thư mục Assets/scene
    }

    public void HienThiThoiGianGame()
    {
        if (timeGameObject != null)
        {
            TextMeshProUGUI timeText = timeGameObject.GetComponent<TextMeshProUGUI>();
            if (timeText != null)
            {
                timeText.SetText(Mathf.FloorToInt(thoiGianChoPhepVeDich).ToString());
            }
        }
    }

    public void KetThucGame()
    {
        if (ketThucGame || winGame) return;
        ketThucGame = true;
        if (timeGameObject != null) timeGameObject.SetActive(false);
        if (gameOverObject != null) gameOverObject.SetActive(true);
    }

    public void QuaCheckPoint()
    {
        if (!ketThucGame && !winGame)
        {
            if (Time.time < thoiGianCoTheQuaLai) return;
            thoiGianCoTheQuaLai = Time.time + 0.5f;
            thoiGianChoPhepVeDich += thoiGianHoiKhiQuaCheckPoint;
        }
    }

    public void QuaWinPoint()
    {
        if (!ketThucGame)
        {
            winGame = true;
            if (winGameObject != null) winGameObject.SetActive(true);
        }
    }
}