@echo off
REM Check .NET SDK version
dotnet --version | findstr /R "^10\." > nul
if errorlevel 1 (
    echo This script requires .NET SDK version 10.x.
    exit /b 1
)
set "projectName=ProductService"
set "solutionName=EcommerceApp"

REM Create solution
if not exist "%solutionName%.slnx" (
    dotnet new sln -n %solutionName%
) else (
    echo Solution %solutionName%.slnx already exists.
)

mkdir %projectName%
cd %projectName%
mkdir src
cd src
mkdir core
cd core
dotnet new classlib -n %projectName%.domain
dotnet new classlib -n %projectName%.application
cd ..
mkdir external
cd external
dotnet new webapi --use-controllers -n %projectName%.api
dotnet new classlib -n %projectName%.infrastructure
cd ../..

REM Infrastructure references
dotnet add src\external\%projectName%.infrastructure\%projectName%.infrastructure.csproj reference src\core\%projectName%.domain\%projectName%.domain.csproj
dotnet add src\external\%projectName%.infrastructure\%projectName%.infrastructure.csproj reference src\core\%projectName%.application\%projectName%.application.csproj
REM API references
dotnet add src\external\%projectName%.api\%projectName%.api.csproj reference src\core\%projectName%.application\%projectName%.application.csproj
dotnet add src\external\%projectName%.api\%projectName%.api.csproj reference src\external\%projectName%.infrastructure\%projectName%.infrastructure.csproj
REM Application references
dotnet add src\core\%projectName%.application\%projectName%.application.csproj reference src\core\%projectName%.domain\%projectName%.domain.csproj
cd ..

REM Add projects to the solution
dotnet sln %solutionName%.slnx add %projectName%\src\core\%projectName%.domain\%projectName%.domain.csproj
dotnet sln %solutionName%.slnx add %projectName%\src\core\%projectName%.application\%projectName%.application.csproj
dotnet sln %solutionName%.slnx add %projectName%\src\external\%projectName%.infrastructure\%projectName%.infrastructure.csproj
dotnet sln %solutionName%.slnx add %projectName%\src\external\%projectName%.api\%projectName%.api.csproj

echo Solution and project setup complete.