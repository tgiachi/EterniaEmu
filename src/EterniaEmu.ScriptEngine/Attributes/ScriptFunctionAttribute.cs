namespace EterniaEmu.ScriptEngine.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class ScriptFunctionAttribute : Attribute
{
    public string Alias { get; set; }
    public string? Help { get; set; }

    public ScriptFunctionAttribute(string? alias, string? help = null)
    {
        Alias = alias ?? string.Empty;
        Help = help;
    }
}
