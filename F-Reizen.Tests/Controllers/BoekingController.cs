using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F_Reizen.Models;
using RepositoryPattern.Repository;
using RepositoryPattern.Factory;
using RepositoryPattern.Context;

namespace F_Reizen.Tests.Controllers
{
    [TestClass]
    public class BoekingController 
    {
        [TestMethod]
        public void PlaatsBoeking()
        {
            Boeking boeking = new Boeking(99, true, 100M, 2, 2, true, true);
            MainCustomer mainCustomer = new MainCustomer("testadress", "testresidence", "testtelephonenumber", "testmail", Preamble.dhr, DateTime.Now, "testname", "testname");
            List<Travelpartner> travelpartners = new List<Travelpartner>();
            
            Travelpartner travelpartner = new Travelpartner(true, Preamble.dhr, DateTime.Now, "testpartnername", "testpartnerlastname");
            travelpartners.Add(travelpartner);

            mainCustomer.Boeking = boeking;

            BoekingRepository boekingRepository = new BoekingRepository(BoekingFactory.Get(1));

            boekingRepository.Insert(mainCustomer, travelpartners);

            //assert GetBoeking() in repositoryPattern moet nog geimplementeerd worden.
            
        }
    }
}
