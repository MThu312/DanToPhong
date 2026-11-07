using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Biến thời gian cho phép về đích ban đầu
    public float thoiGianChoPhepVeDich = 30f;

    // Biến thời gian hồi lại khi xe chạy qua Checkpoint (đã đặt mặc định là 5 giây)
    public float thoiGianHoiKhiQuaCheckPoint = 5f;

    // Khai báo biến chống spam va chạm liên tiếp
    private float thoiGianCoTheQuaLai = 0f;

    public bool ketThucGame = false;

    // --- Ô TRONG INSPECTOR ---
    public GameObject timeGameObject; // Kéo đối tượng chứa chữ time vào đây
    public GameObject gameOverObject; // Kéo bảng Game Over vào đây

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
    }

    void Update()
    {
        if (!ketThucGame)
        {
            thoiGianChoPhepVeDich -= Time.deltaTime;

            if (thoiGianChoPhepVeDich <= 0)
            {
                thoiGianChoPhepVeDich = 0;
                KetThucGame();
            }

            HienThiThoiGianGame();
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
        if (ketThucGame) return;
        ketThucGame = true;
        Debug.Log("Game Over! Hết thời gian.");

        if (timeGameObject != null) timeGameObject.SetActive(false);
        if (gameOverObject != null) gameOverObject.SetActive(true);
    }

    // --- HÀM XỬ LÝ KHI QUA CHECKPOINT ---
    public void QuaCheckPoint()
    {
        if (!ketThucGame)
        {
            // Chống spam: Sau mỗi 0.5 giây mới được nhận checkpoint tiếp theo
            if (Time.time < thoiGianCoTheQuaLai) return;
            thoiGianCoTheQuaLai = Time.time + 0.5f;

            // Cộng chuẩn xác 5 giây mỗi lần qua Checkpoint
            thoiGianChoPhepVeDich += thoiGianHoiKhiQuaCheckPoint;
            Debug.Log("Đã qua Checkpoint! Cộng thêm: " + thoiGianHoiKhiQuaCheckPoint + " giây.");
        }
    }
}