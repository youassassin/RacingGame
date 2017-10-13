﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    #region Properties

    /// <summary>
    /// The number of AI players.
    /// </summary>
    public float NumberOfPlayers;



    /// <summary>
    /// List of player objects.
    /// </summary>
    private List<GameObject> Players;



    /// <summary>
    /// The AI environment.
    /// </summary>
    private Environment Environment;



    /// <summary>
    /// Reference to the player prefab.
    /// </summary>
    public GameObject PlayerPrefab;



    /// <summary>
    /// Reference to the waypoint prefab.
    /// </summary>
    public GameObject WaypointPrefab;



    /// <summary>
    /// Reference to the obstacle prefab.
    /// </summary>
    public GameObject ObstaclePrefab;



    /// <summary>
    /// List of waypoint objects in the environment.
    /// </summary>
    [HideInInspector]
    public List<GameObject> Waypoints;



    /// <summary>
    /// List of obstacle objects in the environment.
    /// </summary>
    [HideInInspector]
    public List<GameObject> Obstacles;



    /// <summary>
    /// Radius within which the player has arrived at the destination.
    /// </summary>
    public float GoalRadius;



    /// <summary>
    /// Radius of the obstacles.
    /// </summary>
    public float ObstacleRadius;

    #endregion



    #region Unity Methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // Initialize the AI driver environment
        Environment = new Environment( -10, 10, -10, 10 );


        // Define a list of waypoint coordiantes
        List<Vector3> WaypointCoordinatesList = new List<Vector3>();
        WaypointCoordinatesList.Add( new Vector3( 10, 0, 0 ) );
        WaypointCoordinatesList.Add( new Vector3( 0, 0, 10 ) );
        WaypointCoordinatesList.Add( new Vector3( -10, 0, 0 ) );
        WaypointCoordinatesList.Add( new Vector3( 0, 0, -10 ) );

        // Define a list of obstacles
        List<Vector3> ObstacleCoordinatesList = new List<Vector3>();
        ObstacleCoordinatesList.Add( new Vector3( 4, 0, 5 ) );
        ObstacleCoordinatesList.Add( new Vector3( 5, 0, 10 ) );
        ObstacleCoordinatesList.Add( new Vector3( -10, 0, -5 ) );
        ObstacleCoordinatesList.Add( new Vector3( -5, 0, -10 ) );


        // Populate the waypoints
        foreach( Vector3 CurrentWaypoint in WaypointCoordinatesList )
        {
            // Create the waypoint in the UI environment
            GameObject NewWaypoint = Instantiate( WaypointPrefab, CurrentWaypoint, Quaternion.identity );
            NewWaypoint.transform.localScale = new Vector3( GoalRadius, GoalRadius, GoalRadius );
            Waypoints.Add( NewWaypoint );

            // Add the waypoint as a goal in the AI environment
            Environment.Goals.Add( new Goal( CurrentWaypoint, GoalRadius ) );
        }
        
        /*
        // Populate the obstacles
        foreach( Vector3 CurrentObstacle in ObstacleCoordinatesList )
        {
            // Create the obstacle in the UI environment
            GameObject NewObstacle = Instantiate( ObstaclePrefab, CurrentObstacle, Quaternion.identity );
            NewObstacle.transform.localScale = new Vector3( ObstacleRadius, ObstacleRadius, ObstacleRadius );
            Obstacles.Add( NewObstacle );

            // Add the obstacle in the AI environment
            //Environment.Obstacles.Add( new Obstacle( CurrentObstacle, ObstacleRadius ) );
        }
        */


        // Create the AI drivers
        Players = new List<GameObject>();
        for( int PlayerNum = 0; PlayerNum < NumberOfPlayers; PlayerNum++ )
        {
            // Calculate the initial position
            Vector3 StartPosition = new Vector3( PlayerNum * 2.5f, 0, 0 );

            // Create the player
            Players.Add( Instantiate( PlayerPrefab, StartPosition, Quaternion.identity ) );
        }

        // Initialize the AI drivers
        foreach( GameObject Player in Players )
        {
            // Create a new driver
            Player.GetComponent<PlayerController>().AiDriver = new Driver( Environment, Player.transform.position.x, Player.transform.position.z, 0.5 );
            Player.GetComponent<PlayerController>().AiDriver.ShouldCheckDirectPath = Player.GetComponent<PlayerController>().CheckDirectPath;
            Player.GetComponent<PlayerController>().AiDriver.Speed = Player.GetComponent<PlayerController>().MaxSpeed;
            Player.GetComponent<PlayerController>().ArriveRadius = GoalRadius;
        }


        // Start the AI drivers
        foreach( GameObject Player in Players )
        {
            // Start the driver
            Player.GetComponent<PlayerController>().AiDriver.PlannerActive = true;
            Player.GetComponent<PlayerController>().AiDriver.NavigationActive = true;
            Player.GetComponent<PlayerController>().AiDriver.RunSimulation = true;
        }
    }



    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Check for a left mouse click
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            
        }
    }

    #endregion
}