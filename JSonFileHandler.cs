using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace Json_Editor
{
    public class jsonFileHandler
    {
        public static string ProcessFile(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);

            if (IsJsonLines(fileContent))
            {
                // Process JSON Lines content
                return ProcessJsonLines(fileContent);
            }
            else
            {
                // Process normal JSON content
                return ProcessJson(fileContent);
            }
        }

        private static bool IsJsonLines(string content)
        {
            bool isJsonLines = true;

            using (StringReader reader = new StringReader(content))
            {
                string line;
                bool canParseJson = true;  // Track if any line can be parsed as JSON

                while ((line = reader.ReadLine()) != null)
                {
                    // Attempt to check if each line is valid JSON
                    try
                    {
                        dynamic jsonResult = JsonConvert.DeserializeObject(line);
                        if (jsonResult != null)
                        {
                            // If parsing succeeds for any line, it's not JSON Lines
                            isJsonLines = true;
                            canParseJson = true;
                            break;
                        }
                    }
                    catch (JsonReaderException)
                    {
                        // If parsing fails for any line, continue checking the next lines
                        canParseJson = false;
                    }
                }

                // If no line can be parsed as JSON, it's JSON Lines
                if (!canParseJson)
                {
                    isJsonLines = true;
                }
            }

            return isJsonLines;
        }

        private static string ProcessJson(string content)
        {
            try
            {
                JObject jsonObj = JsonConvert.DeserializeObject<JObject>(content);

                // Check if the necessary properties are present
                if (jsonObj.ContainsKey("timestamp") && jsonObj.ContainsKey("event"))
                {
                    // Return the original file content for valid JSON
                    return content;
                }
                else
                {
                    JsonConvert.DeserializeObject<dynamic>(content);
                    return content;
                }
            }
            catch (JsonReaderException)
            {
                return "Error: Unable to parse the content as JSON.";
            }
        }

        private static string ProcessJsonLines(string content)
        {
            // Implement your logic to process JSON Lines format
            return content;
        }
    }
}