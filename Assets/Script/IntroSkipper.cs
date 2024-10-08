using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSkipper : MonoBehaviour
{
    private float timer;

    private void Start()
    {
        timer = Time.time;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(Time.time - timer >= 28f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
