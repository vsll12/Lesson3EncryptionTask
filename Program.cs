using System;
using System.IO;
using System.Text;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter the path: ");
        var fileName = Console.ReadLine()!;

        Console.Write("Enter the key : ");
        var key = char.Parse(Console.ReadLine()!);

        ThreadPool.QueueUserWorkItem(o =>
        {
                EncryptFile(fileName, key);
                Console.WriteLine("File encryption complete.");
        });

        Console.ReadKey();
    }

    private static void EncryptFile(string path, char key)
    {
        try
        {
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);

                var sb = new StringBuilder();

                foreach (var item in content)
                {
                    sb.Append((char)(item ^ key));
                }

                var directory = Path.GetDirectoryName(path); 
                var newFileName = Path.GetFileNameWithoutExtension(path) + "Encrypted.txt";
                var newPath = Path.Combine(directory!, newFileName);

                File.WriteAllText(newPath, sb.ToString());
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
