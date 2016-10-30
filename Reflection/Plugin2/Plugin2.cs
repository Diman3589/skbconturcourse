using Reflection;

namespace Plugin2
{
    public class Plugin2 : IPlugin
    {
        public string Name { get; set; }

        public Plugin2(string name)
        {
            Name = name;
        }
    }
}
