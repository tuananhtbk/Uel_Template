@echo off
rem ============================================================================
rem  Build plugin UEL.  build.bat [che do] [ten component] [bo]
rem
rem    build.bat                       build tat ca: 18.0\, 2020+\, _Dev\ va EXE thu nghiem
rem    build.bat core                  CHI build Core ban DEV (sua-thu nhanh, Tekla van mo)
rem    build.bat core RAKER+T_GIRDER   ... chi 1 component
rem    build.bat core RAKER+T_GIRDER 2020   ... chi bo 2020+
rem    build.bat dev                   Core + <Ten>_DEV.dll (can cai lai vao Tekla: install.bat dev ...)
rem    build.bat release               18.0\<Ten>.dll va 2020+\<Ten>.dll
rem    build.bat tester                _Dev\UEL_TESTER.exe
rem    build.bat check                 build tat ca + kiem tra voi moi ban Tekla tren may
rem
rem  Chi tiet: _Tools\build.ps1 va README.md
rem ============================================================================
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0_Tools\build.ps1" %*
exit /b %errorlevel%
