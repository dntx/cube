using System.Collections.Generic;
using Cube;

namespace Cube.Sq1RawCube
{
    class Cube : ICube {
        public Layer Up { get; }
        public Layer Down { get; }

        public Cube(Layer up, Layer down) {
            Up = up;
            Down = down;
        }

        public static bool operator == (Cube lhs, Cube rhs) {
            return (lhs.Up == rhs.Up && lhs.Down == rhs.Down) || (lhs.Up == rhs.Down && lhs.Down == rhs.Up);
            //return lhs.Up == rhs.Up && lhs.Down == rhs.Down;
        }

        public static bool operator != (Cube lhs, Cube rhs) {
            return !(lhs == rhs);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            return this == (obj as Cube);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            int upHashCode = Up.GetHashCode();
            int downHashCode = Down.GetHashCode();
            if (upHashCode <= downHashCode) {
                return upHashCode * Layer.HashCodeUpperBound + downHashCode;
            } else {
                return downHashCode * Layer.HashCodeUpperBound + upHashCode;
            }
        }

        public ICollection<IRotation> GetRotations() {
            List<IRotation> rotations = new List<IRotation>();

            ISet<Division> upDivisions = Up.GetDivisions(ascendingOnly: true);
            ISet<Division> downDivisions = Down.GetDivisions(ascendingOnly: false);

            foreach (Division upDivision in upDivisions) {
                foreach (Division downDivsion in downDivisions) {
                    Rotation rotation = new Rotation(upDivision, downDivsion);
                    if (!rotation.IsIdentical()) {
                        rotations.Add(rotation);
                    }
                }
            }

            return rotations;
        }

        public ICube RotateBy(IRotation iRotation) {
            Rotation rotation = iRotation as Rotation;
            Layer up = new Layer(rotation.Up.Left, rotation.Down.Right);
            Layer down = new Layer(rotation.Down.Left, rotation.Up.Right);

            return new Cube(up, down);
        }

        public override string ToString()
        {
            return ToString(verbose: false);
        }

        public string ToString(bool verbose)
        {
            return string.Format("{0},{1}", Up.ToString(verbose), Down.ToString(verbose));
        }

        public int PredictCost(ICollection<ICube> targetCubes) {
            return 0;
        }

        public int PredictCost(ICube iTargetCube) {
            return 0;
        }

        public static Cube ShapeSolved = 
            new Cube(new Layer(0, 1, 0, 1, 0, 1, 0, 1), new Layer(0, 1, 0, 1, 0, 1, 0, 1));
        public static ISet<ICube> ShapeUnsolvedList = new HashSet<ICube> {
            new Cube(new Layer(0, 0, 1, 1, 1, 1, 1, 1, 1, 1), new Layer(0, 0, 0, 0, 0, 0)),
            new Cube(new Layer(0, 1, 0, 1, 1, 1, 1, 1, 1, 1), new Layer(0, 0, 0, 0, 0, 0)),
            new Cube(new Layer(0, 1, 1, 0, 1, 1, 1, 1, 1, 1), new Layer(0, 0, 0, 0, 0, 0)),
            new Cube(new Layer(0, 1, 1, 1, 0, 1, 1, 1, 1, 1), new Layer(0, 0, 0, 0, 0, 0)),
            new Cube(new Layer(0, 1, 1, 1, 1, 0, 1, 1, 1, 1), new Layer(0, 0, 0, 0, 0, 0))
        };
    }
}