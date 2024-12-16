namespace Day10;

class Program {
    static int rowLen = 0;
    static int colLen = 0;
    static int searchTrail(int prev, int r, int c, int[,] map) {
        if(r < 0 || r>=map.GetLength(0) || c<0 || c>=map.GetLength(1)) {
            return 0;
        }
        else if (map[r,c] != prev + 1) {
            return 0;
        }
        else if(map[r,c] == 9) {
            Console.WriteLine($"Recorded ({r}, {c})");
            return 1;
        }
        int curr = map[r,c];

        int up = searchTrail(curr, r-1, c, map);
        int right = searchTrail(curr, r, c+1, map);
        int down = searchTrail(curr, r+1, c, map);
        int left = searchTrail(curr, r, c-1, map);


        return up + right + down + left;
    }
    static void Main(string[] args) {
        string inputFile = "./Day10/testinput.txt";

        rowLen = File.ReadAllLines(inputFile).Count();
        colLen = 0;
        StreamReader sr = new StreamReader(inputFile);
        string? line = sr.ReadLine();
        if(line != null || line == "") {
            colLen = line.Length;
        } else {
            throw new Exception("Line is null or length is zero.");
        }

        int[,] map = new int[rowLen, colLen];

        int row = 0;
        while(line != null) {
            int col = 0;
            foreach(char c in line) {
                map[row, col] = c - '0';
                col++;
            }

            row++;
            line = sr.ReadLine();
        }

        int sum = 0;
        for(int r = 0 ; r < rowLen ; r++) {
            for(int c = 0 ; c<colLen ; c++) {
                if(map[r,c] == 0) {
                    int score = searchTrail(-1, r, c, map);
                    sum += score;
                    Console.WriteLine($"Score of ({r}, {c}): {score}");
                }
            }
        }

        Console.WriteLine(sum);
    }

}