# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2022-09-17

### Added
- Comment removal can be disabled by setting the scripting define symbol DISABLE_COMMENTREMOVER

## [1.0.1] - 2022-09-16

### Changed
- Improved compatibility with different Unity versions (full support for 2020.1 or newer and limited support for 2019)


## [1.0.0] - 2022-09-13

### This is the initial release.
- Comments can be added to GameObjects
- If no comment text is set, the component shows the text editing UI
- Text editing also allows select an icon type: None, Info, Warning
- Ending text editing displays the readonly UI with text and icon
- The editing state is persisted for the duration of the editor session
- Text editing can be started by double clicking the message or via CTRL + Enter/Return
- Comments in scenes are removed during the build process
- There's a preferences item to enable logging of removed comments
- The removal log indicates the relevant scene and GameObject hierarchy paths
