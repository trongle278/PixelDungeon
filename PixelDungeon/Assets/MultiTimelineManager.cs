using UnityEngine;
using UnityEngine.Playables; // Thư viện để quản lý Timeline
using UnityEngine.SceneManagement; // Thư viện để quản lý scene

public class MultiTimelineManager : MonoBehaviour
{
    public PlayableDirector[] playableDirectors; // Mảng chứa tất cả PlayableDirectors
    public string sceneToLoad; // Tên của cảnh bạn muốn chuyển đến

    void Start()
    {
        foreach (var director in playableDirectors)
        {
            if (director != null)
            {
                director.stopped += OnPlayableDirectorStopped; // Gắn sự kiện khi Timeline dừng
            }
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        SceneManager.LoadScene(sceneToLoad); // Chuyển đến cảnh mới khi bất kỳ Timeline nào kết thúc
    }

    void OnDestroy()
    {
        foreach (var director in playableDirectors)
        {
            if (director != null)
            {
                director.stopped -= OnPlayableDirectorStopped; // Hủy sự kiện khi GameObject bị phá hủy
            }
        }
    }
}
