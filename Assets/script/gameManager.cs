using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Biến thời gian cho phép về đích ban đầu
    public float thoiGianChoPhepVeDich = 30f;

    // Biến thời gian hồi lại khi xe chạy qua Checkpoint (đặt là 5 giây)
    public float thoiGianHoiKhiQuaCheckPoint = 5f;

    // Khai báo biến chống spam va chạm liên tiếp cho checkpoint
    private float thoiGianCoTheQuaLai = 0f;

    public bool ketThucGame = false;
    public bool winGame = false; // Biến kiểm tra chiến thắng theo video

    // --- Ô TRONG INSPECTOR ---
    public GameObject timeGameObject;    // Kéo đối tượng chứa chữ time vào đây
    public GameObject gameOverObject;    // Kéo bảng Game Over vào đây
    public GameObject winGameObject;     // Kéo bảng Win Game vào đây (theo video)

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (timeGameObject != null) timeGameObject.SetActive(true);
        if (gameOverObject != null) gameOverObject.SetActive(false);
        if (winGameObject != null) winGameObject.SetActive(false);
    }

    void Update()
    {
        // Xử lý đếm ngược thời gian thua cuộc
        if (!ketThucGame && !winGame)
        {
            thoiGianChoPhepVeDich -= Time.deltaTime;

            if (thoiGianChoPhepVeDich <= 0)
            {
                thoiGianChoPhepVeDich = 0;
                KetThucGame();
            }

            HienThiThoiGianGame();
        }

        // Xử lý hiển thị khi thắng game (giống hình mẫu video)
        if (winGame)
        {
            if (timeGameObject != null) timeGameObject.SetActive(false);
            if (winGameObject != null) winGameObject.SetActive(true);
        }
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
        Debug.Log("Game Over! Hết thời gian.");

        if (timeGameObject != null) timeGameObject.SetActive(false);
        if (gameOverObject != null) gameOverObject.SetActive(true);
    }

    // --- HÀM XỬ LÝ KHI QUA CHECKPOINT (Cộng đúng 5s, chống spam) ---
    public void QuaCheckPoint()
    {
        if (!ketThucGame && !winGame)
        {
            if (Time.time < thoiGianCoTheQuaLai) return;
            thoiGianCoTheQuaLai = Time.time + 0.5f;

            thoiGianChoPhepVeDich += thoiGianHoiKhiQuaCheckPoint;
            Debug.Log("Đã qua Checkpoint! Cộng thêm: " + thoiGianHoiKhiQuaCheckPoint + " giây.");
        }
    }

    // --- HÀM XỬ LÝ KHI QUA WINPOINT (Đúng y hệt video) ---
    public void QuaWinPoint()
    {
        if (!ketThucGame)
        {
            winGame = true;
            if (winGameObject != null)
            {
                winGameObject.SetActive(true);
            }
            Debug.Log("Chiến thắng! Đã qua WinPoint.");
        }
    }
}