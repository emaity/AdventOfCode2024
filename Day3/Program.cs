using System.Text.RegularExpressions;

// Uses positive lookaround to match the beginning mul( and ending ), leaving only the numbers and comma.
// string pattern = @"(?<=mul\()\d+,\d+(?=\))";
string pattern = @"((?<!(do\(\)|don't\(\)).*)(?<=mul\()\d+,\d+(?=\)))|((?<!don't\(\))(?<=do\(\).*(mul\())(\d+,\d+)(?=\)))";
string text = File.ReadAllText("input.txt");

Regex rgx = new Regex(pattern, RegexOptions.None, TimeSpan.FromSeconds(5));
MatchCollection matches = rgx.Matches(text);

// Match should be two numbers separated by a comma e.g. 12,5
int total = 0;
for(int i=0 ; i < matches.Count ; i++) {
    string[] nums = matches[i].Value.Split(",");
    int left, right;

    if(!int.TryParse(nums[0], out left)) {
        Console.WriteLine("Could not parse, {0}", nums[0]);
    }
    if(!int.TryParse(nums[1], out right)) {
        Console.WriteLine("Could not parse, {0}", nums[1]);
    }

    total += left * right;
}

Console.WriteLine($"Total: {total}");