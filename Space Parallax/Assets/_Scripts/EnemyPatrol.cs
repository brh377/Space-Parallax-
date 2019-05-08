using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour{

    public float movementSpeed;
    private Transform enemy;
    public float countDown;
    private float rotateSpeed = 150f;
    private float COUNTDOWN;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
        COUNTDOWN = countDown;
    }

    // Update is called once per frame
    void Update()
    {
        enemy.position += transform.forward * movementSpeed * Time.deltaTime;
        enemy.position = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z);
        countDown -= Time.deltaTime;
        if (countDown < 0) {
            transform.Rotate(0f, 180f, 0f);
            countDown = COUNTDOWN;
        }
    }
}
