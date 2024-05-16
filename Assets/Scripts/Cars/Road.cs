using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path
{
    List<Vector3> points;
    Road neighbour;
    bool spawn;
    public Path(List<Vector3> points, Road neighbour)
    {
        this.points = points.ToList();
        this.neighbour = neighbour;
        this.spawn = false;
    }
    public Path(List<Vector3> points)
    {
        this.points = points.ToList();
        this.neighbour = null;
        this.spawn = true;
    }
    public void setSpawn(bool spawn)
    {
        this.spawn = spawn;
    }
    public bool isSpawn()
    {
        return (this.neighbour != null && this.spawn) ? true : false;
    }
    public bool isDespawn()
    {
        return (this.neighbour == null && this.spawn) ? true : false;
    }

    public Vector3 getStart()
    {
        return this.points[0];
    }
    public Vector3 getEnd()
    {
        return this.points[points.Count - 1];
    }
    public List<Vector3> getPath()
    {
        return this.points;
    }
    public Road getNeighbour()
    {
        return this.neighbour;
    }
}

public class Road : MonoBehaviour
{
    public Vector3 position;

    [SerializeField]
    public int length;
    [SerializeField]
    public bool isSpawn = false;
    [SerializeField]
    private GameObject spawn;
    [SerializeField]
    private GameObject despawn;

    [SerializeField]
    public GameObject inX;
    [SerializeField]
    public GameObject inNX;
    [SerializeField]
    public GameObject inZ;
    [SerializeField]
    public GameObject inNZ;

    [SerializeField]
    public GameObject outX;
    [SerializeField]
    public GameObject outNX;
    [SerializeField]
    public GameObject outZ;
    [SerializeField]
    public GameObject outNZ;

    [SerializeField]
    public Road neighbourX;
    [SerializeField]
    public Road neighbourNX;
    [SerializeField]
    public Road neighbourZ;
    [SerializeField]
    public Road neighbourNZ;

    private List<Path> paths;

    public void init()
    {
        paths = new List<Path>();
        position = this.transform.position;
        GameObject[] inVectors = { this.inX, this.inNX, this.inZ, this.inNZ };
        GameObject[] outVectors = { this.outX, this.outNX, this.outZ, this.outNZ };
        Road[] neighbours = { neighbourX, neighbourNX, neighbourZ, neighbourNZ };

        for (int i = 0; i < inVectors.Length; i++)
        {
            for (int j = i + 1; j < inVectors.Length; j++)  // check every side-side combination
            {
                if (neighbours[i] != null && neighbours[j] != null)
                {
                    if (inVectors[i] != null && outVectors[j] != null)
                        paths.Add(new Path(new List<Vector3> { inVectors[i].transform.position, outVectors[j].transform.position }, neighbours[j]));

                    if (inVectors[j] != null && outVectors[i] != null)
                        paths.Add(new Path(new List<Vector3> { inVectors[j].transform.position, outVectors[i].transform.position }, neighbours[i]));
                }
            }
        }

        if (isSpawn)
        {
            for (int i = 0; i < neighbours.Length; i++)
            {
                if (neighbours[i] != null)
                {
                    if (spawn != null && outVectors[i] != null)
                    {
                        Path path = new Path(new List<Vector3> { spawn.transform.position, outVectors[i].transform.position }, neighbours[i]);
                        path.setSpawn(true);
                        paths.Add(path);
                    }

                    if (despawn != null && inVectors[i] != null)
                        paths.Add(new Path(new List<Vector3> { inVectors[i].transform.position, despawn.transform.position }));
                }
            }
        }
    }

    public void removeOutPoint(GameObject point)
    {
        if (point == spawn || point == despawn)
        {
            spawn = null;
            despawn = null;
            isSpawn = false;
        }
        if (point == outX || point == inX)
        {
            inX = null;
            outX = null;
        }
        if (point == outZ || point == inZ)
        {
            inZ = null;
            outZ = null;
        }
        if (point == outNX || point == inNX)
        {
            inNX = null;
            outNX = null;
        }
        if (point == outNZ || point == inNZ)
        {
            inNZ = null;
            outNZ = null;
        }
    }

    public bool hasSpawn()
    {
        return spawn != null;
    }

    public bool checkSpawn()
    {
        if (spawn != null)
        {
            SpawnCollider collider = GetComponentInChildren<SpawnCollider>();
            if (collider != null)
                return collider.checkSpawn();
            else
                return false;
        }
        return false;
    }

    public Vector3 getDespawnPoint()
    {
        if (isSpawn && despawn != null)
            return despawn.transform.position;

        else
        {
            return position;
        }
    }
    public Vector3 getSpawnPoint()
    {
        if (isSpawn && spawn != null)
            return spawn.transform.position;

        else
        {
            return position;
        }
    }
    public Quaternion getSpawnRotation()
    {
        foreach (Path p in this.paths)
        {
            if (p.isSpawn())
                return Quaternion.LookRotation(this.getSpawnPoint() - p.getEnd());
        }
        return Quaternion.LookRotation(this.getSpawnPoint(), position);
    }

    public Path getSpawnPath()
    {
        foreach (Path p in this.paths)
        {
            if (p.isSpawn())
                return p;
        }
        return null;
    }
    public List<Path> getPathsFrom(Vector3 startPoint)
    {
        List<Path> paths = new List<Path>();
        if (this.paths == null)
        {
            return null;
        }

        foreach (Path p in this.paths)
        {
            if (p.getStart() == startPoint)
                paths.Add(p);
        }

        return paths;
    }
}
