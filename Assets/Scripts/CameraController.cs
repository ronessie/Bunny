using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;

    private void Awake()
    {
        if (!player)//проверка на нахождение игрока
            player = FindObjectOfType<Hero>().transform;
    }

    private void Update()
    {
        pos = player.position;//нахождение позиции игрока
        pos.z = -10f;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);//перемещение камеры на позицию игрока
    }
}
