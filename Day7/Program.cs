namespace Day7;
using System.IO;
using System.Text.RegularExpressions;

class Program {
    enum Operation {
        Add, Multiply
    }
    static bool recur(int pos, List<double> equation, Operation oper, double currentSolution, ref double total) {
        double solution = equation[0];
        if(pos >= equation.Count) {
            if (currentSolution != solution) {
                return false;
            }
            else {
                total += solution;
                return true;
            }
        }
        else if(currentSolution > solution) {
            return false;
        }

        switch(oper) {
            case Operation.Add:
                currentSolution += equation[pos];
                break;
            default:
                currentSolution *= equation[pos];
                break;
        }

        return recur(pos+1, equation, Operation.Add, currentSolution, ref total) || recur(pos+1, equation, Operation.Multiply, currentSolution, ref total);
    }

    static bool testSolutions(List<double> equation, ref double total) {
        return recur(2, equation, Operation.Add, equation[1], ref total) || recur(2, equation, Operation.Multiply, equation[1], ref total);
    }

    static void Main(string[] args) {
        List<List<double>> equations = new List<List<double>>();

        string inputFile = "./Day7/input.txt";
        try {
            StreamReader sr = new StreamReader(inputFile);
            string? line = sr.ReadLine();
            while(line != null) {
                Regex rgx = new Regex(@"\d+", RegexOptions.None, TimeSpan.FromSeconds(5));
                MatchCollection matches = rgx.Matches(line);

                List<double> eq = new List<double>();
                foreach(Match m in matches) {
                    eq.Add(double.Parse(m.Value));
                }

                equations.Add(eq);

                line = sr.ReadLine();
            }
        }
        catch (Exception e) {
            Console.WriteLine($"Exception: {e}");
        }

        double total = 0;
        foreach(List<double> equation in equations) {
            testSolutions(equation, ref total);
        }

        Console.WriteLine(total);
    }
}