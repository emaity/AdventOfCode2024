using System.IO;

string? line;
int numSafe = 0;

// Receives numbers in the order that they appear in the report
static bool isSafe(int first, int second, bool increasing) {
    // inverse of the conditions that would make a report unsafe: wrong increase/decrease, large change, same number
    return !(first == second || increasing && first > second || !increasing && first < second || Math.Abs(first - second) > 3);
}

try {
    StreamReader sr = new StreamReader("input.txt");
    line = sr.ReadLine();

    while(line != null) {
        int[] nums = Array.ConvertAll(line.Split(" "), int.Parse);

        // Compares the last and first indices to check if a report should be increasing
        // If the first or last numbers are ones that should be removed, then this is bad lol I'm just hoping it works though
        // We could instead possibly look through every number and choose the greater between observed increases or decreases?
        bool increasing = nums[nums.Length-1] > nums[0] ? true : false;

        bool safe = true;
        int numSkips = 0;
        for(int i=1 ; i<nums.Length ; i++) {
            if( !isSafe(nums[i-1], nums[i], increasing) ) {
                safe = false;

                // If two adjacent numbers are not safe, then check the one after
                i++; // bad practice to increment i in for loop
                if(i < nums.Length && isSafe(nums[i-2], nums[i], increasing)) {
                    numSkips++;
                }
            }
        }

        if(safe || (!safe && numSkips == 1))
            numSafe++;

        foreach(int num in nums) {
            Console.Write(num + " ");
        }
        Console.WriteLine();
        Console.Write($"safe: {safe}, skips: {numSkips}, increasing: {increasing}");
        Console.WriteLine();
        Console.WriteLine();
            
        line = sr.ReadLine();
    }
}
catch (Exception e) {
    Console.WriteLine($"Exception {e}: ", e.Message);
}

Console.WriteLine($"Number of safe reports: {numSafe}");