using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;
using UnityEngine;

namespace Managers
{
    public class AIManager : MonoBehaviour
    {
        public static AIManager Instance { get; private set; }

        [SerializeField]
        private Car car;
        [SerializeField]
        private Tilemap roadsTilemap;

        [SerializeField]
        private int maxCars = 5;
        private int cars = 0;

        private float time = 0.0f;
        [SerializeField]
        public float spawnPeriod = 3f;

        private List<Road> spawns;
        private bool spawning = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            spawns = new List<Road>();
            var roads = FindObjectsOfType<Road>();

            Debug.Log(roads.Count());
            foreach (Road road in roads)
            {
                if (road.isSpawn)
                {
                    spawns.Add(road);
                }
            }
        }

        void Update()
        {
            if (spawning)
            {
                time += Time.deltaTime;

                if (time >= spawnPeriod)
                {
                    time = time - spawnPeriod;

                    if (cars < maxCars)
                    {
                        spawnCar();
                    }
                }
            }
        }

        public void startSpawning()
        {
            connectRoads();
            this.spawning = true;
        }
        private void connectRoads()
        {
            List<Transform> roadTransforms = findChildsOfType<Road>(roadsTilemap.transform);
            List<Road> roads = new List<Road>();
            Road road;
            roads.AddRange(spawns);

            Transform neighbourTransform;
            Road neighbour;
            while (roads.Count > 0)
            {
                road = roads[0];
                bool init = false;

                if (road.outX != null && road.length > 1)
                {
                    neighbourTransform = checkX(road.transform, roadsTilemap, roadTransforms);
                    if (neighbourTransform != null && neighbourTransform.gameObject.GetComponent<Road>() != null)
                    {
                        neighbour = neighbourTransform.gameObject.GetComponent<Road>();
                        if (road.neighbourX == null)
                        {
                            init = true;
                            road.neighbourX = neighbour;
                            roads.Add(neighbour);
                        }
                    }
                }

                if (road.outZ != null)
                {
                    neighbourTransform = checkZ(road.transform, roadsTilemap, roadTransforms, road.length);
                    if (neighbourTransform != null && neighbourTransform.gameObject.GetComponent<Road>() != null)
                    {
                        neighbour = neighbourTransform.gameObject.GetComponent<Road>();
                        if (road.neighbourZ == null)
                        {
                            init = true;
                            road.neighbourZ = neighbour;
                            roads.Add(neighbour);
                        }
                    }
                }

                if (road.outNX != null && road.length > 1)
                {
                    neighbourTransform = checkNX(road.transform, roadsTilemap, roadTransforms);
                    if (neighbourTransform != null && neighbourTransform.gameObject.GetComponent<Road>() != null)
                    {
                        neighbour = neighbourTransform.gameObject.GetComponent<Road>();
                        if (road.neighbourNX == null)
                        {
                            init = true;
                            road.neighbourNX = neighbour;
                            roads.Add(neighbour);
                        }
                    }
                }

                if (road.outNZ != null)
                {
                    neighbourTransform = checkNZ(road.transform, roadsTilemap, roadTransforms);
                    if (neighbourTransform != null && neighbourTransform.gameObject.GetComponent<Road>() != null)
                    {
                        neighbour = neighbourTransform.gameObject.GetComponent<Road>();
                        if (road.neighbourNZ == null)
                        {
                            init = true;
                            road.neighbourNZ = neighbour;
                            roads.Add(neighbour);
                        }
                    }
                }

                if (init)
                    road.init();

                roads.Remove(road);
            }
        }

        private Transform checkX(Transform transform, Tilemap map, List<Transform> transforms)
        {
            Vector3Int tilePos = map.WorldToCell(transform.position);
            float rotation = transform.rotation.eulerAngles.y;

            if (rotation == 0)
            {
                tilePos = tilePos + (2 * Vector3Int.right) + Vector3Int.up;
                return checkSquare(tilePos, Side.Left, map, transforms);
            }
            else if (rotation == 90)
            {
                tilePos = tilePos + (2 * Vector3Int.down);
                return checkSquare(tilePos, Side.Top, map, transforms);
            }
            else if (rotation == 180)
            {
                tilePos = tilePos + (3 * Vector3Int.left);
                return checkSquare(tilePos, Side.Right, map, transforms);
            }
            else if (rotation == 270)
            {
                tilePos = tilePos + (3 * Vector3Int.up) + Vector3Int.left;
                return checkSquare(tilePos, Side.Bottom, map, transforms);
            }
            else
            {
                Debug.LogError("Invalid rotation of road ID: " + transform.gameObject.GetInstanceID() + " at position " + transform.position.ToString());
                return null;
            }
        }

