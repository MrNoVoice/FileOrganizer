using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FileOrganizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the File Organizer App!");

            string inputPath = string.Empty;

            // Keep asking for a valid path until the user provides one
            while (true)
            {
                Console.WriteLine();

                // Check if the user provided a directory path
                Console.Write("Enter the path of the directory you want to organize (or type 'exit' to quit): ");
                inputPath = Console.ReadLine();

                if (inputPath.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting the program.");
                    return;
                }

                // Check for empty or null input
                if (string.IsNullOrEmpty(inputPath))
                {
                    Console.WriteLine("No path provided. Please enter a valid path.");
                    continue;
                }

                // Check if the directory exists
                if (!Directory.Exists(inputPath))
                {
                    Console.WriteLine("The directory does not exist. Please provide a valid directory.");
                    continue;
                }

                // If both checks pass, break out of the loop
                break;
            }

            // Get files from the directory
            var filesInDirectory = Directory.GetFiles(inputPath);

            if (filesInDirectory.Length == 0)
            {
                Console.WriteLine("The directory is empty. No files to organize.");
                return;
            }

            // Display the files in the directory
            Console.WriteLine("\nFiles in the directory:");
            foreach (var file in filesInDirectory)
            {
                Console.WriteLine(file);
            }

            // Define categories for file types
            var categories = new Dictionary<string, string>
                {
                    { ".jpg", "Images" },
                    { ".jpeg", "Images" },
                    { ".png", "Images" },
                    { ".gif", "Images" },
                    { ".bmp", "Images" },
                    { ".tiff", "Images" },
                    { ".webp", "Images" },

                    { ".txt", "Documents" },
                    { ".doc", "Documents" },
                    { ".docx", "Documents" },
                    { ".pdf", "Documents" },
                    { ".odt", "Documents" },
                    { ".rtf", "Documents" },
                    { ".xls", "Documents" },
                    { ".xlsx", "Documents" },
                    { ".ppt", "Documents" },
                    { ".pptx", "Documents" },

                    { ".mp4", "Videos" },
                    { ".mkv", "Videos" },
                    { ".avi", "Videos" },
                    { ".mov", "Videos" },
                    { ".wmv", "Videos" },
                    { ".flv", "Videos" },
                    { ".webm", "Videos" },
                    { ".mpg", "Videos" },
                    { ".mpeg", "Videos" },

                    { ".mp3", "Audio" },
                    { ".wav", "Audio" },
                    { ".flac", "Audio" },
                    { ".aac", "Audio" },
                    { ".ogg", "Audio" },
                    { ".wma", "Audio" },

                    { ".zip", "Archives" },
                    { ".rar", "Archives" },
                    { ".7z", "Archives" },
                    { ".tar", "Archives" },
                    { ".gz", "Archives" },
                    { ".iso", "Archives" },

                    { ".html", "Web Files" },
                    { ".css", "Web Files" },
                    { ".js", "Web Files" },
                    { ".json", "Web Files" },
                    { ".xml", "Web Files" },

                    { ".exe", "Programs" },
                    { ".bat", "Programs" },
                    { ".msi", "Programs" },
                    { ".apk", "Programs" },

                    { ".sqlite", "Databases" },
                    { ".db", "Databases" },

                    // Add more file extensions and categories as needed
                };

            Console.WriteLine();

            // Create directories for each category if not already created
            foreach (var category in categories.Values.Distinct()) // Unique category names
            {
                var categoryPath = Path.Combine(inputPath, category);
                if (!Directory.Exists(categoryPath))
                {
                    Directory.CreateDirectory(categoryPath);
                    Console.WriteLine($"Created directory: {categoryPath}");
                }
            }

            // Move files to their respective directories
            foreach (var file in filesInDirectory)
            {
                var extension = Path.GetExtension(file).ToLower();  // Get file extension
                if (categories.ContainsKey(extension))
                {
                    var category = categories[extension];  // Get the category for the file type
                    var destinationPath = Path.Combine(inputPath, category, Path.GetFileName(file));  // Create destination path

                    // Check if the file already exists in the target folder
                    if (File.Exists(destinationPath))
                    {
                        Console.WriteLine($"Warning: File {Path.GetFileName(file)} already exists in the {category} folder. Skipping.");
                    }
                    else
                    {
                        // Move the file and provide feedback
                        try
                        {
                            File.Move(file, destinationPath);
                            Console.WriteLine($"Moved: {Path.GetFileName(file)} to {category} folder.");
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Error moving file {Path.GetFileName(file)}: {ex.Message}");
                        }
                    }
                }
            }

            Console.WriteLine("File organization complete!");
            Console.ReadLine();
        }
    }
}
