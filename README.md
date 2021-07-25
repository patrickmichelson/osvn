# OfflineSVN

A command line tool to open SVN URLs from a local working copy.

Useful for scenarios when having a URL to a document under SVN version control, and the SVN server is at the moment not available (e.g. no access to network of SVN server).
If the document exists in a local SVN working copy, the tool will look up the file and open it from disk.

## Requirements

.NET Core 3.1 (To run app: [Runtime](https://dotnet.microsoft.com/download/dotnet/3.1/runtime) | To build app: [SDK](https://dotnet.microsoft.com/download/dotnet/3.1))

## How To Use

1. Build and publish.

```bat
dotnet publish ./src -c Release -o <target folder>
```

2. Edit `workingcopies.txt` in target folder and add your working copy paths.

3. Add target folder to PATH environment variable.

3. Press Win+R and enter `osvn <your svn url>`
