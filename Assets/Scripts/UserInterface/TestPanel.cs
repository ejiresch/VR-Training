using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPanel : MonoBehaviour
{
    public void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextTask()
    {
        ProcessHandler.Instance.GetTaskManager().NextTask(false);
    }
    public void pressButton2()
    {

    }
    public void pressButton3()
    {

    }
    public void pressButton4()
    {

    }
    public void pressButton5()
    {

    }
}
