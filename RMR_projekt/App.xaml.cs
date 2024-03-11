namespace RMR_projekt
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCfEx3THxbf1x0ZFRGal5XTndXUiweQnxTdEFjWH1ecXRXQGFVUU13XA==");
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
