class Program
{
    static void Main()
    {
        try
        {
            // GenerateFiles();

            // writing/rewriting files
            using (File.Create("no_file.txt")) { }
            using (File.Create("bad_data.txt")) { }
            using (File.Create("overflow.txt")) { }

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
                        // first two lines into int
                        int num1 = int.Parse(lines[0]);
                        int num2 = int.Parse(lines[1]);

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
        catch (OverflowException ov)
        {
            Console.WriteLine("Overflow");
        }
        catch (Exception ex)
        {
            // error writing/rewriting files no_file.txt, bad_data.txt, overflow.txt
            Console.WriteLine($"error: can't write or | rewrite files {ex.Message}");
        }
    }

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