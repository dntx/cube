using System;
using System.Collections.Generic;

namespace sq1code
{
    class Sq1Solver {
        public static void Run() {
            Console.WriteLine("hello");

            Queue<State> openStates = new Queue<State>();
            ISet<Cube> seenCubes = new HashSet<Cube>();

            int id = 0;
            Cube startCube = Cube.Square;
            openStates.Enqueue(new State(id, startCube));
            seenCubes.Add(startCube);
            do {
                State state = openStates.Dequeue();
                Cube cube = state.cube;
                if (cube.isHexagram()) {
                    outputState(state);
                    Console.WriteLine();
                }

                List<Rotation> rotations = cube.GetRotations();
                foreach (Rotation rotation in rotations) {
                    //Console.WriteLine("rotation: " + rotation.ToString());
                    Cube nextCube = cube.ApplyRotation(rotation);
                    if (!seenCubes.Contains(nextCube)) {
                        //Console.WriteLine("new cube: {0}", nextCube.ToString());
                        id++;
                        State nextState = new State(id, state, rotation, nextCube);
                        openStates.Enqueue(nextState);
                        seenCubes.Add(nextCube);
                    }
                }
                //Console.WriteLine();
                //Console.WriteLine("open: {0}, closed: {1}", openQueue.Count, closedQueue.Count);
            } while (openStates.Count > 0); 

            Console.WriteLine("end");
        }

        private static void outputState(State state) {
            Console.Write("states: ");
            List<Rotation> rotations = state.cube.GetRotations();
            foreach (Rotation rotation in rotations) {
                Console.Write("{0} | ", rotation);
            }
            Console.WriteLine();

            Console.Write("depth：{0}", state.depth);
            do {
                Console.Write(" ==> {0}({1})", state.cube, state.id);
                state = state.fromState;
            } while (state != null);
            Console.WriteLine();
        }
    }
}