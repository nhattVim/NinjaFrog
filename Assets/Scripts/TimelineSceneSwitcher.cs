using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineSceneSwitcher : MonoBehaviour
{
    public PlayableDirector playableDirector; // Tham chiếu tới PlayableDirector
    public string sceneToLoad; // Tên của scene cần chuyển đến

    void Start()
    {
        // Đăng ký sự kiện khi timeline kết thúc
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineFinished;
        }
    }

    void OnTimelineFinished(PlayableDirector director)
    {
        // Chuyển scene khi timeline kết thúc
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnDestroy()
    {
        // Hủy đăng ký sự kiện để tránh lỗi
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnTimelineFinished;
        }
    }
}
