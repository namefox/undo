using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Z : MonoBehaviour
{
    public Sprite whitekey, whitekey2;
    private bool exec = false;

    private void Awake()
    {
        GetComponent<KeyDisplay>().fallEffect = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !exec)
        {
            exec = true;
            StartCoroutine(Win(collision.gameObject));
        }
    }

    IEnumerator Win(GameObject obj)
    {
        Color c = new Color(182f / 255f, 205f / 255f, 207f / 255f);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = whitekey;
        sr.DOColor(c, 1f);

        sr = obj.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = whitekey2;
        sr.DOColor(c, 1f);

        yield return new WaitForSeconds(1.1f);

        PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage", 1) + 1);
        SceneLoader.s.ReloadScene();
    }
}