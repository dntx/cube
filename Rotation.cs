using System.Collections.Generic;

namespace sq1code
{
    class Rotation {
        public Division Up { get; }
        public Division Down { get; }

        public Rotation(Division up, Division down) {
            Up = up;
            Down = down;
        }

        public bool IsIdentical() {
            return (Up.Left == Down.Left) || (Up.Right == Down.Right);
        }

        public bool IsSquareShapeLocked() {
            return Up.Left[0].Shape == Down.Left[0].Shape;
        }

        public bool IsSquareQuarterLocked() {
            return Up.Left[0].Degree == 60 && Down.Left[0].Degree == 60;
        }

        public bool IsCounterQuarterLocked() {
            return Up.Left[0].Degree == 30 && Down.Left[0].Degree == 30;
        }

        public override string ToString() {
            return string.Format("{0},{1}", Up, Down);
        }
    }

}