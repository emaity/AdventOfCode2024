namespace Day9;
using System.IO;

class Program{ 
    static void Main(string[] args){
        string inputFile = "./Day9/testinput.txt";

        string? line = "";
        try {
            StreamReader sr = new StreamReader(inputFile);
            line = sr.ReadLine();
        }
        catch(Exception e) {
            Console.WriteLine($"Error when reading file: {e}");
        }
        if(line == null || line == "") {
            throw new Exception("Error! line is empty or null");
        }

        double checksum = 0;
        int currIndex = 1;
        int endFileIndex = line.Length % 2 == 0 ? line.Length-2 : line.Length-1;
        int currPos = line[0] - '0';
        int numFiles = line[endFileIndex] - '0'; // Number of files from the endFileIndex

        while(currIndex < endFileIndex) {
            // on a hole
            if(currIndex % 2 != 0) {
                int numHoles = line[currIndex] - '0';
                int currFileID = endFileIndex/2;

                while(numHoles > 0) {
                    // Console.WriteLine("Curr: " + checksum);
                    if(numFiles == 0) {
                        endFileIndex -= 2;
                        numFiles = line[endFileIndex] - '0';
                        currFileID = endFileIndex/2;
                    }
                    if(endFileIndex < currIndex) {
                        break;
                    }
                    checksum += currFileID * currPos;
                    currPos++;

                    numHoles--;
                    numFiles--;
                }
            }
            else { // on a file
                int currFiles = line[currIndex] - '0'; // Number of files at the current iteration index
                while(currFiles > 0) {
                    checksum += (currIndex / 2) * currPos;
                    // Console.WriteLine("Curr: " + checksum);

                    currFiles--;
                    currPos++;
                }
            }

            currIndex++;
        }

        while(numFiles > 0) {
            checksum += currPos * (endFileIndex/2);
            numFiles--;
        }

        Console.WriteLine(checksum);
    }
}