namespace TurtleChallenge.Models
{
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    public class Turtle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }
        public bool IsPlaced { get; set; } = false;
    }
}
