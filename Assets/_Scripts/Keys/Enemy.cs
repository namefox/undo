using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Sprite whitekey, whitekey2;
    public TMPro.TextMeshPro text;
    private Transform player;
    private bool isGrounded;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<KeyDisplay>().fallEffect = false;
    }

    private void Update()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (player.position.x > transform.position.x && transform.rotation.y > 0)
        {
            transform.DORotate(new Vector3(0, 0), .3f);
            text.transform.DORotate(new Vector3(0, 0), .3f);
        } else if (player.position.x <= transform.position.y && transform.rotation.y < 180)
        {
            transform.DORotate(new Vector3(0, 180), .3f);
            text.transform.DORotate(new Vector3(0, 0), .3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Delete(collision.gameObject));
            return;
        }

        isGrounded = true;
    }

    private IEnumerator Delete(GameObject go)
    {
        Destroy(go.GetComponent<Player>());

        FindObjectOfType<CameraFollow>().target = transform;
        player = null;

        Color c = new(182f / 255f, 205f / 255f, 207f / 255f);
        Color c1 = new(181f / 255f, 150f / 255f, 150f / 255f);

        SpriteRenderer sr = go.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = whitekey2;
        sr.color = c;
        sr.DOColor(Color.white, 1f);

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = whitekey;
        sr.color = c1;
        sr.DOColor(Color.white, 1f);

        go.GetComponentInChildren<TMPro.TextMeshPro>().DOColor(Color.white, 1f);
        text.DOColor(Color.white, 1f);

        yield return new WaitForSeconds(1f);

        Destroy(go.GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Rigidbody2D>());

        go.transform.DOScale(Vector3.zero, .5f);
        gameObject.transform.DOScale(Vector3.zero, .5f);

        yield return new WaitForSeconds(.5f);
        SceneLoader.s.ReloadScene();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}