using System;
using System.Reflection;

namespace Application
{
    public class App
    {
        private static object GetPlugin(string filename, string pluginName)
        {
            var assembly = Assembly.LoadFile(filename);
            var plugin = new object();

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsNotPublic) continue;
                var interfaces = type.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    if (inter.Name != "IPlugin") continue;
                    var ctor = type.GetConstructor(new[] { typeof(string) });
                    if (ctor != null) plugin = ctor.Invoke(new object[] { pluginName });
                }
            }
            return plugin;
        }
        public static void Main(string[] args)
        {
            const string plugin1Dll =
                "c:/users/Dmitry/Documents/Visual Studio 2015/Projects/Git/Reflection/Plugin1/bin/Debug/Plugin1.dll";
            const string plugin2Dll =
                "c:/users/Dmitry/Documents/Visual Studio 2015/Projects/Git/Reflection/Plugin2/bin/Debug/Plugin2.dll";
            var plugin1Object = GetPlugin(plugin1Dll, "Plugin1");
            var plugin2Object = GetPlugin(plugin2Dll, "Plugin2");
            Console.WriteLine(plugin1Object.GetType().GetProperty("Name").GetValue(plugin1Object));
            Console.WriteLine(plugin2Object.GetType().GetProperty("Name").GetValue(plugin2Object));
            Console.ReadLine();
        } 
    }
}