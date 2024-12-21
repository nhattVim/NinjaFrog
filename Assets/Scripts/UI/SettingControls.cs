using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{

    public Slider volumeSlider;


    void Start()
    {
        // Thiết lập Slider âm lượng
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
        volumeSlider.onValueChanged.AddListener(SetVolume);


    }

        // Hàm điều chỉnh âm lượng
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }
}

   
