using Avoda1.Dal;
using Avoda1.Models;
using Avoda1.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace Avoda1.Controllers
{
    public class MangerController : Controller
    {
        //
        // GET: /Manger/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginManger()
        {
            // Making new Conection to Db
            ShopDal sdal = new ShopDal();
            // take the values from Db
            List<Manger> objProduct = sdal.lgoinDb.ToList<Manger>();
            return View("LoginManger");
        }
        public ActionResult Login()
        {
            ShopDal sdal = new ShopDal();
            Manger M = new Manger();
            List<Manger> objProduct = sdal.lgoinDb.ToList<Manger>();

            // Take the values from the TextBox that the user/Manger Enter.
            M.Username = Request.Form["user.Username"].ToString();
            M.Password = Request.Form["user.Password"].ToString();

            // check if the username and the password are in the DB.
            foreach (Manger ob in objProduct)
            {
                // if they are in the Db go to MangerPanel
                if (M.Username == ob.Username && M.Password == ob.Password)
                {
                    return View("MangerPanel");
                }
            }
            // Otherwise return to the Home Web.
            return RedirectToAction("HomeWeb", "Home");
        }
        public ActionResult MangerPanel()
        {
            // Making new Conection to Db
            ShopDal sdal = new ShopDal();
            // take the values from Db
            List<NewProduct> objProduct = sdal.NewproductDB.ToList<NewProduct>();

            NewProductViewModel npvm = new NewProductViewModel();
            npvm.newproduct = new NewProduct();
            npvm.newproducts = objProduct;

            return View(npvm);
        }
        public ActionResult Submit()
        {
            NewProductViewModel npvm = new NewProductViewModel();
            NewProduct objNewProduct = new NewProduct();
            int flag = 0;

            // Take all the values the manger insert.
            objNewProduct.ProductName = Request.Form["newproduct.ProductName"].ToString();
            objNewProduct.Price =  int.Parse(Request.Form["newproduct.Price"]);
            objNewProduct.InitialQuantity = Request.Form["newproduct.InitialQuantity"].ToString();
            objNewProduct.ProductId = Request.Form["newproduct.ProductId"].ToString();

            ShopDal sdal = new ShopDal();

            List<NewProduct> objProduct = sdal.NewproductDB.ToList<NewProduct>();

            // Check if the ProductId allready in use
            foreach (NewProduct ob in objProduct)
            {
                if (objNewProduct.ProductId == ob.ProductId)
                {
                    flag = 1;
                    TempData["Message"] = "Error This key is already in used";
                }
            }
            // if the ProductId is not in used save the new Product in Db
            if (ModelState.IsValid && flag == 0)
            {
                sdal.NewproductDB.Add(objNewProduct);
                sdal.SaveChanges();
                npvm.newproduct = new NewProduct();
            }
            else
                // if the manger insert wrong values, save tha data and Present it again.
                npvm.newproduct = objNewProduct;

            // send the new list to the web  , for the manger can show it.
            npvm.newproducts = sdal.NewproductDB.ToList<NewProduct>();
            return View("MangerPanel", npvm);
        }

        //Table for Products.
        public ActionResult GetProductsByJson()
        {
            ShopDal sdal = new ShopDal();

            // take the values from Db
            List<NewProduct> objProduct = sdal.NewproductDB.ToList<NewProduct>();
            Thread.Sleep(5000);
            // return data with json.
            return Json(objProduct , JsonRequestBehavior.AllowGet);
        }

        // Table for Customers.
        public ActionResult GetOrdersByJson()
        {
            ShopDal sdal = new ShopDal();

            // take the values from Db
            List<Customer> objProduct = sdal.CustomerDB.ToList<Customer>();
            Thread.Sleep(5000);
            // return data with json.
            return Json(objProduct, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(NewProduct obj)
        {
            ShopDal sdal = new ShopDal();
            NewProductViewModel npvm = new NewProductViewModel();
            List<NewProduct> objProduct = sdal.NewproductDB.ToList<NewProduct>();
            int flag = 0;

            // Take all the values the manger want to update.
            obj.ProductId = Request.Form["newproduct.ProductId"].ToString();
            obj.InitialQuantity = Request.Form["newproduct.InitialQuantity"].ToString();

            //Check if the Id in the list.
            foreach (NewProduct ob in objProduct)
            {
                if (obj.ProductId == ob.ProductId)
                {
                    flag = 1;
                }
            }

            if(flag == 0)
            {
                TempData["errorId"] = "The Id Product is not in the list.";
                return View("MangerPanel");
            }

            // find the product we are looking for by ProductId.
            NewProduct np1 =
                (from x in sdal.NewproductDB
                 where x.ProductId == obj.ProductId
                 select x).ToList<NewProduct>()[0];

            // update the new Amount
            np1.InitialQuantity = obj.InitialQuantity;
            // Save the changes.
            sdal.SaveChanges();

            // return to the MangerPanel.
            return View("MangerPanel");
        }

        public ActionResult Delete()
        {
            // Create New Objects.
            ShopDal sdal = new ShopDal();
            NewProductViewModel npvm = new NewProductViewModel();
            NewProduct np = new NewProduct();
            int flag = 0;

            // The list of products from Db. 
            List<NewProduct> objProduct = sdal.NewproductDB.ToList<NewProduct>();

            // Take all the values the manger want to update.
            np.ProductId = Request.Form["newproduct.ProductId"].ToString();

            // find the product we are looking for by ProductId.
            foreach (NewProduct ob in objProduct)
            {
                if (np.ProductId == ob.ProductId)
                {
                    // remove the product we want form the list.
                    sdal.NewproductDB.Remove(ob);
                    sdal.SaveChanges();
                    flag = 1;
                }
            }
            if(flag == 0)
            {
                TempData["errorId2"] = "The Id Product is not in the list.";
            }
            return View("MangerPanel");
        }

        public ActionResult CustomersTable()
        {
            return View("CustomersTable");
        }
    }
}