# SimpleJsonConfig
A minimal C# library for managing json config files in the .net framework.

# TLDR;

1. Package-Install SimpleJsonConfig
2. Use this code

    var reader = new ConfigReader();
    var value = reader.GetSetting<string>
    ("Testing");

3. Create a folder default and add a file default.json

    Example:
    {testing: "foo"}

# Enviroments
The Library uses a convention based method of determining where to look for config files. The default convention is to look in a folder called default that is located in the same directory as the excecuting binary. 

However the idea is to have different configurations for you different environments. This is achieved in 2 ways. 

1. By calling GetSettings with the enviroment variable set. This will look for n folder of the same name and scan for any .json files in this directory. It will then look for the key in all of those files. 
2. By setting the enviroment variable ConfEnv. This is the preferred way. In azure you would need to set this value once int he Application settings. 

 