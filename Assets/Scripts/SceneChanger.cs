using UnityEngine;
using UnityEngine.SceneManagement; // Để sử dụng SceneManager

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad; // Tên Scene bạn muốn chuyển đến

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu Player chạm vào
        if (collision.CompareTag("Player"))
        {
            // Chuyển Scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
