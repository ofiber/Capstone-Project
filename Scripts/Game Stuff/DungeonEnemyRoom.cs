using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the room that requires you kill all the enemies to open the doors
/// </summary>
public class DungeonEnemyRoom : DungeonRoom
{
    [Header("Bosses")]
    public MeleeEnemy bossEnemy;

    [Header("Lists")]
    public Door[] doors;    // List of doors in the room

    [Header("Canvasses")]
    public GameObject winCanvas;

    private int x = 0;

    private void Update()
    {
        // Check if all enemies are dead
        CheckEnemies();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        // If player enters room
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Open the doors
            OpenDoors();

            // Activate all enemies
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }

            // Activate all pots
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }

            // Activate the cinemachine virtual camera
            vCam.SetActive(true);

            // Start the coroutine
            StartCoroutine(DoorCoroutine());
        }   
    }


    IEnumerator DoorCoroutine()
    {
        // Wait a 1/3 second before closing door behind player
        // If there is no wait the door closes before the player enters the room
        yield return new WaitForSeconds(0.33f);
        CloseDoors();
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Dectivate all enemies
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }

            // Dectivate all pots
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }

            // Deactivate the cinemachine virtual camera
            vCam.SetActive(false);
        }
    }

    public void CheckEnemies()
    {
        // For all enemies in the list of enemies
        for(int i = 0; i < enemies.Length; i++)
        {
            // If there is an enemy that is still active (alive), return
            if(enemies[i].gameObject.activeInHierarchy && i < enemies.Length - 1)
            {
                return;
            }
        }


        if (x == 0)
        {
            winCanvas.SetActive(true);
            Debug.Log("Activate canvas");
            bossEnemy.spawnControl.RuntimeValue = true;
            bossEnemy.spawnControl.initVal = true;
        }
        // Only reached if all enemies are dead
        // Open doors
        OpenDoors();
        x++;
    }

    public void CloseDoors()
    {
        // For all doors in the list of doors
        for(int i = 0; i < doors.Length; i++)
        {
            // Close that door
            doors[i].Close();
        }
    }

    public void OpenDoors()
    {
        // For all doors in the list of doors
        for(int i =0; i < doors.Length; i++)
        {
            // Open that door
            doors[i].Open();
        }
    }

    public void Continue()
    {
        winCanvas.SetActive(false);
        Debug.Log("Deactivate canvas");
    }
}