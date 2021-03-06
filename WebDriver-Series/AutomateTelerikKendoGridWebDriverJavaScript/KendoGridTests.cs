﻿// <copyright file="KendoGridTests.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Ui.Automation.Core.Controls.Controls;
using Ui.Automation.Core.Controls.Enums;

namespace AutomateTelerikKendoGridWebDriverJavaScript
{
    [TestClass]
    public class KendoGridTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            this.driver = new FirefoxDriver();
            this.driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(5));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        [TestMethod]
        public void FilterContactName()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            kendoGrid.Filter("ContactName", FilterOperator.Contains, "Thomas");
            var items = kendoGrid.GetItems<GridItem>();
            Assert.AreEqual(1, items.Count);
        }

        [TestMethod]
        public void SortContactTitleDesc()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            kendoGrid.Sort("ContactTitle", SortType.Desc);
            var items = kendoGrid.GetItems<GridItem>();
            Assert.AreEqual("Sales Representative", items[0]);
            Assert.AreEqual("Sales Representative", items[1]);
        }

        [TestMethod]
        public void TestCurrentPage()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            var pageNumber = kendoGrid.GetCurrentPageNumber();
            Assert.AreEqual(1, pageNumber);
        }

        [TestMethod]
        public void GetPageSize()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            var pageNumber = kendoGrid.GetPageSize();
            Assert.AreEqual(20, pageNumber);
        }

        [TestMethod]
        public void GetAllItems()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var items = kendoGrid.GetItems<GridItem>();
            Assert.AreEqual(91, items.Count);
        }
    }
}