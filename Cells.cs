using System;
using System.Text;
using System.Collections.Generic;

namespace sq1code
{
    class Cells {
        // todo: property should be in upper case
        public List<int> cells { get; protected set; }

        public Cells(List<int> cells) {
            this.cells = new List<int>(cells);
        }

        public Cells(List<int> first, List<int> second) {
            this.cells = new List<int>();
            this.cells.AddRange(first);
            this.cells.AddRange(second);
        }

        protected string ToString(string halfSeparator) {
            StringBuilder sb = new StringBuilder();
            int countOf1 = 0;
            int sum = 0;
            foreach (int cell in cells) {
                if (cell == 1) {
                    countOf1++;
                } else {    // cell == 2
                    if (countOf1 > 0) {
                        sum += countOf1;
                        sb.Append(countOf1);
                        countOf1 = 0;
                        if (sum == 6) {
                            sb.Append(halfSeparator);
                        }
                    }
                    sb.Append("0");
                    sum += cell;
                    if (sum == 6) {
                        sb.Append(halfSeparator);
                    }
                }
            }

            if (countOf1 > 0) {
                sb.Append(countOf1);
                countOf1 = 0;
            }

            return sb.ToString();
        }

        public override string ToString() {
            return ToString(halfSeparator: "");
        }

        public static bool operator == (Cells lhs, Cells rhs) {
            if (lhs.cells.Count != rhs.cells.Count) {
                return false;
            }

            for (int i = 0; i < lhs.cells.Count; i++) {
                if (lhs.cells[i] != rhs.cells[i]) {
                    return false;
                }
            }

            return true;
        }

        public static bool operator != (Cells lhs, Cells rhs) {
            return !(lhs == rhs);
        }

        public static bool operator < (Cells lhs, Cells rhs) {
            int minCount = Math.Min(lhs.cells.Count, rhs.cells.Count);
            for (int i = 0; i < minCount; i++) {
                if (lhs.cells[i] < rhs.cells[i]) {
                    return true;
                } else if (lhs.cells[i] > rhs.cells[i]) {
                    return false;
                }
            }
            return (lhs.cells.Count == minCount) && (rhs.cells.Count > minCount);
        }

        public static bool operator > (Cells lhs, Cells rhs) {
            int minCount = Math.Min(lhs.cells.Count, rhs.cells.Count);
            for (int i = 0; i < minCount; i++) {
                if (lhs.cells[i] < rhs.cells[i]) {
                    return false;
                } else if (lhs.cells[i] > rhs.cells[i]) {
                    return true;
                }
            }
            return (lhs.cells.Count > minCount) && (rhs.cells.Count == minCount);
        }

        public static bool operator <= (Cells lhs, Cells rhs) {
            return !(lhs > rhs);
        }

        public static bool operator >= (Cells lhs, Cells rhs) {
            return !(lhs < rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            return this == (obj as Cells);
        }
        
        public override int GetHashCode()
        {
            int code = 0;
            foreach (int cell in cells) {
                code = code * 10 + cell;
            }
            return code;
        }
    }
}