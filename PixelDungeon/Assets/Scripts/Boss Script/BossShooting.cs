using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ShootFireball()
    {
        Debug.Log("Shooting fireball!");
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
