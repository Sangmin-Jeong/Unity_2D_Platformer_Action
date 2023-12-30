using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Transform _subjectTransform;
    [SerializeField] private float _knockbackFactor;
    [SerializeField] private ParticleSystem _powerUpParticle;
    [SerializeField] private ParticleSystem _knockbackParticle;

    void Start()
    {
        _subjectTransform = GetComponent<Transform>();
    }

    public void ApplyKnockback(GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        if (targetPos.x > _subjectTransform.position.x)
            target.transform.DOMoveX(_subjectTransform.position.x + _knockbackFactor, 0.5f);
        else if (targetPos.x < _subjectTransform.position.x)
            target.transform.DOMoveX(_subjectTransform.position.x - _knockbackFactor, 0.5f);
        
        target.GetComponent<Character>()._knockback.PlayKnockbackParticle();
    }

    public void PlayKnockbackParticle()
    {
        _knockbackParticle.gameObject.SetActive(true);
    }
    
    public void PowerUpForSeconds(float seconds)
    {
        StartCoroutine(PowerUpDuration(seconds));
    }

    IEnumerator PowerUpDuration(float seconds)
    {
        _knockbackFactor *= 2;
        _powerUpParticle.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(seconds);
        
        _powerUpParticle.gameObject.SetActive(false);
        _knockbackFactor /= 2;
    }
    
    
}
