using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public void succumb()
    {
        Application.Quit();
    }

    public void Chad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
