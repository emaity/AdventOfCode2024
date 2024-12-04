using System.IO;

String? line;
int lineCount = File.ReadLines("input.txt").Count();

int[] left = new int[lineCount];
int[] right = new int[lineCount];

try {
    StreamReader sr = new StreamReader("input.txt");
    line = sr.ReadLine();
    
    int index = 0;
    while(line != null) {
        string[] nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        if(!int.TryParse(nums[0], out left[index])) {
            Console.WriteLine($"Failure to parse string {nums}");
            foreach(string num in nums) {
                Console.Write($"{num},");
            }
            Console.WriteLine();
            break;
        }
        
        if(!int.TryParse(nums[1], out right[index])) {
            Console.WriteLine($"Failure to parse string: ");
            foreach(string num in nums) {
                Console.Write($"{num},");
            }
            Console.WriteLine();
            break;
        }

        line = sr.ReadLine();
        index++;
    }
}
catch (Exception e) {
    Console.WriteLine($"Exception: {e}");
}

Array.Sort(left);
Array.Sort(right);

int distance = 0;

for (int i=0 ; i<lineCount ; i++) {
    distance += Math.Abs(left[i] - right[i]);
}

Console.WriteLine($"Distance: {distance}");

// key = number in array
// val = number of times that number appears in other array
Dictionary<int, int> leftDupes = new Dictionary<int, int>();

// key = number in right
// value = number of times that value appears
Dictionary<int, int> rightVals = new Dictionary<int, int>();

foreach(int num in left) {
    leftDupes.Add(num, 0);
}

foreach(int num in right) {
    if(!rightVals.TryAdd(num, 1)) {
        rightVals[num]++;
    }
}

// Check if number from left exists in right, if does then increment counter
foreach(KeyValuePair<int, int> pair in leftDupes) {
    if(rightVals.TryGetValue(pair.Key, out _)) {
        leftDupes[pair.Key] += rightVals[pair.Key];
    }
}
// Check if number from right exists in left, if does then increment counter
foreach(KeyValuePair<int, int> pair in rightVals) {
    if(leftDupes.TryGetValue(pair.Key, out _)) {
        rightVals[pair.Key]++;
    }
}

int similarity = 0;
foreach(KeyValuePair<int, int> pair in leftDupes) {
    similarity += pair.Key * pair.Value;
}

Console.WriteLine($"Similarity: {similarity}");