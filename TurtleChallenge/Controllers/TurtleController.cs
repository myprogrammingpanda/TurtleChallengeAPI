using Microsoft.AspNetCore.Mvc;
using TurtleChallenge.Models;

namespace TurtleChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurtleController : ControllerBase
    {
        private static readonly Turtle _turtle = new Turtle();
        private const int TABLE_SIZE = 5;

        [HttpPost("place")]
        public IActionResult Place(int x, int y, Direction direction)
        {
            if (x < 0 || x >= TABLE_SIZE || y < 0 || y >= TABLE_SIZE)
                return BadRequest("Invalid placement coordinates.");

            _turtle.X = x;
            _turtle.Y = y;
            _turtle.Facing = direction;
            _turtle.IsPlaced = true;

            return Ok();
        }

        [HttpPost("move")]
        public IActionResult Move()
        {
            if (!_turtle.IsPlaced)
                return BadRequest("Turtle is not placed on the table.");

            int newX = _turtle.X, newY = _turtle.Y;

            switch (_turtle.Facing)
            {
                case Direction.NORTH:
                    newY++;
                    break;
                case Direction.SOUTH:
                    newY--;
                    break;
                case Direction.EAST:
                    newX++;
                    break;
                case Direction.WEST:
                    newX--;
                    break;
            }

            if (newX < 0 || newX >= TABLE_SIZE || newY < 0 || newY >= TABLE_SIZE)
                return BadRequest("Move would cause the turtle to fall off the table.");

            _turtle.X = newX;
            _turtle.Y = newY;

            return Ok();
        }

        [HttpPost("left")]
        public IActionResult Left()
        {
            if (!_turtle.IsPlaced)
                return BadRequest("Turtle is not placed on the table.");

            _turtle.Facing = (Direction)(((int)_turtle.Facing + 3) % 4);
            return Ok();
        }

        [HttpPost("right")]
        public IActionResult Right()
        {
            if (!_turtle.IsPlaced)
                return BadRequest("Turtle is not placed on the table.");

            _turtle.Facing = (Direction)(((int)_turtle.Facing + 1) % 4);
            return Ok();
        }

        [HttpGet("report")]
        public IActionResult Report()
        {
            if (!_turtle.IsPlaced)
                return BadRequest("Turtle is not placed on the table.");

            return Ok(new { _turtle.X, _turtle.Y, Facing = _turtle.Facing.ToString() });
        }
    }
}
