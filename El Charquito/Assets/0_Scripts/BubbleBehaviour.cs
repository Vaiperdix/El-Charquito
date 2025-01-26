using System.Collections;
using TMPro;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] float _force;
    [SerializeField] float _torque;
    [SerializeField] AudioSource _bubbleSoundSource;
    [SerializeField] AudioClip _bubbleSound;
    float _movementDirection = 0;
    Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _bubbleSoundSource.clip = _bubbleSound;
        _bubbleSoundSource.volume = 0.4f;
    }

    private void OnEnable()
    {
        _rigidbody2D.AddForceX(_force * _movementDirection, ForceMode2D.Impulse);
        _rigidbody2D.AddTorque(_torque * _movementDirection, ForceMode2D.Impulse);
        _bubbleSoundSource.Play();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        DestryBubble();
    }

    public void shootBubble(Vector3 pos, int direction)
    {
        _movementDirection = direction;
        transform.position = pos + (Vector3.right * direction);
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestryBubble();
    }

    void DestryBubble()
    {
        StartCoroutine(DestroyB());
    }

    IEnumerator DestroyB()
    {
        //explosion
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
    }
}
