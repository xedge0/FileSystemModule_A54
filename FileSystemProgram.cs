using System.Drawing;
using System.Drawing.Imaging;
namespace FileSystemTips;

public class FileSystemProgram
    {
    // Dirs
    public string[] folders =
    {
    @"C:\FIO\Workspace\",
    @"C:\FIO\Workspace\Archive\",
    @"C:\FIO\Workspace\tmp\",
    @"C:\FIO\Workspace\Tmp\SavedData\"
    };

    // Dir Enum
    public enum FolderNames
        {
        Workspace,
        Archive,
        tmp,
        SavedData
        }

    // Create Directory
    public void CreateDirectory(string dirName)
        {

        if(Directory.Exists(dirName))
            Console.WriteLine($"\tDir: Invalid_[Exist]: \t{dirName}");
        else
            {
            Directory.CreateDirectory(dirName);
            Console.WriteLine($"\tDir: Created: \t\t{dirName}");
            }
        }

    // Delete Directory
    public void DeleteDirectory(string dirName)
        {
        LineSTART("Deleting Directory:");
        if(Directory.Exists(dirName))
            {
            //Directory.Delete(dirName);
            Directory.Delete(dirName, true); // for recursive Delete
            Console.WriteLine($"\tDir: Deleted: \t{dirName}");
            }
        else
            {
            Console.WriteLine($"\tDir: Invalid: \t{dirName}");
            }
        }

    // Format Archive Data
    public string AppDataArchive()
        {
        string desDirName;
        desDirName = @"C:\FIO\_AppData_";
        desDirName += DateTime.Now.ToString("yyyyMMddHHmmss");
        return desDirName;
        }

    // Copy & Save Data Folder
    public void MoveDirectory(string srcDirName)
        {
        LineSTART("Moving Directory:");
        if(Directory.Exists(srcDirName))
            {
            Directory.Move(srcDirName, AppDataArchive());
            Console.WriteLine($"\tDir: Move: \t{srcDirName} \t>>>>>\t {AppDataArchive()}");
            }
        else
            {
            Console.WriteLine($"\tDir: Invalid: \t{srcDirName}\t: Dir Invalid");
            }
        }

    // Find Dir by Name
    public string GetFolderByName(FolderNames name)
        {
        return folders[(int)name];
        }

    // Create File
    public void CreateFile(string path, string content)
        {
        File.WriteAllText(path, content);

        FileDataInfo(path, content);
        }

    // Get File Info
    public void FileDataInfo(string f, string? cont = null)
        {
        var fileInfo = new FileInfo(f);
        var name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
        var ext = fileInfo.Extension;
        var size = fileInfo.Length;

        string? content;
        if(cont != null)
            {
            content = $"\tFile Contnet:\n";
            content += "\t============\n";
            content += cont.Replace(". ", ".\n");
            }
        else
            {
            content = null;
            }
        Console.WriteLine($"A file is Created!\n\tCreated File:\t {name}\n\textension:\t {ext}\n\tsize:\t\t {size} bytes\n\n{content}");
        LineEND();
        }

    // Create Config File
    public string ConfigFile
        {
        get { return @"C:\FIO\ConfigFile.CONFIG"; }
        }

    public void CreateConfigFile()
        {
        if(!File.Exists(ConfigFile))
            {
            File.WriteAllLines(ConfigFile, folders);
            }
        }

    // Read Config File
    public void ReadConfig()
        {
        var lines = File.ReadAllLines(ConfigFile);
        var total = lines.Length;

        Array.Resize(ref folders, total);

        LineSTART("CONFIG Data:");
        for(int i = 0; i < total; i++)
            {
            var pathString = lines[i];
            Console.WriteLine($"\t{pathString}");
            folders[i] = pathString;
            }
        LineEND();
        }

    // Archive Config File
    public void ArchiveConfig()
        {
        var configPath = ConfigFile;
        var configName = Path.GetFileName(configPath);
        var tmppath = GetFolderByName(FolderNames.tmp) + configName;
        var newpath = GetFolderByName(FolderNames.SavedData) + configName;

        File.Copy(configPath, tmppath);

        var lines = File.ReadAllLines(tmppath);
        var configString = String.Join(Environment.NewLine, lines);
        var workspaceDirName = Path.GetDirectoryName(GetFolderByName(FolderNames.Workspace));
        var newworkspaceDirName = workspaceDirName + DateTime.Now.ToString("yyyyMMddHHmmss");

        configString = configString.Replace(workspaceDirName, newworkspaceDirName);

        File.WriteAllText(tmppath, configString);

        File.Move(tmppath, newpath);

        lines = File.ReadAllLines(newpath);
        var total = lines.Length;
        LineSTART("New CONFIG Data:");
        for(int i = 0; i < total; i++)
            {
            Console.WriteLine($"\t{lines[i]}");
            }
        LineEND();
        }

    // Save a Bitmap Image
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public void SaveImage(string m)
        {
        var imageFileName = GetFolderByName(FolderNames.SavedData) + m;

        var bitmap = new Bitmap(128, 128, PixelFormat.Format24bppRgb);
        var g = Graphics.FromImage(bitmap);
        g.Clear(Color.Magenta);

        LineSTART("Drawing an Image:");
        bitmap.Save(imageFileName, ImageFormat.Jpeg);
        Console.WriteLine($"\tImage:\t{m} \t !Done");
        LineEND();
        }

    // Line Start Format
    public void LineSTART(string? s = null)
        {
        Console.WriteLine(s);
        Console.WriteLine("==========");
        }

    //Line End Format
    public void LineEND()
        {
        Console.WriteLine("========================================================================");
        }
    }