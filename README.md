# InterviewGeneratorBlazorHybrid


## TODO

### For selecting a database. 
The following are paths that need to be addressed when the app is opened, or a change is made to the database path. 

[ ] If the application is opened, but there is no database selected.    
[ ] If the application is opened, but the selected database cannot be found.    
[ ] If the application is opened and the previously selected database can be found.    
[ ] If the person chooses to start a new database.    
[ ] If the person chooses to load a database from a sample.    
[ ] If the person wants to do a `Save As` on the database   


For the misix packaging, the launchSettings.json needs to have the following entry added to the "profiles" section:
```
,
    "Windows Machine1": {
      "commandName": "MsixPackage",
      "nativeDebugging": false
    }```
