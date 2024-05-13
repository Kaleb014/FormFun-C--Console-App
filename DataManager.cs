using System;
using System.Collections.Generic;
using System.IO;

namespace FormFun
{
    class DataManager
    {
        public static string savePath = Path.GetTempPath() + @"\forms.sav";
        public static string copyPath = Path.GetTempPath() + @"\copy.tmp";
        public static Dictionary<string, Form> forms = new Dictionary<string, Form>();
        public static Form currentForm;
        public static string oldFormName;
        public static bool formNameChanged = false;

        public static void SaveToBinaryFile<T>(string _filePath, T _objectToWrite, bool _append = false)
        {
            using (Stream stream = File.Open(_filePath, _append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                try
                {
                    binaryFormatter.Serialize(stream, _objectToWrite);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to save file.\n{e}\n" +
                        "Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
            }
        }
        public static void LoadBinaryFromFile(string _filePath)
        {
            Dictionary<string, Form> _tempFormGrid;
            _tempFormGrid = TryLoad<Dictionary<string, Form>>(_filePath);
            if (_tempFormGrid != default)
            {
                forms = _tempFormGrid;
            }
        }
        public static T TryLoad<T>(string _filePath)
        {
            using (Stream stream = File.Open(_filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                try
                {
                    return (T)binaryFormatter.Deserialize(stream);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load save file.\n{e}\n" +
                        "Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    return default;
                }
            }
        }
    }
}
