using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingEnemyController : MonoBehaviour
{
    [SerializeField]
    private float EnemyStartHidingTime;
    [SerializeField]
    private float EnemyHidingTime;
    [SerializeField]
    private float EnemyShowingTime;
    [SerializeField]
    private float HidingSpeed;

    private Vector3 targetScale;
    PolygonCollider2D myPolygonCollider;
    BoxCollider2D myBoxCollider;
    Collider2D myCollider;

    private void Start()
    {
        myPolygonCollider = GetComponent<PolygonCollider2D>();
        myCollider = myPolygonCollider;
        if (myPolygonCollider == null)
        {
            myBoxCollider = GetComponent<BoxCollider2D>();
            myCollider = myBoxCollider;
        }
        targetScale = Vector3.one;
        StartCoroutine(EnemyHide());
    }

    IEnumerator EnemyHide()
    {
        yield return new WaitForSeconds(EnemyStartHidingTime);

        while (true)
        {
            targetScale = new Vector3(0, 0.1f, 0);
            myCollider.enabled = false;
            yield return new WaitForSeconds(EnemyHidingTime);
            targetScale = Vector3.one;
            myCollider.enabled = true;
            yield return new WaitForSeconds(EnemyShowingTime);
        }
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, HidingSpeed);
    }

}
