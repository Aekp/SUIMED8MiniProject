using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{

    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);

    public AudioSource _collisionAudio;

    public GameObject _collisionObject;

    void Start()
    {
        var r =GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;

        _collisionAudio = GetComponent<AudioSource>();

    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == _collisionObject)
        {
            _collisionAudio.Play();
        }
    }
}
