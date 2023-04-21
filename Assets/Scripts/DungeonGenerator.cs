using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {

    public class Cell {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public GameObject room;
    public Vector2 offset;
    public Vector2Int size;
    public int startingPos = 0;


    List<Cell> board;

    // Start is called before the first frame update
    void Start() {
        GenerateMaze();
    }

    void GenerateDungeon() {
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                //Cell current = board[i + j * size.x];
                var newRoom = Instantiate(room, new Vector3(i * offset.x, 0, j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                newRoom.UpdateRoom(board[i + j * size.x].status);
            }
        }
    }

    void GenerateMaze() {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                board.Add(new Cell());
            }
        }

        int current = startingPos;
        Stack<int> path = new Stack<int>();
        int num = 0;

        while (num < 15) {
            num++;
            board[current].visited = true;
            List<int> adjacent = CheckAdjacentRoom(current);

            if (adjacent.Count == 0) {
                if (path.Count == 0) {
                    break;
                } else {
                    current = path.Pop();
                }
            } else {
                path.Push(current);

                int newCell = adjacent[Random.Range(0, adjacent.Count)];

                if (newCell > current) {
                    if (newCell - 1 == current) {
                        board[current].status[3] = true;
                        current = newCell;
                        board[current].status[2] = true;
                    } else {
                        board[current].status[0] = true;
                        current = newCell;
                        board[current].status[1] = true;
                    }
                } else {
                    if (newCell + 1 == current) {
                        board[current].status[2] = true;
                        current = newCell;
                        board[current].status[3] = true;
                    } else {
                        board[current].status[1] = true;
                        current = newCell;
                        board[current].status[0] = true;
                    }
                }
            }
        }

        GenerateDungeon();
    }


    List<int> CheckAdjacentRoom(int cell) {
        List<int> adjacent = new List<int>();

        if (cell - size.x >= 0 && !board[(cell - size.x)].visited) {
            adjacent.Add(cell - size.x);
        }


        if (cell + size.x < board.Count && !board[cell + size.x].visited) {
            adjacent.Add((cell + size.x));
        }

        if ((cell + 1) % size.x != 0 && !board[cell + 1].visited) {
            adjacent.Add(cell + 1);
        }

        if (cell % size.x != 0 && !board[cell - 1].visited) {
            adjacent.Add(cell - 1);
        }

        return adjacent;
    }
}
