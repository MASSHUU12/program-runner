# Change Log

All notable changes to this project will be documented in this file.

## [Unreleased]

### Added

- The program can add itself to the PATH for simpler startup

### Changed

- The program from now on searches the system PATH and user PATH looking for a program/command to run [TODO]

## [1.2.0 - 2023-04-27]

### Changed

- Better JSON structure
- Name and Args are now fully optional
- Slightly improved some messages
- Restored old way of displaying status updates
- Removed unused package, thus reducing the size of the compiled program
- Updated README to reflect new changes
- Improved error handling

## [1.1.1 - 2023-04-13]

### Added

- Option to run program/command with elevated privileges via JSON or `-e`/`--elevated`

### Changed

- Changed some messages
- Updated README to reflect new changes

## [1.1.0 - 2023-04-10]

### Added

- Options to set the level of log severity, so you can customize what messages to display

### Changed

- Better looking log messages
- Console management via Spectre.Console
- Updated README to reflect recent changes

## [1.0.0 - 2023-04-10]

### Added

- Basic project
