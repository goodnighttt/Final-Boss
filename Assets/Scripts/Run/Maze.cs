using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum WallState
{
    //0000 -> 没有墙
    //1111—> 左，右，上，下
    LEFT = 1,//0001
    RIGHT = 2,//0010
    UP = 4,//0100
    DOWN = 8,//1000

    VISITED = 128,//已访问过的代表什么意思？？？
}

public struct Position
{
    public int X;
    public int Y;
    //这个结构体有什么作用？？？
}


public struct Neighbour
{
    public Position Position;//
    public WallState SharedWall;
}
//public class Maze : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        WallState wallState = WallState.LEFT | WallState.RIGHT;//0001
//        wallState |= WallState.UP;
//        wallState &= ~WallState.RIGHT;
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

public static class Maze
{
    private static WallState GetOppositewWall(WallState wall)
    {
        switch(wall)
        {
            case WallState.RIGHT:
                return WallState.LEFT;
            case WallState.LEFT:
                return WallState.RIGHT;
            case WallState.UP:
                return WallState.DOWN;
            case WallState.DOWN:
                return WallState.UP;
            default: return WallState.LEFT;
        }
    }

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze,int width,int height)
    {
        //这里做出改变
        var rng = new System.Random(/*seed*/);
        var positionStack = new Stack<Position>();
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };

        maze[position.X, position.Y] |= WallState.VISITED;//1000 1111
        positionStack.Push(position);

        while(positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnVisitedNeighbours(current, maze, width, height);

            if(neighbours.Count>0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.Position;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositewWall(randomNeighbour.SharedWall);

                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;

                positionStack.Push(nPosition);
            }
        }

        return maze;

        
    }
    //检测邻居是否被访问
    private static List<Neighbour> GetUnVisitedNeighbours(Position p,WallState[,] maze,int width,int height)
    {
        var list = new List<Neighbour>();

        if(p.X > 0)//left
        {
            if(!maze[p.X - 1,p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });

            }
        }

        if(p.Y > 0)//DOWN
        {
            if(!maze[p.X,p.Y-1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y -1
                    },
                    SharedWall = WallState.DOWN
                });

            }
        }

        if (p.Y < height - 1)//UP
        {
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.UP
                });

            }
        }


        if (p.X < width - 1)//RIGHT
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT
                });

            }
        }

        return list;
    }

    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = initial;


            }
        }

        return ApplyRecursiveBacktracker(maze, width, height);
    }
}