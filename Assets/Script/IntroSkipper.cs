using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSkipper : MonoBehaviour
{
    private float timer;

    private void Start()
    {
        timer = Time.time;
    }
    // Update is called once per frame
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
