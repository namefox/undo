using UnityEngine;
using DG.Tweening;
using System.Collections;

public class EyeMovement : MonoBehaviour
{
    public float speed;
    public float range, rangeY;
    public GameObject[] apply;
    private Vector3[] startPos;

    private void Awake()
    {
        startPos = new Vector3[apply.Length];
        for (int i = 0; i < apply.Length; i++)
        {
            startPos[i] = apply[i].transform.localPosition;
        }
        StartCoroutine(MovePosition());
    }

    IEnumerator MovePosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            Vector3 vec = new(Random.Range(-range, range), Random.Range(-rangeY, rangeY)) ;

            for (int i = 0; i < apply.Length; i++)
            {
                Vector3 vector = vec + apply[i].transform.localPosition;
                apply[i].transform.DOLocalMove(vector, Random.Range(.5f, 2f));
            }
            yield return new WaitForSeconds(3);
            

            for (int i = 0; i < apply.Length; i++)
            {
                apply[i].transform.DOLocalMove(startPos[i], 1f);
            }
        }
    }
}