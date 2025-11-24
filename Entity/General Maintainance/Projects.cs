using AutoCount.Authentication;
using AutoCount.GeneralMaint.Project;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Projects
    {
        private readonly UserSession session;

        public Projects(UserSession userSession)
        {
            session = userSession;
        }

        public void CreateOrUpdate_Project(bool isOverwrite, string proj_code, string proj_desc, string proj_type)
        {
            ProjectDeptCommand cmd = ProjectDeptCommand.Create(ProjectType.Project, session);

            ProjectLevel proj_lvl = proj_type != "P" ? ProjectLevel.Sub : ProjectLevel.Top;

            ProjectEntity project = isOverwrite ? cmd.GetProject(proj_code) : cmd.NewProject(proj_lvl, null);

            project.ProjNo = proj_code;
            project.Description = proj_desc;

            project.Save();
        }
    }
}
