using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepositoryPattern.Repository;
using RepositoryPattern.Context;
using F_Reizen.Models;
using RepositoryPattern.Factory;
using PresentationLayer.ViewModels;
using iTextSharp;
using iTextSharp.text.pdf;
using System.Data;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Web.UI;
using System.Text;

namespace PresentationLayer.Controllers
{
  public class BoekingController : Controller
  {
    public BoekingRepository boekingRepository = new BoekingRepository(BoekingFactory.Get(1));
    MainCustomerRepository maincustomerrepository;
    TravelpartnerRepository travelpartnerrepository;
    TransportRepository transportrepository;

    AccommodatieRepository accommodatieRepository;

    // GET: Boeking
    [HttpGet]
    public ActionResult Index()
    {
      BoekingViewModel bvmsession = new BoekingViewModel();

      accommodatieRepository = new AccommodatieRepository(AccommodatieFactory.Get(1));

      if ((AccommodatieViewModel)Session["Reisdata"] != null)
      {
        AccommodatieViewModel accommodatieViewModel = (AccommodatieViewModel)Session["Reisdata"];
        bvmsession.MainCustomer.Boeking.AmountTravelers = accommodatieViewModel.AantalKinderen;
        bvmsession.MainCustomer.Boeking.Rooms = accommodatieViewModel.AantalKamers;
        bvmsession.MainCustomer.Boeking.Departure = accommodatieViewModel.Datum;
      }

      bvmsession.MainCustomer.Boeking.Accommodatie = (Accommodatie)Session["Accommodatie"];

      Session["Boekingviewmodel"] = bvmsession;

      ViewBag.StepNavIndex = 1;
      return View(bvmsession);
    }
    [HttpPost]
    public ActionResult Persoonsgegevens(BoekingViewModel bvmindex)
    {
      BoekingViewModel bvmsession = Session["Boekingviewmodel"] as BoekingViewModel;

      bvmsession.MainCustomer.Boeking.Rooms = bvmindex.MainCustomer.Boeking.Rooms;
      bvmsession.MainCustomer.Boeking.AmountTravelers = bvmindex.MainCustomer.Boeking.AmountTravelers;
      bvmsession.MainCustomer.Boeking.Departure = bvmindex.MainCustomer.Boeking.Departure;
      bvmsession.MainCustomer.Boeking.Transport = bvmindex.MainCustomer.Boeking.Transport;

      if (bvmsession.MainCustomer.Boeking.Transport.Airport == "Eigen vervoer")
      {
        bvmsession.MainCustomer.Boeking.Transport.OwnTransport = true;
        bvmsession.MainCustomer.Boeking.Transport.Airport = null;
      }
      else
      {
        bvmsession.MainCustomer.Boeking.Transport.OwnTransport = false;
      }

      bvmsession.MainCustomer.Boeking.TravelInsurance = bvmindex.MainCustomer.Boeking.TravelInsurance;
      bvmsession.MainCustomer.Boeking.CancellationInsurance = bvmindex.MainCustomer.Boeking.CancellationInsurance;

      Session["Boekingviewmodel"] = bvmsession;

      ViewBag.StepNavIndex = 2;
      return View(bvmsession);
    }
    [HttpPost]
    public ActionResult Reispartners(BoekingViewModel bvmpersoonsgegevens)
    {
      //Session["Boekingviewmodel"] = boekingviewmodel;
      BoekingViewModel bvmsession = Session["Boekingviewmodel"] as BoekingViewModel;

      bvmsession.MainCustomer.Preamble = bvmpersoonsgegevens.MainCustomer.Preamble;
      bvmsession.MainCustomer.Firstname = bvmpersoonsgegevens.MainCustomer.Firstname;
      bvmsession.MainCustomer.Lastname = bvmpersoonsgegevens.MainCustomer.Lastname;
      bvmsession.MainCustomer.Residence = bvmpersoonsgegevens.MainCustomer.Residence;
      bvmsession.MainCustomer.Telephonenumber = bvmpersoonsgegevens.MainCustomer.Telephonenumber;
      bvmsession.MainCustomer.DateOfBirth = bvmpersoonsgegevens.MainCustomer.DateOfBirth;
      bvmsession.MainCustomer.Adress = bvmpersoonsgegevens.MainCustomer.Adress;
      bvmsession.MainCustomer.Email = bvmpersoonsgegevens.MainCustomer.Email;

      bvmsession.Travelpartner.Clear();

      for (int i = 0; i < bvmsession.MainCustomer.Boeking.AmountTravelers + 1; i++)
      {
        bvmsession.Travelpartner.Add(new Travelpartner());
      }

      Session["Boekingviewmodel"] = bvmsession;

      ViewBag.StepNavIndex = 3;
      return View(bvmsession);
    }

    [HttpPost]
    public ActionResult Factuur(BoekingViewModel bvmreispartners)
    {
      BoekingViewModel bvmsession = Session["Boekingviewmodel"] as BoekingViewModel;

      bvmsession.Travelpartner = bvmreispartners.Travelpartner;
      if (Request.IsAuthenticated)
      {
        bvmsession.MainCustomer.Boeking.Prijsberekening(true);
      }
      else
      {
        bvmsession.MainCustomer.Boeking.Prijsberekening(false);
      }

      Session["Boekingviewmodel"] = bvmsession;

      ViewBag.StepNavIndex = 4;
      return View(bvmsession);
    }

    [HttpGet]
    public ActionResult Bevestiging(BoekingViewModel bvmreispartners)
    {
      BoekingViewModel bvmsession = Session["Boekingviewmodel"] as BoekingViewModel;

      boekingRepository.Insert(bvmsession.MainCustomer, bvmsession.Travelpartner);

      Session["Boekingviewmodel"] = bvmsession;

      ViewBag.StepNavIndex = 4;
      return View(bvmsession);
    }

