namespace EterniaEmu.Core.Utils;

public static class EnvUtils
{
    public static string GetRootDirectory()
    {
        var root = Environment.GetEnvironmentVariable("ETERNIAEMU_ROOT");
        if (string.IsNullOrEmpty(root))
        {
            root = Directory.GetCurrentDirectory();
        }

        return root;
    }
}
