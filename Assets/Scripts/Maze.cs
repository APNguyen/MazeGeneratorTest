using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {
    //public int sizeX;
    //public int sizeZ;
    public MazeCell cellPrefab;
    public GoalCell goalPrefab;
    public float DelayTime;
    public intVector2 size;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
    private MazeCell[,] cells;

    public MazeCell GetCell (intVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }
    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(DelayTime);
        cells = new MazeCell[size.x, size.z]; //sizeY is the Z axis
        //intVector2 coordinates = RandomizeCoordinates;
        List<MazeCell> activeCells = new List<MazeCell>();
        FirstGen(activeCells);
        while(activeCells.Count > 0)
        {
            yield return delay;
            NextGen(activeCells);
        }


        intVector2 toDeleteCoordinates = new intVector2(size.x - 1, 0);
        MazeCell toDelete = GetCell(toDeleteCoordinates);
        Debug.Log(toDelete.gameObject);
        Destroy(toDelete.gameObject);
        CreateGoalCell(toDeleteCoordinates);
        //while (ContainingCoordinates(coordinates) && GetCell(coordinates) == null)
        //{
        //    yield return delay;
        //    CreateCell(coordinates);
        //    coordinates += MazeDirectionClass.RandomValue.ToIntVector2();
        //}
        //for (int x = 0; x < size.x; x++)
        //{
        //    for (int z = 0; z < size.z; z++)
        //    {
        //        yield return delay;
        //        CreateCell(new intVector2(x, z));
        //    }
        //}
    }

    private void FirstGen(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomizeCoordinates));
    }

    private void NextGen(List<MazeCell> activeCells)
    {
        int index = activeCells.Count - 1;
        MazeCell currentCell = activeCells[index];
        if(currentCell.isFullyInitialized)
        {
            activeCells.RemoveAt(index);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        intVector2 coordinates = currentCell.cellCoordinates + direction.ToIntVector2();
        if(ContainingCoordinates(coordinates))
        {
            MazeCell adjacentCell = GetCell(coordinates);
            if(adjacentCell== null)
            {
                adjacentCell = CreateCell(coordinates);
                CreatePassage(currentCell, adjacentCell, direction);
                activeCells.Add(adjacentCell);
            }
            else
            {
                CreateWall(currentCell, adjacentCell, direction);
                //activeCells.RemoveAt(index);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
            //activeCells.RemoveAt(index);
        }


        //if(ContainingCoordinates(coordinates) && GetCell(coordinates) == null)
        //{
        //    activeCells.Add(CreateCell(coordinates));
        //}
        //else
        //{
        //    activeCells.RemoveAt(index);
        //}
    }
    private void CreatePassage(MazeCell currentCell, MazeCell adjacentCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.initialization(currentCell, adjacentCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.initialization(adjacentCell, currentCell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell currentCell, MazeCell adjacentCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.initialization(currentCell, adjacentCell, direction);
        if (adjacentCell != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.initialization(adjacentCell, currentCell, direction.GetOpposite());
        }
    }

    private MazeCell CreateCell(intVector2 coordinates  )
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.cellCoordinates = coordinates;
        newCell.name = "MazeCellAt:" + coordinates.x + "," + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    private MazeCell CreateGoalCell(intVector2 coordinates)
    {
        GoalCell newCell = Instantiate(goalPrefab) as GoalCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.cellCoordinates = coordinates;
        newCell.name = "GoalCellAt:" + coordinates.x + "," + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    public intVector2 RandomizeCoordinates
    {
        get
        {
            return new intVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainingCoordinates (intVector2 Coordinates)
    {
        return Coordinates.x >= 0 && Coordinates.x < size.x && Coordinates.z >= 0 && Coordinates.z < size.z;
    }
}