    [HttpPost]
    public ActionResult Bevestiging()
    {
      if (Request.Form["Home"] != null)
      {
        return RedirectToAction("Index", "Home");
      }

      return View((BoekingViewModel)Session["Boekingviewmodel"]);
    }

    public FileStreamResult Download()
    {
      if (User.Identity.IsAuthenticated)
      {
        return createPDF(createText(true));
      }
      else
      {
        return createPDF(createText(false));
      }
    }


    public string createText(bool logIn)
    {
      BoekingViewModel boekingViewModel = (BoekingViewModel)Session["Boekingviewmodel"];

      StringBuilder sb = new StringBuilder();
      sb.AppendLine("FACTUUR");
      sb.AppendLine("--------------------------------------------------------");
      sb.AppendLine("GEGEVENS");
      sb.AppendLine();
      sb.AppendLine("Bestemming: " + boekingViewModel.MainCustomer.Boeking.Accommodatie.Title);
      sb.AppendLine("Hoofdboeker");
      sb.AppendLine("Naam: " + boekingViewModel.MainCustomer.Preamble + ". " + boekingViewModel.MainCustomer.Firstname + " " + boekingViewModel.MainCustomer.Lastname);
      sb.AppendLine("Geboortedatum: " + boekingViewModel.MainCustomer.DateOfBirth.ToShortDateString());
      sb.AppendLine("Adres: " + boekingViewModel.MainCustomer.Adress);
      sb.AppendLine("Woonplaats: " + boekingViewModel.MainCustomer.Residence);
      sb.AppendLine("Telefoonnummer: " + boekingViewModel.MainCustomer.Telephonenumber);
      sb.AppendLine("Email: " + boekingViewModel.MainCustomer.Email);
      sb.AppendLine();

      for (int i = 0; i < boekingViewModel.Travelpartner.Count; i++)
      {
        if (i == 0)
        {
          sb.AppendLine("Partner");
        }
        else
        {
          sb.AppendLine("Kind " + i);
        }

        sb.AppendLine("Naam: " + boekingViewModel.Travelpartner[i].Preamble + ". " + boekingViewModel.Travelpartner[i].Firstname + " " + boekingViewModel.Travelpartner[i].Lastname);
        sb.AppendLine("Geboortedatum: " + boekingViewModel.Travelpartner[i].DateOfBirth.ToShortDateString());
        sb.AppendLine();
      }
      sb.AppendLine();
      sb.AppendLine();
      sb.AppendLine();

      sb.AppendLine("PRIJZEN");
      sb.AppendLine("Volwassenen   2   €" + boekingViewModel.MainCustomer.Boeking.GetAdultPrice());
      if (boekingViewModel.MainCustomer.Boeking.AmountTravelers != 0)
      {
        sb.AppendLine("Kinderen   " + boekingViewModel.MainCustomer.Boeking.AmountTravelers + "   €" + boekingViewModel.MainCustomer.Boeking.GetChildrenPrice());
      }
      else
      {
        sb.AppendLine("Kinderen   -   -");
      }

      if (boekingViewModel.MainCustomer.Boeking.Rooms == 3)
      {
        sb.AppendLine("Kamers   3   €50");
      }
      else
      {
        sb.AppendLine("Kamers   " + boekingViewModel.MainCustomer.Boeking.Rooms + "   -");
      }

      if (boekingViewModel.MainCustomer.Boeking.Transport.OwnTransport)
      {
        sb.AppendLine("Eigen vervoer   -   -");
      }
      else
      {
        int totalTravelers = boekingViewModel.MainCustomer.Boeking.AmountTravelers + 2;
        decimal price = 150 * totalTravelers;
        sb.AppendLine("Vliegtickets " + boekingViewModel.MainCustomer.Boeking.Transport.Airport + "   " + totalTravelers + "   €" + price);
      }

      if (boekingViewModel.MainCustomer.Boeking.TravelInsurance)
      {
        sb.AppendLine("Reisverzekering   -   €20");
      }
      else
      {
        sb.AppendLine("Reisverzekering   -   -");
      }

      if (boekingViewModel.MainCustomer.Boeking.CancellationInsurance)
      {
        sb.AppendLine("Annuleringsverzekering   -   €30");
      }
      else
      {
        sb.AppendLine("Annuleringsverzekering   -   -");
      }

      sb.AppendLine("Reserveringskosten   -   €18");
      sb.AppendLine("Calamiteitenfonds   -   €2,5");

      if (logIn)
      {
        sb.AppendLine("Reisagent korting   5%   €" + boekingViewModel.MainCustomer.Boeking.Totalprice / 20);
      }

      sb.AppendLine();
      sb.AppendLine("Totaalprijs       €" + boekingViewModel.MainCustomer.Boeking.Totalprice);

      return sb.ToString();
    }


    public FileStreamResult createPDF(string text)
    {
      Document document = new Document(PageSize.A4, 25, 25, 30, 30);
      MemoryStream stream = new MemoryStream();
      PdfWriter writer = PdfWriter.GetInstance(document, stream);

      writer.CloseStream = false;

      document.Open();
      document.Add(new Paragraph(text));
      document.Close();

      //writer.Close();
      //ms.Close();
      //Response.ContentType = "pdf/application";
      //Response.AddHeader("content-disposition", "attachment;filename=Factuur.pdf");
      //Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);

      stream.Flush();
      stream.Position = 0;

      return File(stream, "application/pdf", "Factuur.pdf");
    }
  }
}
