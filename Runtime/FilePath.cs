using System;

public struct FilePath
{
    public string Path { get; }

    public FilePath(string path)
    {
        Path = path.Replace('\\', '/') ?? throw new ArgumentNullException(nameof(path));
    }

    public static FilePath operator /(FilePath left, FilePath right) =>
        new FilePath(System.IO.Path.Combine(left.Path, right.Path).Replace('\\', '/'));

    public static FilePath operator /(FilePath left, string right) =>
        new FilePath(System.IO.Path.Combine(left.Path, right).Replace('\\', '/'));

    public static implicit operator string(FilePath filePath) => filePath.Path;

    public override string ToString() => Path;
}
