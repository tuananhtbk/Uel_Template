@echo off
rem Dong goi bo cai de mang sang may khac: _Dist\UEL_<ngay>\
rem   18.0\*.dll, 2020+\*.dll, install.bat, _Tools\install.ps1, _Tools\tekla.ps1, HUONG_DAN_CAI.txt
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0_Tools\pack.ps1" %*
exit /b %errorlevel%
