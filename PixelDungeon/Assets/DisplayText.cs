using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public TextMeshProUGUI floorText; // Kéo UI Text vào đây trong Inspector
    public float displayDuration = 5f; // Thời gian hiện text (5 giây)

    private void Start()
    {
        // Đảm bảo text bắt đầu xuất hiện
        floorText.gameObject.SetActive(true);
        // Bắt đầu coroutine để tắt text sau một thời gian
        StartCoroutine(HideTextAfterDelay(displayDuration));
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Tắt text sau khoảng thời gian delay
        floorText.gameObject.SetActive(false);
    }
}
