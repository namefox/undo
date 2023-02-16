using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Del : MonoBehaviour
{
    public Sprite whitekey;
    public TMPro.TextMeshPro text;
    private SpriteRenderer sr;
    private bool destroying = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !destroying)
        {
            StartCoroutine(Delete());
        }
    }

    private IEnumerator Delete()
    {
        Destroy(gameObject, 2f);

        Color c = new(182f / 255f, 205f / 255f, 207f / 255f);

        destroying = true;

        sr.sprite = whitekey;
        sr.color = c;

        sr.DOColor(Color.white, 1f);
        text.DOColor(Color.white, 1f);
        yield return new WaitForSeconds(1f);

        GetComponent<Collider2D>().enabled = false;
        transform.DOScale(Vector3.zero, .5f);
    }
}