using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XuLyVaChamChoXe : MonoBehaviour
{
    // Xử lý va chạm dạng đi xuyên qua (Trigger) như Checkpoint, WinPoint, Vạch đích
    private void OnTriggerEnter(Collider other)
    {
        // 1. Khi xe chạy qua Checkpoint
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            Debug.Log("Đã qua một Checkpoint! Đang gọi GameManager...");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.QuaCheckPoint();
            }
        }

        // 2. KHI XE CHẠY QUA WINPOINT (Tách độc lập hoàn toàn bằng "if")
        if (other.gameObject.CompareTag("WinPoint"))
        {
            Debug.Log("Đã về đích (WinPoint)! Đang gọi GameManager...");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.QuaWinPoint();
            }
        }

        // 3. Khi xe chạy qua vạch đích dự phòng (Finish)
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Đã về đích thành công bằng Tag Finish!");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.QuaWinPoint();
            }
        }
    }

    // Xử lý va chạm vật lý thông thường (Collision) như tông vào rào chắn, tường, vật cản
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.name.Contains("Barricade"))
        {
            Debug.Log("Xe đã tông vào chướng ngại vật!");
        }
    }
}