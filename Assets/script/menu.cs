using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Hàm gọi khi bấm nút PLAY
    public void ChoiGame()
    {
        // Điền chính xác tên Scene màn chơi game của bạn vào đây (ví dụ: "game" hoặc "SampleScene")
        SceneManager.LoadScene("game");
    }

    // Hàm gọi khi bấm nút QUIT
    public void ThoatGame()
    {
        Debug.Log("Đã thoát game!");
        Application.Quit();
    }
}