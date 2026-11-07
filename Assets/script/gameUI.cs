using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Thêm thư viện quản lý scene để dùng được hàm chuyển màn

public class GameUi : MonoBehaviour
{
    // Kéo ô Text trên màn hình vào đây trong Unity Inspector
    public TextMeshProUGUI timeText;

    // Update is called once per frame
    void Update()
    {
        HienThiThoiGianGame();
    }

    public void HienThiThoiGianGame()
    {
        // Lấy thời gian từ GameManager và gán lên chữ
        timeText.SetText(Mathf.FloorToInt(GameManager.Instance.thoiGianChoPhepVeDich).ToString());
    }

    // Hàm gọi khi bấm nút Chơi Lại (load lại scene "Game")
    public void ChoiLai()
    {
        SceneManager.LoadScene("game");
    }

    // Hàm gọi khi bấm nút Về Menu (load lại scene "Menu")
    public void VeMenu()
    {
        SceneManager.LoadScene("menu");
    }
}