using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletEffectController : MonoBehaviour
{
    public GameObject BulletEffect;
    public Vector3 targetScale;
    private Sequence effect;
    void Start()
    {
        // Ʈ�� ����
        effect = DOTween.Sequence().Pause().SetAutoKill(false);
        effect.Append(transform.DOScale(targetScale, 1f));
        effect.OnComplete(() => DisableObject());
    }
    private void DisableObject()
    {
        BulletEffect.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
       // gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
    }
    public void EffectPlay()
    {
        BulletEffect.SetActive(true);
        effect.Rewind();
        effect.Play();
    }
}
