using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.XmlTransform;

namespace ConfigTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                PrintError("Отсутствуют необходимые аргументы");
                return;
            }

            var configFileName = args[0];
            var transformFileName = args[1];
            var appName = args.Length == 3 ? args[2] : null;

            try
            {
                TransformConfig(configFileName, transformFileName, appName);
            }
            catch (Exception e)
            {
                PrintError(e.Message);
            }
        }

        public static void TransformConfig(string configFileName, string transformFileName, string appName)
        {
            var document = new XmlTransformableDocument();
            document.PreserveWhitespace = true;
            document.Load(configFileName);

            var transformation = new XmlTransformation(transformFileName);
            if (!transformation.Apply(document))
            {
                throw new Exception("Transformation Failed");
            }

            var saveFileName = appName != null ? $"{appName}.config" : configFileName;
            document.Save(saveFileName);
        }

        static void PrintError(string message)
        {
            var preColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = preColor;
        }
    }
}
