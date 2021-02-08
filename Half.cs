using System.Collections.Generic;

namespace sq1code
{
    class Half {
        public List<int> cells { get; }

        public Half(List<int> cells) {
            this.cells = new List<int>(cells);
        }

        public override string ToString() {
            string s = "";
            int countOf1 = 0;
            foreach (int cell in cells) {
                if (cell == 1) {
                    countOf1++;
                } else {    // cell == 2
                    if (countOf1 > 0) {
                        s += countOf1;
                        countOf1 = 0;
                    }
                    s += "0";
                } 
            }

            if (countOf1 > 0) {
                s += countOf1;
                countOf1 = 0;
            }

            return s;
        }

        public static int CompareToHalf(List<int> cells) {
            int sum = 0;
            foreach (int i in cells) {
                sum += i;
            }
            return sum - 6;
        }

        public bool SameAs(Half other) {
            if (cells.Count != other.cells.Count) {
                return false;
            }

            for (int i = 0; i < cells.Count; i++) {
                if (cells[i] != other.cells[i]) {
                    return false;
                }
            }

            return true;
        }
        public static Half Square = new Half(new List<int>{1,2,1,2});
        public static Half Hexagram = new Half(new List<int>{2,2,2});    
    }
}