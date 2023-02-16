using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader s;
    public CanvasGroup group;

    private void Awake()
    {
        transform.SetParent(null, false);
        if (s != null)
        {
            Destroy(gameObject);
            return;
        }

        s = this;
        DontDestroyOnLoad(s);

        SceneManager.sceneLoaded += (n1, n2) =>
        {
            StartCoroutine(Init());
        };
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(.5f);
        group.DOFade(0f, .5f);
    }

    public async void LoadScene(string name)
    {
        group.DOFade(1f, .5f);
        await Task.Delay(500);

        SceneManager.LoadSceneAsync(name);
    }

    public void LoadScene(int i)
    {
        LoadScene(SceneManager.GetSceneByBuildIndex(i).name);
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}