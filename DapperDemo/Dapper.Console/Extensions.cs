using YamlDotNet.Serialization;

namespace Dapper.Console;

public static class Extensions
{
    public static void Output(this object item)
    {
        var serializer = new SerializerBuilder().Build();
        var yaml = serializer.Serialize(item);
        System.Console.WriteLine(yaml);
    }
}