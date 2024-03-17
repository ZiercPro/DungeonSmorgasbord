using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete]
public class DashGhostFeedback : MonoBehaviour {
    public float duration;
    public Color color;
    public Sprite m_sprite;

    private SpriteRenderer m_render;
    private float timer;
    private void Awake() {
        m_render = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        timer = duration;
        m_render.sprite = m_sprite;
    }
    private void Update() {
        timer -= Time.deltaTime;
        color.a = timer / duration;
        m_render.color = color;
        if (timer <= 0) Destroy(gameObject);
    }
}