        private Transform checkZ(Transform transform, Tilemap map, List<Transform> transforms, int length)
        {
            Vector3Int tilePos = map.WorldToCell(transform.position);
            float rotation = transform.rotation.eulerAngles.y;

            if (rotation == 0)
            {
                if (length > 1)
                    tilePos = tilePos + (3 * Vector3Int.up);
                else
                    tilePos = tilePos + (2 * Vector3Int.up);
                return checkSquare(tilePos, Side.Bottom, map, transforms);
            }
            else if (rotation == 90)
            {
                if (length > 1)
                    tilePos = tilePos + (2 * Vector3Int.right);
                else
                    tilePos = tilePos + Vector3Int.right;
                return checkSquare(tilePos, Side.Left, map, transforms);
            }
            else if (rotation == 180)
            {
                if (length > 1)
                    tilePos = tilePos + (2 * Vector3Int.down) + Vector3Int.left;
                else
                    tilePos = tilePos + Vector3Int.down + Vector3Int.left;
                return checkSquare(tilePos, Side.Top, map, transforms);
            }
            else if (rotation == 270)
            {
                if (length > 1)
                    tilePos = tilePos + (3 * Vector3Int.left) + Vector3Int.up;
                else
                    tilePos = tilePos + (2 * Vector3Int.left) + Vector3Int.up;
                return checkSquare(tilePos, Side.Right, map, transforms);
            }
            else
            {
                Debug.LogError("Invalid rotation of road ID: " + transform.gameObject.GetInstanceID() + " at position " + transform.position.ToString());
                return null;
            }
        }

        private Transform checkNX(Transform transform, Tilemap map, List<Transform> transforms)
        {
            Vector3Int tilePos = map.WorldToCell(transform.position);
            float rotation = transform.rotation.eulerAngles.y;

            if (rotation == 0)
            {
                tilePos = tilePos + (2 * Vector3Int.left) + Vector3Int.up;
                return checkSquare(tilePos, Side.Right, map, transforms);
            }
            else if (rotation == 90)
            {
                tilePos = tilePos + (2 * Vector3Int.up);
                return checkSquare(tilePos, Side.Bottom, map, transforms);
            }
            else if (rotation == 180)
            {
                tilePos = tilePos + Vector3Int.right;
                return checkSquare(tilePos, Side.Left, map, transforms);
            }
            else if (rotation == 270)
            {
                tilePos = tilePos + Vector3Int.down + Vector3Int.left;
                return checkSquare(tilePos, Side.Top, map, transforms);
            }
            else
            {
                Debug.LogError("Invalid rotation of road ID: " + transform.gameObject.GetInstanceID() + " at position " + transform.position.ToString());
                return null;
            }
        }

        private Transform checkNZ(Transform transform, Tilemap map, List<Transform> transforms)
        {
            Vector3Int tilePos = map.WorldToCell(transform.position);
            float rotation = transform.rotation.eulerAngles.y;

            if (rotation == 0)
            {
                tilePos = tilePos + Vector3Int.down;
                return checkSquare(tilePos, Side.Top, map, transforms);
            }
            else if (rotation == 90)
            {
                tilePos = tilePos + (2 * Vector3Int.left);
                return checkSquare(tilePos, Side.Right, map, transforms);
            }
            else if (rotation == 180)
            {
                tilePos = tilePos + (2 * Vector3Int.up) + Vector3Int.left;
                return checkSquare(tilePos, Side.Bottom, map, transforms);
            }
            else if (rotation == 270)
            {
                tilePos = tilePos + Vector3Int.right + Vector3Int.up;
                return checkSquare(tilePos, Side.Left, map, transforms);
            }
            else
            {
                Debug.LogError("Invalid rotation of road ID: " + transform.gameObject.GetInstanceID() + " at position " + transform.position.ToString());
                return null;
            }
        }

        private enum Side
        {
            Left,
            Right,
            Top,
            Bottom,
        }

