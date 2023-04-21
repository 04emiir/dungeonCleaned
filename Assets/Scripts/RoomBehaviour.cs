using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour {
    // 0 - NORTH, 1 - SOUTH, 2 - WEST, 3 - EAST
    public GameObject[] walls, doors;
    bool connected;

    public void UpdateRoom(bool[] status) {
        connected = false;
        for (int i = 0; i < status.Length; i++) {
            walls[i].SetActive(status[i]);
            doors[i].SetActive(!status[i]);
            if (status[i]) {
                connected = true;
            }
        }
        if (!connected) {
            Destroy(gameObject);
        }
    }
}


