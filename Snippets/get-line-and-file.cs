
Getting the Line Number and File Name from C#
Someone wanted to know what the equivalent "preprocessor macros" in C# are for __FILE__ and __LINE__. They watned to log the current file name and line number. Note that the "1" as the first parameter to the StackFrame constructor tells it to skip ONE frame up the stack, while the true tells it to capture the file and line info.
[STAThread]
static void Main(string[] args)
{
ReportError("Yay!");
}
static private void ReportError(string Message)
{
StackFrame CallStack = new StackFrame(1, true);
Console.Write("Error: " + Message + ", File: " + CallStack.GetFileName() + ", Line: " + CallStack.GetFileLineNumber());
}



