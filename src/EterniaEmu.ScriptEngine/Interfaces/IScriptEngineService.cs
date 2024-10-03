using EterniaEmu.ScriptEngine.Data;

namespace EterniaEmu.ScriptEngine.Interfaces;

public interface IScriptEngineService
{
    Task ExecuteFileAsync(string file);

    ScriptEngineExecutionResult ExecuteCommand(string command);

    List<ScriptFunctionDescriptor> Functions { get; }

    Dictionary<string, object> ContextVariables { get; }

    Task<string> GenerateTypeDefinitionsAsync();
}
