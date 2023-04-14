# Program Runner

Program Runner is a CLI tool written in C# that allows users to run multiple programs or commands at once.

The tool reads a list of programs/commands from a JSON file and executes them in parallel.

## Table of Contents

- [Program Runner](#program-runner)
  - [Table of Contents](#table-of-contents)
  - [Compatibility](#compatibility)
  - [Prerequisites](#prerequisites)
  - [How to build and publish the program](#how-to-build-and-publish-the-program)
  - [Usage](#usage)
  - [Notes](#notes)
  - [License](#license)

## Compatibility

The programs work on Windows, and should work on Linux and macOS.
However, it has only been tested on Windows.

## Prerequisites

To build and publish the program, you will need the following:

- .NET 7 SDK
- Git

## How to build and publish the program

1. Clone the repository: `git clone https://github.com/MASSHUU12/program-runner.git`
2. Navigate to the repository root directory: `cd program-runner`
3. Build the project: `dotnet build -c Release -r <runtime>`
4. Publish the program: `dotnet publish -c Release -r <runtime>`

`Runtime` is the runtime of your target platform (e.g., win-x64 for Windows, linux-x64 for Linux, and osx-64 for macOS).

## Usage

To run the Program Runner, use the following command:

```bash
program-runner.exe run ./path/to/the/list.json <list-name>
```

If `list-name` is omitted, the default list "main" will be used.

The JSON file should be in the following format:

> "Name" is just to make everything look nice in the console, you can leave these fields blank, or write anything there.

```json
[
  {
    "Title": "main",
    "Programs": [
      {
        "Name": "Program 1", // Optional
        "Run": "path/to/program1", // Path to the program, program from PATH or a command
        "Args": "arguments for program 1", // Optional
        "Elevated": true // Run with elevated privileges, optional
      },
      {
        "Path": "path/to/program2",
        "Args": "arguments for program 2"
      },
      {
        "Name": "Command 1",
        "Path": "command",
      }
    ]
  }
]
```

## Notes

To make calling the program simpler, you can add it to the PATH, or create a simple script that calls it with the appropriate arguments.

Sample .bat script for Windows:

```bat
start "" ./path/to/program-runner.exe run ./path/to/lists.json main
```

Such a script you can then put, for example, on the desktop and launch programs with two clicks.

## License

This program is licensed under the MIT license.
See the [LICENSE](https://github.com/MASSHUU12/program-runner/blob/master/LICENSE) file for more information.
