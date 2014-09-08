using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Models;
using Moq;
using Logic.LeadLogic;
using mvcDemo.Controllers;

namespace mvcDemo.Tests.Controllers
{
    [TestClass]
    public class LeadControllerTest
    {
        private readonly Mock<LeadContext> _mockLeadCtx;
        public LeadControllerTest()
        {
            var leadData = GetLeadList().AsQueryable();

            var mockLeadSet = new Mock<DbSet<Lead>>();
            mockLeadSet.As<IQueryable<Lead>>().Setup(m => m.Provider).Returns(leadData.Provider);
            mockLeadSet.As<IQueryable<Lead>>().Setup(m => m.Expression).Returns(leadData.Expression);
            mockLeadSet.As<IQueryable<Lead>>().Setup(m => m.ElementType).Returns(leadData.ElementType);
            mockLeadSet.As<IQueryable<Lead>>().Setup(m => m.GetEnumerator()).Returns(leadData.GetEnumerator());

            _mockLeadCtx = new Mock<LeadContext>();
            _mockLeadCtx.Setup(m => m.Leads).Returns(mockLeadSet.Object);
        }

        [TestMethod]
        public void Details()
        {
            //Arrange
            var logic = new LeadLogic(_mockLeadCtx.Object);
            var leadController = new LeadController(logic);

            //Act
            var result = leadController.Details(1);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit()
        {
            var logic = new LeadLogic(_mockLeadCtx.Object);
            var leadController = new LeadController(logic);

            //Act
            var result = leadController.Edit(1);

            //Assert
            Assert.IsNotNull(result);
        }

        //TODO: Need to figure out how to mock the User to test Index & Create

        private static List<Lead> GetLeadList()
        {
            var leadData = new List<Lead>
            {
                new Lead
                {
                    LeadId = 1,
                    FirstName = "Joe1",
                    LastName = "Smith",
                    Email = "joesmith1@test.com",
                    LeadUser = "test@test.com",
                    Address =
                        new Address()
                        {
                            AddressLine1 = "123 Front Street",
                            City = "Boise",
                            State = "ID",
                            ZipCode = "88888",
                            Country = "USA"
                        }
                },
                new Lead
                {
                    LeadId = 2,
                    FirstName = "Joe2",
                    LastName = "Smith",
                    Email = "joesmith2@test.com",
                    LeadUser = "test@test.com",
                    Address =
                        new Address()
                        {
                            AddressLine1 = "1234 Front Street",
                            City = "Boise",
                            State = "ID",
                            ZipCode = "88888",
                            Country = "USA"
                        }
                },
                new Lead
                {
                    LeadId = 3,
                    FirstName = "Joe3",
                    LastName = "Smith",
                    Email = "joesmith3@test.com",
                    Address =
                        new Address()
                        {
                            AddressLine1 = "1235 Front Street",
                            City = "Boise",
                            State = "ID",
                            ZipCode = "88888",
                            Country = "USA"
                        }
                },
                new Lead
                {
                    LeadId = 4,
                    FirstName = "Joe4",
                    LastName = "Smith",
                    Email = "joesmith4@test.com",
                    Address =
                        new Address()
                        {
                            AddressLine1 = "1236 Front Street",
                            City = "Boise",
                            State = "ID",
                            ZipCode = "88888",
                            Country = "USA"
                        }
                }
            };
            return leadData;
        }
    }
}
