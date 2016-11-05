using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
                    if (interfaces.Contains(typeof(IPlugin)))
                    {
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

            Expression<Func<double, double>> f = x => (10 + Math.Sin(x)) * x;
            var g = Differentiator.Differentiate(f);
            var compiled = g.Compile();
            Console.WriteLine(compiled.Invoke(12));
            Console.ReadLine();
        }
    }
}