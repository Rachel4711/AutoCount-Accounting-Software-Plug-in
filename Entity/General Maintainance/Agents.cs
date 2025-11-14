using AutoCount.Authentication;
using AutoCount.GeneralMaint.SalesAgent;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Agents
    {
        private readonly UserSession session;

        public Agents(UserSession userSession)
        {
            session = userSession;
        }

        public string NewAgent(string agentCode, string agentName, string agentEmail)
        {
            SalesAgentCommand cmd = SalesAgentCommand.Create(session, session.DBSetting);
            
            SalesAgentEntity agent = cmd.NewSalesAgent();

            agent.SalesAgent = agentCode;
            agent.Description = agentName;
            agent.EmailAddress = agentEmail;

            agent.Save();

            return agentCode;
        }
    }
}
