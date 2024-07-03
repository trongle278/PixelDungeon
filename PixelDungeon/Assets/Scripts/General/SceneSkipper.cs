using UnityEngine;
using UnityEngine.SceneManagement; // Thư viện để quản lý scene
using UnityEngine.UI; // Thư viện để quản lý UI

public class SceneSkipper : MonoBehaviour
{
    public Button skipButton; // Tham chiếu đến nút "Skip"
    public string sceneToLoad; // Tên của cảnh bạn muốn chuyển đến

    void Start()
    {
        // Kiểm tra nếu skipButton đã được gán trong Inspector
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(SkipScene); // Gắn sự kiện khi nhấn nút
        }
    }

    void SkipScene()
    {
        SceneManager.LoadScene(sceneToLoad); // Chuyển đến cảnh mới
    }
}