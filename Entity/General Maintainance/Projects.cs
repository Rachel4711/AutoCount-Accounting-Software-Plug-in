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

        public void CreateNewProject(string desc)
        {
            ProjectDeptCommand cmd = ProjectDeptCommand.Create(ProjectType.Project, session);

            ProjectEntity project = cmd.NewProject(ProjectLevel.Top, "");

            project.Description = desc;

            project.Save();
        }
    }
}
