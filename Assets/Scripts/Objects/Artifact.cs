using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public void Activate()
    {
        Entity[] Enemies = FindObjectsOfType<Entity>();
        Conveyor[] Con = FindObjectsOfType<Conveyor>();

        for (int i = 0; i < Enemies.Length; ++i)
        {
            if (!Enemies[i].CompareTag("Player"))
            {
                Enemies[i].ApplyDamage(9999);
                Enemies[i].transform.parent = null;
                Destroy(Enemies[i].gameObject, 3);
            }
        }

        for (int i = 0; i < Con.Length; ++i)
        {
            Con[i].MoveObject = null;
        }
    }
}
