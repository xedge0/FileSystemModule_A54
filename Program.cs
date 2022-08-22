using FileSystemTips;
using static FileSystemTips.FileSystemProgram;

// Call App Object
var app = new FileSystemProgram();

// Creating Config File
app.CreateConfigFile();
app.ReadConfig();

// Creating Folders
app.LineSTART("Creating Folders:");
for(int i = 0; i < app.folders.Length; i++)
    {
    app.CreateDirectory(app.GetFolderByName((FileSystemProgram.FolderNames)i));
    }
app.LineEND();

// Creating a Test File
app.LineSTART("Creating File");
var content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
var path = app.GetFolderByName(FileSystemProgram.FolderNames.SavedData);
var fileName = @"\TestFile.txt";
app.CreateFile(path + fileName, content);

// Creating a Test Image
app.SaveImage("TestImage.jpeg");

// Update Config File
app.ArchiveConfig();

// Archive Data
var ArchiveData = app.GetFolderByName(FileSystemProgram.FolderNames.SavedData);
app.MoveDirectory(ArchiveData);

// Discard Tmp Folder
var discardTemp = app.GetFolderByName(FileSystemProgram.FolderNames.tmp);
app.DeleteDirectory(discardTemp);

app.LineEND();
Console.Read();