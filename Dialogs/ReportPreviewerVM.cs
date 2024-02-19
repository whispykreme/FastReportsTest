using FastReport;
using FastReport.Preview;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;

namespace FastReportsTest.Dialogs
{
    public class ReportPreviewerVM : BindableBase, IDialogAware
    {
        public string Title => "Preview Report";

        //FastReport.Preview.PreviewControl PreviewControl = new FastReport.Preview.PreviewControl();

        private PreviewControl _previewControl;
        public PreviewControl PreviewControl
        {
            get { return _previewControl; }
            set { SetProperty(ref _previewControl, value, () => RaisePropertyChanged(nameof(PreviewControl))); }
        }

        public Report Report = new Report();

        private WindowsFormsHost _reportHost = new WindowsFormsHost();
        public WindowsFormsHost ReportHost
        {
            get { return _reportHost; }
            set { SetProperty(ref _reportHost, value, () => RaisePropertyChanged(nameof(ReportHost))); }
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.Count > 0)
            {
                Report = parameters.GetValue<Report>(nameof(Report));

                //Report.Prepare();

                Report.Preview = PreviewControl;

                //RemoveExportItems();
                //RemoveCloseButton(PreviewControl);
                //RemoveEditButton(PreviewControl);
                //RemoveOpenButton(PreviewControl);
                //RemoveOutlineButton(PreviewControl);

                Report.ShowPrepared();
                //ReportHost.Child = PreviewControl;
            }
        }

        // https://www.fast-report.com/en/forum/index.php?showtopic=13992
        //private void RemoveExportItems()
        //{
        //    How to remove the whole toolbar:
        //    PreviewControl.ToolbarVisible = false;

        //    List<FastReport.Utils.ObjectInfo> icons = new List<FastReport.Utils.ObjectInfo>();
        //    FastReport.Utils.RegisteredObjects.Objects.EnumItems(icons);
        //    Find object
        //    foreach (FastReport.Utils.ObjectInfo item in icons)
        //    {
        //        Remove all but the PDF export
        //        if (item.Object != typeof(FastReport.Export.Pdf.PDFExport))
        //        {
        //            item.Enabled = false;
        //        }
        //    }
        //}

        //private void RemoveCloseButton(FastReport.Preview.PreviewControl preview)
        //{
        //    foreach (var item in preview.ToolBar.Items)
        //    {
        //        if (item.ToString() == "Close")
        //        {
        //            preview.ToolBar.Items.Remove((FastReport.DevComponents.DotNetBar.BaseItem)item);
        //            return;
        //        }
        //    }
        //}

        //private void RemoveEditButton(FastReport.Preview.PreviewControl preview)
        //{
        //    foreach (var item in preview.ToolBar.Items)
        //    {
        //        if (item.ToString() == "Edit")
        //        {
        //            preview.ToolBar.Items.Remove((FastReport.DevComponents.DotNetBar.BaseItem)item);
        //            return;
        //        }
        //    }
        //}

        //private void RemoveSaveButton(FastReport.Preview.PreviewControl preview)
        //{
        //    foreach (var item in preview.ToolBar.Items)
        //    {
        //        if (item.ToString() == "Save")
        //        {
        //            preview.ToolBar.Items.Remove((FastReport.DevComponents.DotNetBar.BaseItem)item);
        //            return;
        //        }
        //    }
        //}

        //private void RemoveOpenButton(FastReport.Preview.PreviewControl preview)
        //{
        //    foreach (var item in preview.ToolBar.Items)
        //    {
        //        if (item.ToString() == "Open")
        //        {
        //            preview.ToolBar.Items.Remove((FastReport.DevComponents.DotNetBar.BaseItem)item);
        //            return;
        //        }
        //    }
        //}

        //private void RemoveOutlineButton(FastReport.Preview.PreviewControl preview)
        //{
        //    foreach (var item in preview.ToolBar.Items)
        //    {
        //        if (item.ToString() == "Outline")
        //        {
        //            preview.ToolBar.Items.Remove((FastReport.DevComponents.DotNetBar.BaseItem)item);
        //            return;
        //        }
        //    }
        //}
    }
}
