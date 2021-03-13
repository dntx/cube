using System;
using System.Collections.Generic;
using System.Linq;

namespace Cube.Sq1Cube
{
    class Layer : Cells {
        public Cells Left { get; }
        public Cells Right { get; }

        public Layer(Cells left, Cells right) : base(GetNormalizedCells(new Cells(left, right))) {
            Left = left;
            Right = right;
        }

        public Layer(params int[] cells) 
                : this(DivideCells(cells).Key, DivideCells(cells).Value) {
        }

        private static KeyValuePair<Cells, Cells> DivideCells(int[] cells) {
            int degreeSum = 0;
            for (int i = 0; i < cells.Length; i++) {
                degreeSum += new Cell(i).Degree;
                if (degreeSum == 180) {
                    int firstCount = i + 1;
                    int secondCount = cells.Length - firstCount;
                    return new KeyValuePair<Cells, Cells>(new Cells(cells.Take(firstCount)), new Cells(cells.TakeLast(secondCount)));
                }
            }
            throw new ArgumentException("the given cells can't be divided as two halves");
        }

        private static Cells GetNormalizedCells(Cells cells) {
            Cells minCells = cells;
            for (int start = 1; start < cells.Count; start++) {
                if (cells[start] <= minCells[0]) {
                    Cells shiftedCells = new Cells(cells.GetRange(start, cells.Count - start), cells.GetRange(0, start));
                    if (shiftedCells < minCells) {
                        minCells = shiftedCells;
                    }
                }
            }
            return minCells;
        }

        public override string ToString()
        {
            return ToString(verbose:false);
        }

        public string ToString(bool verbose) {
            String separator = "-";
            if (verbose) {
                return string.Format("{0,4}{1}{2,-4}", Left, separator, Right);
            } else {
                return ToString(degreeBar: 180, separator: separator);
            }
        }

        public static int HashCodeUpperBound = 16^10;

        public ISet<Division> GetDivisions(bool ascendingOnly) {
            bool needTwoDivisionsOnly = (this == Layer.YellowL3) || (this == Layer.WhiteL1);
            ISet<Division> divisions = new HashSet<Division>();
            int start = 0;
            int last = 0;
            int degreeSum = this[start].Degree;
            while (true) {
                if (degreeSum < 180) {
                    last++;
                    if (last >= Count) {
                        break;
                    }
                    degreeSum += this[last].Degree;
                } else {
                    if (degreeSum == 180) {
                        int end = last + 1;
                        Cells first = new Cells(GetRange(start, end - start));
                        Cells second = new Cells(GetRange(end, Count - end), GetRange(0, start));

                        if (first > second) {
                            Cells temp = first;
                            first = second;
                            second = temp;
                        }

                        divisions.Add(new Division(first, second));
                        if (!ascendingOnly && !needTwoDivisionsOnly && first != second) {
                            divisions.Add(new Division(second, first));
                        }
                    }
                    degreeSum -= this[start].Degree;
                    start++;
                }
                if (needTwoDivisionsOnly && divisions.Count == 2) {
                    break;
                }
            }
            return divisions;
        }

        public static Layer WhiteL1 = new Layer(0x8, 0x9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF);
        public static Layer YellowL3 = new Layer(0, 1, 2, 3, 4, 5, 6, 7);
    }
}