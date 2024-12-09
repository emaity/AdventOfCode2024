namespace Day6;

class Program {
    static int traverse(int r, int c, char[,] board, int rowLen, int colLen) {
        int count = 0;

        int[,] directions = {{-1,0}, {0,1}, {1,0}, {0,-1}};
        int currDir = 0;
        
        while(r>=0 && r<rowLen && c>=0 && c<colLen) {
            // Mark current tile as visited if not visited before
            if(board[r,c] != 'X') {
                board[r,c] = 'X';
                count++;
            }

            // Check if next tile possible
            int checkRow = r + directions[currDir, 0];
            int checkCol = c + directions[currDir, 1];
            if(checkRow<0 || checkRow>=rowLen || checkCol<0 || checkCol>=colLen) {
                break;
            }
            if(board[checkRow, checkCol] == '#') {
                currDir = (currDir+1) % 4;
            }

            // Move
            r += directions[currDir, 0];
            c += directions[currDir, 1];
        }

        return count;
    }

    static void Main(string[] args) {
        string inputFile = "./Day6/input.txt";
        // Get board dimensions
        int rowLen = 0;
        int colLen = 0;
        try {
            rowLen = File.ReadAllLines(inputFile).Count();

            StreamReader sr = new StreamReader(inputFile);
            string? line = sr.ReadLine();
            if(line != null) {
                colLen = line.Length;
            }
        }
        catch(Exception e) {
            Console.WriteLine($"Exception: {e}");
        }

        if(colLen == 0) {
            throw new Exception("Could not read number of columns");
        }


        int startRow = 0;
        int startCol = 0;
        // Create and initialize board
        char[,] board = new char[rowLen,colLen];
        try {
            StreamReader sr = new StreamReader(inputFile);
            string? line = sr.ReadLine();
            int row = 0;

            while(line != null) {
                for(int col=0 ; col<colLen ; col++) {
                    if(line[col] == '^') {
                        startRow = row;
                        startCol = col;
                    }
                    board[row,col] = line[col];
                }

                row++;
                line = sr.ReadLine();
            }
        }
        catch(Exception e) {
            Console.WriteLine($"Exception: {e}");
        }

        // Print board
        // for (int i = 0; i < board.GetLength(0); i++)
        // {
        //     for (int k = 0; k < board.GetLength(1); k++ )
        //     {
        //         Console.Write(board[i,k]);
        //     }
        //     Console.WriteLine();
        // }
        // Console.WriteLine($"{startRow}, {startCol}");

        Console.WriteLine($"Distinct positions: {traverse(startRow, startCol, board, rowLen, colLen)}");

        // foreach(char line in board) {
        //     Console.WriteLine(line);
        // }
    }
}