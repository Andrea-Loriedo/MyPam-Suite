using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class MarbleTestSuite
{
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator PlayModeTestingWithEnumeratorPasses() {
        SetupScene();
        yield return new WaitForSeconds(1);
    }

    void SetupScene()
    {
        SceneManager.LoadScene("Marble");   
    }
}
