using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudsController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private Sprite[] cloudSprites;
    
    [Space, Header("Settings")] 
    [SerializeField] private Vector2 direction = Vector2.left;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int amount = 5;
    [SerializeField, Min(.1f)] private float spawnBound = .2f;

    private GameObject[] _clouds;
    private Cloud[] _cloudsData;
    
    private Vector2 spawnAreaCenter;
    private Vector2 spawnAreaExtent;

    private void Start()
    {
        spawnAreaCenter = spawnArea.transform.position;
        spawnAreaExtent = spawnArea.bounds.extents;
        
        _clouds = new GameObject[amount];
        _cloudsData = new Cloud[amount];
        
        for (int i = 0; i < _clouds.Length; i++)
        {
            var cloud = new GameObject();
            cloud.AddComponent<SpriteRenderer>();
            var cloudSprite = cloud.GetComponent<SpriteRenderer>();
            cloudSprite.sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];
            cloudSprite.sortingOrder = -1;
            cloud.transform.parent = transform;
            _clouds[i] = cloud;

            Vector2 newPosition = new Vector2(
                spawnAreaCenter.x + Random.Range(-spawnAreaExtent.x, spawnAreaExtent.x),
                spawnAreaCenter.y + Random.Range(-spawnAreaExtent.y, spawnAreaExtent.y));
            
            _clouds[i].transform.position = newPosition;

            _cloudsData[i] = new Cloud(speed);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _clouds.Length; i++)
        {
            Vector2 cloudPosition = _clouds[i].transform.position;
            
            if (cloudPosition.x < spawnAreaCenter.x - spawnAreaExtent.x)
            {
                cloudPosition = new Vector2(spawnAreaCenter.x + spawnAreaExtent.x, Random.Range(-spawnAreaExtent.y, spawnAreaExtent.y));
                cloudPosition = CheckBounds(cloudPosition);
                _cloudsData[i] = new Cloud(speed);
            }
            if (cloudPosition.x > spawnAreaCenter.x + spawnAreaExtent.x)
            {
                cloudPosition = new Vector2(spawnAreaCenter.x - spawnAreaExtent.x, Random.Range(-spawnAreaExtent.y, spawnAreaExtent.y));
                cloudPosition = CheckBounds(cloudPosition);
                _cloudsData[i] = new Cloud(speed);
            }
            if (cloudPosition.y < spawnAreaCenter.y - spawnAreaExtent.y)
            {
                cloudPosition = new Vector2(Random.Range(-spawnAreaExtent.x, spawnAreaExtent.x),spawnAreaCenter.y + spawnAreaExtent.y);
                cloudPosition = CheckBounds(cloudPosition);
                _cloudsData[i] = new Cloud(speed);
            }
            if (cloudPosition.y > spawnAreaCenter.y + spawnAreaExtent.y)
            {
                cloudPosition = new Vector2(Random.Range(-spawnAreaExtent.x, spawnAreaExtent.x),spawnAreaCenter.y - spawnAreaExtent.y);
                cloudPosition = CheckBounds(cloudPosition);
                _cloudsData[i] = new Cloud(speed);
            }

            _clouds[i].transform.position = cloudPosition;
            _clouds[i].transform.Translate(direction * _cloudsData[i].Speed * Time.deltaTime);
        }
    }

    private Vector2 CheckBounds(Vector2 cloudPosition)
    {
        if (cloudPosition.x >= spawnAreaCenter.x + spawnAreaExtent.x) cloudPosition.x -= spawnBound;
        if (cloudPosition.x <= spawnAreaCenter.x - spawnAreaExtent.x) cloudPosition.x += spawnBound;
        if (cloudPosition.y >= spawnAreaCenter.y + spawnAreaExtent.y) cloudPosition.y -= spawnBound;
        if (cloudPosition.y <= spawnAreaCenter.y - spawnAreaExtent.y) cloudPosition.y += spawnBound;
        return cloudPosition;
    }
    
    private class Cloud
    {
        private float _speed;
        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value + Random.Range(-0.1f, 0.5f);
            }
        }

        public Cloud()
        {
            _speed = 1f;
        }

        public Cloud(float speed)
        {
            Speed = speed;
        }
    }
}
