internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Register Allocation using Graphs)");
        Console.WriteLine("---------------------------------------------------------------\n");

        // Variables
        char[] variables = { 'a', 'b', 'c', 'd' };
        int n = variables.Length;

        // Step 1: Define adjacency matrix (interference graph)
        // 1 = interference, 0 = no interference
        // Example graph:
        // a-b, a-c, b-c
        int[,] graph = new int[,]
        {
                // a  b  c  d
                { 0, 1, 1, 0 },  // a
                { 1, 0, 0, 1 },  // b
                { 1, 0, 0, 0 },  // c
                { 0, 1, 0, 0 }   // d
        };


        string[] registers = { "R1", "R2", "R3" };
        int regCount = registers.Length;
        int[] assigned = new int[n];
        for (int i = 0; i < n; i++) assigned[i] = -1;

       
        for (int i = 0; i < n; i++)
        {
            bool[] used = new bool[regCount];

            // Mark registers used by adjacent nodes
            for (int j = 0; j < n; j++)
            {
                if (graph[i, j] == 1 && assigned[j] != -1)
                    used[assigned[j]] = true;
            }

            // Assign first available register
            for (int r = 0; r < regCount; r++)
            {
                if (!used[r])
                {
                    assigned[i] = r;
                    break;
                }
            }

            // If still unassigned (no register free), mark as spill
            if (assigned[i] == -1)
                assigned[i] = 0; // fallback
        }

        // --- Output
        Console.WriteLine("\nREGISTER ASSIGNMENT RESULT:");
        Console.WriteLine("----------------------------");
        Console.WriteLine("Variable\tRegister");
        Console.WriteLine("----------------------------");

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"{variables[i]}\t\t{registers[assigned[i]]}");
        }
    }
}



    

