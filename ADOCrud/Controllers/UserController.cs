using ADOCrud.Models;
using ADOCrud.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ADOCrud.Controllers
{
   
    public class UserController : Controller
    {
        UserDAL udl = new UserDAL();

        // GET : User
        [HttpGet]
        [Route("UserAll")]
        public ActionResult List()
        {
            var data = udl.Getusers(); // All data are stored in data 
            
            // variable through this method
            return View(data); // We are passing the data into views.
        }

        public ActionResult Create()
        {
            return View(); 
        }

        

        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            if (udl.InsertUser(user))
            {
                TempData["InsertMessage"] = "<script>alert('user saved successfully')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["InsertErrorMessage"] = "<script>alert('Data not saved')</script>";
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var data = udl.Getusers().Find(a => a.Id == id);
            // We are trying to fetch the data which are exactly a match for id in getUser
            // and if matches the id field of get user we just let the edit function to execute

            return View(data);
        }

        [HttpPost]

        public ActionResult Edit(UserModel user)
        {
            if (udl.UpdateUser(user))
            {
                TempData["UpdateMessage"] = "<script>alert('User updated successfully')</script>";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ErrorUpdateMessage"] = "<script>alert('User not updated')</script>";
            }
            return View();
        }

        public ActionResult Delete(int id){
            int r = udl.DeleteUser(id);
            if(r>0){
                TempData["DeleteMessage"] = "<script>alert('User deleted successfully')</script>";
                return RedirectToAction("List");
            }else{
                TempData["DeleteErrorMessage"] = "<script>alert('User not deleted')</script>";
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            var data = udl.Getusers().Find(a => a.Id == id);
            return View(data);
        }
    }
}
