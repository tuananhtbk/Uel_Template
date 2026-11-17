@echo off
rem ============================================================================
rem  Cai plugin UEL vao Tekla Structures.
rem
rem    install.bat 18                      18.0\*.dll  -> Tekla 18.0
rem    install.bat 2020                    2020+\*.dll -> Tekla 2020  (tuong tu 2021, 2025)
rem    install.bat all                     moi ban Tekla tim thay tren may
rem    install.bat 2025 RAKER+T_GIRDER     chi 1 component
rem    install.bat dev 2020                ban DEV (<Ten>_DEV.dll) -> Tekla 2020
rem
rem  Tekla 2019 tro di nam trong Program Files: chuot phai - Run as administrator
rem  (hoac mo cmd quyen admin). Tat ban Tekla can cai truoc khi chep de len DLL cu.
rem ============================================================================
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0_Tools\install.ps1" %*
set "RC=%errorlevel%"
if "%~1"=="" pause
exit /b %RC%
