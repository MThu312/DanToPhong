using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XuLyVaChamChoXe : MonoBehaviour
{
    // Xử lý va chạm dạng đi xuyên qua (Trigger) như Checkpoint, Vạch đích
    private void OnTriggerEnter(Collider other)
    {
        // Khi xe chạy qua Checkpoint
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            Debug.Log("Đã qua một Checkpoint! Đang gọi GameManager...");

            // KÍCH HOẠT DÒNG NÀY ĐỂ CỘNG THỜI GIAN
            if (GameManager.Instance != null)
            {
                GameManager.Instance.QuaCheckPoint();
            }
        }

        // Khi xe chạy qua vạch đích
        else if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Đã về đích thành công!");
            // GameManager.Instance.VeDich(); // Bỏ comment nếu có hàm về đích
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