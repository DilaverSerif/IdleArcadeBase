public interface ISavable
{
    public string SaveKey { get; }
    public void Save();
    public void Load();
}