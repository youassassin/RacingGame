﻿/// <summary>
/// A circular obstacle in the environment.
/// </summary>
public class Obstacle : ObjectBase
{
    #region Constructor

    /// <summary>
    /// Creates a new obstacle.
    /// </summary>
    /// <param name="PositionX">The X position of the obstacle.</param>
    /// <param name="PositionY">The Y position of the obstacle.</param>
    /// <param name="Radius">The radius of the obstacle.</param>
    public Obstacle( double PositionX, double PositionY, double Radius )
        : base( PositionX, PositionY, Radius )
    {
    }



    /// <summary>
    /// Creates a new obstacle as a copy of an existing obstacle.
    /// </summary>
    /// <param name="Original">The obstacle to copy.</param>
    public Obstacle( Obstacle Original )
        : base( Original )
    {

    }

    #endregion
}
