namespace EterniaEmu.ScriptEngine.Data;

public class ScriptEngineExecutionResult
{
    public object Result { get; set; } = null!;
    public Exception? Exception { get; set; }
}
