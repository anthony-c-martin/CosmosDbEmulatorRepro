START "" "%ProgramFiles%\Azure Cosmos DB Emulator\CosmosDB.Emulator.exe" /NoUI /NoExplorer /Consistency=Strong /DisableRateLimiting /DefaultPartitionCount=1 /PartitionCount=100

:wait
  timeout /t 6 >NUL

  START /wait "" "%ProgramFiles%\Azure Cosmos DB Emulator\CosmosDB.Emulator.exe" /GetStatus
  ECHO %TIME% Azure Cosmos DB Emulator status: %ErrorLevel% (1 = Starting, 2 = Running, 3 = Stopped)
if %ErrorLevel% EQU 1 goto wait

echo %TIME% Azure Cosmos DB Emulator Started

exit /B 0