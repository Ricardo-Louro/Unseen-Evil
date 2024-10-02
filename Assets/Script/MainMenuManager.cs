using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject scarecrow;
    [SerializeField] private GameObject spotlight;

    private float lastTimeToggled;
    private float cooldownTimer = 8.3f;
    private float lightOffDuration = .7f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        lastTimeToggled = Time.time;
    }

    private void Update()
    {
        if(spotlight.activeSelf)
        {
            if (Time.time - lastTimeToggled >= cooldownTimer)
            {
                lastTimeToggled = Time.time;
                spotlight.SetActive(false);
            }
        }
        else
        {
            if(Time.time - lastTimeToggled >= lightOffDuration)
            {
                lastTimeToggled = Time.time;
                scarecrow.SetActive(!scarecrow.activeSelf);
                spotlight.SetActive(true);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}