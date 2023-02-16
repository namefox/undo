using DG.Tweening;
using UnityEngine;

public class KeyDisplay : MonoBehaviour
{
    public string keyCode;
    public TMPro.TextMeshPro key;
    private float y;
    [HideInInspector] public bool fallEffect = true;

    private void Start()
    {
        y = transform.position.y;
        key.text = keyCode;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && fallEffect)
        {
            transform.DOMoveY(transform.position.y - .1f, .5f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && fallEffect)
        {
            transform.DOMoveY(y, .5f);
        }
    }
}