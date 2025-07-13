using ArchiveModel;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RLArchiveModel
{
    public class archiveButton : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = application.CreateRibbonPanel("RL Tools");

            PushButtonData buttonData = new PushButtonData(
                "MyButton",
                "Archive Model",
                typeof(archiveButton).Assembly.Location,
                "RLArchiveModel.OpenForm");
            buttonData.ToolTip = "Create an archive of the current model";
            PushButton? pushButton = panel.AddItem(buttonData) as PushButton;

            Bitmap? LoadEmbeddedBitmap(string resourceName)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using Stream? stream = assembly.GetManifestResourceStream(resourceName);
                return stream != null ? new Bitmap(stream) : null;
            }

            Bitmap? bitmap = LoadEmbeddedBitmap("RLArchiveModel.Resources.archive.png");

            if (bitmap != null)
            {
                pushButton.LargeImage = ConvertBitmapToImageSource(bitmap);
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private ImageSource ConvertBitmapToImageSource(Bitmap bitmap)
        {
            using MemoryStream memory = new MemoryStream();
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            memory.Position = 0;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        public static string GetModelPath(Document doc)
        {
            if (doc.IsWorkshared)
            {
                ModelPath centralPath = doc.GetWorksharingCentralModelPath();
                return ModelPathUtils.ConvertModelPathToUserVisiblePath(centralPath);
            }
            else
            {
                return doc.PathName;

            }
        }

        public static string GetProjectTitle(Document doc)
        {
            ProjectInfo projectInfo = doc.ProjectInformation;
            return projectInfo.Name; // This is the project title
        }

        public static DateTime? FindLatestArchiveDate(string parentPath, string projectTitle)
        {
            if (!Directory.Exists(parentPath))
                return null;

            var regex = new Regex(@"^(\d{4}-\d{2}-\d{2})\s-\s(.+)$");

            var matchedDate = Directory.GetDirectories(parentPath)
                .Select(dir => Path.GetFileName(dir))
                .Select(name => new { Name = name, Match = regex.Match(name) })
                .Where(x => x.Match.Success && x.Match.Groups[2].Value.Contains(projectTitle, StringComparison.OrdinalIgnoreCase))
                .Select(x => DateTime.TryParse(x.Match.Groups[1].Value, out var date) ? date : DateTime.MinValue)
                .Where(date => date != DateTime.MinValue)
                .OrderByDescending(date => date)
                .FirstOrDefault();

            return matchedDate == DateTime.MinValue ? null : matchedDate;
        }

    }


    [TransactionAttribute(TransactionMode.Manual)]
    public class OpenForm : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            string filePath = doc.PathName;
            string projectTitle = doc.Title;
            string parentDirectory = Path.GetDirectoryName(filePath);
            DateTime? latestDate = RLArchiveModel.archiveButton.FindLatestArchiveDate(parentDirectory, projectTitle);
            string stLatestDate = latestDate.HasValue ? latestDate.Value.ToString("yyyy-MM-dd") : "None";
            CreateArchive form = new CreateArchive(filePath, stLatestDate, parentDirectory, projectTitle);
            form.ShowDialog();

            return Result.Succeeded;
        }
    }
}
