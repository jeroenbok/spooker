@echo off
set NUGET_EXE=NuGet.exe
set "NUGET_PACKAGES_REPOSITORY=%CD%\packages"
set "CURRENT_DIR=%CD%"

:_INITIALIZE (
	echo ^> Environment
	if not "%NUGET_HOME%" == "" goto _VALIDATE_NUGET_EXE
	set "NUGET_HOME=%CURRENT_DIR%"
	goto _VALIDATE_NUGET_EXE
)

:_VALIDATE_NUGET_EXE (
	if exist "%NUGET_HOME%\%NUGET_EXE%" goto _INSTALL_PACKAGES
	echo NuGet         : %NUGET_HOME%\%NUGET_EXE% [ERROR]
	echo.
	echo                 %NUGET_EXE% does not exist in the given location, but is required to install packages.
	echo                 Suggested solutions:
	echo.
    echo                 - Configure the environment variable 'NUGET_HOME' to a directory containing %NUGET_EXE%.
    echo                 - Copy %NUGET_EXE% to %NUGET_HOME%.
	goto _END
)

:_INSTALL_PACKAGES (
	echo NuGet          : %NUGET_HOME%\%NUGET_EXE%
	echo NuGet packages : %NUGET_PACKAGES_REPOSITORY%
	echo ^> Scanning packages
	for /f "usebackq delims=|" %%f in (`dir /a:d /b /o:n /s`) do (
		if exist "%%f\packages.config" (
			echo In %%f
			"%NUGET_HOME%\%NUGET_EXE%" install "%%f\packages.config" -o "%NUGET_PACKAGES_REPOSITORY%"
		)
	)
)

:_END (
    echo ^> Done
	pause > nul
)
