using System.IO;

List<List<int>> rules = new List<List<int>>();
List<List<int>> pages = new List<List<int>>();

// Read input file
try {
    StreamReader sr = new StreamReader("input.txt");
    string? line = sr.ReadLine();
    while(line != null && line != "") {
        string[] splitLine = line.Split('|');
        rules.Add(new List<int>{int.Parse(splitLine[0]) , int.Parse(splitLine[1])} );

        line = sr.ReadLine();
    }

    line = sr.ReadLine();

    while(line != null) {
        string[] splitLine = line.Split(',');
        List<int> page = new List<int>();

        foreach(string num in splitLine) {
            page.Add(int.Parse(num));
        }
        pages.Add(page);

        line = sr.ReadLine();
    }
}
catch(Exception e) {
    Console.WriteLine($"Exception: {e}");
}

Dictionary<int, HashSet<int>> inList = new Dictionary<int, HashSet<int>>();
Dictionary<int, HashSet<int>> outList = new Dictionary<int, HashSet<int>>();

foreach(List<int> rule in rules) {
    int first = rule[0];
    int second = rule[1];

    if(inList.ContainsKey(second)) {
        inList[second].Add(first);
    }
    else {
        inList.Add(second, new HashSet<int>{first});
    }

    if(outList.ContainsKey(first)) {
        outList[first].Add(second);
    }
    else {
        outList.Add(first, new HashSet<int>{second});
    }
}

// foreach(KeyValuePair<int, HashSet<int>> pair in inList) {
//     Console.Write($"Number {pair.Key}: ");
//     Console.WriteLine(string.Join(", ", pair.Value));
// }
// Console.WriteLine();

int correctTotal = 0;
int incorrectTotal = 0;
// Iterate through each page
foreach(List<int> page in pages) {
    HashSet<int> numbers = new HashSet<int>();
    foreach(int num in page) {
        numbers.Add(num);
    }

    // Create inlist and outlist for this page
    Dictionary<int, HashSet<int>> currInList = new Dictionary<int, HashSet<int>>();
    Dictionary<int, HashSet<int>> currOutList = new Dictionary<int, HashSet<int>>();
    Stack<int> toVisit = new Stack<int>();
    foreach(int num in page) {
        if(inList.ContainsKey(num)) {
            currInList.Add(num, new HashSet<int>(inList[num]));
            currInList[num].IntersectWith(numbers);

        } else {
            currInList.Add(num, new HashSet<int>());
        }
        if(currInList[num].Count == 0)
            toVisit.Push(num);

        if(outList.ContainsKey(num)) {
            currOutList.Add(num, new HashSet<int>(outList[num]));
            currOutList[num].IntersectWith(numbers);
        }
    }
    // foreach(KeyValuePair<int, HashSet<int>> pair in currInList) {
    //     Console.Write($"Number {pair.Key}: ");
    //     Console.WriteLine(string.Join(", ", pair.Value));
    // }
    // Console.WriteLine();

    // Create topological sorted order
    List<int> ordered = new List<int>();
    while(toVisit.Count != 0) {
        int curr = toVisit.Pop();
        ordered.Add(curr);

        if(!currOutList.ContainsKey(curr))
            continue;

        foreach(int num in currOutList[curr]) {
            if(currInList.ContainsKey(num)) {
                currInList[num].Remove(curr);
                if(currInList[num].Count == 0) {
                    toVisit.Push(num);
                }
            }
        }
    }

    // Add up middle page numbers
    if(ordered.SequenceEqual(page)) {
        correctTotal += page[page.Count/2];
    } else {
        incorrectTotal += ordered[ordered.Count/2];
    }


    // Console.WriteLine(ordered.SequenceEqual(page));

    // Console.WriteLine(string.Join(", ", ordered));
    // Console.WriteLine(string.Join(", ", page));
}

Console.WriteLine($"Correct page total: {correctTotal}");
Console.WriteLine($"Incorrect page total: {incorrectTotal}");