# QPass [![CodeFactor](https://www.codefactor.io/repository/github/shiroechi/qpass-password-manager/badge)](https://www.codefactor.io/repository/github/shiroechi/qpass-password-manager)
**This project was made for university project and used for learning only.**

A simple password manager using [rinjdael algortihm](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard) as the core for encrypt and decrypt.

## Getting Started
 
### Prerequisites
Before build this project you need to add reference:
1. [BouncyCastle](https://www.nuget.org/packages/BouncyCastle/)
2. [System.Data.SQLite](https://www.nuget.org/packages/System.Data.SQLite.Core/)

for sqlite, you need copy SQLite.Interop.dll into output folder.

anyone that don't want to download the depedencies, i have prepared it ready in [Library folder](/Library)

### Issue
**Update 2020**

There is a small problem with the master password input, sometimes when opening and closing the database it will make correctly entered master password considered wrong by the program, this is actually a bug and can be overcome by restarting the program.

## Build with
* [Visual Studio 2017 build 15.9](https://visualstudio.microsoft.com/downloads/)

## Credits
Thanks to [BouncyCastle](https://www.bouncycastle.org/) and [System.Data.SQLite](http://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki)
