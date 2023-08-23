using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        string pasta = @"C:\MINIO\LECLAIR\Compras\Analitica\TABELA_COMPRA\Created";
        
        List<string> jsonFiles = Directory.GetFiles(pasta, "*.json", SearchOption.TopDirectoryOnly).ToList();
        
        HashSet<int> idSet = new HashSet<int>();
        
        foreach (string jsonFile in jsonFiles)
        {
            string jsonContent = File.ReadAllText(jsonFile);
            
            JsonDocumentOptions options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };
            
            using (JsonDocument doc = JsonDocument.Parse(jsonContent, options))
            {
                JsonElement root = doc.RootElement;
                
                if (root.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in root.EnumerateArray())
                    {
                        int id = element.GetProperty("R_E_C_N_O_").GetInt32();
                        
                        if (idSet.Contains(id))
                        {
                            Console.WriteLine($"ID {id} está duplicado no arquivo {jsonFile}");
                        }
                        else
                        {
                            idSet.Add(id);
                        }
                    }
                }
            }
        }
    }
}