using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseRenting.Core.Contracts;
using HouseRenting.Core.Services;
using NUnit.Framework;

namespace HouseRenting.Test.UnitTests
{
    [TestFixture]
    public class AgentServiceTests : UnitTestBase
    {
        private IAgentService agentService;

        [OneTimeSetUp]
        public void SetUp()
        {
            agentService = new AgentService(context);
        }

        [Test]
        public void getAgentId_CorrectUserId()
        {
            var resAgentId = agentService.GetAgentId(Agent.UserId);
            Assert.AreEqual(Agent.Id, resAgentId);
        }

        [Test]
        public void ExistsById_Correct()
        {
            var res = agentService.ExistsById(Agent.UserId);
            Assert.IsTrue(res);
        }

        [Test]
        public void PersonWithPhoneNumberExists_Correct()
        {
            var res = agentService.PersonWithPhoneNumberExists(Agent.PhoneNumber);
            Assert.IsTrue(res);
        }

        [Test]
        public void Create_Correct()
        {
            int count = context.Agents.Count();
            agentService.Create(Agent.UserId, Agent.PhoneNumber);
            Assert.AreEqual(count+1, context.Agents.Count());
            var newAgentId = agentService.GetAgentId(Agent.UserId);
            var newAgent = context.Agents.Find(newAgentId);
            Assert.IsNotNull(newAgent);
            Assert.AreEqual(Agent.PhoneNumber, newAgent.PhoneNumber);
            Assert.AreEqual(Agent.UserId, newAgent.UserId);
        }
    }
}
