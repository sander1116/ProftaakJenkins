using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using F_Reizen.Models;

namespace PresentationLayer.ViewModels
{
  public class HomeViewModel
  {
    public List<string> landen { get; set; }
    public string land { get; set; }
    public DateTime date { get; set; }
    public Season season { get; set; }
    public List<Accommodatie> accommodaties { get; set; }
  }
}