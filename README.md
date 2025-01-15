# RestartSteam
Very simple command line program I made that restarts Steam, I use this pretty often so I thought other people might too.

### Built With

* [![.NET][.NET]][framework-url]

# Installation
1. Download `RestartSteam.zip` from the [latest release](https://github.com/KilLo445/RestartSteam/releases/latest)
2. Extract the contents to a safe location such as `%localappdata%\Programs\RestartSteam` using a program like [7-Zip](https://7-zip.org)
3. Create a shortcut to `RestartSteam.exe` in `C:\ProgramData\Microsoft\Windows\Start Menu\Programs`
   - If you do this, the program will show up in Windows Search, so you can just search "RestartSteam"
   - Make sure to add `-r` to the end of the shortcut, additionally, you can add `-y` to the end of the shortcut to bypass the confirmation prompt
   - Recomended shortcut would look similar to `"C:\RestartSteam\RestartSteam.exe" -r -y`
4. Restart `explorer.exe` for it to show up in Windows Search
   - You can do this in Task Manager by Right Clicking on `Windows Explorer` and selecting `Restart`
   - Alternatively, open `CMD` and run this command, `taskkill /F /IM explorer.exe & start explorer`
# Arguments
- -help `Displays all available commands`
- -github `Opens GitHub page`
- -restart `Restart Steam`
- -yes `Skip confirmation`
- -debug `Runs the app in debug mode`
- -version `Shows installed version`

[.NET]: https://img.shields.io/badge/.NET_Framework-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[Framework]: https://img.shields.io/badge/.NET_Framework-4.8-purple
[framework-url]: https://dotnet.microsoft.com/en-us/download/dotnet-framework
