using System;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public GameObject bubble;
    private SpriteRenderer _spriteRenderer;

    public GameObject dialogue;
    private bool isTalking;

    private void Start()
    {
        _spriteRenderer = bubble.GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        
        dialogue.SetActive(false);
    }

    private void Update()
    {
        if (!isTalking)
        {
            dialogue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spriteRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spriteRenderer.enabled = false;
            isTalking = false;
        }
    }

    public void Interact()
    {
        // Debug.Log("Hey kid!");
        dialogue.SetActive(true);
        _spriteRenderer.enabled = false;
        isTalking = true;
    }
}
