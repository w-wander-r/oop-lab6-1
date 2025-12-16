class Program
{
    static void Main()
    {
        try
        {
            // writing/rewriting files
            using (File.Create("no_file.txt")) { }
            using (File.Create("bad_data.txt")) { }
            using (File.Create("overflow.txt")) { }
            
            for (int i = 10; i <= 29; i++)
            {
                string fileName = $"{i}.txt";

                try
                {
                    string[] lines = File.ReadAllLines(fileName);

                    try
                    {
                        // first two lines into int
                        int num1 = int.Parse(lines[0]);
                        int num2 = int.Parse(lines[1]);

                        try
                        {
                            // todo
                        }
                        catch (Exception)
                        {
                            // todo
                        }
                    }
                    catch (FormatException)
                    {
                        // if can't read that data
                        File.AppendAllText("bad_data.txt", fileName + Environment.NewLine);
                    }
                }
                catch (FileNotFoundException)
                {
                    // if file not found
                    File.AppendAllText("no_file.txt", fileName + Environment.NewLine);
                }
            }

        }
        catch (Exception ex)
        {
            // error writing/rewriting files no_file.txt, bad_data.txt, overflow.txt
            Console.WriteLine($"error: can't write or | rewrite files {ex.Message}");
        }
    }
}