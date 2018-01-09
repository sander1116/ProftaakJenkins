using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F_Reizen;
using PresentationLayer.Controllers;
using F_Reizen.Models;
using iTextSharp.text;
using RepositoryPattern.Repository;
using RepositoryPattern.Factory;

namespace F_Reizen.Tests.Controllers
{
    [TestClass]
    public class AccommodationControllerTest
    {
        AccommodatieRepository accommodatieRepository = new AccommodatieRepository(AccommodatieFactory.Get(1));

        [TestMethod]
        public void SearchBarSeizoenWinter()
        {
            string searchText = "winter";
            int season = 0;
            if (searchText == "zomer" || searchText == "winter")
            {
                if (searchText == "winter")
                {
                    season = 1;
                }
            }
            List<Accommodatie> accommodaties = accommodatieRepository.GetByFilterSeason((Season)season);
            {
                Assert.AreEqual(accommodaties[0].ID, 1);
                Assert.AreEqual(accommodaties[0].Title, "Acta Madfor");
                Assert.AreEqual(accommodaties[0].Volwasseneprijs, 288);
                Assert.AreEqual(accommodaties[0].Kinderprijs, 231);
                Assert.AreEqual(accommodaties[0].Description, "Dit hotel heeft een rustige ligging, maar ligt ook centraal. Het ligt op enkele minuten lopen van van het Koninklijk Paleis en de Sabatini tuinen. De metro bevindt zich tegenover het hotel, waardoor je binnen enkele stops, hartje centrum bent. Het hotel is modern ingericht. Je hebt een gave lounge waar je na een lange dag sightseeing de dag kan nabespreken. Ook heeft het hotel een dakterras met een geweldig uitzicht. Hier moet je zeker even een kijkje nemen. De kamers zijn zeer sfeervol, modern en warm ingericht en bieden een comfortabel verblijf tijdens een citytrip Madrid. De luxe badkamers voorzien van een rainshower maken het geheel compleet. Ontdek zelf waarom dit één van onze populairste hotels is.");
                Assert.AreEqual(accommodaties[0].Country, "Spanje");
            }
        }
        [TestMethod]
        public void SearchBarSeizoenZomer()
        {
            string searchText = "zomer";
            int season = 0;
            if (searchText == "zomer" || searchText == "winter")
            {
                if (searchText == "winter")
                {
                    season = 1;
                }
            }
            List<Accommodatie> accommodaties = accommodatieRepository.GetByFilterSeason((Season)season);
            {
                Assert.AreEqual(accommodaties[0].ID, 2);
                Assert.AreEqual(accommodaties[0].Title, "Barbacan");
                Assert.AreEqual(accommodaties[0].Volwasseneprijs, 623);
                Assert.AreEqual(accommodaties[0].Kinderprijs, 570);
                Assert.AreEqual(accommodaties[0].Description, "Dit prachtige complex aan de rand van Playa del Inglés is al jaren zeer populair op de Nederlandse markt, en dat is niet voor niets! Geniet van de ruime, moderne appartementen in het hoofdgebouw of betrek één van de comfortabele bungalows in de mooie, groene tuin. De kwaliteit en sfeer in dit complex zullen je versteld doen staan. Lekker zonnen? Strijk dan neer op een ligbedje naast één van de drie zwembaden, of spring erin en doe mee aan een potje waterpolo. Je hoeft je geen seconde te vervelen! Daar zorgt het entertainmentteam wel voor, dat net als de rest van het personeel nog echt weet wat gastvrijheid is. Sportievelingen kunnen ook kiezen voor een krachtmeting op het volleybalveld of bijvoorbeeld een wat rustiger spelletje jeu de boules. Na een heerlijke dag kun je even bijkomen in de privacy van de bungalow of op je grote balkon. Aan ruimte geen gebrek in Barbacan! Wil je op de hoogte blijven van de ontwikkelingen aan het thuisfront, dan kun je nu mooi even door de net aangeschafte Nederlandse krant bladeren of stuur gelijk fotos met wifi. Daarna kun je aanschuiven in het uitstekende restaurant, waar je elke dag de lekkerste gerechten kunt bestellen. De faam van deze keuken strekt zelfs tot ver buiten het appartementencomplex. Vervolgens kun je de avond afsluiten in de trendy loungebar, waar je met een glimlach terug denkt aan alweer een topdag in Barbacan");
                Assert.AreEqual(accommodaties[0].Country, "Spanje");
            }
        }
        [TestMethod]
        public void SearchBarLandSpanje()
        {
            string searchText = "spanje";
            if (searchText == "spanje" || searchText == "griekenland" || searchText == "frankrijk")
            {
                foreach (Accommodatie accommodatie in accommodatieRepository.GetByFilterLand(searchText))
                {
                    Assert.AreEqual(accommodatie.Country, "Spanje");
                }
            }
        }
        [TestMethod]
        public void SearchBarLandGriekenland()
        {
            string searchText = "griekenland";
            if (searchText == "spanje" || searchText == "griekenland" || searchText == "frankrijk")
            {
                foreach (Accommodatie accommodatie in accommodatieRepository.GetByFilterLand(searchText))
                {
                    Assert.AreEqual(accommodatie.Country, "Griekenland");
                }
            }
        }
        [TestMethod]
        public void SearchBarLandFrankrijk()
        {
            string searchText = "frankrijk";
            if (searchText == "spanje" || searchText == "griekenland" || searchText == "frankrijk")
            {
                foreach (Accommodatie accommodatie in accommodatieRepository.GetByFilterLand(searchText))
                {
                    Assert.AreEqual(accommodatie.Country, "Frankrijk");
                }
            }
        }
        [TestMethod]
        public void SearchBarAccommdatie()
        {
            string searchText = "arb";
            foreach (Accommodatie accommodatie in accommodatieRepository.SearchBar(searchText))
            {
                Assert.IsTrue(accommodatie.Title.Contains("arb"));
            }
        }

        [TestMethod]
        public void Snelzoeker()
        {
            string land = "Spanje";
            Season season = Season.Winter;
            DateTime vertrekdatum = new DateTime(2018, 1, 3);
            List<Accommodatie> accommodaties = new List<Accommodatie>();
            accommodaties = accommodatieRepository.GetByFilter(land, season, vertrekdatum);
            Assert.IsTrue(accommodaties[0].Title.Contains("Acta"));
            Assert.IsTrue(accommodaties.Count == 3);

        }
    }
}
