using FastReport;
using FastReportsTest.Data;
using FastReportsTest.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastReportsTest
{
    public class ContentVM : BindableBase
    {
        private readonly IDialogService _dialog;

        private DelegateCommand _generateReportCommand;
        public DelegateCommand GenerateReportCommand 
        {
            get => _generateReportCommand ?? new DelegateCommand(GenerateReport);
            private set { _generateReportCommand = value; } 
        }

        public ContentVM(IDialogService dialog)
        {
            _dialog = dialog;
        }

        private void GenerateReport()
        {
            string reportPath = @"C:\Users\pauls\Desktop\AFR_FastReport_Reports\";
            string fileName = "ReportTroubleshooting.frx";

            List<ReportData> data = new List<ReportData>();
            data.Add(new ReportData
            {
                PKey = default!,
                Data = default!
            });

            Report report = new Report();

            try
            {
                report.Load(reportPath + fileName);
            }
            catch
            {
                MessageBox.Show("Unable to Load report.");
            }

            var dataTable = data.ToDataTable();
            report.RegisterData(dataTable, "ReportDatas");
            report.GetDataSource("ReportDatas").Enabled = true;

            if (report.Prepare())
            {
                DialogParameters parameters = new DialogParameters();
                parameters.Add("Report", report);
                _dialog.ShowDialog("ReportDialog", parameters, null);
            }
            else
            {
                MessageBox.Show("Unable to Prepare report.");
            }
        }
    }
}
