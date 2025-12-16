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
        }
        catch (Exception ex)
        {
            // error writing/rewriting files no_file.txt, bad_data.txt, overflow.txt
            Console.WriteLine($"error: can't write or | rewrite files {ex.Message}");
        }
    }
}