using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider volumeSlider;

    public GameObject PanelSetting;

    private string volumeParameter = "MasterVolume";

    void Start()
    {
        if (masterMixer.GetFloat(volumeParameter, out float value))
        {
            volumeSlider.value = Mathf.Pow(10, value / 20);
        }

        volumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PanelSetting.SetActive(true);



        }
    }

    public void SetMasterVolume(float sliderValue)
    {
        if (sliderValue <= 0)
        {
            masterMixer.SetFloat(volumeParameter, -80f);
        }
        else
        {
            float volume = Mathf.Log10(sliderValue) * 20;
            masterMixer.SetFloat(volumeParameter, volume);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


}
