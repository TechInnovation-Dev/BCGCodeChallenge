using System.Text;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;
public static class FilesReadWrite
{
    
    public static string CreateJsonFile(IList<Product> data)
    {

        string filePath = GetFileLocation();
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

        
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        // Write the JSON data to the file
        File.WriteAllText(filePath, jsonData);
        string result = $"Data written to {filePath}";
        return result;

    }
    public static List<Product> ReadFromJsonFile()
    {
        string filePath = GetFileLocation();
        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(filePath);

            // Deserialize the JSON data to the specified type
            List<Product> result = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            return result;
        }
        else
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }
    }
    private static string GetFileLocation()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
       
        int index = currentDirectory.IndexOf("ECommerceApi", StringComparison.OrdinalIgnoreCase);

        // If "ECommerceApi" is found, trim the string up to that point
        if (index != -1)
        {
             currentDirectory = currentDirectory.Substring(0, index + "ECommerceApi".Length);
        }
         currentDirectory = currentDirectory + "/files/products.Json";
        return currentDirectory;
    }
}