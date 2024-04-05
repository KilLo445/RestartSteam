# RestartSteam
Very simple command line program I made that restarts Steam, I use this pretty often so I thought other people might too.

# Installation
1. Download the latest release and put it in a safe location, such as `%localappdata%\Programs\RestartSteam`
2. Create a shortcut to `RestartSteam.exe` in `C:\ProgramData\Microsoft\Windows\Start Menu\Programs`
   - If you do this, the program will show up in Windows Search, so you can just search "RestartSteam"
   - Additionally, you can add `-yes` or `-y` to the end of the shortcut to bypass the confirmation prompt
3. Restart `explorer.exe` for it to show up in Windows Search
   - You can do this in Task Manager by Right Clicking on `Windows Explorer` and selecting `Restart`
   - Alternatively, open `CMD` and run this command, `taskkill /F /IM explorer.exe & start explorer`
# Arguments
- -help
- -github `Opens GitHub page`
- -yes, -y `Skip confirmation`
- -debug `Runs the app in debug mode`
- -version, -ver `Shows installed version`
