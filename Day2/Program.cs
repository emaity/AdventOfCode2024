using System.IO;

string? line;
int numSafe = 0;

try {
    StreamReader sr = new StreamReader("input.txt");
    line = sr.ReadLine();

    while(line != null) {
        int[] nums = Array.ConvertAll(line.Split(" "), int.Parse);

        bool increasing = false;
        if(nums.Length > 1 && nums[1] > nums[0]) {
            increasing = true;
        }

        bool safe = true;
        for(int i=1 ; i<nums.Length ; i++) {
            // if decreasing when should be increasing, or if change too fast or nums the same
            if(increasing && nums[i] < nums[i-1] || !increasing && nums[i] > nums[i-1] || Math.Abs(nums[i] - nums[i-1]) > 3 || nums[i] == nums[i-1]) {
                safe = false;
                break;
            }
        }

        if(safe)
            numSafe++;
            
        line = sr.ReadLine();
    }
}
catch (Exception e) {
    Console.WriteLine($"Exception {e}: ", e.Message);
}

Console.WriteLine($"Number of safe reports: {numSafe}");