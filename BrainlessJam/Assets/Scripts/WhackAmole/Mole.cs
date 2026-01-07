using System;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public Animator animator; // optional
    public Collider2D hitCollider;
    public SpriteRenderer spriteRenderer;

    bool active = false;
    float hideAt = 0f;
    Action<Mole, bool> onFinished; // (mole, wasHit)

    void Awake()
    {
        if (!hitCollider) hitCollider = GetComponent<Collider2D>();
        if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();
        SetVisible(false);
    }

    public void Activate(Vector3 pos, float upTime, Action<Mole, bool> callback)
    {
        transform.position = pos;
        onFinished = callback;
        SetVisible(true);
        active = true;
        hideAt = Time.time + upTime;
        // optionally play pop animation
        if (animator) animator.Play("Pop");
    }

    void Update()
    {
        if (!active) return;
        if (Time.time >= hideAt)
        {
            Finish(false);
        }
    }

    // desktop-friendly click; for mobile consider touch/raycast or implement IPointerDownHandler
    void OnMouseDown()
    {
        if (!active) return;
        // play hit animation/sfx
        if (animator) animator.Play("Hit");
        Finish(true);
    }

    void Finish(bool wasHit)
    {
        active = false;
        SetVisible(false);
        onFinished?.Invoke(this, wasHit);
        onFinished = null;
    }

    void SetVisible(bool v)
    {
        if (spriteRenderer) spriteRenderer.enabled = v;
        if (hitCollider) hitCollider.enabled = v;
    }

    public bool IsActive() => active;
}
