class Program
{
    static void Main()
    {
        try
        {
            // GenerateFiles();

            // writing/rewriting files
            using (File.Create("no_file.txt")) { } // log file for missing files
            using (File.Create("bad_data.txt")) { } // log file for when data file contais corrupted data
            using (File.Create("overflow.txt")) { } // log file for when data file contais >int32 || product >int32

            int counterProduct = 0;
            long sumProduct = 0;

            for (int i = 10; i <= 29; i++)
            {
                string fileName = $"{i}.txt";

                try
                {
                    string[] lines = File.ReadAllLines(fileName);

                    try
                    {
                        // parsing two firs lines into int32
                        int num1 = int.Parse(lines[0]);
                        int num2 = int.Parse(lines[1]);

                        // trying to catch the overflow if the product can't fit in int32
                        try
                        {
                            checked
                            {
                                int product = num1 * num2;
                                counterProduct++;
                                sumProduct += product;
                            }
                        }
                        catch (OverflowException)
                        {
                            File.AppendAllText("overflow.txt", fileName + Environment.NewLine);
                        }
                    }
                    catch (FormatException)
                    {
                        // if can't read that data
                        File.AppendAllText("bad_data.txt", fileName + Environment.NewLine);
                    }
                    catch (OverflowException)
                    {
                        // if numders parsed from file can't fit in int32
                        File.AppendAllText("overflow.txt", fileName + Environment.NewLine);
                    }
                }
                catch (FileNotFoundException)
                {
                    // if file not found
                    File.AppendAllText("no_file.txt", fileName + Environment.NewLine);
                }
            }

            // calculate avg
            if (counterProduct > 0)
            {
                double avg = (double)sumProduct / counterProduct;
                Console.WriteLine($"avg: {avg}");
            }
            else
            {
                Console.WriteLine("no product");
            }
        }
        catch (Exception ex)
        {
            // error writing/rewriting files no_file.txt, bad_data.txt, overflow.txt
            Console.WriteLine($"error: can't write or | rewrite files {ex.Message}");
        }
    }

    // method for generating files
    static void GenerateFiles()
    {
        Random rnd = new Random();

        for (int i = 10; i <= 29; i++)
        {
            string fileName = $"{i}.txt";

            int num1 = rnd.Next(-10000, 10000);
            int num2 = rnd.Next(-10000, 10000);

            File.WriteAllText(fileName, $"{num1}{Environment.NewLine}{num2}");
        }
    }
}