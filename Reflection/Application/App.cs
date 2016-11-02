using System;
using System.Reflection;
using Framework;

namespace Application
{
    public class App
    {
        private static IPlugin GetPlugin(string filename)
        {
            var assembly = Assembly.LoadFile(filename);

            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsNotPublic) continue;
                var interfaces = type.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    if (!typeof(IPlugin).IsAssignableFrom(inter)) continue;
                    var ctor = type.GetConstructor(new Type[] {});
                    if (ctor != null)
                    {
                        return (IPlugin) ctor.Invoke(new object[] {});
                    }
                }
            }
            return null;
        }

        public static void Main(string[] args)
        {
            const string plugin1Dll =
                "c:/users/Dmitry/Documents/Visual Studio 2015/Projects/Git/Reflection/Plugin1/bin/Debug/Plugin1.dll";
            const string plugin2Dll =
                "c:/users/Dmitry/Documents/Visual Studio 2015/Projects/Git/Reflection/Plugin2/bin/Debug/Plugin2.dll";
            var plugin1Object = GetPlugin(plugin1Dll);
            var plugin2Object = GetPlugin(plugin2Dll);

            plugin1Object.Name = "Plugin1";
            plugin2Object.Name = "Plugin2";

            Console.WriteLine(plugin1Object.Name);
            Console.WriteLine(plugin2Object.Name);
            Console.ReadLine();
        }
    }
}