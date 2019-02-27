using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    public GameObject block;
    // Start is called before the first frame update
    void Start()
    {
        Maze maze = new Maze(20,32);
        bool[,] boolMaze = maze.getBooleans();
        for(int i =0; i< boolMaze.GetLength(0); i++)
        {
            for(int j = 0; j<boolMaze.GetLength(1); j++)
            {
                if(boolMaze[i,j])
                    Instantiate(block, new Vector3(i*0.5f-8, j*0.5f-6, 0), new Quaternion());
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Maze
{
    Stack<Tile> tileStack;
    Tile start;
    int width, height;

    public Maze(int w, int h)
    {
        this.width = Mathf.CeilToInt((w + 1) / 2.0f);
        this.height = Mathf.CeilToInt((h + 1) / 2.0f);
        start = new Tile("0, 0");
        tileStack = new Stack<Tile>();
        Tile currentTile = start;
        Tile currentDown = start;
        int i;
        for (i = 0; i < height - 1; i++)
        {
            int j;
            for (j = 0; j < width - 1; j++)
            {
                if (i == 0)
                {
                    currentTile.Right = new Tile("0, " + (j + 1));
                    currentTile.Right.Left = currentTile;
                }
                currentTile.Down = new Tile((i + 1).ToString() + ", " + j);
                currentTile.Down.Up = currentTile;
                if (j != 0)
                {
                    currentTile.Down.Left = currentTile.Left.Down;
                    currentTile.Down.Left.Right = currentTile.Down;
                }
                currentTile = currentTile.Right;
            }
            currentTile.Down = new Tile((i + 1).ToString() + ", " + (j));
            currentTile.Down.Up = currentTile;
            currentTile.Down.Left = currentTile.Left.Down;
            currentTile.Down.Left.Right = currentTile.Down;
            currentTile = currentDown.Down;
            currentDown = currentDown.Down;

        }
        GenerateMaze(start);
    }


    public void GenerateMaze(Tile current)
    {
        int next = current.GetRandomNotVisitedNeighbour();
        current.Visited = true;
        if (next != -1)
        {
            // 0 up, 1 right, 2 left, 3 down
            if (next == 0)
            {
                current.Up.WallDown = false;
                tileStack.Push(current);
                current = current.Up;
                GenerateMaze(current);
            }
            else if (next == 1)
            {
                current.WallRight = false;
                tileStack.Push(current);
                current = current.Right;
                GenerateMaze(current);
            }
            else if (next == 2)
            {
                current.Left.WallRight = false;
                tileStack.Push(current);
                current = current.Left;
                GenerateMaze(current);
            }
            else if (next == 3)
            {
                current.WallDown = false;
                tileStack.Push(current);
                current = current.Down;
                GenerateMaze(current);
            }
        }
        else
        {
            if (tileStack.Count != 0)
                GenerateMaze(tileStack.Pop());
        }
    }

    public void PrintMaze()
    {
        bool[,] maze = getBooleans();
        string line = "";
        for (int i = 0; i < maze.GetLength(0) + 2; i++)
            line += "#";
        Debug.Log(line);
        
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            line = "#";
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                line += (maze[i, j] ? "#" : " ");
            }
            Debug.Log(line + "#");
        }
        line = "";
        for (int i = 0; i < maze.GetLength(0) + 2; i++)
            line += "#";
        Debug.Log(line);

    }

    public bool[,] getBooleans()
    {
        bool[,] matrix = new bool[height * 2 - 1, width * 2 - 1];
        Tile current = start;
        Tile currentDown = start;
        int i = 0, j;
        while (currentDown != null)
        {
            j = 0;
            while (current != null)
            {
                matrix[i, j] = false;
                if (j + 1 < width * 2 - 1)
                    matrix[i, j + 1] = current.WallRight;
                if (i + 1 < height * 2 - 1)
                    matrix[i + 1, j] = current.WallDown;
                if (j + 1 < width * 2 - 1 && i + 1 < height * 2 - 1)
                    matrix[i + 1, j + 1] = true;
                j += 2;
                current = current.Right;
            }
            i += 2;
            current = currentDown.Down;
            currentDown = currentDown.Down;
        }
        return matrix;
    }
}


public class Tile
{
    bool visited;
    Tile up, right, left, down;
    bool wallRight, wallDown;
    string name;
    static Random random = new Random();

    public Tile(string name)
    {
        up = right = left = down = null;
        WallRight = WallDown = true;
        this.name = name;
    }

    public bool Visited { set => visited = true; }

    public Tile Up { get => up; set => up = value; }
    public Tile Right { get => right; set => right = value; }
    public Tile Left { get => left; set => left = value; }
    public Tile Down { get => down; set => down = value; }
    public bool WallRight { get => wallRight; set => wallRight = value; }
    public bool WallDown { get => wallDown; set => wallDown = value; }
    public string Name { get => name; set => name = value; }

    public bool IsVisited() { return visited; }

    public int GetRandomNotVisitedNeighbour()
    {
        int[] direction = { -1, -1, -1, -1 };
        while (direction[0] == -1 || direction[1] == -1 || direction[2] == -1 || direction[3] == -1)
        {
            // 0 up, 1 right, 2 left, 3 down
            int random = Mathf.FloorToInt(Random.value*4);
            if (direction[random] == -1)
            {
                if (random == 0 && up != null && !up.IsVisited())
                    return 0;
                else if (random == 1 && right != null && !right.IsVisited())
                    return 1;
                else if (random == 2 && left != null && !left.IsVisited())
                    return 2;
                else if (random == 3 && down != null && !down.IsVisited())
                    return 3;
                direction[random] = random;
            }
        }


        return -1;
    }

    public override string ToString()
    {
        return name;
    }
}