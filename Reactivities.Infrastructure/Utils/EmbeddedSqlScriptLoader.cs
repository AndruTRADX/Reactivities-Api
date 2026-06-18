using System.Reflection;

namespace Reactivities.Infrastructure.Utils;

public static class EmbeddedSqlScriptLoader
{
    public static string Load(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var resourceName = assembly
            .GetManifestResourceNames()
            .SingleOrDefault(name => name.EndsWith(fileName, StringComparison.OrdinalIgnoreCase))
            ?? throw new FileNotFoundException(
                $"The embedded resource '{fileName}' was not found in the assembly'{assembly.GetName().Name}'.");

        using var stream = assembly.GetManifestResourceStream(resourceName)!;
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}