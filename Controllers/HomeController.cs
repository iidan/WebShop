using Avoda1.Dal;
using Avoda1.Models;
using Avoda1.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Avoda1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomeWeb()
        {
            // Make new Connection to DataBase
            ShopDal sdal = new ShopDal();

            // make a list from all the data(NewProdect) in the Db. 
            List<NewProduct> objProduct = sdal.NewproductDB.ToList<NewProduct>();

            // object type New Product ViewModel.
            NewProductViewModel npvm = new NewProductViewModel();

            npvm.newproduct = new NewProduct();

            //take the list the we got  , and show it in the Home Web.
            npvm.newproducts = objProduct;
            return View(npvm);
        }

        public ActionResult Submit()
        {
            ShopDal sdal = new ShopDal();
            Customer cus = new Customer();
            string amount = "";
            DateTime TimeNow = DateTime.Now;
            List<NewProduct> objProduct = sdal.NewproductDB.ToList<NewProduct>();

            // Check if the object is Valid.
            if (ModelState.IsValid)
            {
                // check if the customer name is less than two letter's.
                if (Request.Form["name"].Length != 0 && Request.Form["name"].Length != 1 && Regex.IsMatch(Request["name"],@"^[a-zA-Z]+$"))
                {
                    // if not save the name.
                    cus.CustomerName = Request.Form["name"];
                }
                else
                {
                    TempData["error"] = "Insert Correct Name between 2 and 10 words.";
                    return RedirectToAction("HomeWeb", TempData["error"]);
                }
                // save the id of the prodect the customer bought.
                string id = Request.Form["ko"];

                // Amount of product to buy 1-5.
                string Amount = Request.Form["testSelect"];

                foreach (NewProduct ob in objProduct)
                {
                    // find the id that we take from the list.
                    if (id == ob.ProductId)
                    {
                        // Save the name of the customer 
                        cus.ProductName = ob.ProductName;
                        // Save the Amount 
                        cus.Quantity = Amount;
                        //Subtract the Amount the customer bought from list.
                        amount = (int.Parse(ob.InitialQuantity) - int.Parse(Amount)).ToString();
                        // save the time is purchase
                        cus.Date = TimeNow.ToString();
                    }
                }

                // Check if it Ok
                if (ModelState.IsValid)
                {
                    // Add the Customer to Db.
                    sdal.CustomerDB.Add(cus);
                    sdal.SaveChanges();
                }

                // find the product , the custoemr bought 
                NewProduct np1 =
                    (from x in sdal.NewproductDB
                     where x.ProductId == id
                     select x).ToList<NewProduct>()[0];

                // Update the new Amount of the Product.
                np1.InitialQuantity = amount;
                sdal.SaveChanges();

                // Return to the same page
                return View("CustomerName");
            }
            else
                return RedirectToAction("HomeWeb");
        }

        public ActionResult SerachCustomers()
        {
            // Connection to Db.
            ShopDal sdal = new ShopDal();
            int searchMinNumber = 0;
            int searchMaxNumber = 0;

            // Get the Name form the Search-TextBox.
            string searchName = Request.Form["txtFirstName1"].ToString();

            //Check if the Numbers are "empty" equals to zero.
            if (Request.Form["txtFirstName2"] == "" && Request.Form["txtFirstName3"] == "")
            {
                searchMinNumber = 0;
                searchMaxNumber = 0;
            }
            else // if not and we deside to serach bye numbers 
            {
                searchMinNumber = int.Parse(Request.Form["txtFirstName2"]);
                searchMaxNumber = int.Parse(Request.Form["txtFirstName3"]);
            }
            NewProductViewModel npv = new NewProductViewModel();

            //Go to Db , and Request all the values that we looking for.
            if (Request.Form["txtFirstName1"] != "")
            {
                List<NewProduct> ObjProduct1 =
                    (from x in sdal.NewproductDB
                     // if the name is equals to the name in the DataBsee , show it.
                     where x.ProductName.ToString() == searchName
                     select x).ToList<NewProduct>();
                npv.newproducts = ObjProduct1;
            }
            else // looking for numbers that start with (min) , and End with(Max).
            {
                List<NewProduct> ObjProduct2 =
                    (from x in sdal.NewproductDB
                     where x.Price >= searchMinNumber && x.Price <= searchMaxNumber
                     select x).ToList<NewProduct>();
                npv.newproducts = ObjProduct2;
            }
            // if the Customer send empty values to seacrh it will return the same web.
            if (searchName.Length == 0 && searchMinNumber == 0 && searchMaxNumber == 0)
            {
                return RedirectToAction("HomeWeb");
            }
            return View("HomeWeb",npv);
        }

	}
}