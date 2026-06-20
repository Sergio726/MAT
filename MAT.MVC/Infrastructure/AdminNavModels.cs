using System.Collections.Generic;

namespace MAT.MVC.Infrastructure
{
    public sealed class AdminNavSection
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IList<AdminHelpCatalogItem> Items { get; set; }
    }

    public sealed class AdminNavViewModel
    {
        public string UserName { get; set; }
        public string UserInitials { get; set; }
        public bool IsAdminDev { get; set; }
        public bool IsOffcanvas { get; set; }
        public IList<AdminNavSection> Sections { get; set; }
    }

    public sealed class AdminTopbarViewModel
    {
        public string UserName { get; set; }
        public string UserInitials { get; set; }
        public string PageTitle { get; set; }
        public bool OnAdminIndex { get; set; }
        public bool IsAuthenticated { get; set; }
    }

    public sealed class AdminPageContext
    {
        public string Title { get; set; }
        public string SectionTitle { get; set; }
        public string SectionUrl { get; set; }
        public bool ShowBreadcrumb { get; set; }
    }
}
