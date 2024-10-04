using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pauseMenuComponents;
    [SerializeField] private GameObject background;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TextMeshProUGUI brightnessTMP;
    private float brightnessDefaultValue;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeTMP;
    private float volumeDefaultValue;
    [SerializeField] private Slider lookSenseSlider;
    [SerializeField] private TextMeshProUGUI lookSenseTMP;
    private float lookSenseDefaultValue;
    private PlayerMovement playerMovement;
    private PlayerCameraMovement playerCameraMovement;
    private Scarecrow[] scarecrows;
    private bool pauseActive;


    private Volume volume;
    private VolumeProfile profile;
    private Vector4 gammaValue = new Vector4(1f,1f,1f,1f);

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerCameraMovement = FindObjectOfType<PlayerCameraMovement>();
        scarecrows = FindObjectsOfType<Scarecrow>(true);

        brightnessSlider.onValueChanged.AddListener(delegate {ChangeBrightness();});
        volumeSlider.onValueChanged.AddListener(delegate {ChangeVolume();});
        lookSenseSlider.onValueChanged.AddListener(delegate {ChangeLookSense();});

        volume = FindObjectOfType<Volume>(true);
        profile = volume.profile;

        brightnessDefaultValue = gammaValue.w;
        volumeDefaultValue = AudioListener.volume;
        lookSenseDefaultValue = playerCameraMovement.MouseSensitivity;

        brightnessSlider.value = brightnessDefaultValue;
        volumeSlider.value = volumeDefaultValue;
        lookSenseSlider.value = lookSenseDefaultValue;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        pauseActive = !pauseActive;

        playerMovement.active = !pauseActive;
        playerCameraMovement.active = !pauseActive;

        foreach(Scarecrow scarecrow in scarecrows)
        {
            scarecrow.active = !pauseActive;
        }

        foreach(GameObject component in pauseMenuComponents)
        {
            component.SetActive(pauseActive);
        }

        if(pauseActive)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void ChangeBrightness()
    {
        if(!profile.TryGet<LiftGammaGain>(out var lgg))
        {
            lgg = profile.Add<LiftGammaGain>(false);
        }

        gammaValue.w = brightnessSlider.value;
        lgg.gamma.value = gammaValue;

        brightnessTMP.text = Math.Round(brightnessSlider.value,1,MidpointRounding.AwayFromZero).ToString();
    }

    public void ResetBrightness()
    {
        brightnessSlider.value = brightnessDefaultValue;
        brightnessTMP.text = brightnessDefaultValue.ToString();
    }

    private void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        volumeTMP.text = Math.Round(volumeSlider.value, 1, MidpointRounding.AwayFromZero).ToString();
    }

    public void ResetVolume()
    {
        volumeSlider.value = volumeDefaultValue;
        volumeTMP.text = volumeDefaultValue.ToString();
    }

    private void ChangeLookSense()
    {
        playerCameraMovement.UpdateLookSensitivity(lookSenseSlider.value);
        lookSenseTMP.text = Math.Round(lookSenseSlider.value, 1, MidpointRounding.AwayFromZero).ToString();
    }

    public void ResetLookSense()
    {
        lookSenseSlider.value = lookSenseDefaultValue;
        lookSenseTMP.text = lookSenseDefaultValue.ToString();
    }

    public void ToggleBackground()
    {
        background.SetActive(!background.activeSelf);
    }
}