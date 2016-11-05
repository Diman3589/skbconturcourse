using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Framework;

namespace Application
{
    public class App
    {
        private static List<IPlugin> GetPlugins(string pathToDll)
        {
            var plugins = new List<IPlugin>();
            var files = Directory.GetFiles(pathToDll, "*.dll");
            if (files.Length <= 0) return null;
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);

                foreach (var type in assembly.GetTypes())
                {
                    if (!type.IsClass || type.IsNotPublic) continue;
                    var interfaces = type.GetInterfaces();
                    foreach (var inter in interfaces)
                    {
                        if (!typeof(IPlugin).IsAssignableFrom(inter)) continue;
                        var ctor = type.GetConstructor(new Type[] {});
                        if (ctor == null) continue;
                        var plugin = (IPlugin) ctor.Invoke(new object[] {});
                        plugin.Name = type.Name;
                        plugins.Add(plugin);
                    }
                }
            }
            return plugins.Count < 0 ? null : plugins;
        }

        public static void Main(string[] args)
        {
            const string pluginsPath =
                "c:/users/Dmitry/Documents/Visual Studio 2015/Projects/Git/Reflection/Solution";
            var allPlugins = GetPlugins(pluginsPath);

            foreach (var plugin in allPlugins)
            {
                Console.WriteLine(plugin.Name);
            }
            Console.ReadLine();
        }
    }
}