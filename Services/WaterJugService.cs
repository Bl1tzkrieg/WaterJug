using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WaterJug.Data;
using WaterJug.Models;
using WaterJug.Models.Entities;
using WaterJug.Models.Responses;

namespace WaterJug.Services
{
    /// <summary>
    /// Implements the logic to solve the Water Jug Problem,
    /// stores solution attempts in the database, and formats responses.
    /// </summary>
    public class WaterJugService : IWaterJugService
    {

        private readonly AppDbContext _context;

        public WaterJugService(AppDbContext context) {
            _context = context;
        }

        /// <summary>
        /// Method for attempting to solve the water jug problem.
        /// Stores the result (solved or not) in the database history table.
        /// </summary>
        /// <param name="bucketX">Capacity of bucket X (jug 1).</param>
        /// <param name="bucketY">Capacity of bucket Y (jug 2).</param>
        /// <param name="target">Desired water volume to achieve in either bucket.</param>
        /// <returns>
        /// A response that corresponds to success or failure and the list of steps taken if successful.
        /// </returns>
        public async Task<WaterJugResponse> SolveProblem(int bucketX, int bucketY, int target) {
            var visited = new HashSet<(int, int)>();
            var statesToVisit = new Queue<State>();
            statesToVisit.Enqueue(new State(0, 0, null, "Start"));
            visited.Add((0, 0));

            State? solutionState = null;

            while (statesToVisit.Any()) {
                var currentState = statesToVisit.Dequeue();

                if (IsGoalState(currentState, target)) {
                    solutionState = currentState;
                    break;
                }

                var nextStates = GenerateMoves(currentState, bucketX, bucketY);

                foreach (var nextState in nextStates) {
                    var position = (nextState.x, nextState.y);

                    if (!visited.Contains(position)) {
                        visited.Add(position);
                        statesToVisit.Enqueue(nextState);
                    }
                }
            }

            var steps = solutionState != null ? BuildResponse(solutionState).solution : new List<WaterJugStep>();

            _context.History.Add(new WaterJugHistory {
                Jug1Capacity = bucketX,
                Jug2Capacity = bucketY,
                Target = target,
                Solved = steps.Any(),
                SolvedAt = DateTime.UtcNow,
                StepsJson = JsonSerializer.Serialize(steps)
            });
            await _context.SaveChangesAsync();

            return solutionState != null
                ? BuildResponse(solutionState)
                : Error("No valid solution found.");
        }


        /// <summary>
        /// Method that determines whether the current state matches the target volume in either bucket.
        /// </summary>
        /// <param name="state">Current bucket state.</param>
        /// <param name="target">Target volume to reach.</param>
        /// <returns>True if target is reached; otherwise false.</returns>
        private bool IsGoalState(State state, int target) {
            return state.x == target || state.y == target;
        }


        /// <summary>
        /// Generates valid next moves from the current bucket state.
        /// </summary>
        /// <param name="current">The current state of the buckets.</param>
        /// <param name="maxX">Capacity of bucket X.</param>
        /// <param name="maxY">Capacity of bucket Y.</param>
        /// <returns>A list of possible next states with the actions.</returns>
        private List<State> GenerateMoves(State current, int maxX, int maxY) {
            var moves = new List<State>();
            int x = current.x;
            int y = current.y;

            if (x < maxX)
                moves.Add(new State(maxX, y, current, "Fill bucket X"));

            if (y < maxY)
                moves.Add(new State(x, maxY, current, "Fill bucket Y"));

            if (x > 0)
                moves.Add(new State(0, y, current, "Empty the X cup"));

            if (y > 0)
                moves.Add(new State(x, 0, current, "Empty the Y cup"));

            int pourXtoY = Math.Min(x, maxY - y);
            moves.Add(new State(x - pourXtoY, y + pourXtoY, current, "Transfer from bucket X to Y"));

            int pourYtoX = Math.Min(y, maxX - x);
            moves.Add(new State(x + pourYtoX, y - pourYtoX, current, "Transfer from bucket Y to X"));

            return moves;
        }


        /// <summary>
        /// Builds the response model.
        /// </summary>
        /// <param name="final">Final state that satisfied the target condition.</param>
        /// <returns>
        /// A WaterJugResponse containing the ordered solution steps.
        /// </returns>
        private WaterJugResponse BuildResponse(State final) {
            var path = new Stack<State>();
            var result = new List<WaterJugStep>();
            var current = final;

            while (current.prev != null) {
                path.Push(current);
                current = current.prev;
            }

            int i = 1;
            while (path.Count > 0) {
                var step = path.Pop();
                result.Add(new WaterJugStep
                {
                    step = i++,
                    bucketX = step.x,
                    bucketY = step.y,
                    action = step.action
                });
            }

            if (result.Count > 0)
                result[result.Count - 1].isFinalStep = true;

            return new WaterJugResponse
            {
                success = true,
                message = "Success",
                solution = result
            };
        }


        /// <summary>
        /// Generates an error response with a custom message and empty solution list.
        /// </summary>
        /// <param name="msg">Error message to return.</param>
        /// <returns>A fail response without solution steps.</returns>
        private static WaterJugResponse Error(string msg) =>
            new WaterJugResponse {
                success = false,
                message = msg,
                solution = new()
            };
    }
}
