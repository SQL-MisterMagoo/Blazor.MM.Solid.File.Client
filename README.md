# Blazor.MM.Solid.File.Client
Blazor Library Wrapper around solid-file-client

This is a Blazor library that can be used to connect/login to a Solid Pod (https://solid.community) to read files and folders.

### TODO
 - [ ] Finish wrappers for all solid-file-client methods
 - [ ] Add samples for all methods to the sample file browser app.
 
### Features
 - [x] Login
 - [x] Logout
 - [x] ReadFolder
 - [x] ReadFile
 - [x] LoginChanged - event to notify subscriber pages that the user has logged in/out
 - [x] NameChanged - event to notify subscriber pages that the user name (foaf:name) has been retrieved
 
### Usage
 
Add the library as a reference (not published anywhere yet - so you have to build it)
 
#### LoginChanged
```
bool IsLoggedIn;
protected override void OnInit()
{
  SolidFileClient.LoginStateChanged += LoginChanged;
}

void LoginChanged(bool loggedin)
{
  IsLoggedIn = loggedin;
  StateHasChanged();
}
```
#### Login
```
Task DoLogin()
{
  return SolidFileClient.Login(Webid);
}
```
#### Logout
```
Task DoLogout()
{
  return SolidFileClient.Logout();
}
```
#### ReadFolder
```
Folder[] folders;
File[] files;

Task ReadFolder(string path)
{
  var folder = await SolidFileClient.ReadFolder(path);
  folders = folder?.folders;
  files = folder?.files;
}
```