        private Transform checkSquare(Vector3Int upperLeft, Side side, Tilemap map, List<Transform> roadTransforms) // upperLeft is cell position
        {
            List<Transform> transforms = new List<Transform>();
            Transform transform;
            switch (side)
            {
                case Side.Bottom:
                    upperLeft = upperLeft + Vector3Int.down;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    upperLeft = upperLeft + Vector3Int.right;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    if (transforms.Count > 0)
                        break;

                    upperLeft = upperLeft + Vector3Int.up;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    upperLeft = upperLeft + Vector3Int.left;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    break;

                case Side.Right:
                    upperLeft = upperLeft + Vector3Int.down + Vector3Int.right;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    upperLeft = upperLeft + Vector3Int.up;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    if (transforms.Count > 0)
                        break;

                    upperLeft = upperLeft + Vector3Int.left;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    upperLeft = upperLeft + Vector3Int.down;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    break;

                case Side.Top:
                    upperLeft = upperLeft + Vector3Int.right;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    upperLeft = upperLeft + Vector3Int.left;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    if (transforms.Count > 0)
                        break;

                    upperLeft = upperLeft + Vector3Int.down;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    upperLeft = upperLeft + Vector3Int.right;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    break;

                default:
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    upperLeft = upperLeft + Vector3Int.down;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    if (transforms.Count > 0)
                        break;

                    upperLeft = upperLeft + Vector3Int.right;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    upperLeft = upperLeft + Vector3Int.up;
                    transform = findTransformAtPosition(CellToWorldToRoad(map, upperLeft), roadTransforms);
                    if (transform != null)
                        transforms.Add(transform);

                    break;
            }

            if (transforms.Count == 1)
                return transforms[0];
            else if (transforms.Count > 1)
            {
                Debug.LogError("More than one neighbour found at position: " + CellToWorldToRoad(map, upperLeft));
                return null;
            }
            else
                return null;
        }

        private Vector3 CellToWorldToRoad(Tilemap map, Vector3Int cellPosition)
        {
            Vector3 roadPosition = map.CellToWorld(cellPosition);
            roadPosition.x = roadPosition.x + 1.25f;
            roadPosition.z = roadPosition.z + 1.25f;
            return roadPosition;
        }

        private List<Transform> findChildsOfType<T>(Transform parent)
        {
            List<Transform> transforms = new List<Transform>();
            int childs = parent.childCount;
            for (int i = 0; i < childs; i++)
            {
                Transform child = parent.GetChild(i);
                T component = child.gameObject.GetComponent<T>();
                if (component != null)
                    transforms.Add(child);
            }
            return transforms;
        }

        private Transform findTransformAtPosition(Vector3 position, List<Transform> list)
        {
            List<Transform> transforms = new List<Transform>();
            foreach (Transform t in list)
            {
                if (t.position == position)
                    transforms.Add(t);
            }

            if (transforms.Count == 1)
                return transforms[0];
            else if (transforms.Count > 1)
            {
                Debug.LogError("More than one object found at position: " + position);
                return null;
            }
            else
                return null;
        }

        private void spawnCar()
        {
            if (spawns.Any())
            {
                int randomIndex = Random.Range(0, spawns.Count);
                Road spawn = spawns[randomIndex];

                if (spawn.checkSpawn())
                {
                    List<Vector3> waypoints = findRandomWay(spawn);
                    if (waypoints == null)
                        return;

                    Car c = Instantiate(car, spawn.getSpawnPoint(), spawn.getSpawnRotation());
                    c.SendMessage("setWay", waypoints);
                    cars++;
                }
            }
        }

        public void despawnCar()
        {
            cars--;
        }

        private List<Vector3> findRandomWay(Road spawn)
        {
            List<Vector3> waypoints = new List<Vector3>();

            waypoints.Add(spawn.getSpawnPoint());

            Path path = spawn.getSpawnPath();
            Road next = path.getNeighbour();

            while (next != null)
            {
                Vector3 endPoint = path.getEnd();
                waypoints.Add(endPoint);

                List<Path> nextPaths = next.getPathsFrom(endPoint);

                int randomIndex = Random.Range(0, nextPaths.Count);
                try
                {
                    path = nextPaths[randomIndex];
                }
                catch
                {
                    return null;
                }

                if (path.isDespawn())
                    waypoints.Add(next.getDespawnPoint());

                next = path.getNeighbour();
            }

            return waypoints;
        }
    }
}
