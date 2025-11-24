using AutoCount.Authentication;
using AutoCount.GeneralMaint.Area;
using AutoCount.GeneralMaint.PurchaseAgent;
using AutoCount.GeneralMaint.SalesAgent;
using AutoCount.UDF;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Agents
    {
        private readonly UserSession session;

        public Agents(UserSession userSession)
        {
            session = userSession;
        }

        public string CreateOrUpdate_SalesAgent(bool isOverwrite, string agentName, string agentDesc = "", string agentEmail = "")
        {
            SalesAgentCommand cmd = SalesAgentCommand.Create(session, session.DBSetting);

            SalesAgentEntity agent = isOverwrite ? cmd.GetSalesAgent(agentName) : cmd.NewSalesAgent();

            agent.SalesAgent = agentName;
            agent.Description = agentDesc;
            agent.EmailAddress = agentEmail;

            agent.Save();

            return agentName;
        }

        public string CreateOrUpdate_PurchaseAgent(bool isOverwrite, string agentName, string agentDesc = "")
        {
            PurchaseAgentCommand cmd = PurchaseAgentCommand.Create(session, session.DBSetting);

            PurchaseAgentEntity agent = isOverwrite ? cmd.GetPurchaseAgent(agentName) : cmd.NewPurchaseAgent();

            agent.PurchaseAgent = agentName;
            agent.Description = agentDesc;

            agent.Save();

            return agentName;
        }

        public bool hasSalesAgents(string agent_name)
        {
            SalesAgentCommand cmd = SalesAgentCommand.Create(session, session.DBSetting);

            return cmd.GetSalesAgent(agent_name) != null;
        }

        public bool hasPurchaseAgents(string agent_name)
        {
            PurchaseAgentCommand cmd = PurchaseAgentCommand.Create(session, session.DBSetting);

            return cmd.GetPurchaseAgent(agent_name) != null;
        }
    }
}
