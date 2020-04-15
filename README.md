# World-map-generator

![Earth map generated by Weltschmerz](https://github.com/starandserpent/World-map-generator/blob/dev/screenshots/GeneratorCapture.png)

## Table of Contents

* [About the Project](#about-the-project)
* [Releases](#releases)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Cloning project](#cloning-project)
  * [Building project](#building-project)
* [Usage](#usage)
* [License](#license)
* [Contact](#contact)
  * [Follow us](#follow-us)


## About the project

World map generator is standalone desktop application which uses [Weltschmerz](https://github.com/starandserpent/World-map-generator) library to generate realistically looking biome maps.

### How is this project related to Rituals of the old?
1) Visual reprensentation of Weltschmerz (if you like the maps, please consider trying Weltschmerz library in your project)
2) Exporting config files for the game (you can use this application to generate maps you want in the game)


## Releases
Check [releases](https://github.com/starandserpent/World-map-generator/releases) to download latest version.

## Getting Started

If you want to participate in a develepment or if you do not want to download last release then follow the instructions to get a copy of the aplication

### Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

* *.NET Desktop development* and *Universal Windows Platform Develpment*
* [Godot Engine](https://godotengine.org/download/windows) (Last application version currently supports Godot version 3.2.1)
* [Git](https://git-scm.com/downloads) - OSX and Linux machines typically have this already installed.

### Cloning project
Steps to run the apllication in godot engine

1) Clone repository
```
git clone git@github.com:starandserpent/World-map-generator.git --recursive
```

2) Update package references in project folder
```
nuget restore
cd modules
cd Weltschmerz
nuget restore
```

3) Import project to godot

### Building project
Steps to make a project build
1) Open godot
2) Project -> Export -> Add -> <your platform>
3) Click **Manage Export Templates**
4) Download template
4) Export project

For more detailed information see [Godot documentation](https://docs.godotengine.org/en/3.1/getting_started/workflow/export/exporting_projects.html)

## Usage

For usage instructions, please check our [github wiki](https://github.com/starandserpent/World-map-generator/wiki/Usage)

## Licence
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Contact us
* Discord: https://discord.starandserpent.com/

### Follow us
* Website: https://www.ritualsoftheold.com/
* IndieDB: https://www.indiedb.com/games/rituals-of-the-old
* Subscribe: https://www.youtube.com/c/Ritualsoftheold