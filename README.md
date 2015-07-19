# SimpleJsonConfig
A minimal C# library for managing json config files in the .net framework

# TLDR;

1. Package-Install SimpleJsonConfig
2. Use this code

    var reader = new ConfigReader();
    var value = reader.GetSetting<string>
    ("Testing");

3. Create a folder default and add a file default.json

    Example:
    {testing: "foo"}

