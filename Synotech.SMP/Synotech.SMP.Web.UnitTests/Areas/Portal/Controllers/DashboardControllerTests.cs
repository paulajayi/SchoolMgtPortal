using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Synotech.SMP.Web;
using Synotech.SMP.Web.Areas.Portal.Controllers;


namespace Synotech.SMP.Web.UnitTests.Areas.Portal.Controllers
{
    
    public class DashboardControllerTests
    {

        [Fact]
        public void TestDashboard()
        {
            DashboardController dashboardController = new DashboardController();
            ViewResult? result = dashboardController.Index() as ViewResult;
            Assert.NotNull(result);
        }

        [Fact]
        public void TestDashboardTitleMessage()
        {
            DashboardController dashboardController = new DashboardController();
            ViewResult? result = dashboardController.Index() as ViewResult;
            Assert.Equal("Dashboard", result.ViewData["Title"]);
        }

    }
}
