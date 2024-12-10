namespace Day8;

class Program {
    static void Main(string[] args) {
        string inputFile = "./Day8/input.txt";
        int rowLen = 0;
        int colLen = 0;
        // Get array bounds
        rowLen = File.ReadAllLines(inputFile).Count();

        Dictionary<char, List<List<int>>> coords = new Dictionary<char, List<List<int>>>();
        try {
            StreamReader sr = new StreamReader(inputFile);
            string? line = sr.ReadLine();
            if(line != null) {
                colLen = line.Length;
            }

            int row = 0;
            int col = 0;
            while(line != null) {
                foreach(char c in line) {
                    if(c == '.') {
                        col++;
                        continue;
                    }
                    else if (coords.ContainsKey(c)) {
                        coords[c].Add(new List<int>{col, row});
                    } 
                    else {
                        coords.Add(c, new List<List<int>>{new List<int>{col, row}});
                    }
                    col++;
                }
                col = 0;
                line = sr.ReadLine();
                row++;
            }
        }
        catch (Exception e) {
            Console.WriteLine($"Exception: {e}");
        }

        HashSet<Tuple<int,int>> antinodes = new HashSet<Tuple<int, int>>();
        foreach(KeyValuePair<char, List<List<int>>> kv in coords) {
            int numCoords = kv.Value.Count();
            for(int i=0 ; i<numCoords ; i++) {
                int startX = kv.Value[i][0];
                int startY = kv.Value[i][1];
                for(int j=i+1 ; j<numCoords ; j++) {
                    int xDiff = startX - kv.Value[j][0];
                    int yDiff = startY - kv.Value[j][1];

                    // Console.Write($"({startX}, {startY}) with ");
                    // Console.WriteLine($"({kv.Value[j][0]}, {kv.Value[j][1]}):");

                    int antiX = startX + xDiff;
                    int antiY = startY + yDiff;
                    if(antiX>=0 && antiX<colLen && antiY>=0 && antiY<rowLen) {
                        antinodes.Add(new Tuple<int, int>(antiX, antiY));
                        // Console.WriteLine($"{antiX}, {antiY}");
                    }
                    antiX = startX + (-2*xDiff);
                    antiY = startY + (-2*yDiff);
                    if(antiX>=0 && antiX<colLen && antiY>=0 && antiY<rowLen) {
                        antinodes.Add(new Tuple<int, int>(antiX, antiY));
                        // Console.WriteLine($"{antiX}, {antiY}");
                    }
                }
            }
        }

        Console.WriteLine($"Number of antinodes: {antinodes.Count}");

        // foreach(KeyValuePair<char, List<List<int>>> kv in coords) {
        //     Console.Write($"{kv.Key}: {{");
        //     foreach(List<int> l in kv.Value) {
        //         Console.Write("[" + string.Join(", ", l) + "]");
        //     }
        //     Console.WriteLine("}");
        // }
    }
}

// Part 2 shouldn't be too bad
// replace lines 57-69 with a list of antinodes to check
// Each node should be calculated useing the x and yDiff repeatedly until leaving the bounds
// Create a function that takes in diff variables, bounds, ref to List<Tuple>>
//     function adds valid coords to list as tuples