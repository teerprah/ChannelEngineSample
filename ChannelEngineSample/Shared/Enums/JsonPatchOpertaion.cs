namespace Shared.Enums;

public class JsonPatchOpertaion : Enumeration
{
    private JsonPatchOpertaion(int id, string name) : base(id, name)
    {
        
    }

    public static JsonPatchOpertaion Add => new(1, "add");
    public static JsonPatchOpertaion Remove => new(1, "remove");
    public static JsonPatchOpertaion Replace => new(1, "replace");
}