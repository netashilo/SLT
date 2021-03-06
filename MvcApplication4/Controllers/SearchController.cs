﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MvcApplication4.Models;
using System.IO;

namespace MvcApplication4.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Search_trans()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Search_trans(SearchModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                  if(isExists(model))
                  {
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                { //return RedirectToAction("Index", "printend");
                    
                    return RedirectToAction("Result", "Result");
                }
                  }
            }
            else
            {
                ModelState.AddModelError("", "");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public Boolean isExists(SearchModel model)
        {
            //if (!System.IO.File.Exists(@"C:\Users\oener\Documents\users.txt"))
            //    return false;
            string filePath = Server.MapPath(Url.Content("~/Content/trans.txt"));
             string printFilePath = Server.MapPath(Url.Content("~/Content/toPrint.txt"));

           // System.IO.StreamWriter file2 = new System.IO.StreamWriter(printFilePath);


         string[] arrays={model.First, model.Last, model.Day, model.Hour, model.City, model.Sex};// put the searchModel values into an array
      

    bool flag=true;
    string[] items;
    
    string[] lines= System.IO.File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
          {

             items= lines[i].Split('#');
              for(int j=0;j<items.Length-1;j++)
             {

                  if (items[j]!=null)
                  {
                       if (arrays[j]!=items[j])
                        {
                            flag=false;
                             break; 
                        }
                 }

            }


          if (flag==true)    
          { 
              string final= arrays[0]+arrays[1]+arrays[6];
          

               using (StreamWriter sw = System.IO.File.AppendText(printFilePath)) 
                 {
                   sw.WriteLine(final);
               }

           }

        }

 return true;
}



           // string[] lines = System.IO.File.ReadAllLines(filePath);

           // for (int i = 0; i < lines.Length; i++)
            //    if (lines[i] == model.Day && lines[i + 1] == model.City && lines[i + 2] == model.Gender)
            //        return true;
           //     else
           //         i += 2;
          //  return false;
        }
    }

