using Reflection;

namespace Plugin1
{
    public class Plugin1 : IPlugin
    {
        public string Name { get; set; }

        public Plugin1(string name)
        {
            Name = name;
        }
    }
}
