using AutoCount.Authentication;
using AutoCount.GeneralMaint.PurchaseAgent;
using AutoCount.GeneralMaint.SalesAgent;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Agents
    {
        private readonly UserSession session;

        private readonly SalesAgentCommand sa_cmd;
        private readonly PurchaseAgentCommand pa_cmd;

        public Agents(UserSession userSession)
        {
            session = userSession;

            sa_cmd = SalesAgentCommand.Create(session, session.DBSetting);
            pa_cmd = PurchaseAgentCommand.Create(session, session.DBSetting);
        }

        public string CreateOrUpdate_SalesAgent(bool isOverwrite, string agentName, string agentDesc = "", string agentEmail = "")
        {
            SalesAgentEntity agent = isOverwrite && hasSalesAgents(agentName) ? 
                sa_cmd.GetSalesAgent(agentName) : sa_cmd.NewSalesAgent();

            agent.SalesAgent = agentName;
            agent.Description = agentDesc;
            agent.EmailAddress = agentEmail;

            agent.Save();

            return agentName;
        }

        public string CreateOrUpdate_PurchaseAgent(bool isOverwrite, string agentName, string agentDesc = "")
        {
            PurchaseAgentEntity agent = isOverwrite && hasPurchaseAgents(agentName) ? 
                pa_cmd.GetPurchaseAgent(agentName) : pa_cmd.NewPurchaseAgent();

            agent.PurchaseAgent = agentName;
            agent.Description = agentDesc;

            agent.Save();

            return agentName;
        }

        internal bool hasSalesAgents(string agent_name)
        {
            return sa_cmd.GetSalesAgent(agent_name) != null;
        }

        internal bool hasPurchaseAgents(string agent_name)
        {
            return pa_cmd.GetPurchaseAgent(agent_name) != null;
        }
    }
}
